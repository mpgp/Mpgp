# Mpgp

[![Greenkeeper badge](https://badges.greenkeeper.io/mpgp/Mpgp.svg)](https://greenkeeper.io/)

## Multiplayer Game Platform

---

Api will available on `http://localhost:5000/api/{controller}/{params?}`

WebSocket will available on `ws://localhost:5000/elite-crew`

---

* Docker environment:

```
docker-compose up
```

---

---

* Local development environment:

Follow these instructions: [INSTALL.md](INSTALL.md)

* Publish

```sh
dotnet publish -c release
```

---

* Startup

```sh
(cd ./src/Mpgp.RestApiServer/bin/release/netcoreapp2.0/publish && dotnet Mpgp.RestApiServer.dll)
```

---

* Run tests

```sh
dotnet test ./tests/Mpgp.IntegrationTests
dotnet test ./tests/Mpgp.UnitTests

# Or 
./scripts/bash/test_runner.sh

# Or
for i in $(ls -d ./tests/*/ | grep Mpgp | sed 's/\/$//'); do dotnet test ${i%%/}; done
```

---

* Project Structure

```
.
|-- .github/
|-- artifacts/
|-- docker/
|-- libs/
|-- scripts/
|-- src/
|-- tests/
|-- tools/
|-- .gitignore
|-- .gitattributes
|-- CHANGELOG.md
|-- INSTALL.md
|-- LICENSE
|-- README.md
|-- TODO
L-- VERSION
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
CHANGELOG.md - All notable changes to this project will be documented in this file.
INSTALL.md - How to build, install, compile, how to do database migrations.
LICENSE - BSD 2-Clause "Simplified" License
README.md - Info about the project.
TODO - Any general to do items.
VERSION - Current version.
```

---

*Repository Structure was designed by [Saritasa team](https://github.com/orgs/Saritasa/people).*