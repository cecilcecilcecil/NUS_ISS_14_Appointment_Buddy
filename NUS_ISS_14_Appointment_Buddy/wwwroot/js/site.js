var activeClass = 'active-content'; 
var nonActiveClass = 'content';     
var bellActive = false;

function showHide(eleId, toRemove, toAdd) {

    var ele = document.getElementById(eleId);

    ele.classList.remove(toRemove);
    ele.classList.add(toAdd);
}

function uuidv4() {

    var cry = window.crypto || window.msCrypto;

    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, function (c) {
        return (c ^ cry.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    })
}

function dateCompare(date1, date2, compareAction) {

    var convert1 = $("#" + date1).val().split('/');
    var convert2 = $("#" + date2).val().split('/');

    var cmp1 = new Date(convert1[2], convert1[1] - 1, convert1[0]);
    var cmp2 = new Date(convert2[2], convert2[1] - 1, convert2[0]);
    var now = new Date();

    switch (compareAction) {
        case 'greaterThanEqual':
            return (cmp1 >= cmp2) ? true : false;
            break;
        case 'greaterThan':
            return (cmp1 > cmp2) ? true : false;
            break;
        case 'leasttoday':
            return (cmp1.toDateString() != now.toDateString() && cmp1 < now) ? true : false;
            break;
        case 'today':
            return (cmp1 < now) ? true : false;
            break;
        default:
            break;
    }
}

function dateValueCompare(dt1, dt2, compareAction) {
    var convert1 = dt1.split('/');
    var convert2 = dt2.split('/');

    var cmp1 = new Date(convert1[2], convert1[1] - 1, convert1[0]);
    var cmp2 = new Date(convert2[2], convert2[1] - 1, convert2[0]);
    var now = new Date();

    switch (compareAction) {
        case 'greaterThanEqual':
            return (cmp1 >= cmp2) ? true : false;
            break;
        case 'greaterThan':
            return (cmp1 > cmp2) ? true : false;
            break;
        case 'leasttoday':
            return (cmp1.toDateString() != now.toDateString() && cmp1 < now) ? true : false;
            break;
        case 'today':
            return (cmp1 < now) ? true : false;
            break;
        default:
            break;
    }
}

function dateValueCompare(dt1, dt2, compareAction) {
    var convert1 = dt1.split('/');
    var convert2 = dt2.split('/');

    var cmp1 = new Date(convert1[2], convert1[1] - 1, convert1[0]);
    var cmp2 = new Date(convert2[2], convert2[1] - 1, convert2[0]);
    var now = new Date();

    switch (compareAction) {
        case 'greaterThanEqual':
            return (cmp1 >= cmp2) ? true : false;
            break;
        case 'greaterThan':
            return (cmp1 > cmp2) ? true : false;
            break;
        case 'leasttoday':
            return (cmp1.toDateString() != now.toDateString() && cmp1 < now) ? true : false;
            break;
        case 'today':
            return (cmp1 < now) ? true : false;
            break;
        default:
            break;
    }
}

function monthDiff(d1, d2) {
    var months;
    months = (d2.getFullYear() - d1.getFullYear()) * 12;
    months -= d1.getMonth() + 1;
    months += d2.getMonth();
    return months <= 0 ? 0 : months;
}

function cloneContent(toClone, parentToAppend) {
    var clnNode = toClone.cloneNode(true);

    clnNode.classList.remove('content');
    clnNode.classList.add('active-content');

    parentToAppend.appendChild(clnNode);

    return clnNode;
}

function cloneContentDirect(toClone, parentToAppend) {
    var clnNode = toClone.cloneNode(true);

    clnNode.classList.remove('content');
    clnNode.classList.add('active-content');

    parentToAppend.append(clnNode);

    return clnNode;
}

function getFileFormats() {
    var processString = document.getElementById("formatList").innerHTML.split('/');

    var formatList = [];

    for (var i = 0; i < processString.length; i++) {
        formatList.push(processString[i].split('.')[1])
    }

    return formatList;
}

function GoNewSPA(currentPage) {
    //Get Previous Page
    var previousPage = document.querySelector('.active-page');

    document.getElementById("circle" + previousPage.id.match(/\d+/)[0]).classList.remove('active');

    var curPage = currentPage.match(/\d+/)[0];

    document.getElementById("circle" + curPage).classList.add('active');

    var linkText = document.getElementById("currentLink");

    linkText.innerHTML = 'step ' + curPage;
    linkText.href = "#Step" + curPage;

    previousPage.classList.remove('active-page');
    document.getElementById(currentPage).classList.add('active-page');
    history.pushState({}, currentPage, ('#' + currentPage));
    document.getElementById(currentPage).dispatchEvent(app.show);

    $(window).scrollTop(0);
}

function AccessPage(pageNo) {
    var current = $('.active-page');
    var pages = $('.page');
    var circles = $('._step-inner');

    $('.bread-step').text('Step ' + pageNo);

    for (var c = 0; c < circles.length; c++) {
        var innerVal = $(circles[c]).find('.move-circle')[0].innerText;

        if (innerVal == pageNo) {
            $(circles[c]).addClass('active');
        }
        else {
            $(circles[c]).removeClass('active');
        }
    }

    current.removeClass('active-page');
    $(pages[pageNo - 1]).addClass('active-page');
    history.pushState({}, pageNo, ('#Step' + pageNo));
    pages[pageNo - 1].dispatchEvent(app.show);
}

function GetCurrentStep() {
    var curStepSplit = window.location.href.split('#');
    var curStepString = curStepSplit[curStepSplit.length - 1];
    var curStep = curStepString[curStepString.length - 1];

    return curStep;
}

function validateTime(value, msgId) {
    if (value == "00:00") {
        showHide(msgId, 'content', 'active-content');
        return false;
    }

    else {
        showHide(msgId, 'active-content', 'content');
        return true;
    }
}

function GetDateInDDMMYYYY(date) {
    var dt = new Date(date);

    var dd = dt.getDate();
    var mm = dt.getMonth() + 1; //January is 0!

    var yyyy = dt.getFullYear();

    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }

    var dt = dd + '/' + mm + '/' + yyyy;

    return dt;
}

function GetDateInDDMMMMYYYYHHMM(date) {
    const monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];

    var dt = new Date(date);

    var dd = dt.getDate();
    var mm = dt.getMonth();
    var HH = dt.getHours();
    var MM = dt.getMinutes();

    var yyyy = dt.getFullYear();

    dd = (dd < 10) ? '0' + dd : dd;
    MM = (MM < 10) ? '0' + MM : MM
    period = (HH < 12) ? 'AM' : 'PM';

    dt = dd + ' ' + monthNames[mm] + ' ' + yyyy + ', ' + HH + ':' + MM + ' ' + period;

    return dt;
}

function CheckAltEmail() {
    var altEmail = $('#alt-email-address').val();
    var domain = $('#alt-email-address-domain').val();
    var altName = $('#alt-contact-person').val();

    if (altEmail == '' && domain != '') {
        showHide('altEmailMsg', nonActiveClass, activeClass);
        showHide('altEmailDomainMsg', activeClass, nonActiveClass);
    }

    else if (altEmail != '' && domain == '') {
        showHide('altEmailDomainMsg', nonActiveClass, activeClass);
        showHide('altEmailMsg', activeClass, nonActiveClass);
    }

    else {
        showHide('altEmailDomainMsg', activeClass, nonActiveClass);
        showHide('altEmailMsg', activeClass, nonActiveClass);
    }

    if (altEmail == '' && domain == '' && $('#alt-telephone-number').val() == '' && altName != '') {
        showHide('emailOrPhone', nonActiveClass, activeClass);
    }

    else {
        showHide('emailOrPhone', activeClass, nonActiveClass);
        showHide('altCtcRequired', activeClass, nonActiveClass);
    }

    if (altEmail != '' || domain != '') {
        if ($('#alt-contact-person').val() == '') {
            showHide('altCtcRequired', nonActiveClass, activeClass);
        }

        else {
            showHide('altCtcRequired', activeClass, nonActiveClass);
        }
    }
}

function ValidateDropdownAndAssign(ddl, hiddenAssignedId, validationMsgId) {
    if (ddl.selectedIndex != 0) {
        document.getElementById(hiddenAssignedId).value = ddl.options[ddl.selectedIndex].text;
        document.getElementById(validationMsgId).style.display = 'none';
    }

    else {
        document.getElementById(hiddenAssignedId).value = null;
        document.getElementById(validationMsgId).style.display = 'block';
    }
}

function ValidateDropdownAndAssignValueText(ddl, hiddenAssignedId, hiddenValueId, validationMsgId) {
    if (ddl.selectedIndex != 0) {
        document.getElementById(hiddenAssignedId).value = ddl.options[ddl.selectedIndex].text;
        document.getElementById(hiddenValueId).value = ddl.value;
        document.getElementById(validationMsgId).style.display = 'none';
    }

    else {
        document.getElementById(hiddenAssignedId).value = null;
        document.getElementById(hiddenValueId).value = null;
        document.getElementById(validationMsgId).style.display = 'block';
    }
}

(function ($) {

    $.fn.niceSelect = function (method) {

        // Methods
        if (typeof method == 'string') {
            if (method == 'update') {
                this.each(function () {
                    var $select = $(this);
                    var $dropdown = $(this).next('.nice-select');
                    var open = $dropdown.hasClass('open');

                    if ($dropdown.length) {
                        $dropdown.remove();
                        create_nice_select($select);

                        if (open) {
                            $select.next().trigger('click');
                        }
                    }
                });
            } else if (method == 'destroy') {
                this.each(function () {
                    var $select = $(this);
                    var $dropdown = $(this).next('.nice-select');

                    if ($dropdown.length) {
                        $dropdown.remove();
                        $select.css('display', '');
                    }
                });
                if ($('.nice-select').length == 0) {
                    $(document).off('.nice_select');
                }
            } else {
                console.log('Method "' + method + '" does not exist.')
            }
            return this;
        }

        // Hide native select
        this.hide();

        // Create custom markup
        this.each(function () {
            var $select = $(this);

            if (!$select.next().hasClass('nice-select')) {
                create_nice_select($select);
            }
        });

        function create_nice_select($select) {
            $select.after($('<div></div>')
                .addClass('nice-select')
                .addClass($select.attr('class') || '')
                .addClass($select.attr('disabled') ? 'disabled' : '')
                .addClass($select.attr('multiple') ? 'has-multiple' : '')
                .attr('tabindex', $select.attr('disabled') ? null : '0')
                .html($select.attr('multiple') ? '<span class="multiple-options"></span><div class="nice-select-search-box"><input type="text" class="nice-select-search" placeholder="Search..."/></div><ul class="list"></ul>' : '<span class="current"></span><div class="nice-select-search-box"><input type="text" class="nice-select-search" placeholder="Search..."/></div><ul class="list"></ul>')
            );

            var $dropdown = $select.next();
            var $options = $select.find('option');
            if ($select.attr('multiple')) {
                var $selected = $select.find('option:selected');
                var $selected_html = '';
                $selected.each(function () {
                    $selected_option = $(this);
                    $selected_text = $selected_option.data('display') || $selected_option.text();
                    $selected_html += '<span class="current">' + $selected_text + '</span>';
                });
                $select_placeholder = $select.data('placeholder') || $select.attr('placeholder');
                $select_placeholder = $select_placeholder == '' ? 'Select' : $select_placeholder;
                $selected_html = $selected_html == '' ? $select_placeholder : $selected_html;
                $dropdown.find('.multiple-options').html($selected_html);
            } else {
                var $selected = $select.find('option:selected');
                $dropdown.find('.current').html($selected.data('display') || $selected.text());
            }


            $options.each(function (i) {
                var $option = $(this);
                var display = $option.data('display');

                $dropdown.find('ul').append($('<li></li>')
                    .attr('data-value', $option.val())
                    .attr('data-display', (display || null))
                    .addClass('option' +
                        ($option.is(':selected') ? ' selected' : '') +
                        ($option.is(':disabled') ? ' disabled' : ''))
                    .html($option.text())
                );
            });
        }

        /* Event listeners */

        // Unbind existing events in case that the plugin has been initialized before
        $(document).off('.nice_select');

        // Open/close
        $(document).on('click.nice_select', '.nice-select', function (event) {
            var $dropdown = $(this);

            $('.nice-select').not($dropdown).removeClass('open');
            $dropdown.toggleClass('open');

            if ($dropdown.hasClass('open')) {
                $dropdown.find('.option');
                $dropdown.find('.nice-select-search').val('');
                $dropdown.find('.nice-select-search').focus();
                $dropdown.find('.focus').removeClass('focus');
                $dropdown.find('.selected').addClass('focus');
                $dropdown.find('ul li').show();
            } else {
                $dropdown.focus();
            }
        });

        $(document).on('click', '.nice-select-search-box', function (event) {
            event.stopPropagation();
            return false;
        });
        $(document).on('keyup.nice-select-search', '.nice-select', function () {
            var $self = $(this);
            var $text = $self.find('.nice-select-search').val();
            var $options = $self.find('ul li');
            if ($text == '')
                $options.show();
            else if ($self.hasClass('open')) {
                $text = $text.toLowerCase();
                var $matchReg = new RegExp($text.replace(/([.?*+^$[\]\\(){}|-])/g, "\\$1"), "i");
                if (0 < $options.length) {
                    $options.each(function () {
                        var $this = $(this);
                        var $optionText = $this.text().toLowerCase();
                        var $matchCheck = $matchReg.test($optionText);
                        $matchCheck ? $this.show() : $this.hide();
                    })
                } else {
                    $options.show();
                }
            }
            $self.find('.option'),
                $self.find('.focus').removeClass('focus'),
                $self.find('.selected').addClass('focus');
        })

        // Close when clicking outside
        $(document).on('click.nice_select', function (event) {
            if ($(event.target).closest('.nice-select').length === 0) {
                $('.nice-select').removeClass('open').find('.option');
            }
        });

        // Option click
        $(document).on('click.nice_select', '.nice-select .option:not(.disabled)', function (event) {
            var $option = $(this);
            var $dropdown = $option.closest('.nice-select');
            if ($dropdown.hasClass('has-multiple')) {
                if ($option.hasClass('selected')) {
                    $option.removeClass('selected');
                } else {
                    $option.addClass('selected');
                }
                $selected_html = '';
                $selected_values = [];
                $dropdown.find('.selected').each(function () {
                    $selected_option = $(this);
                    var text = $selected_option.data('display') || $selected_option.text()
                    $selected_html += '<span class="current">' + text + '</span>';
                    $selected_values.push($selected_option.data('value'));
                });
                $select_placeholder = $dropdown.prev('select').data('placeholder') || $dropdown.prev('select').attr('placeholder');
                $select_placeholder = $select_placeholder == '' ? 'Select' : $select_placeholder;
                $selected_html = $selected_html == '' ? $select_placeholder : $selected_html;
                $dropdown.find('.multiple-options').html($selected_html);
                $dropdown.prev('select').val($selected_values).trigger('change');
            } else {
                $dropdown.find('.selected').removeClass('selected');
                $option.addClass('selected');
                var text = $option.data('display') || $option.text();
                $dropdown.find('.current').text(text);
                $dropdown.prev('select').val($option.data('value')).trigger('change');
            }
        });

        // Keyboard events
        $(document).on('keydown.nice_select', '.nice-select', function (event) {
            var $dropdown = $(this);
            var $focused_option = $($dropdown.find('.focus') || $dropdown.find('.list .option.selected'));

            // Space or Enter
            if (event.keyCode == 13) {
                if ($dropdown.hasClass('open')) {
                    $focused_option.trigger('click');
                } else {
                    $dropdown.trigger('click');
                }
                return false;
                // Down
            } else if (event.keyCode == 40) {
                if (!$dropdown.hasClass('open')) {
                    $dropdown.trigger('click');
                } else {
                    var $next = $focused_option.nextAll('.option:not(.disabled)').first();
                    if ($next.length > 0) {
                        $dropdown.find('.focus').removeClass('focus');
                        $next.addClass('focus');
                    }
                }
                return false;
                // Up
            } else if (event.keyCode == 38) {
                if (!$dropdown.hasClass('open')) {
                    $dropdown.trigger('click');
                } else {
                    var $prev = $focused_option.prevAll('.option:not(.disabled)').first();
                    if ($prev.length > 0) {
                        $dropdown.find('.focus').removeClass('focus');
                        $prev.addClass('focus');
                    }
                }
                return false;
                // Esc
            } else if (event.keyCode == 27) {
                if ($dropdown.hasClass('open')) {
                    $dropdown.trigger('click');
                }
                // Tab
            } else if (event.keyCode == 9) {
                if ($dropdown.hasClass('open')) {
                    return false;
                }
            }
        });

        // Detect CSS pointer-events support, for IE <= 10. From Modernizr.
        var style = document.createElement('a').style;
        style.cssText = 'pointer-events:auto';
        if (style.pointerEvents !== 'auto') {
            $('html').addClass('no-csspointerevents');
        }

        return this;

    };

}(jQuery));