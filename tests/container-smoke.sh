#!/usr/bin/env bash
set -euo pipefail
container="dynamicchart-api-smoke-$$"
trap 'docker rm -f "$container" >/dev/null 2>&1 || true' EXIT
docker run --detach --name "$container" "${1:?Pass the built image}" >/dev/null
for attempt in $(seq 1 45); do
    status="$(docker inspect --format '{{.State.Health.Status}}' "$container")"
    [[ "$status" == healthy ]] && break
    [[ "$status" != unhealthy ]]
    sleep 2
done
[[ "$status" == healthy ]]
docker exec "$container" curl -fsS http://localhost:8080/healthz
allowed="$(docker exec "$container" curl -fsS -D - -o /dev/null -X OPTIONS \
    -H 'Origin: https://dynamic-chart-fe.bakibayansalduz.com' \
    -H 'Access-Control-Request-Method: GET' http://localhost:8080/healthz)"
grep -qi '^Access-Control-Allow-Origin: https://dynamic-chart-fe.bakibayansalduz.com' <<< "$allowed"
denied="$(docker exec "$container" curl -sS -D - -o /dev/null -X OPTIONS \
    -H 'Origin: https://untrusted.example' -H 'Access-Control-Request-Method: GET' \
    http://localhost:8080/healthz)"
! grep -qi '^Access-Control-Allow-Origin:' <<< "$denied"
echo 'Production health and CORS smoke checks passed.'
