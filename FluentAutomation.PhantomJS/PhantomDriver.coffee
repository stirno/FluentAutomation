system = require 'system'
page = require('webpage').create()
page.viewportSize =
	width: 1280
	height: 1024

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

	# Commands
	Navigate: (command) ->
		url = command.url

		page.onLoadFinished = (status) =>
			@IncludeJQuery()
			@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })

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
				return { Selector: 'SELECTOR', Html: elem.html(), Attributes: parseAttributes(elem[0]), Value: elem.val(), Text: elem.text(), TagName: elem[0].tagName }
			else if elem.length == 0
				return null
			else
				result = []
				for el in elem
					$el = $(el)
					result.push { Selector: 'SELECTOR', Html: $el.html(), Attributes: parseAttributes( el ), Value: $el.val(), Text: $el.text(), TagName: el.tagName }
				return result

		fnStr = fn.toString().replace(/SELECTOR/g, selector)
		evalResult = page.evaluate fnStr
	
		@owner.socket.send JSON.stringify({ Response: "ActionCompleted", Result: evalResult })

	FindMultiple: (command) ->
		@Find(command)

	Click: (command) ->
		selector = command.selector if command.selector isnt ""
		x = if command.x? then parseInt command.x else 0
		y = if command.y? then parseInt command.y else 0

		offset = { top:0, left:0 }
		if selector?
			fn = ->
				offset = $('SELECTOR').offset()
			offset = page.evaluate fn.toString().replace( 'SELECTOR', selector )

		page.sendEvent 'mousemove', offset.left+x, offset.top+y
		page.sendEvent 'click', offset.left+x, offset.top+y

		@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })
		

	Hover: (command) ->
		selector = command.selector if command.selector isnt ""
		x = if command.x? then parseInt command.x else 0
		y = if command.y? then parseInt command.y else 0

		offset = { top:0, left:0 }
		if selector?
			fn = ->
				offset = $('SELECTOR').offset()
			offset = page.evaluate fn.toString().replace( 'SELECTOR', selector )

		page.sendEvent 'mousemove', offset.left+x, offset.top+y

		@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })


	Focus: (command) ->
		@Click command

	DragAndDrop: (command) ->
		sourceSelector = command.sourceselector
		targetSelector = command.targetselector

		fn = ->
			srcOffset = $('SELECTOR1').offset()
		srcOffset = page.evaluate fn.toString().replace( 'SELECTOR1', sourceSelector )

		fn = ->
			dstOffset = $('SELECTOR2').offset()
		dstOffset = page.evaluate fn.toString().replace( 'SELECTOR2', targetSelector )

		page.sendEvent 'mousedown', srcOffset.left, srcOffset.top
		page.sendEvent 'mousemove', dstOffset.left, dstOffset.top
		page.sendEvent 'mouseup', dstOffset.left, dstOffset.top

		@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })

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

		@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })


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

		@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })

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

		@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })

	TakeScreenshot: (command) ->
		fs = require 'fs'
		filename = new Date().getTime() + 'screenshot.png'
		page.render filename
		@owner.socket.send JSON.stringify({ Response: "ActionCompleted", Result: filename })

	Wait: (command) ->
		# Note this is currently implemented by the CommandProvider
		seconds = command.seconds if command.seconds?
		console.log "Waiting #{seconds} seconds"
		fn = =>
			console.log "Wait complete"
			@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })
		setInterval fn, (seconds * 1000)

	WaitMilliseconds: (command) ->
		# Note this is currently implemented by the CommandProvider
		milliseconds = command.milliseconds if command.milliseconds?
		console.log "Waiting #{milliseconds} milliseconds"
		fn = =>
			console.log "Wait complete"
			@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })
		setInterval fn, milliseconds

	Press: (keys) ->
		@owner.throw "NotImplementedException"

	Type: (text) ->
		@owner.throw "NotImplementedException"

	IncludeJQuery: () ->
		page.includeJs "http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"

new PhantomDriver()