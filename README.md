# ClockSystem
Project for Advanced Programming Module DBS

Visual Studio 2022 17.12.3 required.

Will need to have installed:  .Net Desktop Development and Asp.Net and Web Development

Once repo is cloned locally you will need the following Nuget packages:

Microsoft.Asp.Identity.Core 2.2.4 on Services and DAL project.

Microsoft.AspNetCore.EntityFrameworkCore 8.0.11 on DAL project.

Microsoft.EntityFrameworkCore 9.0.0 on DAL and ClockSystemCA.

Microsoft.EntityFrameworkCore.SqlServer 9.0.0 on DAL and ClockSystemCA.

Newtosoft.Json 13.0.3 on ClockSystemCA.

Dependencies:

ClockSystemCA_AndrewByrne:

Microsoft.AspNetCore.App.

Microsoft.NetCore.App.

Services: 

Microsoft.NetCore.App.

DAL: 

Microsoft.NetCore.App.

Project Refrences:

ClockSystemCA_AndrewByrne needs:

Services and DAL.

Services needs:

DAL.

For the Database you will need to update the connection string in appsettings.json inside ClockSystemCA_AndrewByrne project to whatever the current string is for your local copy. This means redirect the string to the new folder path in this section AttachDbFilename=C:\\Users\\Andrew\\Documents\\ClockSystemDB.mdf

Visual Studio uses IIS Express

Ensure you have SQL Server running to host the database.

The Database project is no longer needed and you can remove if it causes issues since db is directly accessed now.

Project should be good to run now.
On first run if it prompts question about certificate click to accept. If the page then mentions security alert click to advance anyway this is just due to ssl cert.

Login for Admin default user is Test@Admin.ie Employee user is Test@Employee.ie both use the password "test".
System stores passwords as hashes in Database. You can change code to enforce more strict requirements for password.
