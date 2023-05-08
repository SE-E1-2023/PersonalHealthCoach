import requests

url = 'http://localhost:8000/FitnessPlanner'

def test_params():
    #workout_id = input("Enter workout ID: ")
    workout_id = 1
    data = {    "Name": "Alex",
    "ID" : "38284",
    "Age": 30,
    "Height" : 182,
    "Weight" : 60,
    "Sex" : "M",
    "Objective" : "Maintain weight",
    "Disease" : "Cancer",
    "Level of activity" : "Moderately active"
    }
    try:
        response = requests.post(url, json=data)

        print("Response status code: {}\n".format(response.status_code))
        workout=response.json()
        print(workout)
        print("\n")
    except: 
        print("Workout with ID: {} is not in the database\n\n".format(workout_id))