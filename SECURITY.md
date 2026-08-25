# Security Policy

## Supported status

Sky Text Transform is an **engineering beta**. CI verifies restore, strict Release compilation, executable core tests, vulnerable-package inspection, container build, non-root execution, and a liveness smoke check. These checks do not establish production security or deployment readiness.

## Current boundaries

The service accepts bounded plain text and performs deterministic metadata, Base64, or invariant-uppercase transformations. It does not parse or render PDF/Office/HTML documents, execute macros/scripts, scan malware, authenticate callers, isolate tenants, persist content, encrypt stored documents, or provide durable audit retention.

Treat submitted text as potentially sensitive. Do not send credentials, private keys, tokens, regulated data, or unredacted customer documents unless a separately reviewed transport/access-control policy is in place.

## Reporting

Report suspected vulnerabilities privately through GitHub security reporting when available. Do not include sensitive documents, credentials, or customer content in public issues.
