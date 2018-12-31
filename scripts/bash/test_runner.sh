#!/usr/bin/env bash

set -e
set -u

for i in $(ls -d ./tests/*/ | grep Mpgp | sed 's/\/$//'); do
    echo '----------------' && echo test ${i%%/} && dotnet test ${i%%/} && echo test ${i%%/} && echo '----------------';
done
