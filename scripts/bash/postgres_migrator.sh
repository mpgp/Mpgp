#!/usr/bin/env bash

set -e
set -u

echo "[postgres_migrator]: run migrations..."

for migration in $(ls ../../artifacts/sql/); do
	# psql -v ON_ERROR_STOP=1 --username "postgres" << cat "../../artifacts/sql/$migration"
	psql -h localhost -d mpgp -U postgres -p 5432 -a -q -f "../../artifacts/sql/$migration"
done

echo "[postgres_migrator]: migrations done."