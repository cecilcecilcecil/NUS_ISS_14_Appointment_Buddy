set @varrr=0
set @fileName=file_with_variable.txt

call ../../../Helper/StopAllBrowsers.bat
call hats_shell robot --report ../../../../Reports/PatientInfo/CreatePatientInfo.html --log ../../../../Logs/PatientInfo/CreatePatientInfo.html --reporttitle "Report_-_Set_Closing_Time_Super_Admin" --logtitle "Log_-_Set_Closing_Time_Super_Admin" --variable loginUser:"SuperAdmin" ../Tests/CreatePatientInfo.robot

IF exist %@fileName% (for /f "delims=" %%x in (%@fileName%) do set @varrr=%%x & del %@fileName%)
IF %@varrr% EQU 0 (Echo No error found) ELSE (exit /b %@varrr%)
