# Issue: Implement Regulatory and Licensing Platform (Use Case 1 & 2)

## Objective
Build a production-ready MVP for managing license applications, focusing on the back-and-forth workflow between Operators and Officers.

## Scope
### Included
- **Operator Flow**: Guided submission, document upload (local storage), AI status mocking, and resubmission based on feedback.
- **Officer Flow**: Review workspace, contextual commenting, version comparison (snapshot-based), and status management.
- **State Machine**: Implementation of 11 internal statuses with specific UI labels for each persona.
- **Audit Trail**: Tracking of feedback, responses, and status changes.

### Excluded
- Real AI Document Verification (Mocked).
- Formal Auth/Identity (Simulated via UI Toggle).
- Production Cloud Storage (Local Storage used).
- Use Case 3 (Site Inspection).

## Assumptions
- SQLite will be used for persistence.
- A "Snapshot" is created every time an application is submitted or resubmitted to allow version comparison.
- Uploaded files are stored in a path defined in `appsettings.json`.

## Backend Plan (C# API + EF Core)
- **Domain Model**: `Application`, `ApplicationSnapshot`, `Document`, `Feedback`, `StatusHistory`.
- **API Endpoints**:
  - `POST /applications`: Submit new application.
  - `GET /applications`: List applications (queue).
  - `GET /applications/{id}/history`: Get version history.
  - `POST /applications/{id}/feedback`: Add officer feedback.
  - `POST /applications/{id}/resubmit`: Operator resubmission.
- **Services**: `FileStorageService`, `MockAIVerificationService`, `SnapshotService`.

## Frontend Plan (Blazor WASM)
- **Shared Components**: StatusBadge, PersonaToggle, FileUpload.
- **Operator Views**: Dashboard, SubmissionWizard (Stepper), ResubmissionView.
- **Officer Views**: ReviewQueue, ReviewWorkspace (with Side-by-Side version comparison).
- **State Management**: Simple `PersonaService` to track current role and trigger UI updates.

## Edge Cases
- Handling large file uploads.
- Preventing status transitions that violate business logic (e.g., approving a rejected application).
- Maintaining file associations across snapshots.

## Testing Approach
- Unit Tests for State Machine and Snapshot logic.
- Integration Tests for API submission flow.

## Definition of Done
- Both Operator and Officer flows are fully functional.
- Version comparison shows clear differences between resubmissions.
- Local DB persists data correctly.
- Documentation (README, SCOPE.md) is updated.
