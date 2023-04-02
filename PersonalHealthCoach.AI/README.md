# Endpoints
All endpoints are currently situated at ```http://localhost:8000```, with the API suffixes ```/TipGenerator, /DietPlanner, /FitnessPlanner``` respectively. They all respond only to the ```POST``` method.
## TipGenerator
Recieves a json which must contain at least an 'Objective' field, it's value among ```['Lose weight', 'Gain muscular mass', 'Improve overall health', 'Improve cardiovascular health', 'Increase strength', 'Increase endurance', 'Maintain weigth']```. The response is a json with the following format:
```
{
    'tip_type': 'general', 
    'tip': 'Gradually increase the weight and reps you lift to continue challenging your muscles.'
}
```
## DietPlanner
Currenty recieves no arguments, and returns a string with the name of a meal.

Example:
```
"Ceviche - a Fancy, Expensive Seafood Dish Made Easy at Home"
```
## FitnessPlanner
Recieves a json with the following format:
```
{"workout_id": 1}
```
(the only currently existing workout_id is 1).

The response is a json with an excercise list as follows:
```
{
    'workout': [
        {
            'exercise': 'Push-ups', 
            'rep_range': '10-15', 
            'rest_time': '1-2 minutes', 
            'sets': 3, 
            'type': 'Strength'
        },
        {
            'exercise': 'Squats', 
            'rep_range': '10-15', 
            'rest_time': '1-2 minutes', 
            'sets': 3, 
            'type': 'Strength'
        },
        {
            'exercise': 'Lunges', 'rep_range': '10-15', 
            'rest_time': '1-2 minutes',
            'sets': 3,
            'type': 'Strength'}, 
        {
            'exercise': 'Plank', 
            'rest_time': '1-2 minutes', 
            'sets': 3, 
            'time': '30-60 seconds', 
            'type': 'Core'
        }, 
        {
            'exercise': 'Jumping Jacks', 
            'time': '5-10 minutes',
            'type': 'Cardio'
        }
    ]
}
```