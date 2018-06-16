#!/usr/bin/env bash

set -e
set -u

COMMANDS_TO_RUN=()

COMMANDS_TO_RUN+=('echo 1_before_install.sh ...')
# COMMANDS_TO_RUN+=('nvm install lts/*')
# COMMANDS_TO_RUN+=('sudo apt-key adv --fetch-keys http://dl.yarnpkg.com/debian/pubkey.gpg')
# COMMANDS_TO_RUN+=('echo "deb http://dl.yarnpkg.com/debian/ stable main" | sudo tee /etc/apt/sources.list.d/yarn.list')
# COMMANDS_TO_RUN+=('sudo apt-get update -qq')
# COMMANDS_TO_RUN+=('sudo apt-get install -y -qq yarn')

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