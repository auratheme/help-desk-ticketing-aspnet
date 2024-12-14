// self executing function
(function () {
    initDropdownlist();
    formatDateText();
    formatDateTimeText();
    initDateInputs();
    initDateTimeInputs();
    //initialize bootstrap tooltip
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });
})();

function scrollToTop() {
    // Scroll to the top with smooth behavior
    window.scrollTo({
        top: 0,
        behavior: 'smooth'
    });
}

//change top navigation background color when page scrolled
const topnav = document.querySelector('#top-navigation');
if (typeof (topnav) != 'undefined' && topnav != null) {
    document.addEventListener('scroll', () => {
        if (window.scrollY > 100) {
            topnav.classList.add('scrolled');
        } else {
            topnav.classList.remove('scrolled');
        }
        //if top navigation element contains scrolled-shadow class, add shadow class to the element (add shadow to top navigation on page scrolled)
        if (topnav.classList.contains('scrolled-shadow')) {
            window.scrollY > 100 ? topnav.classList.add('shadow') : topnav.classList.remove('shadow');
        }
    });
}

//hide the dummy header and footer of a table after finish loading table data
function hideDummySpinnerHeaderFooter(tablewrapperid) {
    let spinner = document.querySelector("#" + tablewrapperid + " .spinner");
    let dummyFooter = document.querySelector("#" + tablewrapperid + " .dummyfooter");
    spinner.classList.remove("d-flex");
    spinner.classList.add("d-none");
    dummyFooter.classList.add("d-none");
}

function closeBsModal(modalId) {
    var myModal = new bootstrap.Modal(document.getElementById(modalId));
    myModal.hide();
}

//set background image of div with data-img tag
var bgimg = document.querySelector("[data-img]");
if (void 0 !== bgimg && null != bgimg) {
    for (var element, dataimgs = document.querySelectorAll("[data-img]"), i = 0; element = dataimgs[i]; i++) {
        if (element.getAttribute("data-img") != "") {
            var h = element.getAttribute("data-img"),
                a = element.getAttribute("data-img-position"),
                b = element.getAttribute("data-img-attachment");
            element.style.backgroundImage = "url(" + h + ")", void 0 !== a && null != a ? element.style.backgroundPosition = a : element.style.backgroundPosition = "center center", void 0 !== b && null != b ? element.style.backgroundAttachment = b : element.style.backgroundAttachment = "scroll", element.style.backgroundSize = "cover", element.style.backgroundRepeat = "no-repeat"
        }
    }
}
function loading(text) {
    let loadingSpinner = document.getElementById("loading");
    if (text) {
        let textEle = loadingSpinner.querySelector("#loadingtext");
        textEle.innerText = text;
    }
    loadingSpinner.classList.remove("d-none");
}
//move the img by following the cursor
var cursorImg = document.querySelectorAll('.cursor-img');
if (typeof (cursorImg) != 'undefined' && cursorImg != null) {
    document.addEventListener('mousemove', moveImg, false);
}
function moveImg(e) {
    for (var i = cursorImg.length; i--;) {
        cursorImg[i].style.left = e.clientX + 'px';
        cursorImg[i].style.top = e.clientY + 'px';
    }
}

function getNumberAbbreviation(value) {
    var newValue = value;
    if (value >= 1000) {
        var suffixes = ["", "k", "m", "b", "t"];
        var suffixNum = Math.floor(("" + value).length / 3);
        var shortValue = '';
        for (var precision = 2; precision >= 1; precision--) {
            shortValue = parseFloat((suffixNum != 0 ? (value / Math.pow(1000, suffixNum)) : value).toPrecision(precision));
            var dotLessShortValue = (shortValue + '').replace(/[^a-zA-Z 0-9]+/g, '');
            if (dotLessShortValue.length <= 2) { break; }
        }
        if (shortValue % 1 != 0) shortValue = shortValue.toFixed(1);
        newValue = shortValue + suffixes[suffixNum];
    }
    return newValue;
}

function handleDropdownSelection(event) {
    // Get the selected dropdown item
    const selected = event.target;
    // Get the parent dropdown
    const dropdown = selected.closest('.form-dropdown');
    // Get the input element
    const input = dropdown.querySelector('.form-dropdown .dropdown-input');
    // Set the input value to the selected value
    input.value = selected.dataset.value;
    // Remove active class from all items and add to selected
    dropdown.querySelectorAll('.dropdown-item').forEach(item => item.classList.remove('active'));
    selected.classList.add('active');
    // Set the dropdown toggle text to the selected text
    const dropdownToggle = dropdown.querySelector('.form-dropdown .dropdown-toggle');
    dropdownToggle.innerText = selected.innerText;
}

//initialize drop down list

function initDropdownlist() {
    const dropdownButtons = document.querySelectorAll(".form-dropdown.dropdown");
    dropdownButtons.forEach(function (button) {
        button.addEventListener('click', function (event) {
            searchEventInDropDown(button);
        });
    });
    // Get all dropdown items on the page
    const dropdownItems = document.querySelectorAll('.form-dropdown .dropdown-item');
    if (void 0 !== dropdownItems && null != dropdownItems) {
        // Add a click event listener to each dropdown item
        dropdownItems.forEach(item => {
            item.addEventListener('click', handleDropdownSelection);
        });
    }
}

function searchEventInDropDown(button) {
    const searchInput = button.querySelector('.form-control.search');
    if (searchInput) {
        searchInput.focus();
        searchInput.click();
        let currentItem = -1; // Start from the first element
        let filteredList = [];
        let total = button.querySelectorAll('.dropdown-list a').length;
        filteredList.push(...Array.from({ length: total }, (_, i) => i));
        searchInput.addEventListener('input', function (event) {
            filteredList.length = 0;
            const searchText = event.target.value.toLowerCase();
            const items = button.querySelectorAll('.dropdown-list a');
            items.forEach((item, index) => {
                const itemText = item.textContent.toLowerCase();
                if (itemText.includes(searchText)) {
                    item.parentElement.style.display = '';
                    filteredList.push(index);
                    if (index === currentItem) {
                        item.focus(); // Focus on the current item
                    }
                } else {
                    item.parentElement.style.display = 'none';
                }
            });
            currentItem = -1;
        });
        searchInput.addEventListener('keydown', function (event) {
            const items = button.querySelectorAll('.dropdown-list a');
            var proceed = false;
            if (event.key === 'ArrowDown') {
                var last = filteredList.length - 1;
                if (currentItem < (filteredList.length - 1)) {
                    currentItem = (currentItem + 1) % filteredList.length; // Loop around to the first element if end is reached
                    proceed = true;
                }
            } else if (event.key === 'ArrowUp') {
                if (currentItem > 0) {
                    currentItem = (currentItem - 1 + filteredList.length) % filteredList.length; // Loop around to the last element if beginning is reached
                    proceed = true;
                }
            }
            else if (event.key === 'Enter') {
                event.preventDefault(); // Prevent form submission on Enter
                var filteredIndex = filteredList[currentItem];
                items[filteredIndex].click();
            }
            if (proceed == true) {
                items.forEach(item => item.classList.remove('active'));
                var filteredIndex = filteredList[currentItem];
                items[filteredIndex].focus(); // Focus on the current item
                items[filteredIndex].classList.add('active'); // Add active class to the current item
                items[filteredIndex].scrollIntoView({ behavior: "smooth", block: "nearest" });
            }
        });
    }
}

function clickHandler(submitEvent) {
    submitEvent.preventDefault();
    // remove the event listener to enable click event back
    submitEvent.target.removeEventListener('click', clickHandler);
}

function changeDropDownListItems(childDropDownElementId, selectList, placeholder) {
    // Get a reference to the dropdown element
    var dropdown = document.querySelector(`#${childDropDownElementId}`);
    var btn = dropdown.querySelector("button");
    btn.innerText = placeholder;
    var dropdowninput = dropdown.querySelector("input.dropdown-input");
    dropdowninput.value = "";
    // Get a reference to the ul element inside the dropdown
    var ul = dropdown.querySelector('ul');
    // Get all the li elements with class "dropdown-item"
    var liItems = ul.querySelectorAll('.dropdown-list');
    // Loop through the li elements and remove them from the DOM
    for (var i = 0; i < liItems.length; i++) {
        liItems[i].remove();
    }
    selectList.forEach(function (element) {
        var li = document.createElement('li');
        li.className = 'dropdown-list';
        var a = document.createElement('a');
        a.className = 'dropdown-item';
        a.setAttribute('data-value', element.Value);
        a.innerText = element.Text;
        li.appendChild(a);
        ul.appendChild(li);
    });

    initDropdownlist();
}

//this will reset drop down list to placeholder text
function resetDropDown(dropdownId, placeholder) {
    // Get all the dropdown items
    var dropdownItems = document.querySelectorAll(`#${dropdownId}-ddl .dropdown-item`);
    if (dropdownItems != undefined) {
        var searchInputs = document.querySelector(`#${dropdownId}-ddl .dropdown-menu input`);
        if (searchInputs != undefined) {
            searchInputs.values = "";
        }
        var activeitem = document.querySelector(`#${dropdownId}-ddl .dropdown-item.active`);
        if (activeitem != undefined) {
            activeitem.classList.remove('active');
        }
        // Set the dropdown toggle text to the selected text
        const dropdownToggle = document.querySelector(`#${dropdownId}-ddl .dropdown-toggle`);
        if (dropdownToggle != undefined) {
            dropdownToggle.innerText = placeholder;
        }
        //set dropdown input value to ""
        const dropdownInputValue = document.querySelector(`input.dropdown-input[name='${dropdownId}']`);
        if (dropdownInputValue != undefined) {
            dropdownInputValue.value = "";
        }
    }
}

function initDateInputs() {
    // Get all the input elements of type 'date'
    const dateInputs = document.querySelectorAll('input[type="date"]');
    if (dateInputs.length > 0) {
        // Attach event listeners to each date input
        dateInputs.forEach(function (input) {
            input.addEventListener('change', function () {
                setDateValue(input);
            });
        });
    }
}

function initDateTimeInputs() {
    // Get all the input elements of type 'date'
    const datetimeInputs = document.querySelectorAll('input[type="datetime-local"]');
    if (datetimeInputs.length > 0) {
        // Attach event listeners to each date input
        datetimeInputs.forEach(function (input) {
            input.addEventListener('change', function () {
                setDateTimeValue(input);
            });
        });
    }
}

function setDateValue(input) {
    const isoUtcString = input.value;
    if (isoUtcString != "" && isoUtcString != null) {
        const date = new Date(isoUtcString);
        const localDate = date.toISOString().substring(0, 10);
        input.value = localDate;
        input.setAttribute("value", localDate);
    }
}

function setDateTimeValue(input) {
    const isoUtcString = input.value;
    if (isoUtcString != "" && isoUtcString != null) {
        const date = new Date(isoUtcString);
        const localDateTime = date.toISOString().substring(0, 16);
        input.value = localDateTime;
        input.setAttribute("value", localDateTime);
    }
}

function convertToLocalDateIsoString(isoUtcString) {
    const dateTimeUtc = new Date(isoUtcString);
    var options = { year: 'numeric', month: '2-digit', day: '2-digit' };
    var dateString = dateTimeUtc.toLocaleDateString(undefined, options);
    var datePart = dateString.substring(0, 10);
    return datePart;
}

function convertToLocalDatetimeIsoString(isoUtcString) {
    const dateTimeUtc = new Date(isoUtcString);
    const dateTimeLocal = new Date(dateTimeUtc.getTime() - dateTimeUtc.getTimezoneOffset() * 60 * 1000);
    const formattedDateTime = dateTimeLocal.getFullYear() + '-' +
        (dateTimeLocal.getMonth() + 1).toString().padStart(2, '0') + '-' +
        dateTimeLocal.getDate().toString().padStart(2, '0') + 'T' +
        dateTimeLocal.getHours().toString().padStart(2, '0') + ':' +
        dateTimeLocal.getMinutes().toString().padStart(2, '0');
    return formattedDateTime; //example: 2023-02-11T14:44 (this format used in <input type="datetime-local"> element)
}

function getFormattedDateTime(value) {
    // Trim the value to remove any leading/trailing spaces
    var trimmedValue = value.trim();
    if (trimmedValue == null || trimmedValue == "" || trimmedValue == undefined) {
        return "";
    }
    var valueSubString = trimmedValue.substring(0, 16);
    var dateTimeUtc = new Date(valueSubString);
    var dateTimeLocal = new Date(dateTimeUtc.getTime() - dateTimeUtc.getTimezoneOffset() * 60 * 1000);
    var fullDateTime = dateTimeLocal.toLocaleTimeString([], {
        year: 'numeric', //numeric = 2022, 2-digit = 22
        month: '2-digit', //2-digit = 12, short = Dec, long = December
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit'
    });
    //example: 02/11/2023, 09:44 PM
    //this format used in any element for display the date time text
    //Note: do not use this in input element, instead, call convertToLocalDatetimeIsoString function when it's an input
    return fullDateTime;
}

function getFormattedDate(value) {
    const dateTimeUtc = new Date(value);
    // Get the user's time offset in minutes
    var offsetMinutes = new Date().getTimezoneOffset();
    // Convert the offset to milliseconds and add to the UTC time
    var localTime = new Date(dateTimeUtc.getTime() - (offsetMinutes * 60 * 1000));
    // Output the local date and time in a readable format
    var fullDate = localTime.toLocaleString([], {
        year: 'numeric', //numeric = 2022, 2-digit = 22
        month: '2-digit', //2-digit = 12, short = Dec, long = December
        day: '2-digit'
    });
    //example: 02/11/2023
    //this format used in any element for display the date text
    //Note: do not use this in input element, instead, call convertToLocalDatetimeIsoString function when it's an input
    return fullDate;
}

function formatDateTimeText(eleid) {
    var dtText = (typeof eleid === 'undefined') ? document.querySelector(".datetimetext") : document.getElementById(eleid).querySelector(".datetimetext");
    if (dtText) {
        var allDtText = (typeof eleid === 'undefined') ? document.querySelectorAll(".datetimetext") : document.getElementById(eleid).querySelectorAll(".datetimetext");
        for (const option of allDtText) {
            var dt = $(option).text();
            if (dt != "" && dt != null) {
                var result = getFormattedDateTime(dt);
                $(option).text(result);
            }
        }
    }
}
function formatDurationText() {
    var durationElements = document.querySelectorAll(".dateduration");

    if (durationElements) {
        durationElements.forEach(function (element) {
            var timestamp = new Date(element.textContent).getTime(); // Convert to milliseconds since Unix epoch
            var currentTime = Date.now(); // Current time in milliseconds since Unix epoch
            var secondsAgo = Math.floor((currentTime - timestamp) / 1000); // Calculate duration in seconds
            var result = getFormattedDuration(secondsAgo);
            element.innerText = result;
        });
    }
}

// Function to format duration
function getFormattedDuration(seconds) {
    var duration = "";

    if (seconds < 60) {
        duration = "few seconds ago";
    } else if (seconds < 3600) {
        var minutes = Math.floor(seconds / 60);
        duration = minutes + (minutes === 1 ? " minute ago" : " minutes ago");
    } else if (seconds < 86400) {
        var hours = Math.floor(seconds / 3600);
        duration = hours + (hours === 1 ? " hour ago" : " hours ago");
    } else {
        var days = Math.floor(seconds / 86400);
        duration = days + (days === 1 ? " day ago" : " days ago");
    }

    return duration;
}


var dtInput = document.querySelector("input.datetimetext-input");
if (dtInput) {
    for (const datetimeInput of document.querySelectorAll("input.datetimetext-input")) {
        let datetime = new Date(datetimeInput.value);
        let clientMachineDateTime = new Date(datetime.getTime() - datetime.getTimezoneOffset() * 60 * 1000);
        datetimeInput.value = clientMachineDateTime.toISOString().slice(0, -1);
    }
}

function formatDateText(eleid) {
    var dText = (typeof eleid === 'undefined') ? document.querySelector(".datetext") : document.getElementById(eleid).querySelector(".datetext");
    if (dText) {
        var allDText = (typeof eleid === 'undefined') ? document.querySelectorAll(".datetext") : document.getElementById(eleid).querySelectorAll(".datetext");
        for (const option of allDText) {
            var dt = $(option).text();
            if (dt != "" && dt != null) {
                var result = getFormattedDate(dt);
                $(option).text(result);
            }
        }
    }
}

//Hide success message at top right corner automatically after 2500ms (2.5 second)
setTimeout(() => {
    let toast = document.querySelector('#successtoast-container .toast.show');
    if (toast) {
        toast.classList.remove('show');
    }
    $('#successtoast-container').hide();
}, 2500);

//Hide fail message at top right corner automatically after 5200ms (5.2 second)
setTimeout(() => {
    let toast = document.querySelector('#failedtoast-container .toast.show');
    if (toast) {
        toast.classList.remove('show');
    }
    $('#failedtoast-container').hide();
}, 5200);

//Convert 100,000 to 100K etc (Not used in this project, but might used in other projects)
function getNumberAbbreviation(a) {
    var e = a;
    if (a >= 1e3) {
        for (var f = ["", "k", "m", "b", "t"], c = Math.floor(("" + a).length / 3), b = "", d = 2; d >= 1 && !(((b = parseFloat((0 != c ? a / Math.pow(1e3, c) : a).toPrecision(d))) + "").replace(/[^a-zA-Z 0-9]+/g, "").length <= 2); d--);
        b % 1 != 0 && (b = b.toFixed(1)), e = b + f[c]
    }
    return e
}

function shareToX() {
    const currentUrl = encodeURIComponent(window.location.href); // Get and encode the current URL
    const text = encodeURIComponent("Check out this article!"); // Custom share message
    const twitterUrl = `https://x.com/intent/tweet?url=${currentUrl}&text=${text}`;

    window.open(twitterUrl, "_blank", "noopener,noreferrer"); // Open in a new tab
}

function shareToFacebook() {
    copyCurrentUrl(false);
    const currentUrl = encodeURIComponent(window.location.href); // Get and encode the current URL
    const facebookUrl = `https://www.facebook.com/sharer/sharer.php?u=${currentUrl}`;
    window.open(facebookUrl, "_blank", "noopener,noreferrer"); // Open in a new tab
}

function shareToWhatsApp() {
    const currentUrl = encodeURIComponent(window.location.href); // Get and encode the current URL
    const message = encodeURIComponent("Check out this article: "); // Custom share message
    const whatsappUrl = `https://wa.me/?text=${message}${currentUrl}`;

    window.open(whatsappUrl, "_blank", "noopener,noreferrer"); // Open in a new tab
}

// Function to copy the current URL
function copyCurrentUrl(showSuccessMessage) {
    const currentUrl = window.location.href;
    if (navigator.clipboard && window.isSecureContext) {
        navigator.clipboard.writeText(currentUrl)
            .then(() => {
                if (showSuccessMessage) {
                    showSuccessToast("URL copied to clipboard!");
                }
            })
            .catch(err => {
                console.error("Failed to copy URL: ", err);
            });
    } else {
        // Fallback for insecure contexts
        const textarea = document.createElement('textarea');
        textarea.value = currentUrl;
        document.body.appendChild(textarea);
        textarea.select();
        try {
            document.execCommand('copy');
            if (showSuccessMessage) {
                showSuccessToast("URL copied to clipboard!");
            }
        } catch (err) {
            console.error("Fallback copy failed: ", err);
        }
        document.body.removeChild(textarea);
    }
}


function copyText(ele) {
    // Get the text content to copy
    const textToCopy = (ele.innerText ?? ele.innerHTML).trim();

    // Use the Clipboard API to copy text
    navigator.clipboard.writeText(textToCopy).then(function () {
        // Show success message in toast
        var toastBody = document.querySelector('#copiedToast .toast-body');
        toastBody.innerText = "Copied successfully!";

        var toastEl = document.querySelector('#copiedToast');
        var toast = new bootstrap.Toast(toastEl, { delay: 1500, animation: false, autohide: true });
        toast.show();
    }).catch(function (error) {
        // Handle the error if copying fails
        console.error('Unable to copy text: ', error);
    });
}


function copyFromModal(elementWithLinkText, modalId) {
    var openedModal = document.getElementById(modalId);
    var linktext = openedModal.querySelector("#" + elementWithLinkText).innerText.trim();

    // Use the Clipboard API to copy the text
    navigator.clipboard.writeText(linktext).then(function () {
        showCopiedSuccess(); // Show success message after copying
    }).catch(function (error) {
        console.error('Unable to copy text: ', error); // Handle copy error
    });
}


function copyMultipleText(eles) {
    let fulltext = "";

    // Concatenate the inner text of each element with line breaks
    eles.forEach(function (ele) {
        fulltext += ele.innerText + "\n\n";
    });

    // Use the Clipboard API to copy the concatenated text
    navigator.clipboard.writeText(fulltext).then(function () {
        // Show success message in toast
        var toastBody = document.querySelector('#copiedToast .toast-body');
        toastBody.innerText = "Copied successfully!";

        var toastEl = document.querySelector('#copiedToast');
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    }).catch(function (error) {
        // Handle the error if copying fails
        console.error('Unable to copy text: ', error);
    });
}

function showCopiedSuccess() {
    var toastBody = document.querySelector('#copiedToast .toast-body');
    if (toastBody) {
        toastBody.innerText = "Copied success!";
    }
    var toastEl = document.querySelector('#copiedToast');
    if (toastEl) {
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    }
}

function showNotificationToast(message) {
    var toastBody = document.querySelector('#notificationToast .toast-body');
    if (toastBody) {
        toastBody.innerText = message;
    }
    var toastEl = document.querySelector('#notificationToast');
    if (toastEl) {
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    }
}

function showFailedToast(message) {
    var toastBody = document.querySelector('#ajaxFailedToast .toast-body');
    if (toastBody) {
        toastBody.innerText = message;
    }
    var toastEl = document.querySelector('#ajaxFailedToast');
    if (toastEl) {
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    }
}

function showSuccessToast(message) {
    var toastBody = document.querySelector('#ajaxSuccessToast .toast-body');
    if (toastBody) {
        toastBody.innerText = message;
    }
    var toastEl = document.querySelector('#ajaxSuccessToast');
    if (toastEl) {
        var toast = new bootstrap.Toast(toastEl);
        toast.show();
    }
}

function addTextareaEnterListener(textareaElement, submitButton) {
    textareaElement.addEventListener('keydown', function (event) {
        if (event.keyCode === 13 && event.shiftKey) {
            // Shift + Enter key combination pressed
        }
        else if (event.keyCode === 13 && !event.shiftKey) {
            // Enter key pressed
            event.preventDefault();
            const userInput = textareaElement;
            if (userInput != null) {
                if (userInput.value != null && userInput.value != "") {
                    if (submitButton) {
                        submitButton.click();
                    }
                }
            }
        }
    });
}

function autoResizeHeight(element) {
    element.style.height = 'auto';  // reset the height to auto in case it was changed before
    element.style.height = `${element.scrollHeight}px`;  // set the height to match the content
    if (element.scrollHeight > 85) {
        document.getElementById("conversationWrapper").style.maxHeight = "67%";
    } else {
        document.getElementById("conversationWrapper").style.maxHeight = "75%";
    }
}

function decodeHTML(html) {
    const parser = new DOMParser();
    const doc = parser.parseFromString(html, "text/html");
    return doc.documentElement.textContent;
}

function formatResponse(txt) {
    //remove programming language text, sometimes the open ai response will put programming language at the beginning of a code block like this: ```javascript var i = 0;, this regex will remove the text "javascript" and remain ``` var i = 0;
    const removedProgrammingLanguageText = txt.replace(/^.*?(```\w*\n)/s, '$1');
    const replacedTripleBacktick = removedProgrammingLanguageText.replace(/```([\s\S]+?)```/g, (match, group) => {
        return `<pre><code>${group}</code></pre>`;
    });
    var replacedSingleBacktick = replacedTripleBacktick.replace(/`([\s\S]+?)`/g, (match, group) => {
        return `<code>${group}</code>`;
    });
    if (replacedSingleBacktick.startsWith("\n\n")) {
        replacedSingleBacktick = replacedSingleBacktick.substring(2);
    }
    var pattern = /(https?:\/\/[^\s]+)/g;
    var replacedLink = replacedSingleBacktick.replace(pattern, '<a href="$1" class="fw600 color-primarycolor" target="_blank">$1</a>');
    const formattedResponse = replacedLink.replace(/\n/g, "<br>");
    const newHtmlString = formattedResponse;
    return newHtmlString;
}

function splitByHtml(newHtmlString) {
    const regex = /(<pre><code>.*?<\/code><\/pre>)|(<br>)|(<pre>)|(<\/pre>)|(<code>)|(<\/code>)|(<b>.*?<\/b>)|(<a[^>]*?>.*?<\/a>)/gs;
    const dividedByCodeAndBr = newHtmlString.split(regex);
    var removedEmptyElementArr = dividedByCodeAndBr.filter((elem) => elem !== "" && elem !== "undefined" && elem !== undefined);
    let resultArr = [];
    for (let i = 0; i < removedEmptyElementArr.length; i++) {
        if (removedEmptyElementArr[i] != undefined && removedEmptyElementArr[i] != "undefined" && removedEmptyElementArr[i] != null) {
            resultArr.push(removedEmptyElementArr[i]);
        }
    }
    return resultArr;
}

function applyTypingEffectToArray(resultArr, parentElement, conversationWrapper) {
    if (resultArr[0] === "<br>" && resultArr[1] === "<br>") {
        resultArr.splice(0, 2);
    }
    else if (resultArr[0] === "<br>") {
        resultArr.splice(0, 1);
    }
    let i = 0;
    let j = 0;
    var speed = 11;
    const cursor = document.getElementById('cursor');
    if (cursor != null) {
        cursor.classList.remove('invisible');
        cursor.classList.add('visible');
    }
    let interval = setInterval(() => {
        if (i < resultArr.length) {
            if (resultArr[i].startsWith("<pre>")) {
                const div = document.createElement('div');
                var formattedPreCode = resultArr[i].replace(/<br>/g, '\n');
                div.innerHTML = formattedPreCode;
                parentElement.appendChild(div);
                hljs.highlightAll();
                i++;
                j = 0;
            } else if (resultArr[i] == "<br>") {
                parentElement.innerHTML += "<br>";
                i++;
                j = 0;
            } else if (resultArr[i].startsWith("<a")) {
                parentElement.innerHTML += resultArr[i];
                i++;
                j = 0;
            } else if (resultArr[i].startsWith("<b>")) {
                parentElement.innerHTML += resultArr[i];
                i++;
                j = 0;
            }
            else if (j < resultArr[i].length) {
                const char = resultArr[i].charAt(j);
                const charNode = document.createTextNode(char);
                parentElement.appendChild(charNode);
                j++;
            } else {
                i++;
                j = 0;
            }
        } else {
            if (cursor != null) {
                cursor.classList.remove('visible');
                cursor.classList.add('invisible');
            }
            clearInterval(interval);
        }
        if (conversationWrapper != null) {
            conversationWrapper.scrollTop = conversationWrapper.scrollHeight;
        }
    }, speed);
}

function getFileName(extension) {
    let dateTimeString = new Date().toISOString().replace(/:/g, '-').replace(/\..+/, '');
    let result = 'translation-' + dateTimeString + '.' + extension;
    return result;
}

function exportStringToExcel(eleid,filename) {
    let ele = document.getElementById(eleid);
    let tableToExport = `<table class='d-none'><tr><td>${ele.innerText}</td></tr></table>`;
    let tempElement = document.createElement('div');
    tempElement.id = "tableToExport";
    tempElement.innerHTML = tableToExport;
    tempElement.setAttribute("data-filename", filename);
    document.body.appendChild(tempElement);
    let result = exportToExcel("tableToExport");
    if (result == true) {
        document.body.removeChild(tempElement);
    }
}
function exportStringToCsv(eleid, filename) {
    let ele = document.getElementById(eleid);
    let tableToExport = `<table class='d-none'><tr><td>${ele.innerText}</td></tr></table>`;
    let tempElement = document.createElement('div');
    tempElement.id = "tableToExport";
    tempElement.innerHTML = tableToExport;
    tempElement.setAttribute("data-filename", filename);
    document.body.appendChild(tempElement);
    let result = exportToCsv("tableToExport");
    if (result == true) {
        document.body.removeChild(tempElement);
    }
}
function exportStringToPdf(toRemoveClass) {
    const elements = document.querySelectorAll('[id^="PrintMessage_"]');
    elements.forEach(element => {
        element.classList.add("nonprintable");
    });
    const toRemove = document.querySelector(`#${toRemoveClass}`);
    toRemove.classList.remove("nonprintable");
    window.print();
}

function applyTypingEffectToArray(resultArr, parentElement, conversationWrapper) {
    if (resultArr[0] === "<br>" && resultArr[1] === "<br>") {
        resultArr.splice(0, 2);
    }
    else if (resultArr[0] === "<br>") {
        resultArr.splice(0, 1);
    }
    let i = 0;
    let j = 0;
    var speed = 11;
    const cursor = document.getElementById('cursor');
    if (cursor != null) {
        cursor.classList.remove('invisible');
        cursor.classList.add('visible');
    }
    let interval = setInterval(() => {
        if (i < resultArr.length) {
            if (resultArr[i].startsWith("<pre>")) {
                const div = document.createElement('div');
                var formattedPreCode = resultArr[i].replace(/<br>/g, '\n');
                div.innerHTML = formattedPreCode;
                parentElement.appendChild(div);
                hljs.highlightAll();
                i++;
                j = 0;
            } else if (resultArr[i] == "<br>") {
                parentElement.innerHTML += "<br>";
                i++;
                j = 0;
            } else if (resultArr[i].startsWith("<a")) {
                parentElement.innerHTML += resultArr[i];
                i++;
                j = 0;
            } else if (resultArr[i].startsWith("<b>")) {
                parentElement.innerHTML += resultArr[i];
                i++;
                j = 0;
            }
            else if (j < resultArr[i].length) {
                const char = resultArr[i].charAt(j);
                const charNode = document.createTextNode(char);
                parentElement.appendChild(charNode);
                j++;
            } else {
                i++;
                j = 0;
            }
        } else {
            if (cursor != null) {
                cursor.classList.remove('visible');
                cursor.classList.add('invisible');
            }
            clearInterval(interval);
        }
        if (conversationWrapper != null) {
            conversationWrapper.scrollTop = conversationWrapper.scrollHeight;
        }
    }, speed);
}

function getFileExtension(urlOrBaseString) {
    // If the input is a URL
    if (urlOrBaseString.startsWith('http')) {
        // Extract the file extension from the URL
        const url = new URL(urlOrBaseString);
        const path = url.pathname;
        return path.substring(path.lastIndexOf('.') + 1);
    }

    // If the input is a base64-encoded string
    if (urlOrBaseString.startsWith('data:image')) {
        // Extract the MIME type from the base64 string
        const mimeType = urlOrBaseString.split(';')[0].split('/')[1];
        // Get the file extension based on the MIME type
        switch (mimeType) {
            case 'jpeg':
                return 'jpg';
            default:
                return mimeType;
        }
    }

    // If it's neither a URL nor a base64-encoded string
    return null;
}

