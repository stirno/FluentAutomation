var fm = require('front-matter'),
	md = require('github-flavored-markdown'),
	hb = require('handlebars'),
	fs = require('fs'),
	path = require('path'),
	async = require('async'),
	connect = require('connect'),
	livereload = require('livereload'),
	minimist = require('minimist'),
	open = require('open');

var versions = [
	{ folder: 'v2', name: 'v2.0', pages: [
		'intro.md',
		'getting-started.md',
		'settings.md',
		'multi-browser.md',
		'sticky-sessions.md',
		'pageobjects.md',
		'method-chaining.md',
		'remote-webdriver.md',
		'scriptcs.md',
		'actions.intro.md',
		'actions.open.md',
		'actions.enter.md',
		'actions.append.md',
		'actions.select.md',
		'actions.click.md',
		'actions.doubleclick.md',
		'actions.rightclick.md',
		'actions.focus.md',
		'actions.hover.md',
		'actions.press.md',
		'actions.type.md',
		'actions.wait.md',
		'actions.waituntil.md',
		'actions.find.md',
		'actions.drag.md',
		'actions.takescreenshot.md',
		'actions.upload.md',
		'asserts.intro.md',
		'asserts.exists.md',
		'asserts.count.md',
		'asserts.value.md',
		'asserts.text.md',
		'asserts.class.md',
		'asserts.url.md',
		'asserts.throws.md',
		'asserts.true.md',
		'asserts.false.md'
	]},
	{ folder: 'v3', name: 'v3.0', pages: [
		'intro.md',
		'getting-started.md',
		'settings.md',
		'multi-browser.md',
		'sticky-sessions.md',
		'pageobjects.md',
		'method-chaining.md',
		'remote-webdriver.md',
		'scriptcs.md',
		'actions.intro.md',
		'actions.open.md',
		'actions.enter.md',
		'actions.append.md',
		'actions.select.md',
		'actions.click.md',
		'actions.doubleclick.md',
		'actions.rightclick.md',
		'actions.focus.md',
		'actions.hover.md',
		'actions.press.md',
		'actions.type.md',
		'actions.wait.md',
		'actions.waituntil.md',
		'actions.find.md',
		'actions.drag.md',
		'actions.takescreenshot.md',
		'actions.upload.md',
		'asserts.intro.md',
		'asserts.exists.md',
		'asserts.count.md',
		'asserts.value.md',
		'asserts.text.md',
		'asserts.class.md',
		'asserts.url.md',
		'asserts.throws.md',
		'asserts.true.md',
		'asserts.false.md'
	]}
];

function buildMarkdown() {
	var docsViewModel = {
		DocLinks: [],
		APILinks: [],
		Pages: []
	};

	versions.forEach(function (version) {
		async.map(version.pages,
			function (fileName, done) {
				fs.readFile(version.folder + '/' + fileName, 'utf8', function (err, data) {
					done(err, {
						name: fileName,
						content: data
					})
				});
			},
			function (err, files) {
				if (err) throw new Error("Bad shit happened. Give up now.");

				files.forEach(function (file) {
					var pageObject = fm(file.content);
					var htmlContent = md.parse(pageObject.body);
					var trimmedName = file.name.substr(0, file.name.length - 3);
					var id = trimmedName.replace(/\./g, '-');

					var linkArray = docsViewModel.DocLinks;
					if (trimmedName.indexOf('.') != -1)
						linkArray = docsViewModel.APILinks;

					linkArray.push({
						href: '#' + id,
						text: pageObject.attributes.link,
						html: id.substring(id.length - 6) == '-intro' ? ' class="heading"' : ''
					});

					docsViewModel.Pages.push({
						id: id,
						title: pageObject.attributes.title,
						content: htmlContent
					});
				});

				var docsTemplate = fs.readFileSync('template.hbs', 'utf8');
				hb.registerPartial('docsTemplate', docsTemplate);
				fs.writeFileSync('docs-' + version.name + '.html', hb.compile(docsTemplate)(docsViewModel), 'utf8');
				console.log('Writing docs-' + version.name + '.html');
				fs.writeFileSync('docs-' + version.name + '-wrapped.html', hb.compile(fs.readFileSync('template-wrapped.hbs', 'utf8'))(docsViewModel), 'utf8');
				console.log('Writing docs-' + version.name + '-wrapped.html');
			}
		);
	});
}

buildMarkdown();

var args = minimist(process.argv.slice(2));
if (args.watch) {
	// watch is enabled, start a server w/livereload
	var server = connect();
	var port = args.port || 5500;

	server.use(require('connect-livereload')());
	server.use(require('serve-static')(__dirname + '/'));
	server.listen(port);

	lr = livereload.createServer({
		originalPath: 'http://localhost:' + port
	});
	lr.watch(path.join(__dirname, '/*.html'));

	open('http://localhost:' + port + '/');
	require('node-watch')(__dirname, function (filename) {
		if (/\.md$/.test(filename)) {
			console.log('Change detected: ' + filename);
			buildMarkdown();
		}
	});
}