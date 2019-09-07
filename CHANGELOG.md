# [2.4.0](https://github.com/mpgp/Mpgp/compare/v2.3.2...v2.4.0) (2019-09-07)


### Bug Fixes

* **core:** update .gitignore ([6e5f04f](https://github.com/mpgp/Mpgp/commit/6e5f04f))
* **core:** update commitlint rules ([bb6788d](https://github.com/mpgp/Mpgp/commit/bb6788d))
* **core:** update configs for mpgpdb ([069ce9a](https://github.com/mpgp/Mpgp/commit/069ce9a))
* **core:** update docker.compose.prod.yml ([a42c41e](https://github.com/mpgp/Mpgp/commit/a42c41e))
* **core:** update initial migration ([6272014](https://github.com/mpgp/Mpgp/commit/6272014))


### Features

* **core:** add elasticsearch and kibana support ([9e00ae2](https://github.com/mpgp/Mpgp/commit/9e00ae2))
* **core:** add grafana + prometheus ([b7bd8a1](https://github.com/mpgp/Mpgp/commit/b7bd8a1))

## [2.3.3](https://github.com/mpgp/Mpgp/compare/v2.3.2...v2.3.3) (2019-06-23)


### Bug Fixes

* **core:** update .gitignore ([6e5f04f](https://github.com/mpgp/Mpgp/commit/6e5f04f))
* **core:** update configs for mpgpdb ([069ce9a](https://github.com/mpgp/Mpgp/commit/069ce9a))
* **core:** update docker.compose.prod.yml ([a42c41e](https://github.com/mpgp/Mpgp/commit/a42c41e))
* **core:** update initial migration ([6272014](https://github.com/mpgp/Mpgp/commit/6272014))

## [2.3.3](https://github.com/mpgp/Mpgp/compare/v2.3.2...v2.3.3) (2019-06-23)


### Bug Fixes

* **core:** update .gitignore ([6e5f04f](https://github.com/mpgp/Mpgp/commit/6e5f04f))
* **core:** update configs for mpgpdb ([069ce9a](https://github.com/mpgp/Mpgp/commit/069ce9a))
* **core:** update initial migration ([6272014](https://github.com/mpgp/Mpgp/commit/6272014))

## [2.3.2](https://github.com/mpgp/Mpgp/compare/v2.3.1...v2.3.2) (2019-01-06)


### Bug Fixes

* **core:** add missing mapper initialization ([ef60f3c](https://github.com/mpgp/Mpgp/commit/ef60f3c))

## [2.3.1](https://github.com/mpgp/Mpgp/compare/v2.3.0...v2.3.1) (2018-07-24)


### Bug Fixes

* **api:** add checking for oldpassword in UpdatePasswordCommandHandler ([1f48246](https://github.com/mpgp/Mpgp/commit/1f48246))
* **api:** set errorCode to 400 if modelstate is invalid ([131e8f1](https://github.com/mpgp/Mpgp/commit/131e8f1))

# [2.3.0](https://github.com/mpgp/Mpgp/compare/v2.2.1...v2.3.0) (2018-07-22)


### Features

* **api:** add account management ([3d06e76](https://github.com/mpgp/Mpgp/commit/3d06e76))

## [2.2.1](https://github.com/mpgp/Mpgp/compare/v2.2.0...v2.2.1) (2018-07-08)


### Bug Fixes

* **ws:** remove 'status' field from messages ([ba52b3f](https://github.com/mpgp/Mpgp/commit/ba52b3f))

# [2.2.0](https://github.com/mpgp/Mpgp/compare/v2.1.0...v2.2.0) (2018-07-08)


### Features

* add jwt authentication ([fff888f](https://github.com/mpgp/Mpgp/commit/fff888f))

# [2.1.0](https://github.com/mpgp/Mpgp/compare/v2.0.3...v2.1.0) (2018-07-01)


### Features

* **api:** add exceptions endpoint ([e4358a0](https://github.com/mpgp/Mpgp/commit/e4358a0))

## [2.0.3](https://github.com/mpgp/Mpgp/compare/v2.0.2...v2.0.3) (2018-06-16)


### Bug Fixes

* resolve problems with docker ([8a56eaa](https://github.com/mpgp/Mpgp/commit/8a56eaa))

## [2.0.2](https://github.com/mpgp/Mpgp/compare/v2.0.1...v2.0.2) (2018-06-16)


### Bug Fixes

* use alpine versions for docker images ([5f2c027](https://github.com/mpgp/Mpgp/commit/5f2c027))

# 2.0.1 (2018-06-16)


### Bug Fixes

* add  to bash scripts ([5a7b8be](https://github.com/mpgp/Mpgp/commit/5a7b8be))
* use regex  for username ([429e92d](https://github.com/mpgp/Mpgp/commit/429e92d))

---

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

# [2.0.0](https://github.com/mpgp/Mpgp/releases/tag/2.0.0) (2018-05-29)

## _The Reborn_

### What’s New

* Docker support.
* New project structure.
* Stylecop analyzers and rulesets.
* New approach to writing tests.
* Advanced and structured logging for project.

### BREAKING CHANGES

* Completely redesigned source code.
* Combine projects [WebSocketServer](https://github.com/mpgp/WebSocketServer) and [WebApiServer](https://github.com/mpgp/WebApiServer) in [Mpgp](https://github.com/mpgp/Mpgp).
* Rename [WebApiServer](https://github.com/mpgp/WebApiServer) to [RestApiServer](https://github.com/mpgp/Mpgp/tree/master/src/Mpgp.RestApiServer).

### Deprecated code

* **ws**: We have refused to support multiple websocket servers. Now we will have only one websocket server. And, accordingly, removed the [API Endpoint](https://github.com/mpgp/WebApiServer/wiki/Controller.Server) for receiving a servers.

### Bug Fixes

* Fix codestyle warnings.

### Performance Improvements

* Remove duplication logic in RestApiServer and WebSocketServer.
