# Regulatory and Licensing System

A full-stack licensing system built with .NET 10 and Blazor WebAssembly, featuring AI-assisted verification and a robust review workflow.

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PowerShell](https://microsoft.com/powershell) (for Playwright setup)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/satriansyahw/xtre_msf.git
   ```
2. Restore and Build:
   ```bash
   dotnet build
   ```
3. Run the API:
   ```bash
   dotnet run --project src/Licensing.Api
   ```
4. Run the Client:
   ```bash
   dotnet run --project src/Licensing.Client
   ```

## 🧪 Testing
The system includes both unit and end-to-end tests.

### Unit Tests (xUnit)
```bash
dotnet test tests/Licensing.Tests.Unit
```

### E2E Tests (Playwright)
1. Install browsers (first time only):
   ```bash
   powershell tests/Licensing.Tests.E2E/bin/Debug/net10.0/playwright.ps1 install
   ```
2. Run tests:
   ```bash
   dotnet test tests/Licensing.Tests.E2E
   ```

## 🤖 AI Usage Disclosure
This project was developed with the assistance of **Antigravity (AI Coding Assistant)**.
- **Role**: AI assisted in architectural planning, boilerplate generation, UI styling, and implementing the background verification service.
- **Review**: All AI-generated code was strictly reviewed, refactored for Clean Architecture compliance, and verified through manual and automated testing.

## 🛠️ Architecture
See [SCOPE.md](./SCOPE.md) for detailed technical documentation.
