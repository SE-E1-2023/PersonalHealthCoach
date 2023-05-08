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

    


