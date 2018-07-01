# [2.1.0](https://github.com/mpgp/Mpgp/compare/v2.0.3...v2.1.0) (2018-07-01)


### Features

* **api:** add exceptions endpoint ([e4358a0](https://github.com/mpgp/Mpgp/commit/e4358a0))

## [2.0.3](https://github.com/mpgp/Mpgp/compare/v2.0.2...v2.0.3) (2018-06-16)


### Bug Fixes

* resolve problems with docker ([8a56eaa](https://github.com/mpgp/Mpgp/commit/8a56eaa))

## [2.0.2](https://github.com/mpgp/Mpgp/compare/v2.0.1...v2.0.2) (2018-06-16)


### Bug Fixes

* use alpine versions for docker images ([5f2c027](https://github.com/mpgp/Mpgp/commit/5f2c027))

# 1.0.0 (2018-06-16)


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
