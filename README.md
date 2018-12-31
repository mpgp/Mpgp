# Mpgp

https://mpgp.github.io/#/spec/

[![Multiplayer Game Platform](https://sun9-4.userapi.com/c830309/v830309006/7e7bf/GO75bBP796g.jpg)](https://mpgp.github.io/#/spec/)

---

[![GitHub version](https://badge.fury.io/gh/mpgp%2FMpgp.svg)](https://badge.fury.io/gh/mpgp%2FMpgp)
[![License](https://img.shields.io/badge/License-BSD%202--Clause-orange.svg)](LICENSE)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-7fa706.svg?longCache=true)](.github/PULL_REQUEST_TEMPLATE.md)

[![stable](https://img.shields.io/badge/stability-stable-blue.svg?longCache=true)](https://github.com/Naereen/badges)
[![Build status](https://api.travis-ci.com/mpgp/Mpgp.svg?branch=master)](https://api.travis-ci.com/mpgp/Mpgp.svg?branch=master)
[![CircleCI](https://circleci.com/gh/mpgp/Mpgp.svg?style=svg)](https://circleci.com/gh/mpgp/Mpgp)

[![Known Vulnerabilities](https://snyk.io/test/github/mpgp/Mpgp/badge.svg?targetFile=package.json)](https://snyk.io/test/github/mpgp/Mpgp?targetFile=package.json) [![Greenkeeper badge](https://badges.greenkeeper.io/mpgp/Mpgp.svg)](https://greenkeeper.io/)

---

## Prerequisites

* For local development environment follow these instructions: [INSTALL.md](INSTALL.md)
* For docker environment follow these instructions [docker/README.md](https://github.com/mpgp/DevOps/tree/master/docker)

## Development server

Run `yarn run start:dev` for a dev server.

Api will available on `http://localhost:5000/api/{controller}/{params?}`

WebSocket will available on `ws://localhost:5000/elite-crew`


## Run tests

```sh
yarn run test:run
```

---

## Project Structure

```
.
|-- .circleci/
|-- .github/
|-- .vscode/
|-- artifacts/
|-- docker/
|-- libs/
|-- scripts/
|-- src/
|-- tests/
|-- tools/
|-- .gitignore
|-- .gitattributes
|-- .ToDo
|-- .travis.yml
|-- CHANGELOG.md
|-- INSTALL.md
|-- LICENSE
|-- package.json
|-- README.md
L-- yarn.lock
```

```
.github/ - Templates for issues and pull requests.
artifacts/ - HTML pages, SQL scripts, other stuff.
docker/ - Docker images and compose files.
libs/ - Any additional libraries.
scripts/ - Any additional scripts.
src/ - Source code.
tests/ - Project tests.
tools/ - Any additional tools like a appsettings.json, nlog.config, etc.
.ToDo - Any general to do items.
.travis.yml - Travis Ci file.
CHANGELOG.md - All notable changes to this project will be documented in this file.
INSTALL.md - How to build, install, compile, how to do database migrations.
LICENSE - BSD 2-Clause "Simplified" License
package.json - Npm dependencies and configurations.
README.md - Info about the project.
yarn.lock - Current packages version.
```

---

*Repository Structure was designed by [Saritasa team](https://github.com/orgs/Saritasa/people).*
