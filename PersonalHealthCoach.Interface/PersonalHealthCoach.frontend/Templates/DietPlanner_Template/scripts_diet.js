
function showdiet_0(i, containerId)
{
 
  fetch('data.json')
  .then(response => response.json())
  .then(data => {
    const dishContainer = document.getElementById(containerId);
    
    const dish = data.dishes[i];
    
    const dishDiv = document.createElement('div');
    dishDiv.classList.add('dish');
    
    const title = document.createElement('h2');
    title.textContent = dish.title;
    dishDiv.appendChild(title);
    
    const ingredientsList = document.createElement('ul');
    dish.ingredients.forEach(ingredient => {
      const li = document.createElement('li');
      li.textContent = ingredient;
      ingredientsList.appendChild(li);
    });
    dishDiv.appendChild(ingredientsList);
    
    const calories = document.createElement('p');
    calories.textContent = 'Calories: ' + dish.calories;
    dishDiv.appendChild(calories);
    
    dishContainer.appendChild(dishDiv);
  });
}

