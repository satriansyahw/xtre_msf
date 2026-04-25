# Issue: Phase 3 - Officer Application Review & Feedback

## Objective
Implement the Officer workspace, enabling efficient review of license applications, contextual commenting, version comparison, and decision-making (Approval/Rejection/Resubmission).

## Requirements

### 1. Backend Implementation
- **Review Queue API**: `GET /api/officer/applications` to retrieve all applications pending review.
- **Feedback API**: `POST /api/applications/{id}/feedback` to save contextual comments linked to specific fields or documents.
- **Decision API**: `POST /api/applications/{id}/decision` to update status (Approve, Reject, or Request Resubmission).
- **History API**: `GET /api/applications/{id}/snapshots` to retrieve all historical versions for comparison.

### 2. Frontend Implementation (Blazor)
- **Officer Dashboard**: A queue view listing applications with filtering by status and submission date.
- **Review Workspace**:
  - **Contextual Commenting**: Ability to click on a field or document and add a comment.
  - **Version Comparison Side-by-Side**: A UI component that shows the current version vs. a previous version (snapshot) with highlighted changes.
  - **Decision Panel**: A clear set of actions at the bottom of the review page (Approve, Reject, Request Resubmission).
- **Audit Trail Component**: A timeline view showing all feedback rounds and status changes.

### 3. State Management
- Ensure the UI correctly reflects the 11 statuses (e.g., showing "Route to Approval" for `PendingApproval`).
- Implement the "Persona Toggle" to simulate the Officer role.

## Definition of Done
- [ ] Officer can view a list of pending applications in the dashboard.
- [ ] Officer can add targeted feedback to specific application fields.
- [ ] Officer can compare two different versions of an application side-by-side.
- [ ] Decision actions correctly trigger status transitions and notify the operator (simulated).
- [ ] Full history of all review rounds is visible in the audit trail.
