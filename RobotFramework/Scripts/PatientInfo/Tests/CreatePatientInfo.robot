*** Settings ***
Test Teardown		Check Test Results
Documentation		A test suite to set Closing Time as User provided.
Resource			../../../../Helper/seleniumLibrary.robot 
Resource			../../../../Helper/common.robot
Resource			../ClosingTime_Common.robot

*** Test Cases ***
| Set Closing Time Data Success
| | :FOR									| ${BROWSER}				| IN					| @{BROWSERLIST}
| | | Given Host Is Opened and User Logins  | ${BROWSER}                | ${INTRAHOST}			| ${loginUser}
| | | Goodbye AB