import json
import main
import unittest

class TestTipGenerator(unittest.TestCase):
    def test_generate_fitness_tip_based_on_objective(self):
        profile_data = {"Profile": {"Objective": "Lose weight"}}

        tips_data = {
            "Lose weight": {
                "Fitness": [
                    "Try incorporating more high-intensity interval training (HIIT) into your workouts!",
                    "Don't forget to strength train - building muscle will boost your metabolism and help you burn more calories throughout the day."
                ]
            }
        }

        generator = main.TipGenerator(main.tips_file,main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = tips_data
        generated_tip = generator.generate_fitness_tip_based_on_objective()


        self.assertIsInstance(generated_tip, dict)
        self.assertSetEqual(set(generated_tip.keys()), set(["Type", "Importance Level", "Tip"]))

        expected_tips = tips_data[profile_data["Profile"]["Objective"]]["Fitness"]
        self.assertIn(generated_tip["Tip"], expected_tips)

        self.assertIn(profile_data["Profile"]["Objective"], tips_data)

    def test_generate_diet_tip_based_on_objective(self):
        profile_data = {
            "Profile": {
                "Objective": "Gain muscle"
            }
        }

        tips_data = {
            "Lose weight": {
                "Diet": [
                    "Make sure you're eating enough calories to support your muscle-building goals.",
                    "Focus on consuming high-quality protein sources, such as lean meats, fish, and plant-based proteins.",
                    "Incorporate healthy fats into your diet, such as nuts, seeds, and avocados."
                ]
            },
            "Gain muscle": {
                "Diet": [
                    "Make sure you're consuming enough calories to support muscle growth - a caloric surplus is necessary!",
                    "Focus on consuming high-quality protein sources, such as lean meats, fish, and plant-based proteins.",
                    "Incorporate healthy fats into your diet, such as nuts, seeds, and avocados."
                ]
            }
        }

        generator = main.TipGenerator(main.tips_file,main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = tips_data
        generated_tip = generator.generate_diet_tip_based_on_objective()

        self.assertIsInstance(generated_tip, dict)
        self.assertSetEqual(set(generated_tip.keys()), set(["Type", "Importance Level", "Tip"]))

        expected_tips = tips_data[profile_data["Profile"]["Objective"]]["Diet"]
        self.assertIn(generated_tip["Tip"], expected_tips)

        self.assertIn(profile_data["Profile"]["Objective"], tips_data)

    def test_generate_general_tip_based_on_objective(self):
        profile_data = {"Profile": {"Objective": "Lose weight"}}

        tips_data = {
            "Lose weight": {
                "General": [
                    "Make sure to drink plenty of water throughout the day!",
                    "Remember that healthy weight loss is a gradual process - don't get discouraged if you don't see results immediately."
                ]
            }
        }

        generator = main.TipGenerator(main.tips_file,main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = tips_data
        generated_tip = generator.generate_general_tip_based_on_objective()

        self.assertIsInstance(generated_tip, dict)
        self.assertSetEqual(set(generated_tip.keys()), set(["Type", "Importance Level", "Tip"]))

        expected_tips = tips_data[profile_data["Profile"]["Objective"]]["General"]
        self.assertIn(generated_tip["Tip"], expected_tips)

        self.assertIn(profile_data["Profile"]["Objective"], tips_data)

    def test_verify_if_steps_are_introduced(self):
        steps_list = ["Not added", "Not added", "Not added", "Not added", "Not added", "Not added", "Not added"]
        self.assertTrue(main.verify_if_steps_are_introduced(steps_list))

        steps_list = [100, "Not added", "Not added", "Not added", "Not added", "Not added", "Not added"]
        self.assertFalse(main.verify_if_steps_are_introduced(steps_list))
    
    def test_verify_if_slept_hours_are_introduced(self):
        hours_slept_list = ["Not added", "Not added", "Not added", "Not added", "Not added", "Not added", "Not added"]
        self.assertTrue(main.verify_if_slept_hours_are_introduced(hours_slept_list))

        hours_slept_list = [8, "Not added", "Not added", "Not added", 7, "Not added", "Not added"]
        self.assertFalse(main.verify_if_slept_hours_are_introduced(hours_slept_list))

    def test_verify_if_weight_introduced(self):
        weight_list = ["Not added", "Not added", "Not added", "Not added", "Not added", "Not added", "Not added"]
        self.assertTrue(main.verify_if_weight_introduced(weight_list))

        weight_list = ["Not added", 56, "Not added", "Not added", "Not added", "Not added", "Not added"]
        self.assertFalse(main.verify_if_weight_introduced(weight_list))

    def test_verify_if_exercise_logs_introduced(self):
        exercise_logs_list = ["Not added", "Not added", "Not added", "Not added", "Not added", "Not added", "Not added"]
        self.assertTrue(main.verify_if_exercise_logs_introduced(exercise_logs_list))

        exercise_logs_list = ["Not added", "exercise", "Not added", "Not added", "Not added", "Not added", "Not added"]
        self.assertFalse(main.verify_if_exercise_logs_introduced(exercise_logs_list))

    def test_verify_if_food_logs_introduced(self):
        food_logs_list = ["Not added", "Not added", "Not added", "Not added", "Not added", "Not added", "Not added"]
        self.assertTrue(main.verify_if_food_logs_introduced(food_logs_list))

        food_logs_list = ["Not added", "Not added", "mar", "Not added", "Not added", "Not added", "Not added"]
        self.assertFalse(main.verify_if_food_logs_introduced(food_logs_list))

    def setUp(self):
        self.tips_data = {
            "Steps": {
                "Weekly Feedback": [
                    "Have you considered using a tracking app to count your daily steps? By using this type of app, we could track your progress better. The app works by using your phone's built-in sensors to count your steps automatically and then you can introduce the value in our app at the end of the day. We recommend using Google Fit, Fitbit or Apple Health. They are free to download from the app store on your phone.",
                    "You failed to meet your step goal on any day of the past week. To begin with, you could try to aim for a smaller goal and try to take a walk every day for at least half an hour.",
                    "You managed to complete your Steps Goal on {} days this week achieving an average of {} steps per day. {}"
                ]
            }
        }
    
    def test_generate_weekly_number_of_steps_feedback(self):
        profile_data = {
                "Profile": {
                    "Weight": 100
                }
            }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(KeyError):
            generator.generate_weekly_number_of_steps_feedback()

        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            },
            "Progress": [
                {"Steps": 1000},
                {"Steps": 2000},
                {"Steps": 2000},
                {"Steps": 4000},
                {"Steps": 1000},
                {"Steps": 500},
                {"Steps": 3000}
            ]
        }

        generator = main.TipGenerator(main.tips_file,main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = self.tips_data

        # Test invalid input
        invalid_profile_data = profile_data.copy()
        invalid_profile_data['Profile']['Steps Goal'] = -10000
        generator.profile_data = invalid_profile_data
        with self.assertRaises(ValueError):
            generator.generate_daily_number_of_steps_tip()

        with self.assertRaises(ValueError):
            invalid_profile_data['Profile']['Steps Goal'] = 2000000
            generator.profile_data = invalid_profile_data
            generator.generate_daily_number_of_steps_tip()


        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'] = [
                {"Date": "2022-05-01", "Steps": 4000},
                {"Date": "2022-05-03", "Steps": 6000},
                {"Date": "2022-05-04", "Steps": 3000}
            ]
            generator.profile_data = invalid_profile_data
            generator.generate_daily_number_of_steps_tip()

        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'][1]['Steps'] = -1000
            generator.profile_data = invalid_profile_data
            generator.generate_daily_number_of_steps_tip()

        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'][1]['Steps'] = 60000
            generator.profile_data = invalid_profile_data
            generator.generate_daily_number_of_steps_tip()


        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            },
            "Progress": [
                {"Steps": 1000},
                {"Steps": 2000},
                {"Steps": 2000},
                {"Steps": 4000},
                {"Steps": 1000},
                {"Steps": 500},
                {"Steps": 3000}
            ]
        }
        generator.profile_data = profile_data
        generated_tip = generator.generate_weekly_number_of_steps_feedback()

        self.assertEqual(generated_tip["Type"], "Weekly Steps")
        self.assertEqual(generated_tip["Importance Level"], "High")
        self.assertEqual(generated_tip["Tip"], "You failed to meet your step goal on any day of the past week. To begin with, you could try to aim for a smaller goal and try to take a walk every day for at least half an hour.")
        
    def test_generate_daily_number_of_steps_tip(self):
        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            },
            "Progress": [
                {"Date": "2022-05-01", "Steps": 4000},
                {"Date": "2022-05-02", "Steps": 6000},
                {"Date": "2022-05-03", "Steps": 3000},
                {"Date": "2022-05-04", "Steps": 2000},
                {"Date": "2022-05-05", "Steps": 5000},
                {"Date": "2022-05-06", "Steps": 10000},
                {"Date": "2022-05-07", "Steps": 8000}
            ]
        }

        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = self.tips_data

        # Test invalid input
        invalid_profile_data = profile_data.copy()
        invalid_profile_data['Profile']['Steps Goal'] = -10000
        generator.profile_data = invalid_profile_data
        with self.assertRaises(ValueError):
            generator.generate_daily_number_of_steps_tip()

        with self.assertRaises(ValueError):
            invalid_profile_data['Profile']['Steps Goal'] = 2000000
            generator.profile_data = invalid_profile_data
            generator.generate_daily_number_of_steps_tip()


        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'] = [
                {"Date": "2022-05-01", "Steps": 4000},
                {"Date": "2022-05-03", "Steps": 6000},
                {"Date": "2022-05-04", "Steps": 3000}
            ]
            generator.profile_data = invalid_profile_data
            generator.generate_daily_number_of_steps_tip()

        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'][1]['Steps'] = -1000
            generator.profile_data = invalid_profile_data
            generator.generate_daily_number_of_steps_tip()

        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'][1]['Steps'] = 60000
            generator.profile_data = invalid_profile_data
            generator.generate_daily_number_of_steps_tip()


        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            },
            "Progress": [
                {"Date": "2022-05-01", "Steps": 4000},
                {"Date": "2022-05-02", "Steps": 6000},
                {"Date": "2022-05-03", "Steps": 3000},
                {"Date": "2022-05-04", "Steps": 2000},
                {"Date": "2022-05-05", "Steps": 5000},
                {"Date": "2022-05-06", "Steps": 10000},
                {"Date": "2022-05-07", "Steps": 8000}
            ]
        }
        generator.profile_data = profile_data
        generated_tip = generator.generate_daily_number_of_steps_tip()

        self.assertEqual(generated_tip["Type"], "Daily Steps")
        self.assertIn(generated_tip["Importance Level"], ["None", "Low", "High"])
        self.assertIsInstance(generated_tip["Tip"], str)

    def test_generate_steps_goal_objective_age_tip(self):
        profile_data = {
            "Profile": {
                "Steps Goal": 8000,
                "Objective": "Lose weight",
                "Age": 50
            }
        }

        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = self.tips_data

        # Test invalid input
        with self.assertRaises(ValueError):
            generator.profile_data['Profile']['Steps Goal'] = -10000
            generator.generate_steps_goal_objective_age_tip()

        with self.assertRaises(ValueError):
            generator.profile_data['Profile']['Steps Goal'] = 500000
            generator.generate_steps_goal_objective_age_tip()

        with self.assertRaises(ValueError):
            generator.profile_data['Profile']['Objective'] = "Invalid Objective"
            generator.generate_steps_goal_objective_age_tip()

        with self.assertRaises(ValueError):
            generator.profile_data['Profile']['Age'] = -10
            generator.generate_steps_goal_objective_age_tip()

        with self.assertRaises(ValueError):
            generator.profile_data['Profile']['Age'] = 150
            generator.generate_steps_goal_objective_age_tip()

        # Test valid input
        generator.profile_data = {
            "Profile": {
                "Steps Goal": 8000,
                "Objective": "Lose weight",
                "Age": 50
            }
        }

        generated_tip = generator.generate_steps_goal_objective_age_tip()
        self.assertEqual(generated_tip["Type"], "Objective-Steps Goal")
        self.assertIn(generated_tip["Importance Level"], ["None", "Low", "High"])
        self.assertIsInstance(generated_tip["Tip"], str)

    def test_generate_hours_slept_last_night_tip(self):
        # Test invalid input
        profile_data = {
            "Progress": [
                {"HoursSlept": -1},
                {"HoursSlept": 25},
            ]
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data
        with open(main.tips_file, 'r', encoding='utf-8') as f:
            self.tips_data = json.load(f)
        generator.tips_data = self.tips_data

        with self.assertRaises(ValueError):
            generator.generate_hours_slept_last_night_tip()

        # Test valid input
        profile_data = {
            "Progress": [
                {"HoursSlept": 6},
                {"HoursSlept": 7},
                {"HoursSlept": 8},
                {"HoursSlept": 9},
                {"HoursSlept": 10},
            ]
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data
        with open(main.tips_file, 'r', encoding='utf-8') as f:
            self.tips_data = json.load(f)
        generator.tips_data = self.tips_data

        generated_tip = generator.generate_hours_slept_last_night_tip()
        self.assertEqual(generated_tip["Type"], "Hours Slept Last Night")
        self.assertIn(generated_tip["Importance Level"], ["None", "Low", "Medium", "High"])
        self.assertIsInstance(generated_tip["Tip"], str)

    def test_generate_general_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.tips_data = {
            "General": [
                "Take care of your mental health.",
                "Drink plenty of water.",
                "Don't skip breakfast.",
                "Practice gratitude daily.",
                "Find time for physical activity."
            ]
        }
        generated_tip = self.generator.generate_general_tip()
        self.assertEqual(generated_tip["Type"], "General")
        self.assertIn(generated_tip["Importance Level"], ["None", "Low", "Medium", "High"])
        self.assertIn(generated_tip["Tip"], self.generator.tips_data['General'])
    
    def test_generate_hours_slept_weekly_feedback(self):
        # Test invalid input
        profile_data = {
            'Progress': [
                {'HoursSlept': 8},
                {'HoursSlept': 6},
                {'HoursSlept': 4},
                {'HoursSlept': 7},
                {'HoursSlept': -1},
                {'HoursSlept': 2},
                {'HoursSlept': 11}
            ]
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.generate_hours_slept_weekly_feedback()

        profile_data = {
            'Progress': [
                {'HoursSlept': 8},
                {'HoursSlept': 6},
                {'HoursSlept': 4},
                {'HoursSlept': 7},
                {'HoursSlept': 0},
                {'HoursSlept': 2},
                {'HoursSlept': 11}
            ]
        }
        generator.profile_data = profile_data
        feedback = generator.generate_hours_slept_weekly_feedback()

        self.assertIsInstance(feedback, dict)
        self.assertListEqual(sorted(list(feedback.keys())), ['Importance Level', 'Tip', 'Type'])

        self.assertIsInstance(feedback['Importance Level'], str)

        self.assertEqual(feedback['Type'], 'Weekly Slept Hours')

        self.assertIsInstance(feedback['Tip'], str)

        expected_tip = "You had 2 days with 7-9 hours of sleep in the last week. Great job on prioritizing your sleep and maintaining healthy sleep habits! Keep up the good work and continue to prioritize your sleep each night."
        self.assertEqual(feedback['Tip'], expected_tip)

    def test_generate_weekly_weight_objective_tip(self):
        # Test invalid input
        profile_data = {
            "Profile": {"Objective": "Invalid objective"},
            "Progress": [
                {"Weight": 50, "Objective": "Invalid objective"},
                {"Weight": 60, "Objective": "Invalid objective"},
                {"Weight": 70, "Objective": "Invalid objective"},
                {"Weight": 80, "Objective": "Invalid objective"},
                {"Weight": 90, "Objective": "Invalid objective"},
                {"Weight": 100, "Objective": "Invalid objective"},
            ],
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = self.tips_data

        with self.assertRaises(ValueError):
            generator.generate_weekly_weight_objective_tip()

        # Test valid input - weight added, objective not changed
        profile_data = {
            "Profile": {"Objective": "Lose weight", "Weight": 80},
            "Progress": [
                {"Weight": 75, "Objective": "Lose weight"},
                {"Weight": 74, "Objective": "Lose weight"},
                {"Weight": 73, "Objective": "Lose weight"},
                {"Weight": 72, "Objective": "Lose weight"},
                {"Weight": 71, "Objective": "Lose weight"},
                {"Weight": 70, "Objective": "Lose weight"},
            ],
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = self.tips_data

        generated_tip = generator.generate_weekly_weight_objective_tip()
        self.assertEqual(generated_tip["Type"], "Weekly Weight Update")
        self.assertEqual(generated_tip["Importance Level"], "Low")
        self.assertIsInstance(generated_tip["Tip"], str)

        # Test valid input - weight added, objective changed
        profile_data = {
            "Profile": {"Objective": "Gain muscular mass", "Weight": 80},
            "Progress": [
                {"Weight": 75, "Objective": "Gain muscular mass"},
                {"Weight": 74, "Objective": "Gain muscular mass"},
                {"Weight": 73, "Objective": "Gain muscular mass"},
                {"Weight": 72, "Objective": "Lose weight"},
                {"Weight": 71, "Objective": "Lose weight"},
                {"Weight": 70, "Objective": "Lose weight"},
            ],
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = self.tips_data

        generated_tip = generator.generate_weekly_weight_objective_tip()
        self.assertEqual(generated_tip["Type"], "Weekly Weight Update")
        self.assertEqual(generated_tip["Importance Level"], "Low")
        self.assertIsInstance(generated_tip["Tip"], str)

    def test_weekly_food_logs_tip(self):
        profile_data = {
            'Profile': {
                'Gender': 'M',
                'Age': 35,
                'Height': 180,
                'Weight': 75,
                'Objective': 'Lose weight'
            },
            'Progress': [
                {
                    'Date': '2023-05-01',
                    'FoodLogs': [
                        {"Meal": "Breakfast",'FoodItem': 'Banana', "Quantity": "1", 'Calories': 100},
                        {"Meal": "Lunch",'FoodItem': 'Chicken breast', "Quantity": "2", 'Calories': 200},
                        {"Meal": "Dinner",'FoodItem': 'Brown rice', "Quantity": "1", 'Calories': 150}
                    ],
                    'ExerciseLogs': [
                        {'Exercise': 'Running', 'Duration': 30, 'CaloriesBurned': 300}
                    ],
                    'Objective': 'Lose weight'
                },
                {
                    'Date': '2023-05-02',
                    'FoodLogs': [
                        {"Meal": "Breakfast",'FoodItem': 'Banana', "Quantity": "1", 'Calories': 100},
                        {"Meal": "Lunch",'FoodItem': 'Chicken breast', "Quantity": "2", 'Calories': 200},
                        {"Meal": "Dinner",'FoodItem': 'Brown rice', "Quantity": "1", 'Calories': 150}
                    ],
                    'ExerciseLogs': [
                        {'Exercise': 'Cycling', 'Duration': 60, 'CaloriesBurned': 500}
                    ],
                    'Objective': 'Lose weight'
                }
            ]
        }
        generator = main.TipGenerator(main.profile_file,main.tips_file)
        generator.profile_data = profile_data
        result = generator.generate_food_logs_weekly_feedback()
        self.assertEqual(result['Type'], 'Weekly Food Logs')
        self.assertEqual(result['Importance Level'], 'High')
        self.assertIn('Our recommendation is to consume a total of 2637 calories per day. In the last week you had 0 day(s) in which you consumed enough calories, 2 day(s) in which you consumed fewer calories and 0 day(s) in which you consumed more calories. To lose weight, aim to consume 250-500 fewer calories per day than your daily calorie needs. This can result in a weight loss of 0.5-1 kilogram per week. In addition to dietary changes, aim to engage in regular physical activity for at least 30 minutes most days of the week. This can help to increase your calorie burn and promote weight loss.', result['Tip'])
        
    def test_calculate_bmr(self):
        # Test invalid height input
        profile_data = {"Profile": {"Height": 40, "Weight": 70, "Age": 30, "Sex": "M"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.calculate_bmr()

        # Test invalid weight input
        profile_data = {"Profile": {"Height": 170, "Weight": 10, "Age": 30, "Sex": "M"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.calculate_bmr()

        # Test invalid age input
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 140, "Sex": "M"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.calculate_bmr()

        # Test invalid gender input
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "Unknown"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.calculate_bmr()

        # Test valid input for male
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "M"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        self.assertAlmostEqual(generator.calculate_bmr(), 1676.83, places=1)

        # Test valid input for female
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "F"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        self.assertAlmostEqual(generator.calculate_bmr(), 1498.7300000000002, places=1)

    def test_calculate_amr(self):
        # Test invalid level of activity input
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "M", "Level of activity": "Invalid"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.calculate_amr()

        # Test valid input for sedentary activity
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "M", "Level of activity": "Sedentary"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        self.assertEqual(generator.calculate_amr(), (2041))

        # Test valid input for lightly active activity
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "M", "Level of activity": "Lightly active"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        self.assertEqual(generator.calculate_amr(), (2339))

        # Test valid input for moderately active activity
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "M", "Level of activity": "Moderately active"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        self.assertEqual(generator.calculate_amr(), (2637))

        # Test valid input for active activity
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "M", "Level of activity": "Active"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        self.assertEqual(generator.calculate_amr(), (2934))

        # Test valid input for very active activity
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "M", "Level of activity": "Very active"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        self.assertEqual(generator.calculate_amr(), (3232))

    def test_generate_food_recommandation_short_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.tips_data = {
            "Food": {
                "Short": [
                    "Add greens to your plate.",
                    "Use healthy oils when cooking.",
                    "Cut down on sugary drinks.",
                    "Choose whole grains over refined ones.",
                    "Limit processed and red meats."
                ]
            }
        }
        generated_tip = self.generator.generate_food_recommandation_short_tip()
        self.assertEqual(generated_tip["Type"], "Food Short")
        self.assertEqual(generated_tip["Importance Level"], "Low")
        self.assertIn(generated_tip["Tip"], self.generator.tips_data['Food']['Short'])

    def test_generate_food_recommandation_longer_detailed_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.tips_data = {
            "Food": {
                "Longer Detailed": [
                    "Include a variety of colors in your meals by eating different fruits and vegetables.",
                    "Choose complex carbohydrates like brown rice, whole wheat pasta, and quinoa.",
                    "Choose lean protein sources like chicken, turkey, fish, and legumes.",
                    "Avoid or limit foods that are high in added sugars, such as candy, soda, and desserts."
                ]
            }
        }
        generated_tip = self.generator.generate_food_recommandation_longer_detailed_tip()
        self.assertEqual(generated_tip["Type"], "Food Longer Detailed")
        self.assertIn(generated_tip["Importance Level"], ["None", "Low", "Medium", "High"])
        self.assertIn(generated_tip["Tip"], self.generator.tips_data['Food']['Longer Detailed'])

    def test_generate_food_recommandation_longer_try_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.tips_data = {
            "Food": {
                "Longer Try": [
                    "Try a new vegetable each week.",
                    "Experiment with a new spice or herb.",
                    "Cook a recipe from a different culture."
                ]
            }
        }
        generated_tip = self.generator.generate_food_recommandation_longer_try_tip()
        self.assertEqual(generated_tip["Type"], "Food Longer Try")
        self.assertEqual(generated_tip["Importance Level"], "Medium")
        self.assertIn(generated_tip["Tip"], self.generator.tips_data['Food']['Longer Try'])

    def test_generate_amr_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.profile_data = {
            "Profile": {
                "Objective": "Lose weight",
                "Gender": "Male",
                "Age": 30,
                "Height": 175,
                "Weight": 80,
                "Activity Level": "Moderate"
            }
        }

        expected_tip = {
            "Type": "Daily Calories",
            "Importance Level": "Medium",
            "Tip": "You burn 2637 calories during a typical day. To achieve your goal of losing weight, try to stay below your calorie needs and increase your activity level. However, make sure you are eating nutritious meals and not restricting your calories too much - eating too little or losing weight rapidly can be unhealthy and dangerous."
        }
        generated_tip = self.generator.generate_amr_tip()
        self.assertEqual(generated_tip, expected_tip)

        self.generator.profile_data['Profile']['Objective'] = "Gain muscular mass"
        expected_tip = {
            "Type": "Daily Calories",
            "Importance Level": "Medium",
            "Tip": "You burn 2637 calories during a typical day. To achieve your goal of gaining muscular mass, you should try bulking, which involves eating more calories than you need, in order to put on weight, then building muscle via resistance training. A high proportion of your extra calories should come from foods containing protein, which will give you the necessary amino acids to build muscle mass. Without protein, you will just gain fat and little muscle."
        }
        generated_tip = self.generator.generate_amr_tip()
        self.assertEqual(generated_tip, expected_tip)

        self.generator.profile_data['Profile']['Objective'] = "Improve overall health"
        expected_tip = {
            "Type": "Daily Calories",
            "Importance Level": "Medium",
            "Tip": "You burn 2637 calories during a typical day. To improve your overall health, use this information to help you figure out how many calories you should be consuming to maintain your weight. On active days, you\'ll need more calories, so it\'s okay to eat a little more than you would on an average day. But on more sedentary days, you may want to reduce your calorie intake."
        }
        generated_tip = self.generator.generate_amr_tip()
        self.assertEqual(generated_tip, expected_tip)

        self.generator.profile_data['Profile']['Objective'] = "Improve cardiovascular health"
        expected_tip = {
            "Type": "Daily Calories",
            "Importance Level": "Medium",
            "Tip": "You burn 2637 calories during a typical day. To improve your cardiovascular health, it\'s okay to eat a little more than you would on an average day when you do aerobic workouts. However, make sure you don\'t eat too much because you may get fat."
        }
        generated_tip = self.generator.generate_amr_tip()
        self.assertEqual(generated_tip, expected_tip)

        self.generator.profile_data['Profile']['Objective'] = "Increase endurance"
        expected_tip = {
            "Type": "Daily Calories",
            "Importance Level": "Medium",
            "Tip": "You burn 2637 calories during a typical day. To increase your endurance, you\'ll need to increase carbohydrate consumption up to 70% of total daily calories to support the high volume of glucose needed for that level of physical activity. Carbohydrates have 4 calories per gram. Endurance athletes should eat 8 to 10 grams of carbohydrate per kilogram of body weight per day."
        }
        generated_tip = self.generator.generate_amr_tip()
        self.assertEqual(generated_tip, expected_tip)

        self.generator.profile_data['Profile']['Objective'] = "Maintain weight"
        expected_tip = {
            "Type": "Daily Calories",
            "Importance Level": "Medium",
            "Tip": "You burn 2637 calories during a typical day. Use this information to help you figure out how many calories you should be consuming to maintain your weight. On active days, you\'ll need more calories, so it\'s okay to eat a little more than you would on an average day. But on more sedentary days, you may want to reduce your calorie intake."
        }
        generated_tip = self.generator.generate_amr_tip()
        self.assertEqual(generated_tip, expected_tip)


        with self.assertRaises(ValueError):
            self.generator.profile_data['Profile']['Objective'] = "Invalid Objective"
            self.generator.generate_amr_tip()

    def test_generate_bmi_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.profile_data = {
            "Profile": {
                "Gender": "F",
                "Age": 25,
                "Height": 165,
                "Weight": 65,
                "Objective": "Lose weight"
            }
        }
        expected_tip = {
            "Type": "BMI",
            "Importance Level": "Medium",
            "Tip": "Your BMI (Body Mass Index) is 23.9, this means that you fall into the category of Normal weight. The Normal range is 18.5-24.9. It is not necessary for you to lose weight but if you want to do that, make sure not to cross the lower bound of this range."
        }
        
        generated_tip = self.generator.generate_bmi_tip()
        self.assertEqual(generated_tip, expected_tip)

        with self.assertRaises(ValueError):
            self.generator.profile_data['Profile']['Objective'] = "Invalid Objective"
            self.generator.generate_bmi_tip()

        with self.assertRaises(ValueError):
            self.generator.profile_data['Profile']['Height'] = 10
            self.generator.generate_bmi_tip()
        
        with self.assertRaises(ValueError):
            self.generator.profile_data['Profile']['Weight'] = 10
            self.generator.generate_bmi_tip()

    def test_generate_home_exercise_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.tips_data = {
            "Exercises": {
                "Home": [
                    "Try push-ups.",
                    "Try burpies.",
                    "Try spiderman push-ups."
                ]
            }
        }
        generated_tip = self.generator.generate_home_exercise_tip()
        self.assertEqual(generated_tip["Type"], "Home Exercise")
        self.assertEqual(generated_tip["Importance Level"], "Medium")
        self.assertIn(generated_tip["Tip"], self.generator.tips_data['Exercises']['Home'])

    def test_generate_sport_exercise_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.tips_data = {
            "Exercises": {
                "Sports": [
                    "Try football.",
                    "Try basketball.",
                    "Try tennis."
                ]
            }
        }
        generated_tip = self.generator.generate_sport_exercise_tip()
        self.assertEqual(generated_tip["Type"], "Sports")
        self.assertEqual(generated_tip["Importance Level"], "Medium")
        self.assertIn(generated_tip["Tip"], self.generator.tips_data['Exercises']['Sports'])

    def test_calculate_calories_burned_by_steps(self):
        # Test invalid height input
        profile_data = {"Profile": {"Height": 40, "Weight": 70, "Age": 30, "Sex": "M"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.calculate_calories_burned_by_steps(10000)

        # Test invalid weight input
        profile_data = {"Profile": {"Height": 170, "Weight": 10, "Age": 30, "Sex": "M"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.calculate_calories_burned_by_steps(10000)

        # Test invalid age input
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 140, "Sex": "M"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.calculate_calories_burned_by_steps(10000)

        # Test invalid gender input
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "Unknown"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            generator.calculate_calories_burned_by_steps(10000)

        with self.assertRaises(ValueError):
            generator.calculate_calories_burned_by_steps(-1)

        with self.assertRaises(ValueError):
            generator.calculate_calories_burned_by_steps(51000)


        # Test valid input for male
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "M"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        self.assertAlmostEqual(generator.calculate_calories_burned_by_steps(10000), 343.588, places=1)

        # Test valid input for female
        profile_data = {"Profile": {"Height": 170, "Weight": 70, "Age": 30, "Sex": "F"}}
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        self.assertAlmostEqual(generator.calculate_calories_burned_by_steps(10000), 382.298, places=1)

    def test_calculate_calories_burned_by_steps_today(self):
        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            },
            "Progress": [
                {"Date": "2022-05-01", "Steps": 4000},
                {"Date": "2022-05-02", "Steps": 6000},
                {"Date": "2022-05-03", "Steps": 3000},
                {"Date": "2022-05-04", "Steps": 2000},
                {"Date": "2022-05-05", "Steps": 5000},
                {"Date": "2022-05-06", "Steps": 10000},
                {"Date": "2022-05-07", "Steps": 8000}
            ]
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(ValueError):
            profile_data['Progress'][-1]['Steps'] = -1000
            generator.calculate_calories_burned_by_steps_today()

        with self.assertRaises(ValueError):
            profile_data['Progress'][-1]['Steps'] = 60000
            generator.calculate_calories_burned_by_steps_today()

    def test_generate_calories_burned_by_steps_today_tip(self):
        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            }
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(KeyError):
            generator.calculate_calories_burned_by_steps_last_week()

        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            },
            "Progress": [
                {"Date": "2022-05-01", "Steps": 4000},
                {"Date": "2022-05-02", "Steps": 6000},
                {"Date": "2022-05-03", "Steps": 3000},
                {"Date": "2022-05-04", "Steps": 2000},
                {"Date": "2022-05-05", "Steps": 5000},
                {"Date": "2022-05-06", "Steps": 10000},
                {"Date": "2022-05-07", "Steps": 8000}
            ]
        }
        generator.profile_data = profile_data
        invalid_profile_data = profile_data.copy()
        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'][-1]['Steps'] = -1000
            generator.profile_data = invalid_profile_data
            generator.generate_calories_burned_by_steps_today_tip()

        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'][-1]['Steps'] = 60000
            generator.profile_data = invalid_profile_data
            generator.generate_calories_burned_by_steps_today_tip()

        invalid_profile_data['Profile']['Steps Goal'] = -10000
        generator.profile_data = invalid_profile_data
        with self.assertRaises(ValueError):
            generator.generate_calories_burned_by_steps_today_tip()

        with self.assertRaises(ValueError):
            invalid_profile_data['Profile']['Steps Goal'] = 2000000
            generator.profile_data = invalid_profile_data
            generator.generate_calories_burned_by_steps_today_tip()


        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            },
            "Progress": [
                {"Date": "2022-05-01", "Steps": 4000},
                {"Date": "2022-05-02", "Steps": 6000},
                {"Date": "2022-05-03", "Steps": 3000},
                {"Date": "2022-05-04", "Steps": 2000},
                {"Date": "2022-05-05", "Steps": 5000},
                {"Date": "2022-05-06", "Steps": 10000},
                {"Date": "2022-05-07", "Steps": 4000}
            ]
        }
        generator.profile_data = profile_data
        generated_tip = generator.generate_calories_burned_by_steps_today_tip()

        self.assertEqual(generated_tip["Type"], "Daily Steps Calories Burned")
        self.assertIn(generated_tip["Importance Level"], ["None", "Low", "Medium", "High"])
        self.assertIsInstance(generated_tip["Tip"], str)

    def test_generate_weekly_steps_calories_burned_feedback(self):
        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            }
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(KeyError):
            generator.calculate_calories_burned_by_steps_last_week()

        profile_data = {
            "Progress": [
                {"Steps": 1000},
                {"Steps": 2000},
                {"Steps": 2000},
                {"Steps": 4000},
                {"Steps": 1000},
                {"Steps": 500},
                {"Steps": 3000}
            ]
        }

        generator.profile_data = profile_data
        generator.tips_data = self.tips_data

        # Test invalid input
        invalid_profile_data = profile_data.copy()
        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'][1]['Steps'] = -1000
            generator.profile_data = invalid_profile_data
            generator.generate_weekly_steps_calories_burned_feedback()

        with self.assertRaises(ValueError):
            invalid_profile_data['Progress'][1]['Steps'] = 60000
            generator.profile_data = invalid_profile_data
            generator.generate_weekly_steps_calories_burned_feedback()


        profile_data = {
            "Progress": [
                {"Steps": 1000},
                {"Steps": 2000},
                {"Steps": 2000},
                {"Steps": 4000},
                {"Steps": 1000},
                {"Steps": 500},
                {"Steps": 3000}
            ]
        }
        generator.profile_data = profile_data
        generated_tip = generator.generate_weekly_steps_calories_burned_feedback()

        self.assertEqual(generated_tip["Type"], "Weekly Steps Calories Burned")
        self.assertEqual(generated_tip["Importance Level"], "High")
        self.assertEqual(generated_tip["Tip"], "With a total of 13500 steps you managed to burn 462 calories in the last week. As a challenge, you may try to aim for a bigger amount of burned calories in the next weeks!")

    def test_generate_level_of_activity_tip(self):
        profile_data = {
            "Profile": {
                "Objective": "Gain muscular mass",
                "Level of activity": "Moderately active"
            }
        }

        tips_data = {
            "Lose weight": {
                "Level of activity": {
                    "Sedentary": "Sedentary",
                    "Lightly active": "Lightly active",
                    "Moderately active": "Moderately active",
                    "Active": "Active",
                    "Very active": "Very active"
            }
            },
            "Gain muscular mass": {
                "Level of activity": {
                    "Sedentary": "Sedentary",
                    "Lightly active": "Lightly active",
                    "Moderately active": "Moderately active",
                    "Active": "Active",
                    "Very active": "Very active"
            }
            }
        }

        generator = main.TipGenerator(main.tips_file,main.profile_file)
        generator.profile_data = profile_data
        generator.tips_data = tips_data
        generated_tip = generator.generate_level_of_activity_tip()

        self.assertIsInstance(generated_tip, dict)
        self.assertSetEqual(set(generated_tip.keys()), set(["Type", "Importance Level", "Tip"]))

        expected_tips = tips_data[profile_data["Profile"]["Objective"]]['Level of activity']['Moderately active']
        self.assertIn(generated_tip["Tip"], expected_tips)

        self.assertIn(profile_data["Profile"]["Objective"], tips_data)

        with self.assertRaises(ValueError):
            generator.profile_data['Profile']['Objective'] = "Invalid Objective"
            generator.generate_level_of_activity_tip()

        with self.assertRaises(ValueError):
            generator.profile_data['Profile']['Level of activity'] = "Invalid Level"
            generator.generate_level_of_activity_tip()

    def test_generate_prevent_diseases_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.tips_data = {
            "Diseases": {
                "Prevent Diseases": [
                    "Get vaccinated.",
                    "Eat a healthy diet.",
                    "Consume less salt and sugar."
                ]
            }
        }
        generated_tip = self.generator.generate_prevent_diseases_tip()
        self.assertEqual(generated_tip["Type"], "Prevent Diseases")
        self.assertEqual(generated_tip["Importance Level"], "Medium")
        self.assertIn(generated_tip["Tip"], self.generator.tips_data['Diseases']['Prevent Diseases'])

    def test_generate_recover_from_illness_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.tips_data = {
            "Diseases": {
                "Recover from Ilness": [
                    "Worrying or negative thinking.",
                    "Stress and tension.",
                    "Diet, exercise and sleep."
                ]
            }
        }
        generated_tip = self.generator.generate_recover_from_illness_tip()
        self.assertEqual(generated_tip["Type"], "Recover From Illness")
        self.assertEqual(generated_tip["Importance Level"], "Medium")
        self.assertIn(generated_tip["Tip"], self.generator.tips_data['Diseases']['Recover from Ilness'])

    def test_calculate_calories_burned_by_steps_last_week(self):
        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            }
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(KeyError):
            generator.calculate_calories_burned_by_steps_last_week()


        profile_data = {
            "Profile": {
                "Steps Goal": 5000
            },
            "Progress": [
                {"Date": "2022-05-01", "Steps": 1000},
                {"Date": "2022-05-02", "Steps": 6000},
                {"Date": "2022-05-03", "Steps": 3000},
                {"Date": "2022-05-04", "Steps": 2000},
                {"Date": "2022-05-05", "Steps": 5000},
                {"Date": "2022-05-06", "Steps": 10000},
                {"Date": "2022-05-07", "Steps": 8000}
            ]
        }
        generator.profile_data = profile_data
        
        with self.assertRaises(ValueError):
            profile_data['Progress'][-1]['Steps'] = -1000
            generator.calculate_calories_burned_by_steps_last_week()

        with self.assertRaises(ValueError):
            profile_data['Progress'][-1]['Steps'] = 60000
            generator.calculate_calories_burned_by_steps_last_week()

    def test_is_within_interval(self):
        self.assertTrue(main.is_within_interval(5, 0, 10))
        self.assertTrue(main.is_within_interval(0, -5, 5))
        self.assertTrue(main.is_within_interval(-10, -20, 0))
        
        self.assertFalse(main.is_within_interval(15, 0, 10))
        self.assertFalse(main.is_within_interval(-5, 0, 10))
        self.assertFalse(main.is_within_interval(0, 10, 20))

    def test_extract_number(self):
        self.assertEqual(main.extract_number("2-10 seconds"), 10)
        self.assertEqual(main.extract_number("5-20 seconds"), 20)
        self.assertEqual(main.extract_number("1-5 seconds"), 5)
        
        self.assertEqual(main.extract_number("10 seconds"), None)
        self.assertEqual(main.extract_number("15-30 minutes"), None)
        self.assertEqual(main.extract_number("1 hour"), None)

    def test_calculate_minutes_per_workout(self):
        workout_1 = [
            {"type": "Stretching", "rep_range": "Hold for 20-40 seconds", "sets": 3},
            {"type": "Strength", "rep_range": "8-12", "sets": 4},
            {"type": "Cardio", "rep_range": "30-40", "sets": 2}
        ]
        self.assertEqual(main.calculate_minutes_per_workout(workout_1), 8.266666666666666)
        
        workout_2 = [
            {"type": "Stretching", "rep_range": "Hold for 25-45 seconds", "sets": 2},
            {"type": "Strength", "rep_range": "10-15", "sets": 3}
        ]
        self.assertEqual(main.calculate_minutes_per_workout(workout_2), 5.0)
        
        workout_3 = [
            {"type": "Strength", "rep_range": "6-10", "sets": 5},
            {"type": "Stretching", "rep_range": "Hold for 50-60 seconds", "sets": 2}
        ]
        self.assertEqual(main.calculate_minutes_per_workout(workout_3), 5.666666666666666)
        
    def test_convert_minutes_to_hours_minutes(self):
        self.assertEqual(main.convert_minutes_to_hours_minutes(75), "1 hour and 15 minutes")
        self.assertEqual(main.convert_minutes_to_hours_minutes(120), "2 hours")
        self.assertEqual(main.convert_minutes_to_hours_minutes(30), "30 minutes")
        
        self.assertEqual(main.convert_minutes_to_hours_minutes(60), "1 hour")
        self.assertEqual(main.convert_minutes_to_hours_minutes(45), "45 minutes")
        self.assertEqual(main.convert_minutes_to_hours_minutes(90), "1 hour and 30 minutes")
        
    def test_calculate_calories_per_workout(self):
        self.assertAlmostEqual(main.calculate_calories_per_workout(30, 5.0, 70), 183.75, places=2)
        self.assertAlmostEqual(main.calculate_calories_per_workout(60, 6.0, 65), 409.5, places=2)
        self.assertAlmostEqual(main.calculate_calories_per_workout(45, 4.5, 80), 283.5, places=2)
        
        self.assertAlmostEqual(main.calculate_calories_per_workout(90, 7.0, 75), 826.875, places=2)
        self.assertAlmostEqual(main.calculate_calories_per_workout(20, 3.0, 60), 63.0, places=2)
        self.assertAlmostEqual(main.calculate_calories_per_workout(15, 4.0, 70), 73.5, places=2)
        
    def test_generate_daily_exerciselog_tip(self):
        profile_data = {
            "Profile": {
                "Weight": 100
            }
        }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(KeyError):
            generator.generate_daily_exerciselog_tip()

        profile_data_1 = {
            "Profile": {"Weight": 70},
            "Progress": [
                {
                    "ExerciseLogs": [
                        {"type": "Stretching", "rep_range": "10-15 seconds", "sets": 3},
                        {"type": "Strength", "rep_range": "8-12", "sets": 4},
                        {"type": "Cardio", "rep_range": "30-40", "sets": 2}
                    ]
                }
            ]
        }
        generator.profile_data = profile_data_1
        tip_1 = generator.generate_daily_exerciselog_tip()
        expected_tip_1 = {
            "Type": "Daily Workout Feedback",
            "Importance Level": "Medium",
            "Tip": "You managed to burn 118.6 calories today during your workout that lasted 7 minutes. Good job! Make sure you exercise 3-5 times a week to reach your goal."
        }
        self.assertEqual(tip_1, expected_tip_1)
        
        profile_data_2 = {
            "Profile": {"Weight": 85},
            "Progress": [{},{}]
        }
        generator.profile_data = profile_data_2
        tip_2 = generator.generate_daily_exerciselog_tip()
        expected_tip_2 = {
            "Type": "Daily Workout Feedback",
            "Importance Level": "Medium",
            "Tip": "Even if you didn't do your workout today, it's never too late to get moving. Check your generated workout, do it and mark it as done. Set realistic goals, stay motivated, and remember that consistency is key. Your health and well-being will thank you. If you have already met your exercise goal this week, then you can take a day off because breaks are also important in your process of reaching your goal."
        }
        self.assertEqual(tip_2, expected_tip_2)
        
        profile_data_3 = {
            "Profile": {"Weight": 20},
            "Progress": [
                {
                    "ExerciseLogs": [
                        {"type": "Stretching", "rep_range": "Hold for 20-40 seconds", "sets": 2},
                        {"type": "Strength", "rep_range": "10-15", "sets": 3}
                    ]
                }
            ]
        }
        generator.profile_data = profile_data_3
        with self.assertRaises(ValueError):
            generator.generate_daily_exerciselog_tip()
        
        profile_data_4 = {
            "Profile": {"Weight": 210},
            "Progress": [
                {
                    "ExerciseLogs": [
                        {"type": "Strength", "rep_range": "6-10", "sets": 5},
                        {"type": "Stretching", "rep_range": "Hold for 15-30 seconds", "sets": 2}
                    ]
                }
            ]
        }
        generator.profile_data = profile_data_4
        with self.assertRaises(ValueError):
            generator.generate_daily_exerciselog_tip()

    def test_generate_weekly_exerciselogs_feedback(self):
        profile_data = {
                "Profile": {
                    "Weight": 100
                }
            }
        generator = main.TipGenerator(main.tips_file, main.profile_file)
        generator.profile_data = profile_data

        with self.assertRaises(KeyError):
            generator.generate_daily_exerciselog_tip()

        profile_data = {
                "Profile": {
                    "Weight": 150,
                    "Objective": "Lose weight"
                },
                "Progress": [
                    {
                        "ExerciseLogs": [
                            {
                                "exercise": "Pin Presses",
                                "rep_range": "3-5",
                                "rest_time": "3-4 minutes",
                                "sets": 2,
                                "type": "Powerlifting"
                            },
                            {
                                "exercise": "Standing Olympic Plate Hand Squeeze",
                                "rep_range": "6-10",
                                "rest_time": "1-1.5 minutes",
                                "sets": 2,
                                "type": "Strength"
                            }
                        ],
                        "Objective": "Lose weight"
                    },
                    {
                        "ExerciseLogs": [
                            {
                                "exercise": "Standing Olympic Plate Hand Squeeze",
                                "rep_range": "6-10",
                                "rest_time": "1-1.5 minutes",
                                "sets": 2,
                                "type": "Strength"
                            }
                        ],
                        "Objective": "Lose weight"
                    },
                    {
                        "Objective": "Lose weight"
                    }
                ]
            }

        # Create an instance of the class and set the profile_data
        generator.profile_data = profile_data

        # Call the function being tested
        result = generator.generate_weekly_exerciselogs_feedback()
        print(result)

        # Assert the expected tip based on the given profile data
        expected_tip = {
            "Type": "Weekly Exercise Logs",
            "Importance Level": "High",
            "Tip": "You managed to burn 339.0 calories in the last week thanks to the 6 days with exercises that lasted a total of 9 minutes. While your dedication to exercise is commendable, be mindful of overexertion. Allow for proper rest and recovery to optimize results. Aim for a balanced approach with 3-5 days of exercise per week, incorporating cardio and strength training. Listen to your body's signals and adjust accordingly. Embrace self-care and find a sustainable balance. Keep up the great work on your weight loss journey!"
        }
        self.assertEqual(result, expected_tip)

    def test_generate_recover_from_injury_tip(self):
        self.generator = main.TipGenerator(main.tips_file, main.profile_file)
        self.generator.tips_data = {
            "Diseases": {
                "Recover from Injury": [
                    "RICE.",
                    "Eat a healthy diet.",
                    "GET LOOSE AND STRETCH."
                ]
            }
        }
        generated_tip = self.generator.generate_recover_from_injury_tip()
        self.assertEqual(generated_tip["Type"], "Recover from Injury")
        self.assertEqual(generated_tip["Importance Level"], "Medium")
        self.assertIn(generated_tip["Tip"], self.generator.tips_data['Diseases']['Recover from Injury'])

