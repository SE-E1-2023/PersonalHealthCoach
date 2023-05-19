async function DisplayFoodHistory() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/v1/users/" + userId + "/food-history";

    await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(async response => {
        if (!response.ok) {
            const error = await response.json();
            throw new Error(error);
        }

        return response.json();
    })
    .then(async data => {

        const foodhist = data;
        const foodhistoryContainer = document.getElementById('foodhistory_container');
        for(var i=0; i < foodhist.length; i++) {
            foodhistoryContainer.innerHTML = ` 
                
            <div class="foodhistory-div">
                <h3>Dish: ${foodhist[i].Title}</h3>
                <h3>Served at: ${foodhist[i].Meal} </h3>
                <h3>Calories burnt: ${foodhist[i].Calories}</h3>
                <h3>Quantity: ${foodhist[i].Quantity}</h3>
            </div>
             
            `;
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

function displayError(errorMessage) {
    const errorContainer = document.querySelector('.error-container');
    errorContainer.textContent = errorMessage;
    errorContainer.style.display = 'block';

    setTimeout(() => {
        errorContainer.style.display = 'none';
    }, 5000);
}

errorMessages = {

    "FoodHistory.Get.NotFound" : "Data not found. Please try again."
}  