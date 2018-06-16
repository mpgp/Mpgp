#!/usr/bin/env bash

set -e
set -u

COMMANDS_TO_RUN=()

COMMANDS_TO_RUN+=('echo Update GitLab repository')
COMMANDS_TO_RUN+=('git remote add gitlab https://gitlab-ci-token:$GITLAB_TOKEN@gitlab.com/mpgp/mpgp.github.io.git')
COMMANDS_TO_RUN+=('git push gitlab $BRANCH')

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