# Endpoints
All endpoints are currently situated at ```http://localhost:8000```, with the API suffixes ```/TipGenerator, /Wellness, /DietPlanner, /FitnessPlanner``` respectively. They all respond only to the ```POST``` method.
## TipGenerator
Recieves a json which must contain at least an 'Objective' field, it's value among ```['Lose weight', 'Gain muscular mass', 'Improve overall health', 'Improve cardiovascular health', 'Increase strength', 'Increase endurance', 'Maintain weigth']```. The response is a json with the following format:
```
{
    'tip_type': 'general', 
    'tip': 'Gradually increase the weight and reps you lift to continue challenging your muscles.'
}
```
## Wellness
Recieves a JSON with:
The user's data - will generate a response which best fits the data given, if none are given then a general response will generated
"Categories" - optional, used to select only wellness tips that pertain to a subset of the specified categories

Returns a JSON with the fields "Categories" and "Action", which contains "Title" and "Description".

```
Example Input:
{
  "Diseases": ['Osteoporosis']
}
Example Output:
{
  'Action': {
    'Description': 'Take a moment to focus on what's around you. Try to empty your mind, be present in the moment, and percieve the world around you.', 
    'Title': 'Meditation'
  }, 
  'Categories': ['Spiritual', 'Emotional', 'Mental']
}
```
```
Example Input:
{}
Example Output:
{
  'Action': {
    'Description': 'Go outside in nature or in a park. Sunlight helps raise your energy levels. You can also try meditation.', 
    'Title': 'Go for a walk in nature'
  }, 
  'Categories': ['Physical', 'Spiritual', 'Environmental']
}
```

## DietPlanner
Recieves a json of the folowing format:
Example:
```
 {
    "idClient" : "123456",      --the key that the client has in the database
    "alergis" : ["nut","milk"], --all the aliments that the client is alergit too 
    "dietType" : "vegan",       -- diet type : "vegan", "vegetarian", "dairyFree", "glutenFree"
    "goal" : "weight loss",     -- the goal that the client has in mind : "weight loss" , 
                                    "weight gain", "waight maintaining"
    "requestType" : "soup"      -- the type of data that is requested:
                                    "diet" gives a single item for the folowing: diet , soup , breakfast, drink, mainCourse, side Dish, snack,
                                -- one fof the following soup/breakfast/drink/mainCourse/sideDish/snack/soup 
                                    gives 5 or less(in case there aren't any more) of the given food type     

    }
```
Depending on the "requestType" variable it can give:
RequestType : "diet"
```
{
  "diet": {
    "id": 16,
    "name": "Intermittent fasting",
    "use": "weight loss",
    "tag": [
      "vegan",
      "vegetarian",
      "glutenFree",
      "dairyFree"
    ],
    "do": [
      "The rules for this diet are simple. A person needs to decide on and adhere to a 12-hour fasting window every day.",
      "Fasting for 16 hours a day, leaving an eating window of 8 hours, is called the 16:8 method or the Leangains diet.",
      "People following the 5:2 diet eat standard amounts of healthful food for 5 days and reduce calorie intake on the other 2 days.",
      "For some people, alternate day fasting means a complete avoidance of solid foods on fasting days, while other people allow up to 500 calories. On feeding days, people often choose to eat as much as they want.",
      "The Warrior Diet involves eating very little, usually just a few servings of raw fruit and vegetables, during a 20-hourTrusted Source fasting window, then eating one large meal at night. The eating window is usually only around 4 hours."
    ],
    "donot": [
      "eat"
    ]
  },
  "breakfast": {
    "id": 1096176,
    "title": "Mango, Persimmon Smoothie with Cranberries",
    "image": "https://spoonacular.com/recipeImages/1096176-312x231.jpg",
    "ingredients": [
      "cranberries",
      "mango",
      "persimmon"
    ],
    "kcal": 298.46
  },
  "drink": {
    "id": 1096176,
    "title": "Mango, Persimmon Smoothie with Cranberries",
    "image": "https://spoonacular.com/recipeImages/1096176-312x231.jpg",
    "ingredients": [
      "cranberries",
      "mango",
      "persimmon"
    ],
    "kcal": 298.46
  },
  "mainCourse": {
    "id": 640982,
    "title": "Cuban Black Beans & Rice",
    "image": "https://spoonacular.com/recipeImages/640982-312x231.jpg",
    "ingredients": [
      "olive oil",
      "bell pepper",
      "onion",
      "garlic",
      "tomato paste",
      "oregano",
      "ground cumin",
      "rice",
      "black beans",
      "water",
      "salt",
      "bay leaf",
      "red wine vinegar",
      "optional garnishes - such as scallion"
    ],
    "kcal": 543.94
  },
  "sideDish": {
    "id": 660108,
    "title": "Simple Kale Salad",
    "image": "https://spoonacular.com/recipeImages/660108-312x231.jpg",
    "ingredients": [
      "kale",
      "avocado",
      "orange bell pepper",
      "onion",
      "juice of lemon",
      "olive oil",
      "salt"
    ],
    "kcal": 114.55
  },
  "soup": {
    "id": 1096231,
    "title": "Slow Cooker Healthy Sweet Potato Soup with Coconut and Pistachios",
    "image": "https://spoonacular.com/recipeImages/1096231-312x231.jpg",
    "ingredients": [
      "sweet potatoes",
      "onion",
      "celery",
      "carrot sticks",
      "garlic cloves",
      "kettle & fire chicken bone broth",
      "salt",
      "ground pepper",
      "coconut milk",
      "pistachio nuts"
    ],
    "kcal": 427.28
  },
  "snack": {
    "id": 663314,
    "title": "The Perfect Butter Beans Stew",
    "image": "https://spoonacular.com/recipeImages/663314-312x231.jpg",
    "ingredients": [
      "lima beans *soaked overnight",
      "onions",
      "garlic cloves",
      "tarragon",
      "tarragon",
      "paprika",
      "bay leaves",
      "unrefined sunflower oil",
      "ground pepper",
      "sea salt"
    ],
    "kcal": 143.19
  },
  "NOP": 0
}
```
RequestType : "soup"
```
{
  "recipies": [
    {
      "id": 649886,
      "title": "Lemony Greek Lentil Soup",
      "vegetarian": true,
      "dairyFree": true,
      "glutenFree": true,
      "vegan": true,
      "healthScore": 100,
      "image": "https://spoonacular.com/recipeImages/649886-312x231.jpg",
      "ingredients": [
        "brown lentils",
        "carrot",
        "water",
        "thyme",
        "lemon juice",
        "basil",
        "thyme",
        "oregano",
        "pepper",
        "salt",
        "onion",
        "garlic",
        "olive oil",
        "canned tomatoes"
      ],
      "kcal": 368.09
    },
    {
      "id": 660109,
      "title": "Simple lentil soup",
      "vegetarian": true,
      "dairyFree": true,
      "glutenFree": true,
      "vegan": true,
      "healthScore": 39,
      "image": "https://spoonacular.com/recipeImages/660109-312x231.jpg",
      "ingredients": [
        "bay leaf",
        "garlic",
        "lentils",
        "olive oil",
        "onion",
        "salt & pepper",
        "tomato",
        "vinegar",
        "water"
      ],
      "kcal": 261.7
    },
    {
      "id": 631834,
      "title": "10-Minute Bean Stew",
      "vegetarian": true,
      "dairyFree": true,
      "glutenFree": true,
      "vegan": true,
      "healthScore": 75,
      "image": "https://spoonacular.com/recipeImages/631834-312x231.jpg",
      "ingredients": [
        "cherry tomatoes",
        "juice of lemon",
        "thyme",
        "tomato puree",
        "canned tomatoes",
        "kidney beans",
        "garlic gloves",
        "salt & pepper",
        "optional: spinach"
      ],
      "kcal": 356.73
    },
    {
      "id": 1096231,
      "title": "Slow Cooker Healthy Sweet Potato Soup with Coconut and Pistachios",
      "vegetarian": true,
      "dairyFree": true,
      "glutenFree": true,
      "vegan": true,
      "healthScore": 20,
      "image": "https://spoonacular.com/recipeImages/1096231-312x231.jpg",
      "ingredients": [
        "sweet potatoes",
        "onion",
        "celery",
        "carrot sticks",
        "garlic cloves",
        "kettle & fire chicken bone broth",
        "salt",
        "ground pepper",
        "coconut milk",
        "pistachio nuts"
      ],
      "kcal": 427.28
    },
    {
      "id": 636602,
      "title": "Butternut Squash Soup (In Half An Hour!)",
      "vegetarian": true,
      "dairyFree": true,
      "glutenFree": true,
      "vegan": true,
      "healthScore": 86,
      "image": "https://spoonacular.com/recipeImages/636602-312x231.jpg",
      "ingredients": [
        "butternut squash",
        "black-eyed peas",
        "collard greens",
        "quinoa",
        "kombu",
        "nutmeg",
        "olive oil",
        "salt and pepper",
        "vegetable broth",
        "onions"
      ],
      "kcal": 251.7
    }
  ],
  "NOP": 0
}
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
JSON Properties:
    status :  200  | 400
    pro_user: true | false
    goal: Choose one of the following goals:
        "Lose fat"
        "Increase muscle mass"
        "Body recomposition"
        "Improve cardiovascular health"
        "Increase strength"
        "Increase endurance"
        "Overall health"
    workouts_per_week: Integer between 1 and 7 (inclusive)
    equipment_available: Specify the availability of each equipment type with True | False

Each workout has a fixed number of 8 exercises.

The response is a json with a workout plan as the following:
```
{
  "status": 200,
  "workouts": {
    "workout1": [
      {
        "exercise": "Decline Push Up",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Dumbbell One Arm Upright Row",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Dips Triceps Version",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Palms Up Dumbbell Wrist Curl Over A Bench",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "One Arm Flat Bench Dumbbell Flye",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Close Grip Standing Barbell Curl",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Alternate Hammer Curl",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Barbell Hip Thrust",
        "rep_range": "1-5",
        "rest_time": "3-5 minutes",
        "sets": 3,
        "type": "Powerlifting"
      }
    ],
    "workout2": [
      {
        "exercise": "Plank",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Barbell Squat To A Bench",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "One Arm Dumbbell Preacher Curl",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Front Dumbbell Raise",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Stiff Leg Barbell Good Morning",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Decline Dumbbell Bench Press",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Bent Over Two Arm Long Bar Row",
        "rep_range": "8-12",
        "rest_time": "1-2 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Sumo Deadlift With Chains",
        "rep_range": "1-5",
        "rest_time": "3-5 minutes",
        "sets": 3,
        "type": "Powerlifting"
      }
    ]
  }
}
```
