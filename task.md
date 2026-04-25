# Task List: Regulatory and Licensing Platform (Use Case 1 & 2)

## Phase 1: Setup & Infrastructure
- [x] Initialize Clean Architecture solution and projects
- [x] Setup SQLite and Entity Framework Core
- [x] Configure Local File Storage path in appsettings.json
- [x] Create Domain Entities (Application, Snapshot, Document, Feedback)
- [x] Basic API setup and Swagger configuration

## Phase 2: Use Case 1 (Operator Workflow)
- [ ] Implement Application Submission API (with Snapshotting)
- [ ] Implement File Upload API (Local Storage)
- [ ] Implement Mock AI Verification Service
- [ ] Build Operator Dashboard (Application List)
- [ ] Build Guided Submission Wizard (Multi-step)
- [ ] Build Resubmission Portal (Highlighting flagged items)

## Phase 3: Use Case 2 (Officer Workflow)
- [ ] Build Officer Dashboard (Review Queue)
- [ ] Build Review Workspace:
    - [ ] Document/Field Review UI
    - [ ] Contextual Feedback System
    - [ ] Decision Logic (Approve/Reject/Resubmit)
- [ ] Implement Version Comparison (Current vs. Previous Snapshot)
- [ ] Implement Audit Trail and Status History view

## Phase 4: Polish & Validation
- [ ] Persona Switcher (UI Toggle)
- [ ] Real-time status updates (Polling/Mock)
- [ ] Update README.md with AI Usage and Setup instructions
- [ ] Final Verification against JD requirements
