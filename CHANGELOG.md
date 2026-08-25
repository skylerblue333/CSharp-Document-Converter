# Changelog

## 0.1.0 — Engineering beta

- narrow the misleading general document-converter surface to explicit plain-text transforms
- support metadata, UTF-8 Base64, and invariant-uppercase outputs only
- reject unsupported input/output formats and cap input at 100,000 characters
- add a reusable transformation core and dependency-free executable tests
- enforce strict Release builds, vulnerable-package inspection, Docker build, non-root execution, and runtime health smoke CI
- fix the container publish entrypoint
- remove fake Node build/test scaffolding
- document SKYCOIN4444 integration and security/product boundaries

No PDF/Office conversion, OCR, rich-document fidelity, file storage, HA, or production deployment is claimed.
