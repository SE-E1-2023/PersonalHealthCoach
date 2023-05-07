import json
import unittest
import os
import itertools
import sys
sys.path.append(os.path.dirname(os.path.dirname(__file__)))
from generate_workout import generate_workouts

abspath = os.path.dirname(__file__)
class TestGenerateWorkouts(unittest.TestCase):

    def setUp(self):
        data_path = os.path.join(os.path.dirname(os.path.dirname(__file__)), 'data')
        
        with open(os.path.join(data_path, 'main_muscle_groups.json'), 'r') as f:
            self.main_muscle_groups = json.load(f)

        with open(os.path.join(data_path, 'types_of_exercises_for_goals.json'), 'r') as f:
            self.exercise_types = json.load(f)

        with open(os.path.join(data_path, 'exercise_database.json'), 'r') as f:
            self.exercise_database = json.load(f)

    def test_goal(self):
        # goal valid
        user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 2,
            "fitness_score": 3,
            "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        self.assertEqual(response["status"], 200)

        #goal invalid
        user_data = {
            "goal": "Fly to the Moon",
            "workouts_per_week": 2,
            "fitness_score": 3,
            "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        expected_response={
            "status": 400,
            "message": "Invalid goal. Please choose from the available goals."
        }
        self.assertEqual(response,expected_response)

        #goal inexistent
        user_data = {
            "workouts_per_week": 2,
            "fitness_score": 3,
            "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        expected_response={
            "status": 400,
            "message": "Must select a goal"
        }
        self.assertEqual(response,expected_response)

        #goal care nu e string
        user_data = {
        "goal": 123,
        "workouts_per_week": 2,
        "fitness_score": 3,
        "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        self.assertEqual(response["status"], 400)

        # goal empty string
        user_data = {
            "goal": "",
            "workouts_per_week": 2,
            "fitness_score": 3,
            "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        self.assertEqual(response["status"], 400)

    def test_workouts_per_week(self):

        #1-7
        user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 2,
            "fitness_score": 3,
            "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        self.assertEqual(len(response), user_data["workouts_per_week"])

        #<1
        user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 0,
            "fitness_score": 3,
            "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        expected_response = {
        "status": 400,
        "message": "The number of workouts per week must be greater than 0 and less than 7"
        }
        self.assertEqual(response, expected_response)

       
        #>7
        user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 8,
            "fitness_score": 3,
            "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        expected_response = {
        "status": 400,
        "message": "The number of workouts per week must be greater than 0 and less than 7"
        }
        self.assertEqual(response, expected_response)

         # workouts_per_week string
        user_data = {
            "goal": "Lose fat",
            "workouts_per_week": "abc",
            "fitness_score": 3,
            "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        self.assertEqual(response["status"], 400)

        # workouts_per_week float
        user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 2.5,
            "fitness_score": 3,
            "pro_user": True,
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
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        self.assertEqual(response["status"], 400)

        # workouts_per_week lipsa
        user_data = {
        "goal": "Increase muscle mass",
        "fitness_score": 3,
        "pro_user": True,
        "equipment_available": {"Dumbbell": True}
         }
        response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
        self.assertEqual(response["status"], 400)
        self.assertEqual(response["message"], "Must select a number of workouts per week")

    def test_equipment(self):
            #equip valid
            user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 5,
            "fitness_score": 3,
            "pro_user": True,
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
            
            response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
            self.assertEqual(response["status"], 200)

            #equip gol
            user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 5,
            "fitness_score": 3,
            "pro_user": True,
            "equipment_available": {
            }
            }
            response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
            self.assertEqual(response["status"], 200)
           
            #equip field inexistent
            user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 5,
            "fitness_score": 3,
            "pro_user": True
            }
            response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
            self.assertEqual(response["status"], 200)

            # equip field ne-dictionar
            user_data = {
                "goal": "Lose fat",
                "workouts_per_week": 2,
                "fitness_score": 3,
                "pro_user": True,
                "equipment_available": 123
            }
            response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
            self.assertEqual(response["status"], 400)

    def test_insufficient_exercises_for_goal(self):
        user_data = {
            "goal": "Increase muscle mass",
            "workouts_per_week": 3,
            "fitness_score": 3,
            "pro_user": True,
            "equipment_available": {"Dumbbell": True}
        }

        # reducem baza de date la exercitii cu gantere
        exercise_database = [exercise for exercise in self.exercise_database if exercise["equipment"] == "Dumbbell"]

        # reducem exericitiile de forta lad oar 1
        exercise_database = [exercise for exercise in exercise_database if exercise["type"] == "Strength"][:1]

        response = generate_workouts(user_data, exercise_database, self.main_muscle_groups, self.exercise_types)
        self.assertEqual(response["status"], 400)
        self.assertEqual(response["message"], "Not enough exercises available for the selected goal. Please update the exercise database.")

    # baza de date de exercitii goala
    def test_no_matching_exercises(self):
        user_data = {
            "goal": "Increase muscle mass",
            "workouts_per_week": 3,
            "fitness_score": 3,
            "pro_user": True,
            "equipment_available": {"Dumbbell": True}
        }
        # golim baza de date
        exercise_database = []

        response = generate_workouts(user_data, exercise_database, self.main_muscle_groups, self.exercise_types)
        self.assertEqual(response["status"], 400)
        self.assertEqual(response["message"], "No exercises available for the selected equipment. Please update the equipment list.")

    #testare completa a tuturor variantelor de combinatii valide sa vedem daca fiecare workout va avea suficiente exercitii
    def test_complete_user_data_combinations(self):
        self.user_data={
            "user_id":0,
            "workouts_per_week":7,
            "diseases":[0,1,2],
            "pro_user":True,
            "goal":"Lose fat",
            "equipment_available":{   "Other": False,
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
                "Body Only": False},
            "fitness_score":0

        }
        self.goals = ["Lose fat", "Increase muscle mass", "Body recomposition", "Improve cardiovascular health", "Increase strength", "Increase endurance", "Overall health"]
        self.fitness_scores = [2.5, 3.5, 6.5]
        self.equipment_fields = ["Other", "Machine", "Barbell", "Dumbbell", "Kettlebells", "Cable", "E-Z Curl Bar", "None", "Bands", "Medicine Ball", "Exercise Ball", "Foam Roll", "Body Only"]
        self.equipment_combinations = list(itertools.product([True, False], repeat=len(self.equipment_fields)))
        test_count = 0
        for pro_user in [False, True]:
            for goal in self.goals:
                for fitness_score in self.fitness_scores:
                    for equipment_combination in self.equipment_combinations:
                        self.user_data["pro_user"] = pro_user
                        self.user_data["goal"] = goal
                        self.user_data["fitness_score"] = fitness_score
                        self.user_data["equipment_available"] = dict(zip(self.equipment_fields, equipment_combination))
                        response = generate_workouts(self.user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
                        try:
                            self.assertEqual(response["status"], 200)
                        except:
                            print(self.user_data)
                            print(response)
                            exit(0)
                        for workout in response["workouts"]:
                         try:
                            self.assertEqual(len(workout),8)
                         except:
                             print(self.user_data)
                             print(response)
                             exit(0)
                        test_count+=1
                        print(f"Passed test {test_count}/{2*7*3*2**13}")

#ignoram
if __name__ == '__main__':
    unittest.main() # pragma: no cover 
