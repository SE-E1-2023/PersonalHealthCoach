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
    "user_id": 132,
    "pro_user": true,
    "goal": "Increase endurance",
    "workouts_per_week": 3,
    "fitness_score": 3,
    "diseases": [
        1,
        240,
        401,
        302,
        502
    ],
    "equipment_available": {
        "Other": false,
        "Machine": false,
        "Barbell": false,
        "Dumbbell": true,
        "Kettlebells": false,
        "Cable": false,
        "E-Z Curl Bar": false,
        "None": false,
        "Bands": false,
        "Medicine Ball": false,
        "Exercise Ball": false,
        "Foam Roll": false,
        "Body Only": false
    }
}
```
```
JSON Properties:
    
    pro_user: true | false
    fitness_score:[1,10] - calculat de BE| sau FE la onboarding
    disease:[] -lista cu id-uri de boli -> TBD
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
```
The response is a json with a workout plan as the following ( status :  200 | 400):
```
{
  "status": 200,
  "workouts": {
    "workout1": [
      {
        "exercise": "Double Under",
        "images": [
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/5043/Male/l/5043_1.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/5043/Male/l/5043_2.jpg"
        ],
        "instructions": [
          "From a standing position, grasp a rope handle in each hand and place your feet in front of the jump rope, so the rope rests on the ground behind your heels. Hold your arms down at arm's length, tucking your elbows into your sides and bring your hands up so your forearms are parallel to the ground. This will be your starting position.",
          "Begin the exercise by swinging the rope up and over your head, utilizing wrist and shoulder rotation; jump as the rope approaches your feet.",
          "Every third rotation, jump a little higher and whip the wrists as fast as you can, causing the rope to pass under you twice before the jump is complete."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Recumbent Bike",
        "images": [
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/650/Male/l/650_1.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/650/Male/l/650_2.jpg"
        ],
        "instructions": [
          "To begin, seat yourself on the bike and adjust the seat to your height.",
          "Select the desired option from the menu. You may have to start pedaling to turn it on. You can use the manual setting, or you can select a program to use. Typically, you can enter your age and weight to estimate the amount of calories burned during exercise. The level of resistance can be changed throughout the workout. The handles can be used to monitor your heart rate to help you stay at an appropriate intensity.",
          "Recumbent bikes offer convenience, cardiovascular benefits, and have less impact than other activities. A 150 lb person will burn about 230 calories cycling at a moderate rate for 30 minutes, compared to 450 calories or more running."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Lateral Speed Step",
        "images": [
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/3173/Male/l/3173_1.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/3173/Male/l/3173_2.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/3173/Male/l/3173_3.jpg"
        ],
        "instructions": [
          "Begin in an athletic position with your knees bent, your feet shoulder-width apart, and your arms bent and at your sides.",
          "Lift your right knee high and step laterally to the right. As soon as your right foot hits the ground, lift your left knee high and step it toward your right foot.",
          "When your left foot touches the ground. Lift your right knee again and take another lateral step.",
          "Repeat this motion by taking three steps in one direction and three steps in the other.",
          "From there, jump forward 2-3 feet. You should be in the same position you started.",
          "Move quickly and stay on your toes."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Bicycling Stationary",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-52m-stationary-bike-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-52m-stationary-bike-m1-16x9.jpg"
        ],
        "instructions": [
          "To begin, seat yourself on the bike and adjust the seat to your height.",
          "Select the desired option from the menu. You may have to start pedaling to turn it on. You can use the manual setting, or you can select a program to use. Typically, you can enter your age and weight to estimate the amount of calories burned during exercise. The level of resistance can be changed throughout the workout. The handles can be used to monitor your heart rate to help you stay at an appropriate intensity."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Low Cable Triceps Extension",
        "images": [
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/157/Male/l/157_1.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/157/Male/l/157_2.jpg"
        ],
        "instructions": [
          "Select the desired weight and lay down face up on the bench of a seated row machine that has a rope attached to it. Your head should be pointing towards the attachment.",
          "Grab the outside of the rope ends with your palms facing each other (neutral grip).",
          "Position your elbows so that they are bent at a 90 degree angle and your upper arms are perpendicular (90 degree angle) to your torso. Tip: Keep the elbows in and make sure that the upper arms point to the ceiling while your forearms point towards the pulley above your head. This will be your starting position.",
          "As you breathe out, extend your lower arms until they are straight and vertical. The upper arms and elbows remain stationary throughout the movement. Only the forearms should move. Contract the triceps hard for a second.",
          "As you breathe in slowly return to the starting position.",
          "Repeat for the recommended amount of repetitions."
        ],
        "rep_range": "8-12",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Push Up Wide",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-241a-wide-push-up-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-241a-wide-push-up-m2-16x9.jpg"
        ],
        "instructions": [
          "With your hands wide apart, support your body on your toes and hands in a plank position. Your elbows should be extended and your body straight. Do not allow your hips to sag. This will be your starting position.",
          "To begin, allow the elbows to flex, lowering your chest to the floor as you inhale.",
          "Using your pectoral muscles, press your upper body back up to the starting position by extending the elbows. Exhale as you perform this step.",
          "After pausing at the contracted position, repeat the movement for the prescribed amount of repetitions."
        ],
        "rep_range": "8-12",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Jumping Knee Up Down",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-72a-jumping-knee-up-down-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-72a-jumping-knee-up-down-m3-16x9.jpg"
        ],
        "instructions": [
          "Begin in a standing position with your feet shoulder-width apart. Descend into a squat position by bending at the knees and hips. This will be your starting position.",
          "Initiate the movement by taking a step to the rear, allowing your hips and knees to flex to lower your body. Keeping your chest up and abs braced, bring your back knee all the way down to the floor, then bring your other leg back, placing your knee on the floor.",
          "Immediately bring that same leg back up, followed by your other leg so that you are in a low squat position.",
          "Extend through the hips and knees to jump up. Land back in the squat position and repeat for the desired number of repetitions."
        ],
        "rep_range": "10-14",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Plyometrics"
      },
      {
        "exercise": "Ankle On The Knee",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-238a-lying-glute-stretch-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-238a-lying-glute-stretch-m2-16x9.jpg"
        ],
        "instructions": [
          "From a lying position, bend your knees and keep your feet on the floor.",
          "Place your ankle of one foot on your opposite knee.",
          "Grasp the thigh or knee of the bottom leg and pull both of your legs into the chest. Relax your neck and shoulders. Hold for 10-20 seconds and then switch sides."
        ],
        "rep_range": "Hold for 30-45 seconds",
        "rest_time": "20-30 seconds",
        "sets": 3,
        "type": "Stretching"
      }
    ],
    "workout2": [
      {
        "exercise": "Punches",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-1a-shadow-boxing-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-1a-shadow-boxing-m2-16x9.jpg"
        ],
        "instructions": [
          "Start with your feet shoulder width apart, and place your dominant foot forward slightly. Bend your knees, and hold your hands up near the top of your chest.",
          "Starting with your dominant arm first, punch your arm forward, slightly rotating your shoulder, and twisting at the torso. The target of your punch should be straight in front of you and at shoulder height.",
          "As you retract your dominant arm by pulling your elbow back to your side, extend your other arm forward, again rotating at the shoulder and torso. Make sure to keep your abs flexed during the movement."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Burpee",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-38a-burpee-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-38a-burpee-m4-16x9.jpg"
        ],
        "instructions": [
          "Begin standing with your legs shoulder-width apart.",
          "Place your hands on the floor and kick your legs back so you end up with your stomach and thighs on the floor. Your elbows should be bent.",
          "From this position, press up like you're doing a push-up and push your hips up.",
          "Jump your feet under your hips and stand.",
          "Finish the movement by jumping in the air and bringing your hands over your head.",
          "Repeat."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Walking Treadmill",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-79m-treadmill-walking-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-79m-treadmill-walking-m1-16x9.jpg"
        ],
        "instructions": [
          "To begin, step onto the treadmill and select the desired option from the menu. Most treadmills have a manual setting, or you can select a program to run. Typically, you can enter your age and weight to estimate the amount of calories burned during exercise. Elevation can be adjusted to change the intensity of the workout.",
          "Treadmills offer convenience, cardiovascular benefits, and usually have less impact than walking outside. When walking, you should move at a moderate to fast pace, not a leisurely one. Being an activity of lower intensity, walking doesn\u00e2\u20ac\u2122t burn as many calories as some other activities, but still provides great benefit. A 150 lb person will burn about 175 calories walking 4 miles per hour for 30 minutes, compared to 450 calories running twice as fast. Maintain proper posture as you walk, and only hold onto the handles when necessary, such as when dismounting or checking your heart rate."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Bicycling Stationary",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-52m-stationary-bike-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-52m-stationary-bike-m1-16x9.jpg"
        ],
        "instructions": [
          "To begin, seat yourself on the bike and adjust the seat to your height.",
          "Select the desired option from the menu. You may have to start pedaling to turn it on. You can use the manual setting, or you can select a program to use. Typically, you can enter your age and weight to estimate the amount of calories burned during exercise. The level of resistance can be changed throughout the workout. The handles can be used to monitor your heart rate to help you stay at an appropriate intensity."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Calf Press On The Leg Press Machine",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-49m-leg-press-calf-raise-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-49m-leg-press-calf-raise-m2-16x9.jpg"
        ],
        "instructions": [
          "Using a leg press machine, sit down on the machine and place your legs on the platform directly in front of you at a medium (shoulder width) foot stance.",
          "Lower the safety bars holding the weighted platform in place and press the platform all the way up until your legs are fully extended in front of you without locking your knees. (Note: In some leg press units you can leave the safety bars on for increased safety. If your leg press unit allows for this, then this is the preferred method of performing the exercise.) Your torso and the legs should make perfect 90-degree angle. Now carefully place your toes and balls of your feet on the lower portion of the platform with the heels extending off. Toes should be facing forward, outwards or inwards as described at the beginning of the chapter. This will be your starting position.",
          "Press on the platform by raising your heels as you breathe out by extending your ankles as high as possible and flexing your calf. Ensure that the knee is kept stationary at all times. There should be no bending at any time. Hold the contracted position by a second before you start to go back down.",
          "Go back slowly to the starting position as you breathe in by lowering your heels as you bend the ankles until calves are stretched.",
          "Repeat for the recommended amount of repetitions."
        ],
        "rep_range": "8-12",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Seated Bent Over Two Arm Dumbbell Triceps Extension",
        "images": [
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/353/Male/l/353_1.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/353/Male/l/353_2.jpg"
        ],
        "instructions": [
          "Sit down at the end of a flat bench with a dumbbell in both arms using a neutral grip (palms of the hand facing you).",
          "Bend your knees slightly and bring your torso forward, by bending at the waist, while keeping the back straight until it is almost parallel to the floor. Make sure that you keep the head up.",
          "The upper arms with the dumbbells should be close to the torso and aligned with it (lifted up until they are parallel to the floor while the forearms are pointing towards the floor as the hands hold the weights). Tip: There should be a 90-degree angle between the forearms and the upper arm. This is your starting position.",
          "Keeping the upper arms stationary, use the triceps to lift the weight as you exhale until the forearms are parallel to the floor and the whole arm is extended. Like many other arm exercises, only the forearm moves.",
          "After a second contraction at the top, slowly lower the dumbbells back to the starting position as you inhale.",
          "Repeat the movement for the prescribed amount of repetitions.",
          "This exercise can be executed also one arm at a time or alternating (like alternating dumbbell curls)"
        ],
        "rep_range": "8-12",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Front Box Jump",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-75e-box-jump-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-75e-box-jump-m3-16x9.jpg"
        ],
        "instructions": [
          "Begin with a box of an appropriate height 1-2 feet in front of you. Stand with your feet should width apart. This will be your starting position.",
          "Perform a short squat in preparation for jumping, swinging your arms behind you.",
          "Rebound out of this position, extending through the hips, knees, and ankles to jump as high as possible. Swing your arms forward and up.",
          "Land on the box with the knees bent, absorbing the impact through the legs. You can jump from the box back to the ground, or preferably step down one leg at a time."
        ],
        "rep_range": "10-14",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Plyometrics"
      },
      {
        "exercise": "Overhead Stretch",
        "images": [
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/432/Male/l/432_1.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/432/Male/l/432_2.jpg"
        ],
        "instructions": [
          "Standing straight up, lace your fingers together and open your palms to the ceiling. Keep your shoulders down as you extend your arms up.",
          "To create a full torso stretch, pull your tailbone down and stabilize your torso as you do this. Stretch the muscles on both the front and the back of the torso."
        ],
        "rep_range": "Hold for 30-45 seconds",
        "rest_time": "20-30 seconds",
        "sets": 3,
        "type": "Stretching"
      }
    ],
    "workout3": [
      {
        "exercise": "High Knee Jog",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-5a-high-knees-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-5a-high-knees-m3-16x9.jpg"
        ],
        "instructions": [
          "Begin in an athletic position with your knees bent, your feet shoulder-width apart, and your arms bent and at your sides.",
          "Flex the hip and bring your right knee up toward your belly button.",
          "As the right leg comes down, bring the left knee up.",
          "Alternate lifting the knees high as you jog in place."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Walking High Knees",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/3053_1l.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/3053_2l.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/3053_3l.jpg"
        ],
        "instructions": [
          "Begin standing with your feet shoulder-width apart. Your arms should be bent and at your sides.",
          "Lift your right knee up until it reaches your waist. Your left arm should swing forward while your right arm swings back.",
          "Return your right foot to the ground as you lift your left knee. Swing your right arm forward and your left arm back.",
          "Repeat."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Trail Runningwalking",
        "images": [
          "https://www.bodybuilding.com/images/2020/april/657_1-large.jpg",
          "https://www.bodybuilding.com/images/2020/april/657_2-large.jpg",
          "https://www.bodybuilding.com/images/2020/april/657_3-large.jpg"
        ],
        "instructions": [
          "Running or hiking on trails will get the blood pumping and heart beating almost immediately. Make sure you have good shoes. While you use the muscles in your calves and buttocks to pull yourself up a hill, the knees, joints and ankles absorb the bulk of the pounding coming back down. Take smaller steps as you walk downhill, keep your knees bent to reduce the impact and slow down to avoid falling.",
          "A 150 lb person can burn over 200 calories for 30 minutes walking uphill, compared to 175 on a flat surface. If running the trail, a 150 lb person can burn well over 500 calories in 30 minutes."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Running Treadmill",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-81m-treadmill-running-m1-16x9.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/xdb-81m-treadmill-running-m2-16x9.jpg"
        ],
        "instructions": [
          "To begin, step onto the treadmill and select the desired option from the menu. Most treadmills have a manual setting, or you can select a program to run. Typically, you can enter your age and weight to estimate the amount of calories burned during exercise. Elevation can be adjusted to change the intensity of the workout.",
          "Treadmills offer convenience, cardiovascular benefits, and usually have less impact than running outside. A 150 lb person will burn over 450 calories running 8 miles per hour for 30 minutes. Maintain proper posture as you run, and only hold onto the handles when necessary, such as when dismounting or checking your heart rate."
        ],
        "rep_range": "12-18",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Cardio"
      },
      {
        "exercise": "Front Raise And Pullover",
        "images": [
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/273/Male/l/273_1.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/273/Male/l/273_2.jpg"
        ],
        "instructions": [
          "Lie on a flat bench while holding a barbell using a palms down grip that is about 15 inches apart.",
          "Place the bar on your upper thighs, extend your arms and lock them while keeping a slight bend on the elbows. This will be your starting position.",
          "Now raise the weight using a semicircular motion and keeping your arms straight as you inhale. Continue the same movement until the bar is on the other side above your head . (Tip: the bar will travel approximately 180-degrees). At this point your arms should be parallel to the floor with the palms of your hands facing the ceiling.",
          "Now return the barbell to the starting position by reversing the motion as you exhale.",
          "Repeat for the recommended amount of repetitions."
        ],
        "rep_range": "8-12",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Bodyweight Mid Row",
        "images": [
          "https://www.bodybuilding.com/images/2020/xdb/originals/2019-xdb-141c-upside-down-pull-up-m3-new.jpg",
          "https://www.bodybuilding.com/images/2020/xdb/originals/2019-xdb-141c-upside-down-pull-up-m4.jpg"
        ],
        "instructions": [
          "Begin by taking a medium to wide grip on a pull-up apparatus with your palms facing away from you. From a hanging position, tuck your knees to your chest, leaning back and getting your legs over your side of the pull-up apparatus. This will be your starting position.",
          "Beginning with your arms straight, flex the elbows and retract the shoulder blades to raise your body up until your legs contact the pull-up apparatus.",
          "After a brief pause, return to the starting position."
        ],
        "rep_range": "8-12",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Strength"
      },
      {
        "exercise": "Linear Acceleration Wall Drill",
        "images": [
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/951/Male/l/951_1.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/951/Male/l/951_2.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/951/Male/l/951_3.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/951/Male/l/951_4.jpg"
        ],
        "instructions": [
          "Lean at around 45 degrees against a wall. Your feet should be together, glutes contracted.",
          "Begin by lifting your right knee quickly, pausing, and then driving it straight down into the ground.",
          "Switch legs, raising the opposite knee, and then attacking the ground straight down.",
          "Repeat once more with your right leg, and as soon as the right foot strikes the ground hammer them out rapidly, alternating left and right as fast as you can."
        ],
        "rep_range": "10-14",
        "rest_time": "1-1.5 minutes",
        "sets": 3,
        "type": "Plyometrics"
      },
      {
        "exercise": "Shoulder Raise",
        "images": [
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/492/Male/l/492_1.jpg",
          "https://www.bodybuilding.com/exercises/exerciseImages/sequences/492/Male/l/492_2.jpg"
        ],
        "instructions": [
          "Relax your arms to your sides and raise your shoulders up toward your ears, then back down."
        ],
        "rep_range": "Hold for 30-45 seconds",
        "rest_time": "20-30 seconds",
        "sets": 3,
        "type": "Stretching"
      }
    ]
  }
}

```
or when there is a problem:
```
{
        "status": 400,
        "message": "The number of workouts per week must be greater than 0 and less than 7"
        }
        
 ```
