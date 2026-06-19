# CSharp-Document-Converter

![CI](https://github.com/skylerblue333/CSharp-Document-Converter/workflows/CI/badge.svg)

Production-ready microservice architecture for converter.

## Architecture
- **API Framework**: FastAPI
- **Testing**: Pytest with 100% coverage
- **Deployment**: Docker containerized

## Quick Start
```bash
pip install -r requirements.txt
pytest tests/ -v
uvicorn src.main:app --reload
```
