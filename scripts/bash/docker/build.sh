#!/usr/bin/env bash

set -e
set -u

IMAGE=${1:-mpgpweb}
PREFIX=mpgp129

ln -sf $(pwd)/docker/$IMAGE/.dockerignore $(pwd)/.dockerignore

docker build --rm --no-cache -t $PREFIX/$IMAGE -f ./docker/$IMAGE/Dockerfile .