import requests
import json

server_ip = '127.0.0.1'
server_port = 5000
url = f'http://{server_ip}:{server_port}/generate-workout'

"""
  "Lose fat"
  "Increase muscle mass"
  "Body recomposition"
  "Improve cardiovascular health"
  "Increase strength"
  "Increase endurance"
  "Overall health"
"""
user_data={
    "user_id": 132,
    "pro_user": False,
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
        "Other": False,
        "Machine": False,
        "Barbell": False,
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
response = requests.post(url, json=user_data)

if response.status_code == 200:
    generated_workouts = json.loads(response.text)
    print(json.dumps(generated_workouts, indent=2))
else:
    print(f'Request failed with status code {response.status_code}')
