async function FoodForm() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/v1/users/" + userId + "/food-history";

    document.getElementById('foodForm').addEventListener('submit', function(event) {
        event.preventDefault();

        var form = document.getElementById('foodForm');
        var foodName = form.elements['foodName'].value;
        var foodType = form.elements['foodType'].value;
        var calories = form.elements['calories'].value;
        var quantity = form.elements['quantity'].value;

        var data = {
            Foods: [
                {
                    Title: foodName,
                    Meal: foodType,
                    Calories: parseInt(calories),
                    Quantity: parseInt(quantity)
                }
            ]
        };

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