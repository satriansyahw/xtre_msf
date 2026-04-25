# Task List: Regulatory and Licensing Platform (Use Case 1 & 2)

## Phase 1: Setup & Infrastructure
- [x] Initialize Clean Architecture solution and projects
- [x] Setup SQLite and Entity Framework Core
- [x] Configure Local File Storage path in appsettings.json
- [x] Create Domain Entities (Application, Snapshot, Document, Feedback)
- [x] Basic API setup and Swagger configuration

## Phase 2: Use Case 1 (Operator Workflow)
- [x] Implement Application Submission API (with Snapshotting)
- [x] Implement File Upload API (Local Storage)
- [x] Implement Mock AI Verification Service
- [x] Build Operator Dashboard (Application List)
- [x] Build Guided Submission Wizard (Multi-step)
- [ ] Build Resubmission Portal (Highlighting flagged items)

## Phase 3: Use Case 2 (Officer Workflow)
- [x] Implement Review Queue (Officer Dashboard)
- [x] Build Review Workspace (Interactive JSON Viewer)
- [x] Implement Field-level Feedback mechanism
- [x] Implement Version Comparison tool (Snapshot based)
- [x] Build Decision Panel (Approve/Reject/Resubmit logic)
- [x] Implement Version Comparison (Current vs. Previous Snapshot)
- [x] Implement Audit Trail and Status History view

## Phase 4: Polish & Validation
- [ ] Persona Switcher (UI Toggle)
- [ ] Real-time status updates (Polling/Mock)
- [ ] Update README.md with AI Usage and Setup instructions
- [ ] Final Verification against JD requirements
