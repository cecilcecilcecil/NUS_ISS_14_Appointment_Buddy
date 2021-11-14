*** Settings ***
Documentation		This resource define keywords of that provides ease of access and usage across all robot scripts
Library				String
Library				DateTime
Library             OperatingSystem
Library             Process

######################################################################################
check document ready
	[Arguments]			${BROWSER}
	Run Keyword If		'${BROWSER}' != 'ie'	Wait For Condition		return document.readyState=="complete"
	...					ELSE					Sleep					${TIMEOUT}

open AB		
	[Arguments]						${BROWSER}					${ENV}
    Open Browser					${ENV}						${BROWSER}
	Run Keyword If					'${BROWSER}' != 'ie'		Wait For Condition		return document.readyState=="complete"
    Maximize Browser Window
	Set Selenium Speed				0.1 seconds
	Set Selenium Timeout			15 seconds

Goodbye AB
	go page top
	Sleep							${LONGSLEEP}
	logout from gems2
    Close Browser

Check Test Results
	Run Keyword If Test Failed		Set Result to File			1
	Run Keyword If Test Failed		Fatal Error

Set Result to File
	[Arguments]						${res}
	Create File						${EXECDIR}/${resFile}		${res}

Host Is Opened and User Logins
	[Arguments]				${BROWSER}										${HOST}							${USER}
	open AB					${BROWSER}										${HOST}
	check document ready	${BROWSER}
	login as staff
	check document ready	${BROWSER} 
	Sleep					${LONGSLEEP}
	check document ready	${BROWSER}
	Sleep					${LONGSLEEP}