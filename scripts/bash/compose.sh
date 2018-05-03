#!/usr/bin/env bash

ENV=${1:-prod}

 (cd ./docker/ && docker-compose -f docker-compose.$ENV.yml up -d)