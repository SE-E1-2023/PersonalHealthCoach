async function createPersonalData() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/api/v1/users/" + userId + "/data/personal";

    const createPersonalDataCommand = {
        DateOfBirth: getFormItem("birthdate"),
        Weight: getFormItem("weight"),
        Height: getFormItem("height"),
        MedicalHistory: getFormItem("medical-history").split(", "),
        CurrentIllnesses: getFormItem("current-illnesses").split(", "),
        Goal: getFormItem("goal"),
        UnwantedExercises: getFormItem("unwanted-exercises").split(", "),
        DailySteps: getFormItem("steps"),
        HoursOfSleep: getFormItem("sleep"),
        Gender: getFormItem("gender"),
    };
	
	await fetch(url, {
		method: 'POST',
		headers: {
			'Content-Type': 'application/json'
		},
		body: JSON.stringify(createPersonalDataCommand)
	})
    .then(async response => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error);
        }

        return response.json();
    })
    .then(data => {
        window.location.href = "../main/Homepage.html";
    })
    .catch(error => {
        if (errorMessages[error.message] == undefined) {
            displayError("Something went wrong. Please try again.");
        }
        else {
            displayError(errorMessages[error.message]);
        }
    });
}

errorMessages = {
    "PersonalData.Create.DateOfBirthNull": "Please enter your date of birth.",
    "PersonalData.Create.UserNotOldEnough": "You must be at least 18 years old to use this app.",
    "PersonalData.Create.InvalidWeight": "Please enter a valid weight.",
    "PersonalData.Create.InvalidHeight": "Please enter a valid height.",
    "PersonalData.Create.InvalidDailySteps": "Please enter a valid number of daily steps.",
    "PersonalData.Create.InvalidHoursOfSleep": "Please enter a valid number of hours of sleep.",
    "PersonalData.Create.InvalidGender": "Please select a valid gender",
    "PersonalData.Create.GoalIsNullOrEmpty": "Please enter your goal.",
    "PersonalData.Create.GoalIsUnrecognized": "Please enter a valid goal.",
    "PersonalData.Create.UserNotFound": "User not found. Please try again."
}

function getCookie(name) {
    const cookies = document.cookie.split('; ').reduce((acc, cookie) => {
        const [name, value] = cookie.split('=');
        acc[name] = value;
        return acc;
    }, {});

    return cookies[name];
}

function displayError(errorMessage) {
    const errorContainer = getFormItem('error-container');
    errorContainer.textContent = errorMessage;
    errorContainer.style.display = 'block';

    setTimeout(() => {
        errorContainer.style.display = 'none';
    }, 5000);
}

function getFormItem(id) {
    return document.getElementById(id).value ?? "";
}