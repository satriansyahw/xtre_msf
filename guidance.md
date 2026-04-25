# User Guidance: Regulatory and Licensing System

This document provides step-by-step instructions for using the **Regulatory and Licensing System**, from initial application submission to the final decision.

---

## 🚀 Initial Setup

1. **Start the API**:
   Open a terminal in `src/Licensing.Api` and run:
   ```bash
   dotnet run --launch-profile http
   ```
   (API will run on [http://localhost:5200](http://localhost:5200))

2. **Start the Frontend**:
   Open a terminal in `src/Licensing.Client` and run:
   ```bash
   dotnet run --launch-profile http
   ```
   (Frontend UI will run on [http://localhost:5237](http://localhost:5237))

3. **Open Browser**:
   Access the UI at: [http://localhost:5237](http://localhost:5237)

---

## 👨‍💻 Operator Workflow (Applicant)

### 1. Submitting a New Application
1. Ensure the Persona at the top right is set to **Operator**.
2. Click the **"Submit New Application"** button on the dashboard.
3. **Step 1 (Basic Info)**: Enter Full Name, Business Name, and Email. Click **Next**.
4. **Step 2 (Details)**: Enter Business Address and Employee Count. Click **Next**.
5. **Step 3 (Documents)**:
   - Drag and drop document files into the designated area.
   - Wait for the AI status to change from **Pending** to **Verified** (typically ~15-20 seconds).
   - Click **Submit Application**.

### 2. Monitoring Status & Notifications
1. View the dashboard to see your list of applications and their current status.
2. Check the **Notification Bell** icon in the top right corner for status updates from Officers.
3. Click the **Details** button on an application to view full information.

### 3. Viewing Audit Trail & Revision History
1. On the **Details** page, click the **"History & Revisions"** tab.
2. You will see the **Timeline** (chronological events) and **Version Comparison** if the application has been resubmitted.

### 4. Correcting an Application (Resubmission)
1. If the application status is **"Pending Pre-Site Resubmission"**, click **Details**.
2. Click the **"Resubmit Application"** button.
3. The system will display the form with **red highlights** on the flagged fields.
4. Read the Officer's comments displayed directly under the flagged fields.
5. Correct the requested data and click **Submit Updates**.

---

## 👮 Officer Workflow (Regulatory Officer)

### 1. Reviewing the Application Queue
1. Switch the Persona at the top right to **Officer**.
2. Navigate to the **Officer Dashboard** to see incoming applications.
3. Click **"Open Workspace"** for the application you wish to review.

### 2. Using the Review Workspace
1. **Application Data**: View raw JSON data and uploaded documents.
2. **AI Status**: Check if the AI detected issues with the documents (Flagged).
3. **Providing Feedback**:
   - Select the field you want to comment on (e.g., `ApplicantName`).
   - Enter a comment or use **Common Templates** (e.g., "Clearer Scan") to speed up the process.
   - Click **Add**.
4. **Compare Versions**: Use the **Version Comparison** panel at the bottom to see specific changes if this is a resubmission.

### 3. Making a Decision
1. Enter any internal notes in the **"Overall Internal Note"** box.
2. Choose one of the following decisions:
   - **Approve**: If all data is valid.
   - **Request Resubmission**: If data needs correction by the Operator.
   - **Reject**: If the application does not meet requirements.

---

## 🛡️ Key Features

- **Audit Trail**: Every status change is automatically recorded in a vertical timeline that cannot be modified.
- **Snapshot Versioning**: Every time an Operator updates their application, the system saves the old version for side-by-side comparison.
- **Contextual Feedback**: Officer comments are linked directly to specific fields, making it clear for the Operator what needs to be fixed.
