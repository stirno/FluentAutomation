COFFEE := node_modules/.bin/coffee
SQUASH := node_modules/.bin/squash

ifeq ($(OS),Windows_NT)
	# Change path separators on Windows as it does not like forward-slashes in binary paths
	COFFEE := $(subst /,\,$(COFFEE))
	SQUASH := $(subst /,\,$(SQUASH))
endif

build: build-node build-browser

build-node:
	$(COFFEE) -o lib -c src

build-browser:
	$(SQUASH) --coffee -f yaml.js -o -r ./=yaml
	$(SQUASH) --coffee -c -f yaml.min.js -o -r ./=yaml

test: build
	$(COFFEE) test/test.coffee

.PHONY: build build-node build-browser test