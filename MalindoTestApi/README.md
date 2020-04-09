# MalindoTestAPI
Malindo - Customer Service API

Technology:
----------
SwaggerUI
.Netcore 3.1
Serielog 2.9
Entity Frameworks 3.1

Database Set up:
---------------
In the project folder go to folder Data and run dbsetup.sql in sql server
This will set up the MalindoTest database.

Connection to Database:
----------------------
In appsettings.json under CustomerConnection update Connection strings to use your server name and set database name to MalindoTest.

Authentication
--------------
In Appsettings.json
Include your API Authentication key that says  "ApiKey": ''

Swagger UI: 
http://localhost:<port>/


