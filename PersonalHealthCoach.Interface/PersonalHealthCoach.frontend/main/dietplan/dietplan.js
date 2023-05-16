async function createDietPlan() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/api/v1/users/" + userId + "/plans/diet";

    await fetch(url, {
        method: 'POST',
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
    .then(data => {
    })
    .catch(error => {
        displayError(errorMessages[error]);
    });

    await getAndDisplayDietPlan();
}

async function getAndDisplayDietPlan() {
    const userId = getCookie("userId");
    const url = "http://localhost:7071/api/v1/users/" + userId + "/plans/diet";

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
        if (data == null) {
            await createDietPlan();
            return;
        }

        const dietPlan = data;
        const dietPlanContainer = document.querySelector('.diet-plan-container');
        dietPlanContainer.innerHTML = `
            <div class="diet-plan-title">
                <h2>Diet Plan - ${dietPlan.Name}</h2>
            </div>
            <div class="diet-plan-content">
                <div class="diet-plan-content-item">
                    <h3>Scope - ${dietPlan.Scope}</h3>
                </div>
                <div class="diet-plan-content-item">
                    <h3>Diet Type - ${dietPlan.DietType}</h3>
                </div>
                <div class="diet-plan-content-item">
                    <h3>Breakfast</h3>
                    <img src="${dietPlan.Breakfast.ImageUrl}" alt="Breakfast Image">
                    <div class="diet-plan-content-item">
                        <h4>Title - ${dietPlan.Breakfast.Title}</h4>
                        <h4>Ingredients</h4>
                        <ul>
                            ${dietPlan.Breakfast.Ingredients.map(ingredient => {
                                return `<li>${ingredient}</li>`;
                            }).join('')}
                        </ul>
                        <h4>Calories - ${dietPlan.Breakfast.Calories}</h4>
                    </div>
                </div>
                <div class="diet-plan-content-item">
                    <h3>Main Course</h3>
                    <img src="${dietPlan.MainCourse.ImageUrl}" alt="Main Course Image">
                    <div class="diet-plan-content-item">
                        <h4>Title - ${dietPlan.MainCourse.Title}</h4>
                        <h4>Ingredients</h4>
                        <ul>
                            ${dietPlan.MainCourse.Ingredients.map(ingredient => {
                                return `<li>${ingredient}</li>`;
                            }).join('')}
                        </ul>
                        <h4>Calories - ${dietPlan.MainCourse.Calories}</h4>
                    </div>
                </div>
                <div class="diet-plan-content-item">
                    <h3>Side Dish</h3>
                    <div class="diet-plan-content-item">
                        <h4>Title - ${dietPlan.SideDish.Title}</h4>
                        <img src="${dietPlan.SideDish.ImageUrl}" alt="Side Dish Image">
                        <h4>Ingredients</h4>
                        <ul>
                            ${dietPlan.SideDish.Ingredients.map(ingredient => {
                                return `<li>${ingredient}</li>`;
                            }).join('')}
                        </ul>
                        <h4>Calories - ${dietPlan.SideDish.Calories}</h4>
                    </div>
                </div>
                <div class="diet-plan-content-item">
                    <h3>Soup</h3>
                    <div class="diet-plan-content-item">
                        <h4>Title - ${dietPlan.Soup.Title}</h4>
                        <img src="${dietPlan.Soup.ImageUrl}" alt="Soup Image">
                        <h4>Ingredients</h4>
                        <ul>
                            ${dietPlan.Soup.Ingredients.map(ingredient => {
                                return `<li>${ingredient}</li>`;
                            }).join('')}
                        </ul>
                        <h4>Calories - ${dietPlan.Soup.Calories}</h4>
                    </div>
                </div>
                <div class="diet-plan-content-item">
                    <h3>Snack</h3>
                    <div class="diet-plan-content-item">
                        <h4>Title - ${dietPlan.Snack.Title}</h4>
                        <img src="${dietPlan.Snack.ImageUrl}" alt="Snack Image">
                        <h4>Ingredients</h4>
                        <ul>
                            ${dietPlan.Snack.Ingredients.map(ingredient => {
                                return `<li>${ingredient}</li>`;
                            }).join('')}
                        </ul>
                        <h4>Calories - ${dietPlan.Snack.Calories}</h4>
                    </div>
                </div>
                <div class="diet-plan-content-item">
                    <h3>Drink</h3>
                    <div class="diet-plan-content-item">
                        <h4>Title - ${dietPlan.Drink.Title}</h4>
                        <img src="${dietPlan.Drink.ImageUrl}" alt="Drink Image">
                        <h4>Ingredients</h4>
                        <ul>
                            ${dietPlan.Drink.Ingredients.map(ingredient => {
                                return `<li>${ingredient}</li>`;
                            }).join('')}
                        </ul>
                        <h4>Calories - ${dietPlan.Drink.Calories}</h4>
                    </div>
                </div>
            </div>
        `;
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
    "DietPlan.Create.UserNotFound": "User not found. Please try again.",
    "DietPlan.Create.PersonalDataNotFound": "Personal data not found. Please try again."
}