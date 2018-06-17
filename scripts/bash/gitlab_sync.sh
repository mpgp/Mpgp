COMMANDS_TO_RUN+=('echo Update GitLab repository')
COMMANDS_TO_RUN+=('git remote add gitlab https://gitlab-ci-token:$GITLAB_TOKEN@gitlab.com/$GITLAB_REPO_OWNER/$GITLAB_REPO_NAME.git')
COMMANDS_TO_RUN+=('git push gitlab $BRANCH')