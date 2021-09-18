function DigitsOnly(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    return true;
}

function isPhoneNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode > 31 && charCode < 48) || (charCode > 57)) {
        return false;
    }

    return true;
}

function isTime(inputValue, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var value = inputValue.value;

    if (
        ((charCode > 31 && (charCode < 48 || charCode > 57)) || charCode == 191) //check must be a number only
        &&
        (charCode != 35 && charCode != 36 && charCode != 37 && charCode != 39) //allow arrow keys, home, end keys
    ) {
        return false;
    }

    //add / for 2nd and 5th position
    if (value.length == 2) {
        if (charCode != 8) {
            inputValue.value = value + ":";
        }
    }
    //delete another character if the length is 3 or 6
    if (value.length == 3) {
        if (charCode == 8) {
            inputValue.value = value.substr(0, value.length - 1);
        }
    }
}

function isDate(inputValue, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var value = inputValue.value;

    if (
        ((charCode > 31 && (charCode < 48 || charCode > 57)) || charCode == 191) //check must be a number only
        &&
        (charCode != 35 && charCode != 36 && charCode != 37 && charCode != 39) //allow arrow keys, home, end keys
    ) {
        return false;
    }
    //add / for 2nd and 5th position
    if (value.length == 2 || value.length == 5) {
        if (charCode != 8) {
            inputValue.value = value + "/";
        }
    }
    //delete another character if the length is 3 or 6
    if (value.length == 3 || value.length == 6) {
        if (charCode == 8) {
            inputValue.value = value.substr(0, value.length - 1);
        }
    }
}

function isMoney2DP(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if ((charCode > 31 && charCode < 46) || (charCode > 46 && charCode < 48) || (charCode > 57)) {
        return false;
    }

    if (evt.target.selectionStart == 0) {
        if (charCode == 48) {
            return false;
        }
    }

    return true;
}

function isCurrency(input, evt) {
    var currentValue;
    var charCode = (evt.which) ? evt.which : event.keyCode
    var value = input.value
    var noOfdecimals = value.split(".").length - 1;
    var valueAfterDecimals = value.split(".");
    var keyPosition = input.selectionStart;

    if (!String.prototype.endsWith) {
        String.prototype.endsWith = function (search, this_len) {
            if (this_len === undefined || this_len > this.length) {
                this_len = this.length;
            }
            return this.substring(this_len - search.length, this_len) === search;
        };
    }

    if (noOfdecimals > 0 && valueAfterDecimals[1] == undefined && valueAfterDecimals[1].length>1) {
        return false;
    }
    if (valueAfterDecimals[1] != undefined && valueAfterDecimals[1].length > 1 && keyPosition > value.length - 3) {
        return false;
    }
    if (noOfdecimals > 0 && charCode == 46) {
        return false;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
        return false;
    }

    input.addEventListener('input', function (event) {
        var cursorPosition = getCaretPosition(input);
        var valueBefore = input.value;
        var lengthBefore = input.value.length;
        var specialCharsBefore = getSpecialCharsOnSides(input.value);
        var number = removeThousandSeparators(input.value);

        if (input.value == '00') {
            input.value = '';
            return;
        }

        if (input.value == '') {
            return;
        }

        var numberWithoutComma = number.replace(getCommaSeparator(), '.');
        var numberBeforeAfterDecimal = numberWithoutComma.split(".");
        input.value = formatNumber(numberBeforeAfterDecimal[0]);

        if (numberBeforeAfterDecimal[1] != undefined && numberBeforeAfterDecimal[1].length > 0) {
            input.value += "." + numberBeforeAfterDecimal[1];
            setCaretPosition(input, cursorPosition);
            return;
        }

        //input.value = formatNumber(number.replace(getCommaSeparator(), '.'));

        // if deleting the comma, delete it correctly
        if (currentValue == input.value && currentValue == valueBefore.substr(0, cursorPosition) + getThousandSeparator() + valueBefore.substr(cursorPosition)) {
            input.value = formatNumber(removeThousandSeparators(valueBefore.substr(0, cursorPosition - 1) + valueBefore.substr(cursorPosition)));
            cursorPosition--;
        }

        // if entering comma for separation, leave it in there (as well support .000)
        var commaSeparator = getCommaSeparator();
        if (valueBefore.endsWith(commaSeparator) || valueBefore.endsWith(commaSeparator + '0') || valueBefore.endsWith(commaSeparator + '00') || valueBefore.endsWith(commaSeparator + '000')) {
            input.value = input.value + valueBefore.substring(valueBefore.indexOf(commaSeparator));
        }

        // move cursor correctly if thousand separator got added or removed
        var specialCharsAfter = getSpecialCharsOnSides(input.value);
        if (specialCharsBefore[0] < specialCharsAfter[0]) {
            cursorPosition += specialCharsAfter[0] - specialCharsBefore[0];
        } else if (specialCharsBefore[0] > specialCharsAfter[0]) {
            cursorPosition -= specialCharsBefore[0] - specialCharsAfter[0];
        }
        setCaretPosition(input, cursorPosition);

        currentValue = input.value;
    });

    function getSpecialCharsOnSides(x, cursorPosition) {
        var specialCharsLeft = x.substring(0, cursorPosition).replace(/[0-9]/g, '').length;
        var specialCharsRight = x.substring(cursorPosition).replace(/[0-9]/g, '').length;
        return [specialCharsLeft, specialCharsRight]
    }

    function formatNumber(x) {
        return getNumberFormat().format(x);
    }

    function removeThousandSeparators(x) {
        return x.toString().replace(new RegExp(escapeRegExp(getThousandSeparator()), 'g'), "");
    }

    function getThousandSeparator() {
        return getNumberFormat().format('1000').replace(/[0-9]/g, '')[0];
    }

    function getCommaSeparator() {
        return getNumberFormat().format('0.01').replace(/[0-9]/g, '')[0];
    }

    function getNumberFormat() {
        return new Intl.NumberFormat();
    }

    function escapeRegExp(str) {
        return str.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
    }

    /*
    ** Returns the caret (cursor) position of the specified text field.
    ** Return value range is 0-oField.value.length.
    */
    function getCaretPosition(oField) {
        // Initialize
        var iCaretPos = 0;

        // IE Support
        if (document.selection) {

            // Set focus on the element
            oField.focus();

            // To get cursor position, get empty selection range
            var oSel = document.selection.createRange();

            // Move selection start to 0 position
            oSel.moveStart('character', -oField.value.length);

            // The caret position is selection length
            iCaretPos = oSel.text.length;
        }

        // Firefox support
        else if (oField.selectionStart || oField.selectionStart == '0')
            iCaretPos = oField.selectionStart;

        // Return results
        return iCaretPos;
    }

    function setCaretPosition(elem, caretPos) {
        if (elem != null) {
            if (elem.createTextRange) {
                var range = elem.createTextRange();
                range.move('character', caretPos);
                range.select();
            }
            else {
                if (elem.selectionStart) {
                    elem.focus();
                    elem.setSelectionRange(caretPos, caretPos);
                }
                else
                    elem.focus();
            }
        }
    }
}

function isNumber(input, evt) {

    var currentValue;
    var charCode = (evt.which) ? evt.which : event.keyCode

    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    if (charCode == null) {
        if (!isNaN(input.value)) {
            var len = input.maxLength;

            if (input.value.length >= len) {
                $(input).val('0');
                return false;
            }
        }

        else {
            $(input).val('0');
            return false;
        }
    }

    input.addEventListener('input', function (event) {
        var cursorPosition = getCaretPosition(input);
        var valueBefore = input.value;
        var lengthBefore = input.value.length;
        var specialCharsBefore = getSpecialCharsOnSides(input.value);
        var number = removeThousandSeparators(input.value);

        if (input.value == '0') {
            input.value = '';
            return;
        }   

        if (input.value == '') {
            return;
        }

        input.value = formatNumber(number.replace(getCommaSeparator(), '.'));

        // if deleting the comma, delete it correctly
        if (currentValue == input.value && currentValue == valueBefore.substr(0, cursorPosition) + getThousandSeparator() + valueBefore.substr(cursorPosition)) {
            input.value = formatNumber(removeThousandSeparators(valueBefore.substr(0, cursorPosition - 1) + valueBefore.substr(cursorPosition)));
            cursorPosition--;
        }

        // if entering comma for separation, leave it in there (as well support .000)
        var commaSeparator = getCommaSeparator();
        if (valueBefore.endsWith(commaSeparator) || valueBefore.endsWith(commaSeparator + '0') || valueBefore.endsWith(commaSeparator + '00') || valueBefore.endsWith(commaSeparator + '000')) {
            input.value = input.value + valueBefore.substring(valueBefore.indexOf(commaSeparator));
        }

        // move cursor correctly if thousand separator got added or removed
        var specialCharsAfter = getSpecialCharsOnSides(input.value);
        if (specialCharsBefore[0] < specialCharsAfter[0]) {
            cursorPosition += specialCharsAfter[0] - specialCharsBefore[0];
        } else if (specialCharsBefore[0] > specialCharsAfter[0]) {
            cursorPosition -= specialCharsBefore[0] - specialCharsAfter[0];
        }
        setCaretPosition(input, cursorPosition);

        currentValue = input.value;
    });

    function getSpecialCharsOnSides(x, cursorPosition) {
        var specialCharsLeft = x.substring(0, cursorPosition).replace(/[0-9]/g, '').length;
        var specialCharsRight = x.substring(cursorPosition).replace(/[0-9]/g, '').length;
        return [specialCharsLeft, specialCharsRight]
    }

    function formatNumber(x) {
        return getNumberFormat().format(x);
    }

    function removeThousandSeparators(x) {
        return x.toString().replace(new RegExp(escapeRegExp(getThousandSeparator()), 'g'), "");
    }

    function getThousandSeparator() {
        return getNumberFormat().format('1000').replace(/[0-9]/g, '')[0];
    }

    function getCommaSeparator() {
        return getNumberFormat().format('0.01').replace(/[0-9]/g, '')[0];
    }

    function getNumberFormat() {
        return new Intl.NumberFormat();
    }

    function escapeRegExp(str) {
        return str.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
    }

    /*
    ** Returns the caret (cursor) position of the specified text field.
    ** Return value range is 0-oField.value.length.
    */
    function getCaretPosition(oField) {
        // Initialize
        var iCaretPos = 0;

        // IE Support
        if (document.selection) {

            // Set focus on the element
            oField.focus();

            // To get cursor position, get empty selection range
            var oSel = document.selection.createRange();

            // Move selection start to 0 position
            oSel.moveStart('character', -oField.value.length);

            // The caret position is selection length
            iCaretPos = oSel.text.length;
        }

        // Firefox support
        else if (oField.selectionStart || oField.selectionStart == '0')
            iCaretPos = oField.selectionStart;

        // Return results
        return iCaretPos;
    }

    function setCaretPosition(elem, caretPos) {
        if (elem != null) {
            if (elem.createTextRange) {
                var range = elem.createTextRange();
                range.move('character', caretPos);
                range.select();
            }
            else {
                if (elem.selectionStart) {
                    elem.focus();
                    elem.setSelectionRange(caretPos, caretPos);
                }
                else
                    elem.focus();
            }
        }
    }
}