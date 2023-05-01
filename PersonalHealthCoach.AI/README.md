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
{
    "pro_user": True,
    "goal": "Increase muscle mass",
    "workouts_per_week":2,
    "equipment_available": {
        "Other": False,
        "Machine": False,
        "Barbell": True,
        "Dumbbell": True,
        "Kettlebells": False,
        "Cable": False,
        "E-Z Curl Bar": False,
        "None": False,
        "Bands": False,
        "Medicine Ball": False,
        "Exercise Ball": False,
        "Foam Roll": False,
        "Body Only": False
    }
}
```
Note: Goal will be a string from this list:["Lose fat","Increase muscle mass","Body recomposition", "Improve cardiovascular health","Increase strength", "Increase endurance","Overall health"]
The response is a json with an excercise list as follows:
```
{
  "workout1": [
    {
      "exercise": "Barbell Front Raise",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Stability Ball Pike With Knee Tuck",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Flexor Incline Dumbbell Curls",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Standing Palms In Dumbbell Press",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Monster Walk",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Straight Legged Hip Raise",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Side Bridge",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Sumo Deadlift With Bands",
      "rep_range": "1-5",
      "rest_time": "3-5 minutes",
      "sets": 3,
      "type": "Powerlifting"
    }
  ],
  "workout2": [
    {
      "exercise": "Palms Up Dumbbell Wrist Curl Over A Bench",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Fire Hydrant",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Seated Bent Over One Arm Dumbbell Triceps Extension",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Alternating Deltoid Raise",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Incline Hammer Curls",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Cross Crunch",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Barbell Reverse Lunge",
      "rep_range": "8-12",
      "rest_time": "1-2 minutes",
      "sets": 3,
      "type": "Strength"
    },
    {
      "exercise": "Pin Presses",
      "rep_range": "1-5",
      "rest_time": "3-5 minutes",
      "sets": 3,
      "type": "Powerlifting"
    }
  ]
}
```
