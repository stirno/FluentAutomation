system = require 'system'
page = require('webpage').create()

class PhantomWebSocketServer
	constructor: () ->
		if system.args.length is 1
			@throw "Arguments required: portNumber"
		else
			portNumber = new Number system.args[1], 10
			@throw "Port Number must be a valid Number" if isNaN portNumber

		if !isNaN portNumber
			@controller = new PhantomBrowserController this

			@socket = new WebSocket "ws://127.0.0.1:#{portNumber}"
			console.log "Connecting to ws://127.0.0.1:#{portNumber} ..."
			@socket.send "here!"
			@socket.onmessage = (message) ->
				command = JSON.parse message.data
				if command.Action?
					@controller[command.Action].apply this, command.slice(1)

	throw: (errorMessage) ->
		console.log errorMessage
		#phantom.exit()

class PhantomBrowserController
	constructor: (@owner) ->

	# Commands
	Navigate: (url) ->
		page.onLoadFinished = (status) ->
			@socket.send JSON.stringify({ Response: "ActionCompleted" })

		page.open url

	Find: (selector) ->
		domElement = page.evaluate ->
			document.querySelector(selector)

		@socket.send JSON.stringify({ Response: "ActionCompleted", Element: domElement })

	FindMultiple: (selector) ->
		domElement = page.evaluate ->
			document.querySelectorAll(selector)

		@socket.send JSON.stringify({ Response: "ActionCompleted", Element: domElement })

	Click: (selector, x, y) ->
		page.evaluate ->
			document.querySelector(selector)

	Hover: (select, x, y) ->

	Focus: (select, x, y) ->

	DragAndDrop: (sourceSelector, targetSelector) ->

	SelectText: (selector, text) ->

	SelectValue: (selector, value) ->

	SelectIndex: (selector, index) ->

	TakeScreenshot: (fileName) ->

	Wait: ->

	WaitMilliseconds: (milliseconds) ->

	Press: (keys) ->

	Type: (text) ->

new PhantomWebSocketServer()