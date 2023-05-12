async function signIn() {
    const url = "http://localhost:7071/api/v1/users?EmailAddress=";

        const email = document.getElementById("email").value;

        await fetch(url + email, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        })
            .then(async response => {
                if (!response.ok) {
                    const error = await response.json();
                    throw new Error(error);
                }

                return response.json();
            })
            .then(data => {
                setCookie("userId", data);
                window.location.href = "../main/Homepage.html";
            })
            .catch(error => {
                console.error("Error:", error);

                var errorMessage = "";
                if (error.message == "User.Get.EmailAddressDoesntExist") {
                    errorMessage = errorMessages["User.Get.EmailAddressDoesntExist"];
                } else {
                    errorMessage = "An error occurred. Please try again.";
                }

                displayError(errorMessage);
            });
}

async function signUp() {
    const url = "http://localhost:7071/api/v1/users";

    const name = document.getElementById("name").value;
    const surname = document.getElementById("surname").value;
    const email = document.getElementById("email").value;

    const createUserCommand = {
        Name: name,
        FirstName: surname,
        EmailAddress: email,
    }

        await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(createUserCommand),
        })
            .then(async response => {
                if (!response.ok) {
                    const error = await response.json();
                    throw new Error(error);
                }

                return response.json();
            })
            .then(data => {
                window.location.href = "form.html";
            })
            .catch(error => {
                console.error("Sign up error: ", error);

                var errorMessage = "";
                if (error.message == "User.Create.EmailAddressAlreadyInUse") {
                    errorMessage = errorMessages["User.Create.EmailAddressAlreadyInUse"];
                } else if (error.message == "User.Create.NameNullOrEmpty") {
                    errorMessage = errorMessages["User.Create.NameNullOrEmpty"];
                } else if (error.message == "User.Create.EmailAddressNullOrEmpty") {
                    errorMessage = errorMessages["User.Create.EmailAddressNullOrEmpty"];
                } else if (error.message == "User.Create.FirstNameNullOrEmpty") {
                    errorMessage = errorMessages["User.Create.FirstNameNullOrEmpty"];
                } else if (error.message == "User.Create.InvalidEmailAddressFormat") {
                    errorMessage = errorMessages["User.Create.InvalidEmailAddressFormat"];
                } else {
                    errorMessage = "An error occurred. Please try again.";
                }
            });
}

errorMessages = {
    // Sign in errors
    "User.Get.EmailAddressDoesntExist": "No account exists with that email address. Please try again.",

    // Sign up errors
    "User.Create.EmailAddressAlreadyInUse": "An account already exists with that email address. Please try again.",
    "User.Create.NameNullOrEmpty": "Please enter your name.",
    "User.Create.EmailAddressNullOrEmpty": "Please enter your email address.",
    "User.Create.FirstNameNullOrEmpty": "Please enter your first name.",
    "User.Create.InvalidEmailAddressFormat": "Please enter a valid email address."
}

function setCookie(name, value) {
    let expires = '';
    const date = new Date();
    date.setTime(date.getTime() + (1000 * 24 * 60 * 60 * 1000));
    expires = '; expires=' + date.toUTCString();
    document.cookie = name + '=' + (value || '') + expires + '; path=/';
}

function displayError(errorMessage) {
    const errorContainer = document.getElementById('error-container');
    errorContainer.textContent = errorMessage;
    errorContainer.style.display = 'block';

    setTimeout(() => {
        errorContainer.style.display = 'none';
    }, 5000);
}