
let fitnessScore = 0;
let formContainer;
let formData = {
    user_id: 123,
    pro_user: true,
    goal: "",
    workouts_per_week: 0,
    fitness_score: 0,
    equipment_available: {}
};

const formParts = [
    `<form id="part1">
        <label for="goal">Goal</label>
        <select id="goal" required>
            <option value="">Choose a Goal</option>
            <option value="Lose weight">Lose weight</option>
            <option value="Gain muscular mass">Gain muscular mass</option>
            <option value="Maintain weight">Maintain weight</option>
            <option value="Improve cardiovascular health">Improve cardiovascular health</option>
            <option value="Increase strength">Increase strength</option>
            <option value="Increase endurance">Increase endurance</option>
            <option value="Improve overall health">Improve overall health</option>
        </select>
        <br>
        <label for="age">Age</label>
        <input type="number" id="age" min="12" max="100" required>
        <br>
        
        <label for="gender">Gender</label>
        <select id="gender" required>
            <option value="">Choose a Gender</option>
            <option value="male">Male</option>
            <option value="female">Female</option>
        </select>
        <button type="button" onclick="nextPart(1)">Next</button>
    </form>`,

    `<form id="part2">
        <label for="workouts">How many workouts per week?</label>
        <input type="number" id="workouts" min="1" max="7" required>
        <br>
        <button type="button" onclick="nextPart(2)">Next</button>
    </form>`,

    `<form id="part3">
        <label>What equipment do you have available?</label>
        <div>
        <input type="checkbox" id="dumbbell" name="equipment" value="Dumbbell">
        <label for="dumbbell">Dumbbell</label>
        </div>
        <div>
        <input type="checkbox" id="barbell" name="equipment" value="Barbell">
        <label for="barbell">Barbell</label>
         </div>
      <div>
        <input type="checkbox" id="kettlebells" name="equipment" value="Kettlebells">
        <label for="kettlebells">Kettlebells</label>
         </div>
      <div>
        <input type="checkbox" id="cable" name="equipment" value="Cable">
        <label for="cable">Cable</label>
         </div>
      <div>
        <input type="checkbox" id="machine" name="equipment" value="Machine">
        <label for="machine">Machine</label>
         </div>
       <div>
        <input type="checkbox" id="other" name="equipment" value="Other">
        <label for="other">Other</label>
         </div>
        <div>
        <input type="checkbox" id="body_only" name="equipment" value="Body Only">
        <label for="body_only">Body Only</label>
         </div>
         <div>
        <input type="checkbox" id="ez_curl_bar" name="equipment" value="E-Z Curl Bar">
        <label for="ez_curl_bar">E-Z Curl Bar</label>
         </div>
       
         <div>
        <input type="checkbox" id="none" name="equipment" value="None">
        <label for="none">None</label>
         </div>
        
         <div>
        <input type="checkbox" id="bands" name="equipment" value="Bands">
        <label for="bands">Bands</label>
         </div>
     
         <div>
        <input type="checkbox" id="medicine_ball" name="equipment" value="Medicine Ball">
        <label for="medicine_ball">Medicine Ball</label>
         </div>
    
         <div>
        <input type="checkbox" id="exercise_ball" name="equipment" value="Exercise Ball">
        <label for="exercise_ball">Exercise Ball</label>
         </div>
 
         <div>
        <input type="checkbox" id="foam_roll" name="equipment" value="Foam Roll">
        <label for="foam_roll">Foam Roll</label>
         </div>


        <button type="button" onclick="nextPart(3)">Next</button>
    </form>`,

    `<form id="part4">
        <div>
            <p>What is your past training experience?</p>
            <div>
                <input type="radio" id="no_experience" name="experience" value="no_experience" required>
                <label for="no_experience">No experience</label>
            </div>
            <div>
                <input type="radio" id="less_than_1_year" name="experience" value="less_than_1_year" required>
                <label for="less_than_1_year">Less than 1 year</label>
            </div>
            <div>
                <input type="radio" id="1_to_2_years" name="experience" value="1_to_2_years" required>
                <label for="1_to_2_years">1 to 2 years</label>
            </div>
            <div>
                <input type="radio" id="2_to_3_years" name="experience" value="2_to_3_years" required>
                <label for="2_to_3_years">2 to 3 years</label>
            </div>
            <div>
                <input type="radio" id="more_than_3_years" name="experience" value="more_than_3_years" required>
                <label for="more_than_3_years">More than 3 years</label>
            </div>
        </div>
        <button type="button" onclick="nextPart(4)">Next</button>
    </form>`,

    `<form id="part5">
        <div>
            <p>How many Push-Ups can you do in a row?</p>
            <div>
                <input type="radio" id="0-10_pushups" name="push-ups" value="0-10" required>
                <label for="0-10_pushups">0-10</label>
            </div>
            <div>
                <input type="radio" id="10-30_pushups" name="push-ups" value="10-30" required>
                <label for="10-30_pushups">10-30</label>
            </div>
            <div>
                <input type="radio" id="30-50_pushups" name="push-ups" value="30-50" required>
                <label for="30-50_pushups">30-50</label>
            </div>
            <div>
                <input type="radio" id="50-70_pushups" name="push-ups" value="50-70" required>
                <label for="50-70_pushups">50-70</label>
            </div>
            <div>
                <input type="radio" id="70-90_pushups" name="push-ups" value="70-90" required>
                <label for="70-90_pushups">70-90</label>
            </div>
            <div>
                <input type="radio" id="90+_pushups" name="push-ups" value="90+" required>
                <label for="90+_pushups">90+</label>
            </div>
        </div>
        <button type="button" onclick="nextPart(5)">Next</button>
    </form>`,

    `<form id="part6">
        <div>
            <p>How many Pull-Ups can you do in a row?</p>
            <div>
                <input type="radio" id="0_pullups" name="pull-ups" value="0" required>
                <label for="0_pullups">0</label>
            </div>
            <div>
                <input type="radio" id="1-3_pullups" name="pull-ups" value="1-3" required>
                <label for="1-3_pullups">1-3</label>
            </div>
            <div>
                <input type="radio" id="4-7_pullups" name="pull-ups" value="4-7" required>
                <label for="4-7_pullups">4-7</label>
            </div>
            <div>
                <input type="radio" id="8-15_pullups" name="pull-ups" value="8-15" required>
                <label for="8-15_pullups">8-15</label>
            </div>
            <div>
                <input type="radio" id="15-30_pullups" name="pull-ups" value="15-30" required>
                <label for="15-30_pullups">15-30</label>
            </div>
            <div>
                <input type="radio" id="30+_pullups" name="pull-ups" value="30+" required>
                <label for="30+_pullups">30+</label>
            </div>
        </div>
        <button type="button" onclick="nextPart(6)">Next</button>
    </form>`,

    `<form id="part7">
        <div>
            <p>Have you had any previous injuries?</p>
            <div>
                <input type="radio" id="recent_severe" name="injuries" value="recent_severe" required>
                <label for="recent_severe">Recent severe injury</label>
            </div>
            <div>
                <input type="radio" id="old_severe" name="injuries" value="old_severe" required>
                <label for="old_severe">Old severe injury</label>
            </div>
            <div>
                <input type="radio" id="recent_minor" name="injuries" value="recent_minor" required>
                <label for="recent_minor">Recent minor injury</label>
            </div>
            <div>
                <input type="radio" id="old_minor" name="injuries" value="old_minor" required>
                <label for="old_minor">Old minor injury</label>
            </div>
            <div>
                <input type="radio" id="no_injuries" name="injuries" value="no_injuries" required>
                <label for="no_injuries">No injuries</label>
            </div>
        </div>
        <button type="button" onclick="nextPart(7)">Generate</button>
    </form>`
];

function validateForm(partIndex) {
    let formDiv = document.querySelector(`#part${partIndex}`);
    let form = formDiv.querySelector('form');
    let isValidForm = form.checkValidity();
    if (!isValidForm) {
        form.reportValidity();
    }
    return isValidForm;
}

function updateFormData(formElement, partIndex) {
    switch (partIndex) {
        case 1:
            formData.goal = formElement.querySelector("#goal").value;
            let age = formElement.querySelector("#age").value;
            if (age <= 15) fitnessScore += 0.25;
            else if (age <= 20) fitnessScore += 0.5
            else if (age <= 25) fitnessScore += 0.75;
            else if (age <= 30) fitnessScore += 1;
            else if (age <= 35) fitnessScore += 0.75;
            else if (age <= 40) fitnessScore += 0.5;
            else if (age <= 45) fitnessScore += 0.25;
            else if (age <= 50) fitnessScore += 0.15;
            else fitnessScore += 0.1;
            break;
        case 2:
            formData.workouts_per_week = formElement.querySelector("#workouts").value;
            formData["workouts_per_week"] = parseInt(formData["workouts_per_week"]);
            break;
        case 3:
            let equipments = formElement.querySelectorAll('input[type="checkbox"]');
            equipments.forEach(equipment => {
                formData.equipment_available[equipment.value] = equipment.checked;
            });
            break;
        case 4:
            let experience = formElement.querySelector('input[name="experience"]:checked').value;
            if (experience === "no_experience") fitnessScore += 0;
            else if (experience === "less_than_1_year") fitnessScore += 0.5;
            else if (experience === "1_to_2_years") fitnessScore += 1;
            else if (experience === "2_to_3_years") fitnessScore += 1.5;
            else if (experience === "more_than_3_years") fitnessScore += 2;
            break;
        case 5:
            let pushUps = formElement.querySelector('input[name="push-ups"]:checked').value;
            if (pushUps === "0-10") fitnessScore += 0;
            else if (pushUps === "10-30") fitnessScore += 0.5;
            else if (pushUps === "30-50") fitnessScore += 1;
            else if (pushUps === "50-70") fitnessScore += 1.5;
            else if (pushUps === "70-90") fitnessScore += 2;
            else if (pushUps === "90+") fitnessScore += 2.5;

            break;
        case 6:
            let pullUps = formElement.querySelector('input[name="pull-ups"]:checked').value;
            if (pullUps === "0") fitnessScore += 0;
            else if (pullUps === "1-3") fitnessScore += 0.5;
            else if (pullUps === "4-7") fitnessScore += 1;
            else if (pullUps === "8-15") fitnessScore += 1.5;
            else if (pullUps === "15-30") fitnessScore += 2;
            else if (pullUps === "30+") fitnessScore += 2.5;
            break;
        case 7:
            let injuries = formElement.querySelector('input[name="injuries"]:checked').value;
            if (injuries == "recent_severe") fitnessScore = 0;
            if (injuries == "old_severe") fitnessScore -= 0.5;
            if (injuries == "recent_minor") fitnessScore -= 0.5;
            if (injuries == "old_minor") fitnessScore -= 0.25;
            if (injuries == "no_injuries") fitnessScore += 0.25;
            break;
    }
    formData.fitness_score = fitnessScore;
    return { formData, fitnessScore };
}

function nextPart(partIndex) {
    let formElement = formContainer.querySelector(`#part${partIndex}`);
    if (validateForm(partIndex)) {
        let result = updateFormData(formElement, partIndex);
        formData = result.formData;
        fitnessScore = result.fitnessScore;
        formElement.style.display = 'none';
        if (partIndex < formParts.length) {
            formElement = formContainer.querySelector(`#part${partIndex + 1}`);
            formElement.style.display = 'block';
            console.log(formData);
        } else {
            fetchAndDisplayWorkout(formData)
        }
    }
}

function fetchAndDisplayWorkout(formData) {
    console.log(formData);

    let userId = getCookie('userId');
    if (userId == "" || userId == null)
        userId = '93706ca4-5d9b-4ddf-8b68-3041cd7829ab';

    let url = `http://localhost:7071/api/v1/users/${userId}/plans/fitness`;

    formData["user_id"] = userId;
    console.log(formData);
    let data = JSON.stringify(formData);

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: data
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const contentType = response.headers.get('content-type');
            if (!contentType || !contentType.includes('application/json')) {
                throw new TypeError("Oops, we haven't got JSON!");
            }
            return response.json();
        })
        .then(data => {
            const container = document.querySelector('#workout-container');
            container.innerHTML = '';
            if (data && data.Workouts.length > 0) {
                let workoutNumber = 1;
                let regenerateButton = document.createElement('button');
                regenerateButton.innerText = 'Regenerate Workout';
                regenerateButton.onclick = function () {
                    this.remove();
                    fetchAndDisplayWorkout(formData);
                }
                document.body.appendChild(regenerateButton);

                pdfButton.style.display = 'block';

                data.Workouts.forEach(workout => {
                    let workoutContainer = document.createElement('div');
                    workoutContainer.className = "workout-container";

                    let workoutDiv = document.createElement('div');
                    workoutDiv.className = "workout";
                    workoutDiv.style.display = "none";

                    let showHideButton = document.createElement('button');
                    showHideButton.innerText = `Workout ${workoutNumber++}`;
                    showHideButton.onclick = function () {
                        let workoutContent = this.nextElementSibling;
                        if (workoutContent.style.display === "none") {
                            workoutContent.style.display = "block";
                        } else {
                            workoutContent.style.display = "none";
                        }
                    }

                    workoutContainer.appendChild(showHideButton);
                    workoutContainer.appendChild(workoutDiv);
                    container.appendChild(workoutContainer);

                    workout.Exercises.forEach(exercise => {
                        let exerciseDiv = document.createElement('div');
                        exerciseDiv.className = "exercise";

                        let exerciseHeader = document.createElement('h3');
                        exerciseHeader.innerText = exercise.exercise;
                        exerciseDiv.appendChild(exerciseHeader);

                        let detailsDiv = document.createElement('div');
                        detailsDiv.className = "exercise-details";

                        let setsDiv = document.createElement('div');
                        setsDiv.className = "detail sets";
                        setsDiv.innerText = `Sets: ${exercise.sets}`;
                        detailsDiv.appendChild(setsDiv);

                        let repRangeDiv = document.createElement('div');
                        repRangeDiv.className = "detail rep-range";
                        repRangeDiv.innerText = `Rep Range: ${exercise.rep_range}`;
                        detailsDiv.appendChild(repRangeDiv);

                        let restTimeDiv = document.createElement('div');
                        restTimeDiv.className = "detail rest-time";
                        restTimeDiv.innerText = `Rest Time: ${exercise.rest_time}`;
                        detailsDiv.appendChild(restTimeDiv);

                        let typeDiv = document.createElement('div');
                        typeDiv.className = "detail type";
                        typeDiv.innerText = `Type: ${exercise.type}`;
                        detailsDiv.appendChild(typeDiv);


                        exerciseDiv.appendChild(detailsDiv);

                        exercise.images.forEach(imageUrl => {
                            let img = document.createElement('img');
                            img.src = imageUrl;
                            exerciseDiv.appendChild(img);
                        });

                        let instructionsButton = document.createElement('button');
                        instructionsButton.innerText = 'Show Instructions';
                        instructionsButton.onclick = function () {
                            let instructionDiv = this.nextElementSibling;
                            if (instructionDiv.style.display === "none") {
                                instructionDiv.style.display = "block";
                            } else {
                                instructionDiv.style.display = "none";
                            }
                        }
                        exerciseDiv.appendChild(instructionsButton);

                        let instructionsDiv = document.createElement('div');
                        instructionsDiv.style.display = "none";

                        exercise.instructions.forEach(instruction => {
                            let instructionP = document.createElement('p');
                            instructionP.innerText = instruction;
                            instructionsDiv.appendChild(instructionP);
                        });

                        exerciseDiv.appendChild(instructionsDiv);

                        workoutDiv.appendChild(exerciseDiv);



                    });

                });
            } else {
                container.innerText = 'No workouts found for the user';
            }
        })
        .catch((error) => {
            console.error('Error:', error);
        });
}

function init() {
    formContainer = document.getElementById('form-container');
    formParts.forEach((part, i) => {
        let formPartElement = document.createElement('div');
        formPartElement.id = `part${i + 1}`;
        formPartElement.innerHTML = part;
        formContainer.appendChild(formPartElement);
        if (i !== 0) {
            formPartElement.style.display = 'none';
        }
    });
}
function getCookie(name) {
    const cookies = document.cookie.split('; ').reduce((acc, cookie) => {
        const [name, value] = cookie.split('=');
        acc[name] = value;
        return acc;
    }, {});

    return cookies[name];
}



let pdfButton = document.createElement('button');
pdfButton.innerText = 'Export to PDF';
pdfButton.onclick = function () {
    let doc = new jsPDF();

    let workoutContainers = document.querySelectorAll('.workout-container');

    let yOffset = 20;
    const pageHeight = doc.internal.pageSize.height || doc.internal.pageSize.getHeight();
    const margin = 20;

    workoutContainers.forEach((workoutContainer, index) => {

        let workoutNumber = workoutContainer.querySelector('button').innerText;
        let exercises = Array.from(workoutContainer.querySelectorAll('.exercise'));


        if (yOffset + 10 > pageHeight) {
            doc.addPage();
            yOffset = 20;
        }
        doc.text(workoutNumber, margin, yOffset);
        yOffset += 10;

        exercises.forEach(exercise => {
            let exerciseName = exercise.querySelector('h3').innerText;
            let sets = exercise.querySelector('.sets').innerText;
            let reps = exercise.querySelector('.rep-range').innerText;
            let rest = exercise.querySelector('.rest-time').innerText;
            let type = exercise.querySelector('.type').innerText;


            if (yOffset + 10 > pageHeight) {
                doc.addPage();
                yOffset = 20;
            }
            doc.text(exerciseName, margin, yOffset);
            yOffset += 10;

            let details = [sets, reps, rest, type];

            details.forEach(detail => {
                if (yOffset + 10 > pageHeight) {
                    doc.addPage();
                    yOffset = 20;
                }
                doc.text(detail, margin, yOffset);
                yOffset += 10;
            });


            yOffset += 10;


            if (yOffset >= pageHeight) {
                doc.addPage();
                yOffset = 20;
            }
        });


        if (index !== workoutContainers.length - 1) {
            doc.addPage();
            yOffset = 20;
        }
    });

    doc.save('workout.pdf');
}


pdfButton.style.display = 'none';
document.body.appendChild(pdfButton);

window.onload = init;