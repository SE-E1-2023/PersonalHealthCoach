import requests

url = 'http://localhost:8000/FitnessPlanner'

while True:
    workout_id = input("Enter workout ID: ")
    data = {"workout_id": workout_id}
    try:
        response = requests.post(url, json=data)

        print("Response status code: {}\n".format(response.status_code))
        workout=response.json()["workout"]
        print(workout)
        print("\n")
    except: 
        print("Workout with ID: {} is not in the database\n\n".format(workout_id))