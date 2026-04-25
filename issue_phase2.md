# Issue: Phase 2 - Operator Application Submission & Resubmission

## Objective
Implement the complete Operator workflow, from initial application submission with document uploads to responding to Officer feedback in a resubmission cycle.

## Requirements

### 1. Backend Implementation
- **Submission API**: Create `POST /api/applications` to handle form data and trigger a snapshot creation.
- **File Upload API**: Create `POST /api/documents/upload` to save files to local storage and link them to an application.
- **Mock AI Service**: Implement a background task/service that automatically updates document status to `Verified` or `Flagged` after 5-10 seconds.
- **Resubmission API**: Create `POST /api/applications/{id}/resubmit` to process updates for flagged sections only.

### 2. Frontend Implementation (Blazor)
- **Operator Dashboard**: A table view listing all applications submitted by the user with their current status and a "New Application" button.
- **Submission Wizard**:
  - **Step 1**: Basic Information (Applicant, Business, Contact).
  - **Step 2**: Detailed Data (using a JSON-based form approach).
  - **Step 3**: Document Upload (Drag-and-drop support with real-time progress).
- **Resubmission Portal**: A view that displays Officer comments and allows the operator to edit only the fields or documents that were flagged.

### 3. State Management
- Ensure the UI correctly reflects the 11 statuses (e.g., showing "Submitted" for `ApplicationReceived`).
- Implement the "Persona Toggle" to simulate the Operator role.

## Definition of Done
- [ ] Operator can successfully submit a multi-part application with documents.
- [ ] Uploaded files are correctly saved to the local path defined in `appsettings.json`.
- [ ] Mock AI status updates are visible on the UI.
- [ ] Operator can view and address feedback in the resubmission flow.
- [ ] Application history tracks the transition from `ApplicationReceived` to `Pending Pre-Site Resubmission` (simulated).
