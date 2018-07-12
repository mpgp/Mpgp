COMMANDS_TO_RUN+=('echo Build image and push to hub.docker.com')
COMMANDS_TO_RUN+=('echo "$DOCKER_PASSWORD" | docker login -u "$DOCKER_USERNAME" --password-stdin')

COMMANDS_TO_RUN+=('bash scripts/bash/docker/build.sh $DOCKER_IMAGE_NAME_WEB')
COMMANDS_TO_RUN+=('docker push $DOCKER_IMAGE_OWNER/$DOCKER_IMAGE_NAME_WEB')

COMMANDS_TO_RUN+=('bash scripts/bash/docker/build.sh $DOCKER_IMAGE_NAME_DB')
COMMANDS_TO_RUN+=('docker push $DOCKER_IMAGE_OWNER/$DOCKER_IMAGE_NAME_DB')