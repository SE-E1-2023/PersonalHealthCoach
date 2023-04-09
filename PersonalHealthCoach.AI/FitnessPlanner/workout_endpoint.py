from flask import Flask, request, jsonify
import json


from generate_workout import generate_workouts

with open(r'Data\\main_muscle_groups.json', 'r') as f:
        main_muscle_groups = json.load(f)

with open(r'Data\\types_of_exercises_for_goals.json', 'r') as f:
        exercise_types = json.load(f)

with open(r'Data\\exercise_database.json', 'r') as f:
        exercise_database = json.load(f)


app = Flask(__name__)

@app.route('/generate-workout', methods=['POST'])
def generate_workout_endpoint():
    user_data = request.json
    
    workouts = generate_workouts(user_data, exercise_database, main_muscle_groups, exercise_types)

    return jsonify(workouts)

if __name__ == '__main__':
   app.run(host='0.0.0.0', port=5000)
