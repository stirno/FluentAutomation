**Working on FluentAutomation Documentation**

We use a node.js build script to compile the markdown to the HTML that is imported and deployed on [http://fluent.stirno.com](http://fluent.stirno.com).

A set of scripts have been added to this repository so that the docs can be viewed similarly to how they will be deployed but its not exact. File an issue if you see any serious differences.

To start development just cd into the Docs directory and run `npm install` and then `npm start`

This will start a webserver on http://localhost:5500, open your web browser and use livereload to refresh results.