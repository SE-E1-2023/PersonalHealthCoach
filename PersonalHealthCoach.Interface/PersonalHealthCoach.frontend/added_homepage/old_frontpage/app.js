let counter = 0;
  
const counterValue = document.getElementById('counter-value');
const incrementBtn = document.getElementById('increment-btn');
const decrementBtn = document.getElementById('decrement-btn');
const resetBtn = document.querySelector('#reset');
  

incrementBtn.addEventListener('click', () => {
    counter++;
    counterValue.innerHTML = counter;
});
  

decrementBtn.addEventListener('click', () => {
    if(counter > 0){
        counter--;
        counterValue.innerHTML = counter;
    }
    else
        counter = 0;
});
const form = document.querySelector('form');
const caloriesList = document.querySelector('#calories-list');
const totalCalories = document.querySelector('#total-calories');
let total = 0;

form.addEventListener('submit', (event) => {
  event.preventDefault();
  const food = document.querySelector('#food').value;
  const calories = document.querySelector('#calories').value;
  total += parseInt(calories);
  const item = document.createElement('li');
  item.innerHTML = `${food} - ${calories} calories`;
  caloriesList.appendChild(item);
  totalCalories.innerHTML = `Total calories: ${total}`;
  form.reset();
});
