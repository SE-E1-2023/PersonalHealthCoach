let foodCount = 1;

async function FoodForm() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/api/v1/users/" + userId + "/food-history";

    document.getElementById('foodForm').addEventListener('submit', function(event) {
        event.preventDefault();

        var form = document.getElementById('foodForm');
        var data = { Foods: [] };

        for(let i = 0; i < foodCount; i++){
            var foodName = form.elements['foodName' + i].value;
            var foodType = form.elements['foodType' + i].value;
            var calories = form.elements['calories' + i].value;
            var quantity = form.elements['quantity' + i].value;

            data.Foods.push({
                Title: foodName,
                Meal: foodType,
                Calories: parseInt(calories),
                Quantity: parseInt(quantity)
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

    document.querySelector('.add-food').addEventListener('click', function(event) {
        event.preventDefault();
        addFood();
    });

    document.querySelector('.remove-food').addEventListener('click', function(event) {
        event.preventDefault();
        removeFood();
    });
}

function addFood() {
    const form = document.getElementById('foodForm');
    const div = document.createElement('div');
    div.classList.add('form-group', 'food-group');
    div.innerHTML = `
        <label for="foodName${foodCount}">Food Name:</label>
        <input type="text" class="form-control" id="foodName${foodCount}" name="foodName${foodCount}" required>

        <label for="foodType${foodCount}">Food Type:</label>
        <select class="form-control" id="foodType${foodCount}" name="foodType${foodCount}" required>
            <option value="">Select Type</option>
            <option value="Breakfast">Breakfast</option>
            <option value="Drink">Drink</option>
            <option value="Main Course">Main Course</option>
            <option value="Side Dish">Side Dish</option>
            <option value="Snack">Snack</option>
            <option value="Soup">Soup</option>
        </select>

        <label for="calories${foodCount}">Calories:</label>
        <input type="number" class="form-control" id="calories${foodCount}" name="calories${foodCount}" required>

        <label for="quantity${foodCount}">Quantity:</label>
        <input type="number" class="form-control" id="quantity${foodCount}" name="quantity${foodCount}" required>
    `;
    form.insertBefore(div, form.querySelector('.add-food'));
    foodCount++;
}

function removeFood() {
    if(foodCount > 1){
        const form = document.getElementById('foodForm');
        const lastFoodGroup = form.querySelectorAll('.food-group')[foodCount-1];
        lastFoodGroup.remove();
        foodCount--;
    }
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
