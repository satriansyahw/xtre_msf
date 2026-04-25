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
- **Validation**: Data Annotations (Backend) and EditForm/Manual Validation (Frontend).
- **Error Handling**: Comprehensive UI alerts and logical state-machine protection.

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
- **Gemini 3 Flash**: Used for most tasks (planning, coding, issue generation).
- **Gemini 3.1 Pro/Claude Opus**: Used specifically for Pull Request reviews to provide critical and detailed feedback.

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
  - Manual testing.
- **PR Workflow**:
  - Automated PR creation and cross-model PR reviews (Claude Opus/Gemini 3.1 Pro).
  - Iterative bug fixing based on detailed review feedback.

### Example Prompts / Instructions
Key instructions and interaction patterns used during the development lifecycle:

- **Workflow Initiation**:
  - `create workflow @task-breakdown.md`
  - `i want to make use case 1 & 2 using C# API and Blazor with local db, use @task-breakdown.md`
- **Strict Execution Control**:
  - `"don’t code unless explicitly asked"`
  - `"if create-issue -> only create issue, no coding"`
- **Planning & GitHub Integration**:
  - `use create-issue to create issue.md for Phase 1: Setup & Infrastructure`
  - `translate plan to English -> submit as GitHub issue`
  - `use create-issue -> Phase 2, 3, 4 -> submit all as github issues`
- **Critical Review Cycle**:
  - `switch model -> Claude Opus`
  - `use @review-pr.md review pr <link>`
  - `fix all issues from this PR review`
- **Testing & Stability**:
  - `run Playwright, test all requirements, no skip`
  - `"ensure API is running before testing (avoid port refused)"`
  - `separated API & FE ports`
- **Validation & Cleanup**:
  - `validate against requirements (Senior_SWE_JD.pdf)`
  - `cleanup unused code (weather, about, etc)`
  - `check persona toggle issue`
  - `create guidance.md`

### Validation & Review Process
All AI-generated outputs were subject to human oversight:
- **Manual Review**: Code and plans were reviewed before execution.
- **Cross-Model Verification**: Utilizing different models for development vs. review.
- **Local Validation**: Manual verification of Swagger endpoints and UI behavior.
- **Testing**: Final validation via a full suite of Playwright E2E tests adn manual testing

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

## 🛡️ Error Handling & Validation

### Backend Validation
- **Data Annotations**: All DTOs (`SubmitApplicationRequest`, `ProvideFeedbackRequest`, etc.) are decorated with validation attributes (`[Required]`, `[EmailAddress]`, `[StringLength]`).
- **State Machine Protection**: The `ApplicationService` enforces strict status transition rules, preventing invalid state changes even if bypassed via API calls.

### Frontend Reliability
- **Input Masking**: Wizards and forms use real-time validation to prevent navigation if mandatory fields are missing.
- **Visual Feedback**: Global error handling in `SubmitApplication` and `ReviewWorkspace` captures API failures and displays user-friendly alerts.
- **Resilient Polling**: Async document verification includes retry logic to handle temporary network fluctuations during status polling.

---

## 📄 Documentation
- [SCOPE.md](./SCOPE.md): Detailed Technical Justification and Architecture.
- [guidance.md](./guidance.md): User manual and test scenarios.
