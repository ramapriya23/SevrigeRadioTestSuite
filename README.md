# Sverige Radio Test Suite

### Description

This is a Test Suite written for the Sverige Radio API endpoint (http://api.sr.se/api/v2/channels/). It aims to make some basic tests on the API and its associated APIs, using C#.NET with RESTSharp. This project has further scope of integrating advanced test cases as an improvement.




### Project Walk-through

Below is a short description of what each file in the repo is:

- **SRTestSuite** :
    This is the entry point for the test suite. It contains all tests performed on the given API. Each element in the API is stored in objects before being tested against. The test cases are performed on the response element as soon it is retrieved for immediate logging.
    
    Every test case is represented with comments as shown  below:

    
    
    //-----------Test Case---------------------
       
      test code;


    //------------------------------------------


   
- **SverigesRadio.cs** : 
  This is the master class whose object stores all elements read from the main API URL XML response.
   

- **SRChannel.cs** : 
  The object of this class holds all details of a particular channel identified by a channel ID and channel name.


- **SRSchedule.cs** : 
  The object of this class holds various program schedules of a particular channel.


- **SREpisode.cs** : 
  The object of this class contains details of one particular program schedule and its related responses.


- **logTestCaseResults.cs** : 
  This class holds the methods used to create the Test Case Log file and write into them. The SRTestSuite.cs class makes use of the methods in this function to log test cases under the file name *TestCaseResults.txt*.
  This file is created in the directory *C:\SRAPITestSuiteResults_ddmmyyyy*

- **Test Cases.md** :  Contains list of Test Cases implemented.

- **Open Items.md** : Outlines all questions and doubts related to the project.
  
### Deployment

- Miscrosoft Visual Studio (any latest version should do)
- RESTSharp : Install from NuGet package manager
- Importing the project code from this GITHub repo 
- Hit run!
- Find the TestResults in the directory *C:\SRAPITestSuiteResults_ddmmyyyy\TestCaseResults.txt*.


### Acknowledgements

- Google 
- and fellow tech friends !


  




