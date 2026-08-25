# Contributing to Sky Text Transform

## Development setup

```bash
dotnet restore CSharp-Document-Converter.csproj
dotnet build CSharp-Document-Converter.csproj -c Release
dotnet run --project tests/ConverterTests.csproj -c Release
```

For container changes:

```bash
docker build -t sky-text-transform .
docker run --rm --entrypoint=id sky-text-transform -u
```

## Scope and correctness

- Keep the service focused on bounded plain-text transforms unless a new format is fully implemented and tested.
- Add tests for every supported transform and validation edge case.
- Do not silently pass through unsupported formats.
- Do not claim PDF, Office, OCR, rich-document fidelity, malware scanning, HA, or production deployment without implementation and evidence.
- Keep warnings-as-errors and non-root packaging intact unless a documented replacement provides equivalent verification.

## Pull requests

1. Create a focused branch.
2. Make the smallest coherent change.
3. Run the Release build and executable test project.
4. Document any format/security boundary changes.
5. Open a pull request with a truthful maturity status.

## License

See `LICENSE`.
