```
docker run --rm --hostname localhost -p 8080:8080 --name mpgpweb mpgp129/mpgpweb:latest
```

```
# param1: dev or prod
./scripts/bash/docker/compose.sh dev
```

```
# param1: mpgpweb or mpgpdb
./scripts/bash/docker/build.sh mpgpweb
```
