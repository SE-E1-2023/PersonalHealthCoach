from flask import Flask, request, jsonify
import json
import os
import importlib
abspath = os.path.dirname(__file__)
import sys
sys.path.append(abspath)
gk = importlib.import_module("generate_workout")

from generate_workout import generate_workouts

with open(os.path.join(os.path.join(abspath,"data"), "main_muscle_groups.json"), 'r') as f:
        main_muscle_groups = json.load(f)

with open(os.path.join(os.path.join(abspath,"data"), "types_of_exercises_for_goals.json"), 'r') as f:
        exercise_types = json.load(f)

with open(os.path.join(os.path.join(abspath,"data"), "exercise_database.json"), 'r') as f:
        exercise_database = json.load(f)


app = Flask(__name__)

@app.route('/generate-workout', methods=['POST'])
def generate_workout_endpoint():    
    user_data = request.json
    workouts = gk.generate_workouts(user_data, exercise_database, main_muscle_groups, exercise_types)
    return workouts

if __name__ == '__main__':
   app.run(host='0.0.0.0', port=5000)
