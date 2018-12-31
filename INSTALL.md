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

### Change params:

DefaultConnectionString to "psql"

RestApiUrl to "http://0.0.0.0:5000"

---

## Configure database

Create database "mpgp" and user "agent4mpgp" with password "v3ry23C93tp422w0Rd"

Grant all privileges on database "mpgp" to "agent4mpgp"

```sh
./scripts/bash/postgres_initializer.sh
```

---

## Logs

```
sudo mkdir -p /var/log/mpgp
sudo chown -R $USER /var/log/mpgp
sudo chmod -R 777 /var/log/mpgp
