#!/usr/bin/env bash

set -e

COMMANDS_TO_RUN=()

COMMANDS_TO_RUN+=('echo Build image and push to hub.docker.com')
COMMANDS_TO_RUN+=('echo "$DOCKER_PASSWORD" | docker login -u "$DOCKER_USERNAME" --password-stdin')
COMMANDS_TO_RUN+=('docker build --rm --no-cache -t mpgp129/mpgpspec -f ./docker/Dockerfile .')
COMMANDS_TO_RUN+=('docker push mpgp129/mpgpspec')

RETURN_CODES=()
FAILURE=0

if [ -n "${COMMANDS_TO_RUN[0]}" ]; then
  echo "Preparing to run commands:"
  for cmd in "${COMMANDS_TO_RUN[@]}"; do
    echo "- $cmd"
  done

  for cmd in "${COMMANDS_TO_RUN[@]}"; do
    echo
    echo "$ $cmd"
    set +e
    eval $cmd
    rc=$?
    set -e
    RETURN_CODES+=($rc)
    if [ $rc -ne 0 ]; then
      FAILURE=$rc
    fi
  done

  echo
  for i in "${!COMMANDS_TO_RUN[@]}"; do
    echo "Received return code ${RETURN_CODES[i]} from: ${COMMANDS_TO_RUN[i]}"
  done
  exit $FAILURE
else
  echo "No commands to run."
fi