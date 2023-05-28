let exerciseCount = 1;

async function ExerciseForm() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/api/v1/users/" + userId + "/exercise-history";

    document.getElementById('exerciseForm').addEventListener('submit', function(event) {
        event.preventDefault();

        var form = document.getElementById('exerciseForm');
        var data = { Exercises: [] };

        for(let i = 0; i < exerciseCount; i++){
            var exerciseName = form.elements['exerciseName' + i].value;
            var duration = form.elements['duration' + i].value;
            var caloriesBurned = form.elements['caloriesBurned' + i].value;

            data.Exercises.push({
                Title: exerciseName,
                Duration: parseInt(duration),
                CaloriesBurned: parseInt(caloriesBurned)
            });
        }

        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
        .then(function(response) {
            if (response.ok) {
                console.log('Form submitted successfully');
            } else {
                console.error('Error submitting the form');
            }
        })
        .catch(function(error) {
            console.error('Error submitting the form:', error);
        });
    });

    document.querySelector('.add-exercise').addEventListener('click', function(event) {
        event.preventDefault();
        addExercise();
    });

    document.querySelector('.remove-exercise').addEventListener('click', function(event) {
        event.preventDefault();
        removeExercise();
    });
}

function addExercise() {
    const form = document.getElementById('exerciseForm');
    const div = document.createElement('div');
    div.classList.add('form-group', 'exercise-group');
    div.innerHTML = `
        <label for="exerciseName${exerciseCount}">Exercise Name:</label>
        <input type="text" class="form-control" id="exerciseName${exerciseCount}" name="exerciseName${exerciseCount}" required>

        <label for="duration${exerciseCount}">Duration (minutes):</label>
        <input type="number" class="form-control" id="duration${exerciseCount}" name="duration${exerciseCount}" required>

        <label for="caloriesBurned${exerciseCount}">Calories Burned:</label>
        <input type="number" class="form-control" id="caloriesBurned${exerciseCount}" name="caloriesBurned${exerciseCount}" required>
    `;
    form.insertBefore(div, form.querySelector('.add-exercise'));
    exerciseCount++;
}

function removeExercise() {
    if(exerciseCount > 1){
        const form = document.getElementById('exerciseForm');
        const lastExerciseGroup = form.querySelectorAll('.exercise-group')[exerciseCount-1];
        lastExerciseGroup.remove();
        exerciseCount--;
    }
}

function getCookie(name) {
    const cookies = document.cookie.split('; ').reduce((acc, cookie) => {
        const [cookieName, value] = cookie.split('=');
        acc[cookieName] = value;
        return acc;
    }, {});

    return cookies[name];
}

function displayError(errorMessage) {
    const errorContainer = document.getElementById('error-container');
    errorContainer.textContent = errorMessage;
    errorContainer.style.display = 'block';

    setTimeout(() => {
        errorContainer.style.display = 'none';
    }, 5000);
}
