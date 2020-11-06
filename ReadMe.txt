How to use BenMAPJiraConnector

1) load the BenMAPCrypt.sln file
2) Set BenMAPCrypt as the StartUp Project
3) Click F5 (or start debugging) to run the project
4) In the main form, enter the Encryption Key, Encryption IV, URL, etc.  All fields are required.
Notes: 	- All fields are required.
		- Encryption Key and Encryption IV must be exactly 32 characters in length.  
		Also, the characters used in the Encryption Key and Encryption IV must be in the UTF8 character set 
		as they will be encoded from strings to bytes and vice versa using UTF8.
		- The file name must be "BenMAPJiraConnector.txt"; no other file name will work.
		- Make sure and use the Jira account's email address as the username and an API key as the password.
5) Click Generate File to generate the BenMAPJiraConnector.txt file.
6) Copy the Encryption Key and Encryption IV you used into notepad or some other text editor
7) Close the BenMAPCrypt application.
8) In the BenMAPJiraConnector project, open the Connection.cs file.
9) Set the ENCRYPTION_KEY and ENCRYPTION_IV constants to the Encryption Key and Encryption IV you used 
to create the BenMAPJiraConnector.txt file.
10) Build a Release version of BenMAPJiraConnector.dll.
11) Copy BenMAPJiraConnector.dll and BenMAPJiraConnector.txt into the bin\debug directory of 
the BenMAPJiraConnectorTest project.
12) Debug the BenMAPJiraConnectorTest project.
13) Click the Decrypt button.  You will see the URL, Username, etc. you entered in BenMAPCrypt in 
the form. Your BenMAPJiraConnector.dll and BenMAPJiraConnector.txt are working.
14) To use BenMAPJiraConnector.dll and BenMAPJiraConnector.txt in the main BenMAP application, simply copy
the files into the same directory in which BenMAP.exe resides.
