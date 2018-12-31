```
docker pull mpgp129/mpgpclient
docker run --rm --hostname localhost -p 8080:8080 --name mpgpclient mpgp129/mpgpclient:latest
```

```
# param1: dev or prod
./scripts/bash/docker/compose.sh dev
```

```
# param1: mpgpweb or mpgpdb
./scripts/bash/docker/build.sh mpgpweb
```
