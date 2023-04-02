import requests

url = 'http://localhost:8000/TipGenerator'

while True:
    workout_id = input("Enter workout ID: ")
    data = {
    "Name": "George",
    "ID" : "38284",
    "Age": 21,
    "Height" : 182,
    "Weight" : 63,
    "Sex" : "M",
    "Objective" : "Gain muscular mass", "Diseases":[]}
    try:
        response = requests.post(url, json=data)
        """
        print("Response status code: {}\n".format(response.status_code))
        workout=response.json()["workout"]"""
        print(response.json())
        print("\n")
    except: 
        print("Workout with ID: {} is not in the database\n\n".format(workout_id))