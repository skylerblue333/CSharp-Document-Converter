# Sky Text Transform — C# Engineering Beta

Sky Text Transform is a focused ASP.NET Core service for deterministic transformations of bounded plain-text input. It deliberately replaces the broader “document converter” claim with the behaviors this repository actually implements and verifies.

## Status

**Engineering beta.** The service accepts text input up to 100,000 characters and supports exactly three outputs: `metadata`, `base64`, and `upper`. It validates unsupported input/output formats, exposes health/readiness endpoints, has dependency-free executable core tests, strict Release compilation, vulnerable-package inspection, and non-root container verification.

It does **not** claim PDF or Office conversion, HTML rendering, OCR, file upload parsing, document storage, malware scanning, rich-format fidelity, HA, or production deployment.

## API

- `GET /healthz` — process liveness.
- `GET /readyz` — current size and format contract.
- `POST /v1/convert` — transform text.

Example:

```json
{
  "content": "Hello Sky",
  "from": "text",
  "to": "base64"
}
```

Supported `to` values:

- `metadata` — preserve content and report character/UTF-8 byte counts.
- `base64` — UTF-8 encode and return Base64 text.
- `upper` — invariant uppercase transformation.

Unsupported `from`/`to` values return HTTP 422 instead of silently pretending a conversion succeeded.

## Run locally

```bash
dotnet restore CSharp-Document-Converter.csproj
dotnet run --project CSharp-Document-Converter.csproj
```

## Verify

```bash
dotnet build CSharp-Document-Converter.csproj -c Release
dotnet run --project tests/ConverterTests.csproj -c Release
dotnet list CSharp-Document-Converter.csproj package --vulnerable --include-transitive
docker build -t sky-text-transform .
docker run --rm --entrypoint=id sky-text-transform -u
```

The runtime image uses the non-root `app` user from the .NET 8 image. CI also starts the image and verifies `/healthz`.

## Architecture

`TextTransforms.cs` contains the reusable deterministic transformation core. `Program.cs` exposes that core through a small ASP.NET Core HTTP boundary. The service keeps content process-local and does not persist documents.

## SKYCOIN4444 integration

SKYCOIN4444 services can use this component for small deterministic text normalization/encoding tasks through a stable HTTP boundary instead of embedding duplicate transformation logic. Rich document conversion should use a separate purpose-built service whose supported formats and security model are independently verified.

## Security and operational boundaries

The service does not authenticate callers, scan content for malware/secrets, provide tenant isolation, encrypt stored documents, or retain conversion history. Input is treated as text only and is bounded by character count. Put appropriate gateway/access controls in front of it before use outside a trusted development environment.

## License

See `LICENSE`.
