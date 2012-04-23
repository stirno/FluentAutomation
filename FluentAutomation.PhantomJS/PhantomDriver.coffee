system = require 'system'
page = require('webpage').create()
viewportWidth = 1280
viewportHeight = 1024
page.viewportSize =
	width: viewportWidth
	height: viewportHeight

class PhantomDriver
	constructor: () ->
		@className = "PhantomWebSocketServer"

		if system.args.length is 1
			@throw "Arguments required: portNumber"
		else
			portNumber = new Number system.args[1], 10
			@throw "Port Number must be a valid Number" if isNaN portNumber

		if !isNaN portNumber
			@controller = new PhantomBrowserController this

			@socket = new WebSocket "ws://127.0.0.1:#{portNumber}"
			console.log "Connecting to ws://127.0.0.1:#{portNumber} ..."

			@socket.onmessage = (message) =>
				command = JSON.parse message.data
				if command.Action?
					cleanCommand = {}
					cleanCommand[ param.toLowerCase() ] = value for param, value of command
					console.log "Running: " + JSON.stringify cleanCommand
					@controller[command.Action]( cleanCommand )

	throw: (errorMessage) ->
		console.log errorMessage
		@owner.socket.send JSON.stringify({ ErrorMessage: errorMessage })
		#phantom.exit()

class PhantomBrowserController
	constructor: (@owner) ->
		@className = "PhantomBrowserController"

	CompleteAction: ( response ) ->
		response = {} if not response
		response.Response = "ActionCompleted"
		response.Url = page.evaluate -> window.location.href
		@owner.socket.send JSON.stringify response

	# Commands
	Navigate: (command) ->
		url = command.url

		page.onLoadFinished = (status) =>
			@IncludeJQuery()
			@CompleteAction()

		page.open url

	Find: (command) ->
		selector = command.selector

		fn = ->
			parseAttributes = (el) ->
				results = []
				for attr in el.attributes
					results.push { Name: attr.name, Value: attr.value }
				return results

			elem = $('SELECTOR')

			if elem.length == 1
				result =
					Selector: 'SELECTOR'
					Html: elem.html()
					Attributes: parseAttributes elem[0]
					Value: elem.val()
					Text: elem.text()
					TagName: elem[0].tagName
					PosX: elem.offset().left
					PosY: elem.offset().top
					Width: elem.width()
					Height: elem.height()

			else if elem.length == 0
				result = null

			else
				result = []
				for el in elem
					$el = $(el)
					result.push
						Selector: 'SELECTOR'
						Html: $el.html()
						Attributes: parseAttributes el
						Value: $el.val()
						Text: $el.text()
						TagName: el.tagName
						PosX: $el.offset().left
						PosY: $el.offset().top
						Width: $el.width()
						Height: $el.height()
			return result

		fnStr = fn.toString().replace(/SELECTOR/g, selector)
		evalResult = page.evaluate fnStr
		@CompleteAction { Result: evalResult }

	FindMultiple: (command) ->
		@Find(command)

	ScrollToPoint: (x, y) ->
		fn = ->
			viewportOffset = { x:0, y:0 }
			$(document).scrollTop SCROLL_Y
			viewportOffset.y = $(document).scrollTop()
			$(document).scrollLeft SCROLL_X
			viewportOffset.x = $(document).scrollLeft()
			return viewportOffset
		viewportOffset = page.evaluate fn.toString().replace( 'SCROLL_X', x ).replace( 'SCROLL_Y', y )
		console.log "Scrolled to: #{viewportOffset.x}, #{viewportOffset.y}"
		return viewportOffset

	ResolveSelectorXY: ( selector = null, x = 0, y = 0 ) ->
		offset = { top:0, left:0 }

		if selector?
			fn = ->
				offset = $('SELECTOR').offset()
			offset = page.evaluate fn.toString().replace( 'SELECTOR', selector )

		clickX = parseInt( offset.left, 10 ) + parseInt( x, 10 )
		clickY = parseInt( offset.top, 10 ) + parseInt( y, 10 )

		viewportOffset = @ScrollToPoint clickX, clickY

		clickX -= viewportOffset.x
		clickY -= viewportOffset.y

		return { x: clickX, y: clickY }

	Click: (command) ->
		selector = command.selector if command.selector isnt ""
		x = if command.x? then parseInt( command.x, 10 ) else 0
		y = if command.y? then parseInt( command.y, 10 ) else 0

		target = @ResolveSelectorXY selector, x, y

		page.sendEvent 'mousemove', target.x, target.y
		page.sendEvent 'click', target.x, target.y

		@CompleteAction()

	DoubleClick: (command) ->
		selector = command.selector if command.selector isnt ""
		x = if command.x? then parseInt( command.x, 10 ) else 0
		y = if command.y? then parseInt( command.y, 10 ) else 0
		
		throw "NotImplementedException" if x or y

		target = @ResolveSelectorXY selector, x, y
		page.sendEvent 'mousemove', target.x, target.y

		if selector?
			fn = ->
				$('SELECTOR').dblclick()
			page.evaluate fn.toString().replace( 'SELECTOR', selector )

		@CompleteAction()
		
	RightClick: (command) ->
		selector = command.selector if command.selector isnt ""

		target = @ResolveSelectorXY selector
		page.sendEvent 'mousemove', target.x, target.y

		if selector?
			fn = ->
				$('SELECTOR').trigger 'contextmenu'
			page.evaluate fn.toString().replace( 'SELECTOR', selector )

		@CompleteAction()

	Hover: (command) ->
		selector = command.selector if command.selector isnt ""
		x = if command.x? then parseInt( command.x, 10 ) else 0
		y = if command.y? then parseInt( command.y, 10 ) else 0

		target = @ResolveSelectorXY selector, x, y

		page.sendEvent 'mousemove', target.x, target.y

	Focus: (command) ->
		@Click command

	DragAndDrop: (command) ->
		sourceSelector = command.sourceselector
		targetSelector = command.targetselector

		target = @ResolveSelectorXY sourceSelector
		page.sendEvent 'mousedown', target.x, target.y

		target = @ResolveSelectorXY targetSelector
		page.sendEvent 'mousemove', target.x, target.y
		page.sendEvent 'mouseup', target.x, target.y

		@CompleteAction()

	SelectText: (command) ->
		selector = command.selector if command.selector?
		text = command.text if command.text?

		fn = ->
			success = false
			$('SELECTOR option').each ->
				if $(this).text() is 'TEXT'
					$(this).attr('selected','selected')
					success = true
			return success
		success = page.evaluate fn.toString().replace( 'SELECTOR', selector ).replace( 'TEXT', text )

		if !success 
			@owner.throw "Could not find option"

		@CompleteAction()


	SelectValue: (command) ->
		selector = command.selector if command.selector?
		value = command.value if command.value?

		fn = ->
			success = false
			$('SELECTOR option').each ->
				if $(this).val() is 'VALUE'
					$(this).attr('selected','selected')
					success = true
			return success
		success = page.evaluate fn.toString().replace( 'SELECTOR', selector ).replace( 'VALUE', value )

		if !success 
			@owner.throw "Could not find option"

		@CompleteAction()

	SelectIndex: (command) ->
		selector = command.selector if command.selector?
		index = command.index if command.index?

		fn = ->
			opt = $('SELECTOR option:eq(INDEX)')
			opt.attr('selected','selected')
			if opt.length > 0 then true else false
		success = page.evaluate fn.toString().replace( 'SELECTOR', selector ).replace( 'INDEX', index )

		if !success 
			@owner.throw "Could not find option"

		@CompleteAction()

	TakeScreenshot: (command) ->
		fs = require 'fs'
		filename = new Date().getTime() + 'screenshot.png'
		page.render filename
		@CompleteAction { Result: filename }

	Wait: (command) ->
		# Note this is currently implemented by the CommandProvider
		seconds = command.seconds if command.seconds?
		console.log "Waiting #{seconds} seconds"
		fn = =>
			console.log "Wait complete"
			@CompleteAction()
		setInterval fn, (seconds * 1000)

	WaitMilliseconds: (command) ->
		# Note this is currently implemented by the CommandProvider
		milliseconds = command.milliseconds if command.milliseconds?
		console.log "Waiting #{milliseconds} milliseconds"
		fn = =>
			console.log "Wait complete"
			@CompleteAction()
		setInterval fn, milliseconds

	Press: (keys) ->
		@owner.throw "NotImplementedException"

	Type: (text) ->
		@owner.throw "NotImplementedException"

	IncludeJQuery: () ->
		page.includeJs "http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"

new PhantomDriver()