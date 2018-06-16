#!/usr/bin/env bash

set -e
set -u

for i in $(ls -d ./tests/*/ | grep Mpgp | sed 's/\/$//'); do
	dotnet test ${i%%/};
done