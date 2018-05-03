#!/usr/bin/env bash

for i in $(ls -d ./tests/*/ | grep Mpgp | sed 's/\/$//'); do
	dotnet test ${i%%/};
done