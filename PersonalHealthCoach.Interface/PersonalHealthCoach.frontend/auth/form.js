async function createPersonalData() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/api/v1/users/" + userId + "/data/personal";

    const createPersonalDataCommand = {
        DateOfBirth: getFormItem("birthdate"),
        Weight: getFormItem("weight") == "" ? -1 : getFormItem("weight"),
        Height: getFormItem("height") == "" ? -1 : getFormItem("height"),
        MedicalHistory: getFormItem("medical-history").split(", "),
        CurrentIllnesses: getFormItem("current-illnesses").split(", "),
        Goal: getFormItem("goal"),
        UnwantedExercises: getFormItem("unwanted-exercises").split(", "),
        DailySteps: getFormItem("steps") == "" ? 1 : getFormItem("steps"),
        HoursOfSleep: getFormItem("sleep") == "" ? 1 : getFormItem("sleep"),
        Gender: getFormItem("gender"),
        IsProUser: getFormItem("isProUser") == "on" ? true : false,
        WorkoutsPerWeek: getFormItem("workouts") == "" ? 3 : getFormItem("workouts"),
        HasOther: getFormItem("hasOther") == "on" ? true : false,
        HasMachine: getFormItem("hasMachine") == "on" ? true : false,
        HasBarbell: getFormItem("hasBarbell") == "on" ? true : false,
        HasDumbbell: getFormItem("hasDumbbell") == "on" ? true : false,
        HasKettlebells: getFormItem("hasKettlebells") == "on" ? true : false,
        HasCable: getFormItem("hasCable") == "on" ? true : false,
        hasEasyCurlBar: getFormItem("hasEasyCurlBar") == "on" ? true : false,
        HasNone: getFormItem("hasNone") == "on" ? true : false,
        HasBands: getFormItem("hasBands") == "on" ? true : false,
        HasMedicineBall: getFormItem("hasMedicineBall") == "on" ? true : false,
        HasExerciseBall: getFormItem("hasExerciseBall") == "on" ? true : false,
        HasFoamRoll: getFormItem("hasFoamRoll") == "on" ? true : false,
        WantsBodyOnly: getFormItem("wantsBodyOnly") == "on" ? true : false
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

async function displayForm() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/api/v1/users/" + userId + "/data/personal/latest";

    var latestPersonalData = {
        DateOfBirth: "",
        Weight: "",
        Height: "",
        MedicalHistory: [""],
        CurrentIllnesses: [""],
        Goal: "",
        UnwantedExercises: [""],
        DailySteps: "",
        HoursOfSleep: "",
        CreatedAt: "",
        Gender: "",
        IsProUser: false,
        WorkoutsPerWeek: "",
        HasOther: false,
        HasMachine: false,
        HasBarbell: false,
        HasDumbbell: false,
        HasKettlebells: false,
        HasCable: false,
        hasEasyCurlBar: false,
        HasNone: true,
        HasBands: false,
        HasMedicineBall: false,
        HasExerciseBall: false,
        HasFoamRoll: false,
        WantsBodyOnly: false
    };

    //get latest personal data first
    if (userId != "" && userId != null) {
        latestPersonalData = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(async response => {
            if (!response.ok) {
                const error = await response.json();
            }

            return response.json();
        })
        .then(data => {
            return data;
        })
        .catch(error => {
        });
    }

    //if any of the fields are null, replace with empty string
    if (latestPersonalData == null) {
        latestPersonalData = {
            DateOfBirth: "",
            Weight: "",
            Height: "",
            MedicalHistory: [""],
            CurrentIllnesses: [""],
            Goal: "",
            UnwantedExercises: [""],
            DailySteps: "",
            HoursOfSleep: "",
            CreatedAt: "",
            Gender: "",
            IsProUser: false,
            WorkoutsPerWeek: "",
            HasOther: false,
            HasMachine: false,
            HasBarbell: false,
            HasDumbbell: false,
            HasKettlebells: false,
            HasCable: false,
            hasEasyCurlBar: false,
            HasNone: true,
            HasBands: false,
            HasMedicineBall: false,
            HasExerciseBall: false,
            HasFoamRoll: false,
            WantsBodyOnly: false
        }
    }
    if (latestPersonalData.DateOfBirth == null) {
        latestPersonalData.DateOfBirth = "";
    }
    if (latestPersonalData.Weight == null) {
        latestPersonalData.Weight = "";
    }
    if (latestPersonalData.Height == null) {
        latestPersonalData.Height = "";
    }
    if (latestPersonalData.MedicalHistory == null) {
        latestPersonalData.MedicalHistory = "";
    }
    else {
        latestPersonalData.MedicalHistory = latestPersonalData.MedicalHistory.join(", ");
    }
    if (latestPersonalData.CurrentIllnesses == null) {
        latestPersonalData.CurrentIllnesses = "";
    }
    else {
        latestPersonalData.CurrentIllnesses = latestPersonalData.CurrentIllnesses.join(", ");
    }
    if (latestPersonalData.Goal == null) {
        latestPersonalData.Goal = "";
    }
    if (latestPersonalData.UnwantedExercises == null) {
        latestPersonalData.UnwantedExercises = "";
    }
    else {
        latestPersonalData.UnwantedExercises = latestPersonalData.UnwantedExercises.join(", ");
    }
    if (latestPersonalData.DailySteps == null) {
        latestPersonalData.DailySteps = "";
    }
    if (latestPersonalData.HoursOfSleep == null) {
        latestPersonalData.HoursOfSleep = "";
    }
    if (latestPersonalData.CreatedAt == null) {
        latestPersonalData.CreatedAt = "";
    }
    if (latestPersonalData.IsProUser == null) {
        latestPersonalData.IsProUser = false;
    }
    if (latestPersonalData.WorkoutsPerWeek == null) {
        latestPersonalData.WorkoutsPerWeek = "";
    }
    if (latestPersonalData.HasOther == null) {
        latestPersonalData.HasOther = false;
    }
    if (latestPersonalData.HasMachine == null) {
        latestPersonalData.HasMachine = false;
    }
    if (latestPersonalData.HasBarbell == null) {
        latestPersonalData.HasBarbell = false;
    }
    if (latestPersonalData.HasDumbbell == null) {
        latestPersonalData.HasDumbbell = false;
    }
    if (latestPersonalData.HasKettlebells == null) {
        latestPersonalData.HasKettlebells = false;
    }
    if (latestPersonalData.HasCable == null) {
        latestPersonalData.HasCable = false;
    }
    if (latestPersonalData.hasEasyCurlBar == null) {
        latestPersonalData.hasEasyCurlBar = false;
    }
    if (latestPersonalData.HasNone == null) {
        latestPersonalData.HasNone = false;
    }
    if (latestPersonalData.HasBands == null) {
        latestPersonalData.HasBands = false;
    }
    if (latestPersonalData.HasMedicineBall == null) {
        latestPersonalData.HasMedicineBall = false;
    }
    if (latestPersonalData.HasExerciseBall == null) {
        latestPersonalData.HasExerciseBall = false;
    }
    if (latestPersonalData.HasFoamRoll == null) {
        latestPersonalData.HasFoamRoll = false;
    }
    if (latestPersonalData.WantsBodyOnly == null) {
        latestPersonalData.WantsBodyOnly = false;
    }

    await drawForm(latestPersonalData);
}

async function drawForm(latestPersonalData) {
    const form = document.querySelector('.insertable-form');
    form.innerHTML = `
            <div class="field">
				<center><label>Height(cm)*</label></center>
				<input type="number" id="height" name="height" class="input" value="${latestPersonalData.Height}" required/>
			</div>
			<div class="field">
				<center><label>Weight(kg)*</label></center>
				<input type="number" id="weight" name="weight" class="input" value="${latestPersonalData.Weight}" required/>
			</div>
			<div class="field">
				<center><label>Date of birth*</label></center>
				<input type="date" id="birthdate" name="birthdate" class="input" value="${latestPersonalData.DateOfBirth}" required/>
			</div>
			<div class="field">
				<center><label>Medical History</label></center>
				<input type="text" id="medical-history" name="medical-history" value="${latestPersonalData.MedicalHistory}" class="input">
			</div>
			<div class="field">
				<center><label>Unwanted Exercises</label></center>
				<input type="text" id="unwanted-exercises" name="unwanted" value="${latestPersonalData.UnwantedExercises}" class="input">
			</div>
			<div class="field">
				<center><label>Current Illnesses</label></center>
				<input type="text" id="current-illnesses" name="illnesses" value="${latestPersonalData.CurrentIllnesses}" class="input">
			</div>
			<div class="field">
				<center><label>Last Night's Hours of Sleep</label></center>
				<input type="text" id="sleep" name="sleep" value="${latestPersonalData.HoursOfSleep}" class="input">
			</div>
			<div class="field">
				<center><label>Today's Steps</label></center>
				<input type="text" id="steps" name="steps" value="${latestPersonalData.DailySteps}" class="input">
			</div>
            <div class="field">
				<center><label>Workouts per Week</label></center>
				<input type="text" id="workouts" name="workouts" value="${latestPersonalData.WorkoutsPerWeek}" class="input">
			</div>
			<div class="field">
				<center><label for="gender">Gender</label></center>
				<select id="gender" name="gender" selected="${latestPersonalData.Gender}">
					<option value="M">Male</option>
					<option value="F">Female</option>
				</select>
			</div>
			<div class="field">
				<center><label for="goal">Goal</label></center>
				<select id="goal" name="goal" selected="${latestPersonalData.Goal}">
					<option value="Lose weight">Lose Weight</option>
					<option value="Gain muscular mass">Gain muscular mass</option>
					<option value="Improve overall health">Improve overall health</option>
					<option value="Improve cardiovascular health">Improve cardiovascular health</option>
					<option value="Increase strength">Increase strength</option>
					<option value="Increase endurance">Increase endurance</option>
					<option value="Maintain weigth">Maintain weigth</option>
				</select>
			</div>
            <div class="field">
                <center><label for="isProUser">Pro User</label></center>
                <input type="checkbox" id="isProUser" name="isProUser" ${latestPersonalData.IsProUser ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasOther">Has Other</label></center>
                <input type="checkbox" id="hasOther" name="hasOther" ${latestPersonalData.HasOther ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasMachine">Has Machine</label></center>
                <input type="checkbox" id="hasMachine" name="hasMachine" ${latestPersonalData.HasMachine ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasBarbell">Has Barbell</label></center>
                <input type="checkbox" id="hasBarbell" name="hasBarbell" ${latestPersonalData.HasBarbell ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasDumbbell">Has Dumbbell</label></center>
                <input type="checkbox" id="hasDumbbell" name="hasDumbbell" ${latestPersonalData.HasDumbbell ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasKettlebells">Has Kettlebells</label></center>
                <input type="checkbox" id="hasKettlebells" name="hasKettlebells" ${latestPersonalData.HasKettlebells ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasCable">Has Cable</label></center>
                <input type="checkbox" id="hasCable" name="hasCable" ${latestPersonalData.HasCable ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasEasyCurlBar">Has Easy Curl Bar</label></center>
                <input type="checkbox" id="hasEasyCurlBar" name="hasEasyCurlBar" ${latestPersonalData.hasEasyCurlBar ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasNone">Has None</label></center>
                <input type="checkbox" id="hasNone" name="hasNone" ${latestPersonalData.HasNone ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasBands">Has Bands</label></center>
                <input type="checkbox" id="hasBands" name="hasBands" ${latestPersonalData.HasBands ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasMedicineBall">Has Medicine Ball</label></center>
                <input type="checkbox" id="hasMedicineBall" name="hasMedicineBall" ${latestPersonalData.HasMedicineBall ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasMedicineBall">Has Exercise Ball</label></center>
                <input type="checkbox" id="hasExerciseBall" name="hasExerciseBall" ${latestPersonalData.HasExerciseBall ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="hasFoamRoll">Has Foam Roll</label></center>
                <input type="checkbox" id="hasFoamRoll" name="hasFoamRoll" ${latestPersonalData.HasFoamRoll ? 'checked' : ''}>
            </div>
            <div class="field">
                <center><label for="wantsBodyOnly">Body Only</label></center>
                <input type="checkbox" id="wantsBodyOnly" name="wantsBodyOnly" ${latestPersonalData.WantsBodyOnly ? 'checked' : ''}>
            </div>
            <div id="error-container" class="error-container"></div>
			<button type="button" value="Submit" class="submit-form" onClick="createPersonalData()">Submit</button>
		`;
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