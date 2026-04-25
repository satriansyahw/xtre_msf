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

## 🔮 What I Would Do Next (Gaps & Priorities)
While the MVP covers the core requirements for Use Case 1 and 2, the following features would be next in a production roadmap:
1.  **Identity & Access Management (IAM)**: Implement real authentication using OIDC/OAuth2 (e.g., Entra ID or Auth0) to secure the persona-based views.
2.  **Use Case 3 (On-Site Assessment)**: Complete the mobile-first inspection checklist and post-site clarification workflow.
3.  **Real-Time Notifications**: Integrate SignalR to notify Officers when resubmissions occur and Operators when decisions are made.
4.  **Blob Storage Integration**: Migrate from local file storage to a cloud-native solution like Azure Blob Storage or AWS S3 for scalability.
5.  **Audit Trail UI**: Add a dedicated "Audit History" tab for each application to show every status change and internal note in a timeline view.
6.  **Advanced Version Diffing**: Improve the JSON comparison UI to highlight specific field changes rather than just showing raw side-by-side JSON.

## 🤖 AI Usage Disclosure
This project was developed with the assistance of **Antigravity (AI Coding Assistant)**.
- **Role**: AI assisted in architectural planning, boilerplate generation, UI styling, and implementing the background verification service.
- **Review**: All AI-generated code was strictly reviewed, refactored for Clean Architecture compliance, and verified through manual and automated testing.

## 🛠️ Architecture
See [SCOPE.md](./SCOPE.md) for detailed technical documentation.

## 📖 User Guidance
See [guidance.md](./guidance.md) for step-by-step instructions on how to use the system as an Operator or Officer.

## ✨ Latest Features
- **Audit Trail**: Full visibility of application status transitions in a vertical timeline.
- **Revision History**: Comparison tool for both Operators and Officers to track data changes across submission rounds.
- **Drag-and-Drop Uploads**: Enhanced UX for document submissions with real-time AI status polling.
