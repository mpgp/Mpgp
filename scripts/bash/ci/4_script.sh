#!/usr/bin/env bash

set -e

COMMANDS_TO_RUN=()

COMMANDS_TO_RUN+=('echo 4_script.sh ...')
COMMANDS_TO_RUN+=('echo run tests')
COMMANDS_TO_RUN+=('yarn run ci:e2e')
COMMANDS_TO_RUN+=('yarn run ci:test')

COMMANDS_TO_RUN+=('echo Generate coverage info and deploy it to coveralls.io')
COMMANDS_TO_RUN+=('cat ./coverage/lcov.info | ./node_modules/coveralls/bin/coveralls.js')

COMMANDS_TO_RUN+=('echo Copy ci files for prevent errors when deploying')
COMMANDS_TO_RUN+=('cp .travis.yml ./dist')
COMMANDS_TO_RUN+=('cp -r .circleci ./dist')

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