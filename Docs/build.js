var fm = require('front-matter'),
	md = require('github-flavored-markdown'),
	hb = require('handlebars'),
	fs = require('fs'),
	async = require('async');

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

			var template = fs.readFileSync('template.hbs', 'utf8');
			var templateFn = hb.compile(template);
			var htmlResult = templateFn(docsViewModel);

			fs.writeFileSync('docs-' + version.name + '.html', htmlResult, 'utf8');
		}
	);
});