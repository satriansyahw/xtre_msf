# Project Scope & Technical Architecture

## Technical Justification
The technology stack was selected to balance enterprise-grade robustness with the rapid development needs of a technical assessment:

- **C# & .NET 10**: Chosen for its strong type safety, high performance, and mature ecosystem. Using a single language across the entire stack (Frontend to Backend) reduces context switching and enables shared Domain models.
- **Blazor WebAssembly**: Provides a rich, interactive Single Page Application (SPA) experience. It allows for C# execution in the browser, enabling complex UI logic (like the Version Diff Viewer) to be written with the same safety as backend code.
- **SQLite**: A lightweight, zero-configuration database that is perfect for portability. It allows the entire system to be run without external dependencies (like SQL Server or Docker), making it ideal for demonstration and local assessment.
- **Bootstrap 5 & Vanilla CSS**: Provides a clean, responsive layout system while allowing for deep aesthetic customization through custom CSS tokens.

## Tech Stack & Architecture Summary
This system follows a **Clean Architecture** pattern implemented via a **Blazor WebAssembly Hosted** model. The solution is decoupled into Domain, Application, Infrastructure, and API layers to ensure clear separation of concerns and high testability. The backend serves as a stateless RESTful API, while the frontend maintains a reactive state managed by localized services, ensuring a responsive and modern user experience.

## Feature Scope
The following core functional requirements have been implemented:

- **Use Case 1 (Application Versioning)**: Full implementation of immutable snapshots. Every resubmission creates a new version, allowing for historical comparisons.
- **Use Case 2 (Review Workspace)**: A dedicated workspace for Officers to provide field-level feedback, manage status transitions, and view document AI status.
- **Use Case 3 (Audit Trail & Timeline)**: A comprehensive lifecycle tracking system that records every status change, internal comment, and persona interaction.

## Deferred & Mocked Features
- **Authentication**: Real JWT/Identity implementation is deferred in favor of a **Persona Switcher**. This allows evaluators to toggle roles instantly without login overhead, focusing the assessment on business logic.
- **AI Processing**: The AI document verification is **mocked** via an asynchronous background service. This demonstrates the system's ability to handle long-running async tasks and UI polling without requiring expensive external AI APIs.
- **File Storage**: Documents are stored in the local file system (`wwwroot/Uploads`) rather than Cloud Storage (S3/Azure Blob) to maintain the "zero-dependency" requirement for local execution.

## Assumptions
- **Single-User Demo**: The system assumes a single active operator/officer for the purpose of the demonstration, although the underlying data model supports multi-tenancy.
- **Simplified Site Visits**: The "Site Visit" phase is represented in the state machine and audit trail but does not include a full scheduling calendar or geolocation tracking.
- **Document Validity**: The system assumes documents are standard image or PDF files (max 10MB) as per the common regulatory requirements.

## State Machine
Applications follow a strict state transition model:
1. `ApplicationReceived` (Initial)
2. `UnderReview` (Officer opens)
3. `PendingPreSiteResubmission` / `PendingPostSiteResubmission` (Officer requests info)
4. `PreSiteResubmitted` / `PostSiteClarificationResubmitted` (Operator updates)
5. `Approved` / `Rejected` (Final decisions)
