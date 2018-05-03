#!/usr/bin/env bash

IMAGE=${1:-mpgpweb}
PREFIX=mpgp129

ln -sf $(pwd)/docker/$IMAGE/.dockerignore $(pwd)/.dockerignore

docker build --rm -t $PREFIX/$IMAGE -f ./docker/$IMAGE/Dockerfile .