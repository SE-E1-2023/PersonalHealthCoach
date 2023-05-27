let daysCount = 1;

async function StressForm() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/api/v1/users/" + userId + "/food-history";

    document.getElementById('stressForm').addEventListener('submit', function(event) {
        event.preventDefault();

        var form = document.getElementById('stressForm');
        var data = { Days: [] };

        for(let i = 0; i < daysCount; i++){
            var sleepingHours = form.elements['sleepingHours' + i].value;
            var dayType = form.elements['dayType' + i].value;
            var heartBeats = form.elements['heartBeats' + i].value;
            var breathingRate = form.elements['breathingRate' + i].value;

            data.Foods.push({
                Day: dayType,
                HeartBeats: parseInt(heartBeats),
                BreathingRate: parseInt(breathingRate),
                SleepingHours: parseInt(sleepingHours),
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
