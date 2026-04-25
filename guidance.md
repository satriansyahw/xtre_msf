# User Guidance & Test Scenarios

This document provides a step-by-step guide on how to test the core features of the Regulatory and Licensing System.

---

## 🎭 Persona Switching
The system uses a **Persona Switcher** at the top of the screen.
- **Operator**: Represents the business applicant.
- **Officer**: Represents the government regulator.

**Note**: Switching personas will trigger a full page reload to ensure a clean data state and secure workspace transition.

---

## 📋 Test Scenario 1: New Application (Operator)
1. Set Persona to **Operator**.
2. Click **"Submit New Application"**.
3. **Step 1**: Enter "John Doe", "Acme Bakery", and "john@example.com". Click **Next**.
4. **Step 2**: Enter a business address and employee count. Click **Next**.
5. **Step 3**: 
   - Drag and drop a sample PDF/Image.
   - Observe the **AI Status** change from "Pending" to "Verified" (approx. 10-15 seconds).
   - Click **Submit Application**.
6. You should be redirected to the dashboard where your application appears with status **"Application Received"**.

---

## 🔍 Test Scenario 2: Officer Review (Officer)
1. Switch Persona to **Officer**.
2. You will see the **Review Queue**. Click **"Open Workspace"** for the application created in Scenario 1.
3. **Review Data**: View the JSON data and the uploaded document.
4. **Provide Feedback**:
   - Select "ApplicantName" from the dropdown.
   - Enter "Please provide full legal name". Click **Add**.
5. **Make Decision**:
   - Enter an internal note.
   - Click **"Request Resubmission"**.
6. You will be redirected to the queue. The application status is now **"Pending Pre-Site Resubmission"**.

---

## 🔄 Test Scenario 3: Resubmission (Operator)
1. Switch Persona to **Operator**.
2. Click **"Details"** on the application.
3. Observe the **Feedback** panel on the right showing the Officer's comment.
4. Click **"Resubmit Application"**.
5. Update the name to "Johnathon Doe" and click **Submit**.
6. Observe the **History & Revisions** tab to see the version timeline.

---

## 🛡️ Validation & Error Testing
Try these "Negative Tests" to verify robustness:
1. **Empty Fields**: Try to progress through the wizard with empty required fields. The "Next" button will remain disabled.
2. **Unauthorized Access**: While as an Operator, try to manually navigate to `/officer`. The system will automatically clear the screen and redirect you back to the home/operator page.
3. **Missing Comments**: Try to request a resubmission as an Officer without providing a global comment. The system will block the action and show an error alert.

---

## 🔔 Notifications
Check the **Notification Bell** in the top-right corner:
- As an **Operator**, you will see a notification when an Officer requests a resubmission.
- As an **Officer**, you will see a notification when an Operator submits a new application or a revision.
