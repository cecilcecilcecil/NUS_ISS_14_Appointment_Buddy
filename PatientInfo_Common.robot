*** Settings ***
Documentation     Common file for variables and keywords in helping to test Patient Info

###########################################Variables###########################################

*** Variables ***
#InputKeyIn
${AlphabetTime}							!@#$%^&*()_+-=qwertyuiop[]{}asdfghjkl;':"zxcvbnm,/<>?"QWERTYUIOPASDFGHJKLZXCVBNM.
${0000Time}								0000
${2400Time}								2400
${CorrectTime}							0800

#Buttons And Labels
${ClosingTimeSaveBtn}					SetClosingTime
${ClosingTimeInputClass}				ClosingTimeClass
${ClosingTimeValidationClass}			field-validation-valid valiationpos
${ClosingTimeCfmMsgId}					successText
${closeBtn}								closeBox

#ValidationMsg
${0000hrsValidationMsg}					The time cannot be 00:00.
${2400hrsValidationMsg}					Please enter a valid time in HH:MM format.
${alphabethrsValidationMsg}				This field is required.
${emptytimeValidationMsg}				This field is required.
${ClosingTimeCfmMsg}					Changes saved successfully.

######################################################################################

*** Keywords ***
######################################type in closing time################################################
type closing time
	[Arguments]												${closingTimeInput}
	Wait Until Element Is Visible							//input[@class='${ClosingTimeInputClass}']
	${elements}=						Get WebElements		//input[@class='${ClosingTimeInputClass}']
	FOR									${element}			IN													@{elements}
	type								${element}			${closingTimeInput}
	END
	
User Fills in Data
	[Arguments]							${TargetTime}
	Run Keyword							type closing time	${TargetTime}

######################################clear input time################################################
User Empties Field
	${elements}=						Get WebElements		//input[@class='${ClosingTimeInputClass}']
	FOR									${element}			IN													@{elements}
	Clear Element Text					${element}	
	END

######################################click on save button################################################
User Saves Closing Time
	Wait Until Element Is Visible		id=${ClosingTimeSaveBtn}
	click								id=${ClosingTimeSaveBtn}	

######################################validation msg################################################
show validation
	[Arguments]							${validationMsg}
	${validations}=						Get WebElements			//span[@class='${ClosingTimeValidationClass}']
	FOR									${validation}			IN												@{validations}
	Should Be True 						'${validation.text}' == '${validationMsg}'
	END
	
Closing Time Validation Shown
	[Arguments]							${TargetMsg}
	Run Keyword							show validation			${TargetMsg}
######################################successful confirmation msg################################################
Saved Successful Shown
	[Arguments]							${BROWSER}
	Wait Until Element Is Visible		id=${ClosingTimeCfmMsgId}
	${cfmMsg}=							Get Text				id=${ClosingTimeCfmMsgId}
	click								id=${closeBtn}
	check document ready				${BROWSER}
	Sleep								${LONGSLEEP}

User Access Closing Time
	[Arguments]							${BROWSER}
	access closing time
	check document ready				${BROWSER}
		
