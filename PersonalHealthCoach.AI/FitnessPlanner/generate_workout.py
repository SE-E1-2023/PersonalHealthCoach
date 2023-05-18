import json
import random
import copy

#new
def customize_exercise_parameters(fitness_score, exercise_type_info):
    modified_exercise_type_info = copy.deepcopy(exercise_type_info)

    # Adjust rep range, sets, and rest time based on the user's fitness_score
    if fitness_score < 3:
        for key, value in modified_exercise_type_info.items():
            value["sets"] = 2
            if key == "Cardio":
                value["rep_range"] = "8-12"
                value["rest_time"] = "1-1.5 minutes"
            elif key == "Strength":
                value["rep_range"] = "6-10"
                value["rest_time"] = "1-1.5 minutes"
            elif key == "Plyometrics":
                value["rep_range"] = "5-10"
                value["rest_time"] = "1-1.5 minutes"
            elif key == "Powerlifting" or key == "Olympic Weightlifting":
                value["rep_range"] = "3-5"
                value["rest_time"] = "3-4 minutes"
            elif key == "Stretching":
                value["rep_range"] = "Hold for 20-40 seconds"
                value["rest_time"] = "20-30 seconds"

    elif 3 <= fitness_score < 6:
        for key, value in modified_exercise_type_info.items():
            if key == "Cardio":
                value["rep_range"] = "12-18"
                value["rest_time"] = "1-1.5 minutes"
            elif key == "Strength":
                value["rep_range"] = "8-12"
                value["rest_time"] = "1-1.5 minutes"
            elif key == "Plyometrics":
                value["rep_range"] = "10-14"
                value["rest_time"] = "1-1.5 minutes"
            elif key == "Powerlifting" or key == "Olympic Weightlifting":
                value["rep_range"] = "2-4"
                value["rest_time"] = "3-4 minutes"
            elif key == "Stretching":
                value["rep_range"] = "Hold for 30-45 seconds"
                value["rest_time"] = "20-30 seconds"

    elif fitness_score >= 6:
        for key, value in modified_exercise_type_info.items():
            value["sets"] = 4
            if key == "Cardio":
                value["rep_range"] = "15-20"
                value["rest_time"] = "45 seconds-1 minute"
            elif key == "Strength":
                value["rep_range"] = "10-15"
                value["rest_time"] = "45 seconds-1 minute"
            elif key == "Plyometrics":
                value["rep_range"] = "12-15"
                value["rest_time"] = "45 seconds-1 minute"
            elif key == "Powerlifting" or key == "Olympic Weightlifting":
                value["rep_range"] = "1-3"
                value["rest_time"] = "4-5 minutes"
            elif key == "Stretching":
                value["rep_range"] = "Hold for 40-60 seconds"
                value["rest_time"] = "15-20 seconds"

    return modified_exercise_type_info


def filter_exercises_by_difficulty_and_pro_status(user_data, exercise_database):
    fitness_score = user_data["fitness_score"]
    pro_user = user_data["pro_user"]
    
    filtered_exercises = []
    for exercise in exercise_database:
        if fitness_score < 3 and exercise["level"] == "Beginner":
            if pro_user and exercise["score"] > 6:
                filtered_exercises.append(exercise)
            elif not pro_user:
                filtered_exercises.append(exercise)
        elif 3 <= fitness_score < 6 and exercise["level"] in ["Beginner", "Intermediate"]:
            if pro_user and exercise["score"] > 6:
                filtered_exercises.append(exercise)
            elif not pro_user:
                filtered_exercises.append(exercise)
        elif fitness_score >= 6 and exercise["level"] in ["Intermediate", "Expert"]:
            if pro_user and exercise["score"] > 6:
                filtered_exercises.append(exercise)
            elif not pro_user:
                filtered_exercises.append(exercise)
    return filtered_exercises


def generate_workouts(user_data, exercise_database, main_muscle_groups, exercise_types):
    try:
        goal = user_data["goal"]
    except:
        return {
            "status": 400,
            "message": "Must select a goal"
        }

    try:
        workouts_per_week = user_data["workouts_per_week"]
    except:
         return {
            "status": 400,
            "message": "Must select a number of workouts per week"
        }
    #sa fie de tip int
    if not isinstance(workouts_per_week, int):
        return {
            "status": 400,
            "message": "The number of workouts per week must be an integer"
        }

    try:
        equipment_available = user_data["equipment_available"]
    except:
        equipment_available={"None":True,"Body Only":True}
    try:
        exercises_for_goal = exercise_types[goal]
    except:
        return {
            "status": 400,
            "message": "Invalid goal. Please choose from the available goals."
        }
    #sa fie de tip dictionar
    if not isinstance(equipment_available, dict):
        return {
            "status": 400,
            "message": "Equipment available must be a dictionary"
        }

    if workouts_per_week <= 0 or workouts_per_week>7:
        return {
            "status": 400,
            "message": "The number of workouts per week must be greater than 0 and less than 7"
        }
 
    # cazul in care userul nu a selectat si nu avem suficiente exercitii sa-i dam
    equipment_available["None"] = True
    equipment_available["Body Only"] = True

    # eliminam exercitiile pe care userul nu le poate face
    #print(user_data)
    available_exercises = filter_exercises_by_difficulty_and_pro_status(user_data, exercise_database) # filtrare dupa scor, si nivel de dificultate mai intai
    for exercise in exercise_database:
        if exercise["equipment"] not in equipment_available:
            try:
                if equipment_available[exercise["equipment"]]:
                 available_exercises.remove(exercise)
            except:
                useless=1

    if not available_exercises:
        return {
            "status": 400,
            "message": "No exercises available for the selected equipment. Please update the equipment list."
        }
    # organizare pe tipri
    organized_exercises = {}
    for exercise in available_exercises:
        exercise_type = exercise["type"]
        if exercise_type not in organized_exercises:
            organized_exercises[exercise_type] = []
        organized_exercises[exercise_type].append(exercise)

    # distributie exercitii pentru obiectiv #new
    if user_data["fitness_score"]<3:
        exercises_for_goal = {
        "Lose weight": {"Cardio": 4, "Strength": 3, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 0},
        "Gain muscular mass": {"Cardio": 1, "Strength": 5, "Plyometrics": 0, "Powerlifting": 1, "Olympic Weightlifting": 0, "Stretching": 1},
        "Maintain weigth": {"Cardio": 3, "Strength": 4, "Plyometrics": 0, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 1},
        "Improve cardiovascular health": {"Cardio": 5, "Strength": 2, "Plyometrics": 0, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 1},
        "Increase strength": {"Cardio": 1, "Strength": 4, "Plyometrics": 0, "Powerlifting": 2, "Olympic Weightlifting": 0, "Stretching": 1},
        "Increase endurance": {"Cardio": 5, "Strength": 1, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 1},
        "Improve overall health": {"Cardio": 3, "Strength": 2, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 2}
    }
    elif user_data["fitness_score"]>=3 and user_data["fitness_score"]<=3:
         exercises_for_goal = {
        "Lose weight": {"Cardio": 3, "Strength": 4, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 0},
        "Gain muscular mass": {"Cardio": 1, "Strength": 4, "Plyometrics": 1, "Powerlifting": 1, "Olympic Weightlifting": 1, "Stretching": 0},
        "Maintain weigth": {"Cardio": 2, "Strength": 4, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 1, "Stretching": 0},
        "Improve cardiovascular health": {"Cardio": 4, "Strength": 2, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 1},
        "Increase strength": {"Cardio": 0, "Strength": 4, "Plyometrics": 1, "Powerlifting": 2, "Olympic Weightlifting": 1, "Stretching": 0},
        "Increase endurance": {"Cardio": 4, "Strength": 2, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 1},
        "Improve overall health": {"Cardio": 2, "Strength": 3, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 2}
    }
    else:
        exercises_for_goal={
    "Lose weight": {"Cardio": 3, "Strength": 3, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 1, "Stretching": 0},
    "Gain muscular mass": {"Cardio": 1, "Strength": 4, "Plyometrics": 1, "Powerlifting": 1, "Olympic Weightlifting": 1, "Stretching": 0},
    "Maintain weigth": {"Cardio": 2, "Strength": 4, "Plyometrics": 1, "Powerlifting": 1, "Olympic Weightlifting": 0, "Stretching": 0},
    "Improve cardiovascular health": {"Cardio": 4, "Strength": 2, "Plyometrics": 1, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 1},
    "Increase strength": {"Cardio": 1, "Strength": 3, "Plyometrics": 0, "Powerlifting": 2, "Olympic Weightlifting": 2, "Stretching": 0},
    "Increase endurance": {"Cardio": 4, "Strength": 1, "Plyometrics": 2, "Powerlifting": 0, "Olympic Weightlifting": 0, "Stretching": 1},
    "Improve overall health": {"Cardio": 2, "Strength": 3, "Plyometrics": 1, "Powerlifting": 1, "Olympic Weightlifting": 0, "Stretching": 1}
}


    insufficient_exercises = False

    for exercise_type, num in exercises_for_goal[goal].items():
        if len(organized_exercises.get(exercise_type, [])) < num:
            insufficient_exercises = True
            break

    if insufficient_exercises and user_data["pro_user"]:
        # reincercam cu exercitii si cu scor mai prost decat 6 daca nu sunt suficiente exercitii pentru a crea un workout
        available_exercises = filter_exercises_by_difficulty_and_pro_status(
            {**user_data, "pro_user": False}, exercise_database
        )
        organized_exercises = {}
        for exercise in available_exercises:
            exercise_type = exercise["type"]
            if exercise_type not in organized_exercises:
                organized_exercises[exercise_type] = []
            organized_exercises[exercise_type].append(exercise)

        insufficient_exercises = False
        for exercise_type, num in exercises_for_goal[goal].items():
            if len(organized_exercises.get(exercise_type, [])) < num:
                insufficient_exercises = True
                break

    if insufficient_exercises:
        return {
            "status": 400,
            "message": "Not enough exercises available for the selected goal. Please update the exercise database."
    }

    exercise_type_info = {
    "Cardio": {"rep_range": "10-15", "sets": 3, "rest_time": "1-2 minutes"},
    "Strength": {"rep_range": "8-12", "sets": 3, "rest_time": "1-2 minutes"},
    "Plyometrics": {"rep_range": "8-12", "sets": 3, "rest_time": "1-2 minutes"},
    "Powerlifting": {"rep_range": "1-5", "sets": 3, "rest_time": "3-5 minutes"},
    "Olympic Weightlifting": {"rep_range": "1-5", "sets": 3, "rest_time": "3-5 minutes"},
    "Stretching": {"rep_range": "Hold for 30-60 seconds", "sets": 3, "rest_time": "30 seconds to 1 minute"}
    }
    #new
    exercise_type_info=customize_exercise_parameters(user_data["fitness_score"],exercise_type_info)


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

    
    return {
        "status": 200,
        "workouts": workouts
    }
#ignoram
def main():
    """"""
if __name__ == '__main__':
    main() # pragma: no cover

