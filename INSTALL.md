# Mpgp

Multiplayer Game Platform

---

## Download .NET Core

[https://www.microsoft.com/net/learn/get-started/linuxubuntu](https://www.microsoft.com/net/learn/get-started/linuxubuntu)

---

## Download PostgreSQL

[https://www.postgresql.org/download/](https://www.postgresql.org/download/)

---

## Edit configuration

```sh
cp ./tools/appsettings.default.json ./tools/appsettings.json
(cd ./src/Mpgp.DataAccess && dotnet restore)
nano ./tools/appsettings.json
```

---

## Configure database

Create database "mpgp" and user "agent4mpgp" with password "v3ry23C93tp422w0Rd"

Grant all privileges on database "mpgp" to "agent4mpgp"

```sh
./scripts/bash/postgres_initializer.sh
```

---

## Update database, create tables

```sh
# Recommended using dotnet CLI
(cd ./src/Mpgp.DataAccess && dotnet ef database update)

# Or run migrator script
(cd ./scripts/bash/ && ./postgres_migrator.sh)
```
