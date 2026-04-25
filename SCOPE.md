# Project Scope & Technical Architecture

## Overview
The Regulatory and Licensing System is a modern, web-based platform designed to automate and streamline the application process for business licenses. It features a dual-persona workflow (Operator and Officer) with integrated AI-driven document verification.

## Technology Stack
- **Frontend**: Blazor WebAssembly (.NET 10)
- **Backend**: ASP.NET Core Web API (.NET 10)
- **Persistence**: SQLite with Entity Framework Core
- **Styling**: Vanilla CSS with Bootstrap 5
- **Testing**: xUnit (Unit), Playwright (E2E)

## Key Technical Features

### 1. Snapshot-based Versioning
Every application submission and resubmission creates a full immutable snapshot of the application data. This allows:
- Officers to compare changes between versions.
- Historical auditing of exactly what was submitted at any point in time.

### 2. AI-Driven Verification (Mocked)
The system integrates a background verification service that simulates AI analysis of uploaded documents:
- **Async Processing**: Submissions are accepted immediately, while AI status updates happen in the background.
- **Feedback Loop**: Flagged documents are highlighted to Officers for manual verification.

### 3. Dual-Persona Workflow
A built-in Persona Switcher allows seamless toggling between roles during development and demonstration:
- **Operator**: Application submission, document management, and addressing feedback.
- **Officer**: Queue management, field-level feedback, and final decision-making (Approve/Reject/Resubmit).

## State Machine
Applications follow a strict state transition model:
1. `ApplicationReceived` (Initial)
2. `UnderReview` (Officer opens)
3. `PendingPreSiteResubmission` / `PendingPostSiteResubmission` (Officer requests info)
4. `PreSiteResubmitted` / `PostSiteClarificationResubmitted` (Operator updates)
5. `Approved` / `Rejected` (Final)

## Security & Reliability
- **Reference Generation**: Robust, unique reference numbers generated using `LIC-YYYYMMDDHHMMSS-RANDOM`.
- **Validation**: Strict server-side validation of status transitions to prevent unauthorized state changes.
- **Memory Efficiency**: File uploads use streaming to handle large documents without memory spikes.
