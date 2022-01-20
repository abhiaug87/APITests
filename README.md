A. Framework Description:

This is an API testing framework created using Restsharp libraries, Specflow & scripted in C#. The framework is seggregated into various parts for better understandablibity & easier maintainability. This solution uses .Net framework 4.7.2

Scenarios Folder: Conatains specflow file with the BDD user scenarios.

Stepdefinition Folder: This contains the Stepdefinition and RestAPIHelper class files. The Stepdefinition class file has logic for the user scenarios mentioned in the specflow file along with the assertions for the received responses. Whereas, the RestAPIHelper class file contains the logic for creating, executing the requests & getting back the response for API calls. The RestAPIHelper class file also contains logic for adding error logs when a test fails by capturing the response message of the request in a Log folder in the user's test directory on the local machine.

Data Folder: This contains a class file which has logic for allowing the framework to read data from a json file rather than hard coding the test data into the step definition. It also contains a json file which stores all the test data that will be used by the application under test.

B. Pre- requisites:

Install Visual studio on your machine (community edition will also do)


C. Running Tests:

Download the Project in your local machine
Open "APITests.sln" file using Visual Studio IDE
Build the solution
Use test explorer to run the tests