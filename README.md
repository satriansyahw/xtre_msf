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

### Tools & Models
- **Antigravity**: Used as the main orchestration tool for workflows, planning, coding, and execution.
- **Gemini 1.5 Pro**: Used for most tasks (planning, coding, issue generation).
- **Claude 3.5 Sonnet**: Used specifically for Pull Request reviews to provide critical and detailed feedback.

### How AI Was Used
AI was an integral assistant across the full development lifecycle:

- **Workflow Creation**: Implementation of standard operating procedures:
  - `task-breakdown.md`, `create-issue.md`, `git-assistant.md`, `review-pr.md`.
  - `senior-engineer.md` (Global personality and quality rules).
- **Planning & Documentation**:
  - Phase-based issue generation.
  - Development of detailed implementation plans and GitHub issue translations.
- **Development**:
  - Backend (C# / ASP.NET Core) and Frontend (Blazor WebAssembly) implementation.
  - Database schema design and SQLite integration.
  - Multi-part file upload handling and local storage management.
- **Testing**:
  - Functional validation and unit testing.
  - Automated End-to-End (E2E) testing using Playwright.
- **PR Workflow**:
  - Automated PR creation and cross-model PR reviews (Claude Opus).
  - Iterative bug fixing based on detailed review feedback.

### Example Prompts / Instructions
Key instructions given to the AI during the process:
- **Strict Control**: "Don't code unless explicitly asked", "If create-issue -> only create issue, no coding".
- **Planning**: "Create implementation plan for Phase X", "Translate plan to English and submit as GitHub issue".
- **Review**: "Fix all issues identified in this PR review".
- **Testing**: "Run Playwright, test all requirements, ensure API is running before testing to avoid port refused errors".

### Validation & Review Process
All AI-generated outputs were subject to human oversight:
- **Manual Review**: Code and plans were reviewed before execution.
- **Cross-Model Verification**: Utilizing different models for development vs. review.
- **Local Validation**: Manual verification of Swagger endpoints and UI behavior.
- **Testing**: Final validation via a full suite of Playwright E2E tests.

### Corrections & Iterations
The process involved several iterative cycles to ensure quality:
- **Refinement**: Correcting the AI when it attempted to code prematurely during the planning phase.
- **Bug Squashing**: Manual debugging of UI issues like persona toggle reactivity.
- **E2E Stability**: Resolving port conflicts and environment readiness issues during Playwright setup.

### Limitations & Challenges
- Occasionally required stricter prompting to follow complex sequence instructions.
- Minor bug introductions after merges required rapid follow-up iterations.
- Environment-specific challenges (port management in dual-app setups).

### Key Takeaway
AI significantly improved development speed, plan clarity, and test coverage. However, **human validation was essential** to ensure production-ready correctness, completeness, and adherence to the specific assessment requirements.

---

## 📄 Documentation
- [SCOPE.md](./SCOPE.md): Detailed Technical Justification and Architecture.
- [guidance.md](./guidance.md): User manual and test scenarios.
