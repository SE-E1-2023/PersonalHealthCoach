import json
import random

def generate_workouts(user_data, exercise_database, main_muscle_groups, exercise_types):
    goal = user_data["goal"]
    workouts_per_week = user_data["workouts_per_week"]
    equipment_available = user_data["equipment_available"]
    exercises_for_goal = exercise_types[goal]

    # cazul in care useruul nu a selectat si nu avem suficiente exercitii sa-i dam
    equipment_available["None"] = True
    equipment_available["Body Only"] = True

    # eliminam exercitiile pe care useru-ul nu le poate face
    available_exercises = [e for e in exercise_database if equipment_available[e["equipment"]]]

    # organizare pe tipri
    organized_exercises = {}
    for exercise in available_exercises:
        exercise_type = exercise["type"]
        if exercise_type not in organized_exercises:
            organized_exercises[exercise_type] = []
        organized_exercises[exercise_type].append(exercise)

    # distributie exercitii pentru obiectiv
    exercises_for_goal = {
        "Lose fat": {"Cardio": 2, "Strength": 5, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 0},
        "Increase muscle mass": {"Cardio": 0, "Strength": 7, "Plyometrics": 0, "Powerlifting": 1, "Olympic Weightlifting": 0, "Stretching": 0},
        "Body recomposition": {"Cardio": 1, "Strength": 5, "Plyometrics": 0, "Powerlifting": 1, "Olympic Weightlifting": 1, "Stretching": 0},
        "Improve cardiovascular health": {"Cardio": 5, "Strength": 2, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 0},
        "Increase strength": {"Cardio": 0, "Strength": 7, "Plyometrics": 0, "Powerlifting": 1, "Olympic Weightlifting": 0, "Stretching": 0},
        "Increase endurance": {"Cardio": 5, "Strength": 1, "Plyometrics": 2, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 0},
        "Overall health": {"Cardio": 2, "Strength": 3, "Plyometrics": 2, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 1}
    }
    exercise_type_info = {
    "Cardio": {"rep_range": "10-15", "sets": 3, "rest_time": "1-2 minutes"},
    "Strength": {"rep_range": "8-12", "sets": 3, "rest_time": "1-2 minutes"},
    "Plyometrics": {"rep_range": "8-12", "sets": 3, "rest_time": "1-2 minutes"},
    "Powerlifting": {"rep_range": "1-5", "sets": 3, "rest_time": "3-5 minutes"},
    "Olympic Weightlifting": {"rep_range": "1-5", "sets": 3, "rest_time": "3-5 minutes"},
    "Stretching": {"rep_range": "Hold for 30-60 seconds", "sets": 3, "rest_time": "30 seconds to 1 minute"}
    }

    
    exercise_goal = exercises_for_goal[goal]

    # initializare obiecte de tip workout
    workouts = {}
    for i in range(workouts_per_week):
        workout_key = f"workout{i + 1}"
        workouts[workout_key] = []

        selected_exercises = []

        for exercise_type, num in exercise_goal.items():
            type_exercises = organized_exercises.get(exercise_type, [])
            selected_exercises.extend(random.sample(type_exercises, num))

        # adaugam exercitiile
        for exercise in selected_exercises:
            exercise_info = {
                "exercise": exercise["name"],
                "type": exercise["type"],
            }
            exercise_info.update(exercise_type_info[exercise["type"]])
            workouts[workout_key].append(exercise_info)

    return workouts

def main():
    """"""
if __name__ == '__main__':
    main()

