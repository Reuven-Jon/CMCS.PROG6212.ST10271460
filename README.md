# CMCS.PROG6212.ST10271460
This is a prototype of the Contract Monthly Claim System developed using ASP.NET Core. The system is designed to allow independent contractor lecturers to submit and track their monthly claims. Administrators can manage claims, review, approve, or reject them. This version is a non-functional prototype focusing on the design and layout of the system.




## Running the Project
1. Open the Project
Open the cloned repository in Visual Studio or your preferred IDE.

2. Build the Project
Before running the project, build the solution to restore the required packages and ensure everything compiles:

In Visual Studio: Go to Build > Build Solution or press Ctrl+Shift+B.
3. Run the Application
After the build is successful, you can run the project:

In Visual Studio, click the green Start button, or press F5.
The application will launch in your default web browser at https://localhost:5001/ (or the port assigned by your IDE).

## RUNNING THE PROJECT FOR PART 2

2. Login Credentials
Username: The username must be exactly 4 characters long.
Password: The password must be exactly 8 characters long. It cannot contain spaces or mathematical symbols like +, =, etc.
3. Logging In
On the login page, enter your Username and Password.
Select your Role from the dropdown menu:
Lecturer
Manager or Coordinator
Click the Login button.
4. Lecturer Dashboard
After logging in as a Lecturer, you will be directed to the Lecturer Dashboard where you can:

Submit a Claim: Navigate to the "Submit Claim" page to enter details for a new claim, including hours worked and the hourly rate. You can also upload supporting documents (PDF).
View Submitted Claims: After submitting a claim, it will appear in the "Your Claims" section where you can see the status (Pending, Approved, or Rejected) and the submission date.
5. Manager/Coordinator Dashboard
After logging in as a Manager/Coordinator, you will be directed to the Manager Dashboard where you can:

Manage Claims: View all lecturer claims, and approve or reject them. You can add a note when approving or rejecting a claim.
View Supporting Documents: Click the "View Document" button to review any uploaded files for the claims.
6. Switching Roles
To switch roles:

Click the Switch Role button from the top-right hamburger menu.
This will redirect you back to the login page, where you can log in as a different user (Lecturer or Manager).
7. Logging Out
To log out of the application:

Click the Logout option from the top-right hamburger menu.
This will clear your session and redirect you to the login page.
