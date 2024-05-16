document.body.addEventListener("click", (e) => {
    const target = e.target.classList;
    if (target.contains("custom-form-control")) {
        showRegularCommentButton(e.target);
    } else {
        hideAllCommentButtons();
    }
})

const divs = document.getElementsByClassName("comment-button") || [];

function showRegularCommentButton(input) {
    const divId = input.id.replace("R", "D");
    
    hideAllCommentButtons();
    showCommentButton(divId);
}

function hideAllCommentButtons() {
    for (const element of divs) {
        element.classList.add("hide");
    }
}

function showCommentButton(divId) {
    const div = document.getElementById(divId);
    if (div) {
        div.classList.remove("hide");
    }
}

function getValueFromInput(inputRef) {
    return { Id: inputRef.id, Value: inputRef.value };
}

function getAllCheckedUsers() {
    const checkedUsers = [];
    const checkboxes = document.getElementsByClassName("user");

    for (const checkbox of checkboxes) {
        if (checkbox.checked) {
            checkedUsers.push(+checkbox.id);
        }
    }

    return checkedUsers;
}


function getCookie(cname) {
    // Split the cookie string into an array of key-value pairs
    const cookies = document.cookie.split(';');

    for (let i = 0; i < cookies.length; i++) {
        let c = cookies[i];

        // Trim leading and trailing whitespace
        c = c.trim();

        // Check if the current cookie name matches the requested name
        if (c.indexOf(cname + '=') === 0) {
            // Extract the value after the '=' sign
            return c.substring(cname.length + 1);
        }
    }

    // If the cookie is not found, return an empty string
    return "";
}





