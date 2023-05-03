import json
import unittest
import os
import importlib
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
        #Valid Goal
        user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 2,
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

        #Invalid Goal
        user_data = {
            "goal": "Fly to the Moon",
            "workouts_per_week": 2,
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

        #No Goal
        user_data = {
            "workouts_per_week": 2,
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
    def test_workouts_per_week(self):

        #1-7
        user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 2,
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

    def test_equipment(self):
            #Valid Equip
            user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 5,
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

            #Missing Equip
            #Valid Equip
            user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 5,
            "equipment_available": {
            }
            }
            response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
            self.assertEqual(response["status"], 200)
            #No Equip Field
            #Valid Equip
            user_data = {
            "goal": "Lose fat",
            "workouts_per_week": 5,
            }
            response = generate_workouts(user_data, self.exercise_database, self.main_muscle_groups, self.exercise_types)
            self.assertEqual(response["status"], 200)

if __name__ == '__main__':
    unittest.main()
