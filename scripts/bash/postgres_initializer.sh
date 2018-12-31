#!/usr/bin/env bash

set -e
set -u

echo "[postgres_initializer]: begin..."

psql -v ON_ERROR_STOP=1 --username "postgres" <<-EOSQL
    CREATE USER agent4mpgp;
    CREATE DATABASE mpgp;
    GRANT ALL PRIVILEGES ON DATABASE mpgp TO agent4mpgp;
    ALTER USER agent4mpgp WITH PASSWORD 'v3ry23C93tp422w0Rd';
EOSQL

echo "[postgres_initializer]: end..."
