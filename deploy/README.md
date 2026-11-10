# CI/CD

`master` push runs Release checks and the production container smoke tests. Pull requests run the same checks without deploying. In **Actions → CI and production deployment → Run workflow**, select `master`; disable `deploy` for CI only.

Successful releases produce `dynamicchart-api:sha-<commit>`, upload a one-day artifact and request the `production-dynamicchart-api` environment deployment. The job waits for Ubuntu's result; it fails rather than reporting success if the agent is offline or the rollout fails. Environment protection rules apply before the release job starts.

The Ubuntu outbound-only deployment agent is maintained in [MyCMS](https://github.com/bayansalduza/MyCMS/blob/master/docs/cicd.tr.md). It verifies the trusted branch, successful checks, artifact provenance and SHA256, updates only the API service using the existing live Compose configuration, checks HTTPS health and rolls back an unhealthy image. No production runner, incoming SSH port or registry password is needed. The agent must be installed once before enabling live releases.

Production Docker configuration preserves the live `/healthz`, Caddy forwarding and origin allowlist. Override `Cors__AllowedOrigins__0` and `ReverseProxy__Address` in the server's runtime configuration if the deployment addresses change. Database credentials are supplied per API request, not built into the image. Do not publish real connection credentials in source, documentation, artifacts or logs. Local Development keeps its existing CORS/HTTPS behavior.
