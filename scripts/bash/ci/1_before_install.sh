#!/usr/bin/env bash

set -e

COMMANDS_TO_RUN=()

COMMANDS_TO_RUN+=('echo 1_before_install.sh ...')
COMMANDS_TO_RUN+=('sudo chown root /opt/google/chrome/chrome-sandbox')
COMMANDS_TO_RUN+=('sudo chmod 4755 /opt/google/chrome/chrome-sandbox')
COMMANDS_TO_RUN+=('export CHROME_BIN=chromium-browser')
COMMANDS_TO_RUN+=('export DISPLAY=:99.0')
COMMANDS_TO_RUN+=('sh -e /etc/init.d/xvfb start')

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