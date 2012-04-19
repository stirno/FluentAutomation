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
		#phantom.exit()

class PhantomBrowserController
	constructor: (@owner) ->
		@className = "PhantomBrowserController"

	# Commands
	Navigate: (command) ->
		url = command.url

		page.onLoadFinished = (status) =>
			@InjectSizzle()
			@owner.socket.send JSON.stringify({ Response: "ActionCompleted" })

		page.open url

	Find: (command) ->
		selector = command.selector

		fn = ->
			$( 'SELECTOR' )
		fnStr = fn.toString().replace( 'SELECTOR', selector )
		domElement = page.evaluate fnStr
		
		element = ""
		element = selector if domElement?

		@owner.socket.send JSON.stringify({ Response: "ActionCompleted", Element: element })

	FindMultiple: (command) ->
		@Find(command)

	Click: (command) ->
		selector = command.selector if command.selector?
		x = command.x if command.y?
		y = command.y if command.x?

		@owner.throw "NotImplementedException"

	Hover: (command) ->
		selector = command.selector if command.selector?
		x = command.x if command.y?
		y = command.y if command.x?

		@owner.throw "NotImplementedException"

	Focus: (command) ->
		selector = command.selector if command.selector?
		x = command.x if command.y?
		y = command.y if command.x?

		@owner.throw "NotImplementedException"

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

	SelectText: (selector, text) ->
		@owner.throw "NotImplementedException"

	SelectValue: (selector, value) ->
		@owner.throw "NotImplementedException"

	SelectIndex: (selector, index) ->
		@owner.throw "NotImplementedException"

	TakeScreenshot: (fileName) ->
		@owner.throw "NotImplementedException"

	Wait: ->
		@owner.throw "NotImplementedException"

	WaitMilliseconds: (milliseconds) ->
		@owner.throw "NotImplementedException"

	Press: (keys) ->
		@owner.throw "NotImplementedException"

	Type: (text) ->
		@owner.throw "NotImplementedException"

	InjectSizzle: () ->
		page.includeJs "http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"

new PhantomDriver()