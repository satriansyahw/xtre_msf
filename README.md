# Regulatory and Licensing System

A modern, full-stack regulatory platform built with **.NET 10** and **Blazor WebAssembly**. This system automates the lifecycle of business licensing, from initial submission and AI-assisted document verification to complex multi-step officer reviews.

---

## 🌟 Key Features

- **Dual-Persona Interface**: Specialized workspaces for **Operators** (Applicants) and **Officers** (Regulators).
- **Snapshot-Based Versioning**: Every resubmission creates an immutable version of the application, ensuring a full historical record.
- **AI Document Verification (Mocked)**: Asynchronous background processing of uploaded documents with "Verified" or "Flagged" status detection.
- **Strict State Machine**: Robust handling of application lifecycles including `Under Review`, `Pending Resubmission`, and `Final Decision`.
- **Audit Trail**: Detailed chronological log of every action taken on an application.
- **Reactive Notification System**: Role-specific alerts for status updates and feedback requests.

---

## 🛠️ Technology Stack

- **Frontend**: Blazor WebAssembly (.NET 10) with Vanilla CSS & Bootstrap 5.
- **Backend**: ASP.NET Core 8 Web API.
- **Persistence**: SQLite with Entity Framework Core (Code First).
- **Testing**: xUnit for Business Logic, Playwright for E2E flows.
- **Architecture**: Clean Architecture (Domain-Driven Design principles).

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Node.js (Optional, for advanced styling)

### Installation & Local Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/satriansyahw/xtre_msf.git
   cd xtre_msf
   ```

2. **Database Initialization**:
   The system uses SQLite. Run the following to ensure the schema is up to date:
   ```bash
   dotnet ef database update --project src/Licensing.Infrastructure --startup-project src/Licensing.Api
   ```

3. **Run the Application**:
   It is recommended to run the API and Client separately to mirror the production dual-port environment:

   - **Terminal 1 (API - Port 5200)**:
     ```bash
     dotnet run --project src/Licensing.Api --launch-profile http
     ```
   - **Terminal 2 (Client - Port 5237)**:
     ```bash
     dotnet run --project src/Licensing.Client --launch-profile http
     ```

4. **Access the System**:
   Open [http://localhost:5237](http://localhost:5237) in your browser.

---

## 🧪 Testing

### Automated Unit Tests
```bash
dotnet test tests/Licensing.Tests.Unit
```

### End-to-End Tests (Playwright)
The system includes E2E tests to verify persona switching and application submission flows.
1. **Install Playwright Browsers**:
   ```bash
   pwsh tests/Licensing.Tests.E2E/bin/Debug/net10.0/playwright.ps1 install
   ```
2. **Run E2E Tests**:
   ```bash
   dotnet test tests/Licensing.Tests.E2E
   ```

---

## 📖 System Workflows

### Operator Persona
- **Dashboard**: Track personal applications and status.
- **Submission**: Multi-step wizard with drag-and-drop document uploads.
- **Revisions**: Address Officer feedback and resubmit new versions.

### Officer Persona
- **Review Queue**: Manage incoming applications from all operators.
- **Review Workspace**: Field-level feedback, document verification, and status transitions.
- **Versioning**: Compare the "Current" vs "Previous" versions of an application.

---

## 🤖 AI Usage Disclosure
This project was developed with the assistance of **Antigravity (AI Coding Assistant)**.
- **Role**: AI assisted in architectural planning, boilerplate generation, UI styling, and implementing the background verification service.
- **Review**: All AI-generated code was strictly reviewed, refactored for Clean Architecture compliance, and verified through manual and automated testing.

---

## 📄 Documentation
- [SCOPE.md](./SCOPE.md): Detailed Technical Justification and Architecture.
- [guidance.md](./guidance.md): User manual and test scenarios.
