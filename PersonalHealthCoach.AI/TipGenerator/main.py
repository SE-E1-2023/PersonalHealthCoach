import json
import random

import os
abspath = os.path.dirname(__file__)
import sys
sys.path.append(abspath)
from unittest.mock import MagicMock

def verify_if_steps_are_introduced(steps_list):
        for steps_per_day in steps_list:
            if steps_per_day != "Not added":
                return False;
        return True;

def verify_if_slept_hours_are_introduced(hours_slept_list):
        for hours_slept_per_day in hours_slept_list:
            if hours_slept_per_day != "Not added":
                return False;
        return True;

def verify_if_weight_introduced(weight_list):
        for weight in weight_list:
            if weight != "Not added":
                return False;
        return True;
        
def verify_if_exercise_logs_introduced(exercise_logs_list):
        for exercise_log in exercise_logs_list:
            if exercise_log != "Not added":
                return False;
        return True;

def verify_if_food_logs_introduced(food_logs_list):
        for food_log in food_logs_list:
            if food_log != "Not added":
                return False;
        return True;

class TipGenerator:
    def __init__(self, tips_file, profile_json):
        with open(tips_file, 'r', encoding='utf-8') as f:
            self.tips_data = json.load(f)
        
        with open(profile_file) as f:
            self.profile_data = json.load(f)
        # self.profile_data = profile_json
    
    def generate_tips(self):
        tips = []
        tips.append(generator.generate_general_tip())
        tips.append(generator.generate_fitness_tip_based_on_objective())
        tips.append(generator.generate_diet_tip_based_on_objective())
        tips.append(generator.generate_general_tip_based_on_objective())
        tips.append(generator.generate_amr_tip())
        tips.append(generator.generate_bmi_tip())
        tips.append(generator.generate_level_of_activity_tip())
        tips.append(generator.generate_weekly_number_of_steps_feedback())
        tips.append(generator.generate_daily_number_of_steps_tip())
        tips.append(generator.generate_steps_goal_objective_age_tip())
        tips.append(generator.generate_hours_slept_last_night_tip())
        tips.append(generator.generate_hours_slept_weekly_feedback())
        tips.append(generator.generate_weekly_weight_objective_tip())
        tips.append(generator.generate_food_logs_weekly_feedback())

        with open(f"{abspath}/generated_tips.json", 'w') as tips_file:
            json.dump(tips, tips_file, indent=4)

        tip = random.choice(tips)
        return tip

    def calculate_bmr(self):
        height = self.profile_data['Profile']['Height']
        if height <= 50 or height > 300:
            raise ValueError("Height should be between 50 and 300.")
        weight = self.profile_data['Profile']['Weight']
        if weight <= 30 or weight > 200:
            raise ValueError("Weight should be between 30 and 200.")
        age = self.profile_data['Profile']['Age']
        if age <= 0 or age > 130:
            raise ValueError("Age should be between 1 and 130.")
        gender = self.profile_data['Profile']['Sex']
        gender_list = ["M", "F"]
        if gender not in gender_list:
            raise ValueError("Gender not found in list.")

        if gender == 'F':
            bmr = 655.1 + (9.563 * weight) + (1.850 * height) - (4.676 * age)
        else:
            bmr = 66.47 + (13.75 *weight) + (5.003 * height) - (6.755 * age)
        return bmr
    
    def calculate_amr(self):
        bmr = generator.calculate_bmr()
        level_of_activity = self.profile_data['Profile']['Level of activity']

        if level_of_activity == 'Sedentary':
            amr = bmr * 1.2
        elif level_of_activity == 'Lightly active':
            amr = bmr * 1.375
        elif level_of_activity == 'Moderately active':
            amr = bmr * 1.55
        elif level_of_activity == 'Active':
            amr = bmr * 1.725
        elif level_of_activity == 'Very active':
            amr = bmr * 1.9
        return int(amr)
    
    def generate_amr_tip(self):
        objective = self.profile_data['Profile']['Objective']
        amr = generator.calculate_amr()
        tip = 'You burn ' + str(amr) + ' calories during a typical day.'

        if objective == 'Lose weight':
            tip += ' To achieve your goal of losing weight, try to stay below your calorie needs and increase your activity level. However, make sure you are eating nutritious meals and not restricting your calories too much - eating too little or losing weight rapidly can be unhealthy and dangerous.'
        elif objective == 'Gain muscular mass':
            tip += ' To achieve your goal of gaining muscular mass, you should try bulking, which involves eating more calories than you need, in order to put on weight, then building muscle via resistance training. A high proportion of your extra calories should come from foods containing protein, which will give you the necessary amino acids to build muscle mass. Without protein, you will just gain fat and little muscle.'
        elif objective == 'Improve overall health':
            tip += ' To improve your overall health, use this information to help you figure out how many calories you should be consuming to maintain your weight. On active days, you\'ll need more calories, so it\'s okay to eat a little more than you would on an average day. But on more sedentary days, you may want to reduce your calorie intake.'
        elif objective == 'Improve cardiovascular health':
            tip += ' To improve your cardiovascular health, it\'s okay to eat a little more than you would on an average day when you do aerobic workouts. However, make sure you don\'t eat too much because you may get fat.'
        elif objective == 'Increase endurance':
            tip += ' To increase your endurance, you\'ll need to increase carbohydrate consumption up to 70% of total daily calories to support the high volume of glucose needed for that level of physical activity. Carbohydrates have 4 calories per gram. Endurance athletes should eat 8 to 10 grams of carbohydrate per kilogram of body weight per day.'
        elif objective == 'Maintain weight':
            tip += ' Use this information to help you figure out how many calories you should be consuming to maintain your weight. On active days, you\'ll need more calories, so it\'s okay to eat a little more than you would on an average day. But on more sedentary days, you may want to reduce your calorie intake.'

        generated_tip = {
                            "Type": "Daily Calories",
                            "Importance Level": "Medium",
                            "Tip": tip
                        }
        return generated_tip

        # tip_dict = {'tip_type': 'Daily Calories', 'tip': tip}
        # return tip_dict

    def generate_bmi_tip(self):
        height = self.profile_data['Profile']['Height']
        weight = self.profile_data['Profile']['Weight']
        objective = self.profile_data['Profile']['Objective']

        height = height / 100
        bmi = weight / (height * height)
        bmi = round(bmi, 1)

        with open(f"{abspath}/bmi_categories.json") as f:
            bmi_categories = json.load(f)['categories']
        
        for category in bmi_categories:
            if category['upper_bound'] is None and bmi >= category['lower_bound']:
                weight_category = category['name']
            elif bmi >= category['lower_bound'] and bmi <= category['upper_bound']:
                weight_category = category['name']

        normal_weight_lower_bound = 18.5
        normal_weight_upper_bound = 24.9

        if bmi < normal_weight_lower_bound:
            target_weight = (normal_weight_lower_bound * (height * height))
            diff_weight = round(target_weight - weight, 1)
        elif bmi > normal_weight_upper_bound:
            target_weight = (normal_weight_upper_bound * (height * height))
            diff_weight = round(target_weight - weight, 1)
        else:
            diff_weight = 0

        tip = "Your BMI (Body Mass Index) is " + str(bmi) + ", this means that you fall into the category of " + weight_category + ". The Normal range is " + str(normal_weight_lower_bound) + "-" + str(normal_weight_upper_bound) + ". "

        if diff_weight == 0:
            if objective == 'Lose weight':
                tip += "It is not necessary for you to lose weight but if you want to do that, make sure not to cross the lower bound of this range."
            elif objective == 'Gain muscular mass':
                tip += "Make sure you keep it in this range while gaining muscle mass."
            elif objective == 'Improve overall health':
                tip += "Try to keep it in this range."
            elif objective == 'Improve cardiovascular health':
                tip += "Try to keep it in this range."
            elif objective == 'Increase endurance':
                tip += "Make sure you keep it in this range while increasing yout endurance."
            elif objective == 'Maintain weigth':
                tip += "It's very good that you want to maintain your weight."
            tip_type = "Normal BMI"
        elif 0 < diff_weight < 5:
            if objective == 'Lose weight':
                tip += "You are not far from this range, but your objective shouldn't be to lose weight, but to gain a little more instead."
            elif objective == 'Gain muscular mass':
                tip += "You are not far from this range. It's good that you want to gain muscular mass so make sure you'll be getting in the Normal range."
            elif objective == 'Improve overall health':
                tip += "You are not far from this range so, to improve your overall health, you should gain a little weight in order to get into the Normal range."
            elif objective == 'Improve cardiovascular health':
                tip += "You are not far from this range, but try to get gain some weight to get yourself into the Normal range."
            elif objective == 'Increase endurance':
                tip += "You are not far from this range but if you want to increase your endurance, you should get in the Normal range first."
            elif objective == 'Maintain weigth':
                tip += "You are not far from this range, but for now you shouldn't aim to maintain your weight and gain a little in order to get into the Normal range."
            tip_type = "Almost Normal BMI"
        elif diff_weight > 5:
            if objective == 'Lose weight':
                tip += "Your objective shouldn't be to lose weight, but to gain a little more instead to get into the Normal range."
            elif objective == 'Gain muscular mass':
                tip += "It's good that you want to gain muscular mass because you need to gain overall weight to get in the Normal range."
            elif objective == 'Improve overall health':
                tip += "To improve your overall health, firstly, you should gain some weight to get into the Normal range."
            elif objective == 'Improve cardiovascular health':
                tip += "You should gain some weight to get yourself into the Normal range to protect your health."
            elif objective == 'Increase endurance':
                tip += "If you want to increase your endurance, you should get in the Normal range by gaining some weight first."
            elif objective == 'Maintain weigth':
                tip += "You shouldn't aim to maintain your weight at the moment. Try to gain some weight in order to get into the Normal range."
            tip_type = "Low BMI"
        elif -5 < diff_weight < 0:
            if objective == 'Lose weight':
                tip += "You are not far from this range so it shouldn't be hard for you to achieve your goal of losing a little weight."
            elif objective == 'Gain muscular mass':
                tip += "You are not far from this range. It's good that you want to gain muscular mass, but you can also try some cardio exercises to get in the Normal range."
            elif objective == 'Improve overall health':
                tip += "You are not far from this range so, to improve your overall health, you should lose a little weight in order to get into the Normal range."
            elif objective == 'Improve cardiovascular health':
                tip += "You are not far from this range, but try to lose some weight to get yourself into the Normal area."
            elif objective == 'Increase endurance':
                tip += "You are not far from this range but if you want to increase your endurance, you should get in the Normal range first."
            elif objective == 'Maintain weigth':
                tip += "You are not far from this range, but for now you shouldn't aim to maintain your weight and lose a little in order to get into the Normal range."
            tip_type = "Almost Normal BMI"
        elif diff_weight < -5:
            if objective == 'Lose weight':
                tip += "It's good that you want to lose weight so be careful what you eat and do sports as often as possible to get into the Normal range."
            elif objective == 'Gain muscular mass':
                tip += "It's good that you want to gain muscular mass, but your focus should be to lose some weight first to get in the Normal range and then try to build some muscles."
            elif objective == 'Improve overall health':
                tip += "To improve your overall health, you should lose some weight in order to get into the Normal range."
            elif objective == 'Improve cardiovascular health':
                tip += "Try to lose weight to get yourself into the Normal area and improve your cardiovascular system."
            elif objective == 'Increase endurance':
                tip += "To increase and also lose weight at the same time you may try jogging."
            elif objective == 'Maintain weigth':
                tip += "You shouldn't aim to maintain your weight, but make an effort to lose some in order to get into the Normal range."
            tip_type = "High BMI"

        generated_tip = {
                            "Type": "BMI",
                            "Importance Level": "Medium",
                            "Tip": tip
                        }
        return generated_tip
    
        # tip_dict = {'tip_type': tip_type, 'tip': tip}
        # return tip_dict

    def generate_level_of_activity_tip(self):
        level_of_activity = self.profile_data['Profile']['Level of activity']
        objective = self.profile_data['Profile']['Objective']

        tip = self.tips_data[objective]['Level of activity'][level_of_activity]

        generated_tip = {
                            "Type": "Level of Activity",
                            "Importance Level": "High",
                            "Tip": tip
                        }
        return generated_tip


    def generate_fitness_tip_based_on_objective(self):
        objective = self.profile_data['Profile']['Objective']
        if objective not in self.tips_data or 'Fitness' not in self.tips_data[objective]:
            print(f"No fitness tip found for objective: {objective}")
            return
        fitness_tips = self.tips_data[objective]['Fitness']
        tip = random.choice(fitness_tips)
        generated_tip = {
                            "Type": "Fitness",
                            "Importance Level": "Medium",
                            "Tip": tip
                        }
        return generated_tip

    def generate_diet_tip_based_on_objective(self):
        objective = self.profile_data['Profile']['Objective']
        diet_tips = self.tips_data[objective]['Diet']

        tip = random.choice(diet_tips)
        generated_tip = {
                            "Type": "Diet",
                            "Importance Level": "Medium",
                            "Tip": tip
                        }
        return generated_tip

    def generate_general_tip_based_on_objective(self):
        objective = self.profile_data['Profile']['Objective']
        general_tips = self.tips_data[objective]['General']

        tip = random.choice(general_tips)
        generated_tip = {
                            "Type": "General",
                            "Importance Level": "Medium",
                            "Tip": tip
                        }
        return generated_tip

    def generate_weekly_number_of_steps_feedback(self):
        steps_goal = self.profile_data['Profile']['Steps Goal']
        if steps_goal <= 0 or steps_goal > 100000:
            raise ValueError("Steps Goal should be between 1 and 100000.")
            
        progress = self.profile_data['Progress']
        for i in range(len(progress)):
            if progress[i]['Steps'] < 0 or progress[i]['Steps'] > 50000:
                raise ValueError("Steps should be between 0 and 50000.")
        
        steps_list = []
        for day in self.profile_data['Progress']:
            steps = day['Steps']
            steps_list.append(steps)
        
        tip = ""
        if(verify_if_steps_are_introduced(steps_list) == True):
            tip = "Have you considered using a tracking app to count your daily steps? By using this type of app, we could track your progress better. The app works by using your phone's built-in sensors to count your steps automatically and then you can introduce the value in our app at the end of the day. We recommend using Google Fit, Fitbit or Apple Health. They are free to download from the app store on your phone."
            generated_tip = {
                                "Type": "Weekly Steps",
                                "Importance Level": "High",
                                "Tip": tip
                            }
            return generated_tip

        count_days_not_introduced = 0
        count_days_objective_completed = 0
        average_number_of_steps = 0
        for steps_per_day in steps_list:
            if steps_per_day < 0:
                generated_tip = {
                            "Type": "Weekly Steps",
                            "Importance Level": "Low",
                            "Tip": "Not generated"
                        }
                return generated_tip
            elif steps_per_day == "Not added":
                count_days_not_introduced += 1
            elif steps_per_day >= steps_goal:
                average_number_of_steps += steps_per_day
                count_days_objective_completed += 1
        
        average_number_of_steps = average_number_of_steps / len(steps_list)
        tip = "You managed to complete your Steps Goal on " + str(count_days_objective_completed) + " days this week achieving an average of " + str(int(average_number_of_steps)) + " steps per day. "
        if count_days_objective_completed >= 5:
            if steps_goal < 6000:
                tip += "That's a good thing, but why don't you try a higher goal? The goal of 10,000 steps is the recommended daily step target for healthy adults to achieve health benefits. Update your steps goal and make a bigger challenge for yourself."
            if steps_goal >= 6000 and steps_goal < 10000:
                tip += "Congratulations! You're doing fine! If you'd like to step up to the next level, the goal of 10,000 steps is the recommended daily step target for healthy adults to achieve health benefits. You can try to set this goal for better benefits."
            else:
                tip += "Congratulations! You exceeded the recommended daily step target for healthy adults this week. Just keep going like this!"
        elif count_days_objective_completed >= 3 and count_days_objective_completed < 5:
            if steps_goal < 6000:
                tip += "Maybe you could try to take a daily walk outside to increase the number of days for the next weeks."
            if steps_goal >= 6000:
                tip += "Maybe you could try to set a lower steps goal so that you could manage to achieve it daily and if it works fine for you, then you could increase it again."
        elif count_days_objective_completed >= 1 and count_days_objective_completed < 3:
            tip += str(count_days_objective_completed) + " days with the objective of steps completed just aren't enough. Try to achieve your goal for at least 4 days and if the goal is too high for you, you should try to lower it."
        
        if count_days_not_introduced == 0:
            if count_days_objective_completed == 0:
                tip = "You failed to meet your step goal on any day of the past week. To begin with, you could try to aim for a smaller goal and try to take a walk every day for at least half an hour."
        else:
            tip += " Also, make sure that you enter at the end of each day the number of steps you took that day so that we can better analyze your progress."
        
        generated_tip = {
                            "Type": "Weekly Steps",
                            "Importance Level": "High",
                            "Tip": tip
                        }
        return generated_tip

    def generate_daily_number_of_steps_tip(self):
        steps_goal = self.profile_data['Profile']['Steps Goal']

        if steps_goal <= 0 or steps_goal > 100000:
            raise ValueError("Steps Goal should be between 1 and 100000.")
            
        progress = self.profile_data['Progress']
        for i in range(len(progress)):
            if progress[i]['Steps'] < 0 or progress[i]['Steps'] > 50000:
                raise ValueError("Steps should be between 0 and 50000.")

        steps_list = []
        for day in self.profile_data['Progress']:
            steps = day['Steps']
            steps_list.append(steps)

        if steps_list[len(steps_list)-1] == "Not added":
            generated_tip = {
                            "Type": "Daily Steps",
                            "Importance Level": "None",
                            "Tip": "Not generated"
                        }
            return generated_tip
        
        tip = ""
        importance_level = "Low"
        steps_difference = steps_list[len(steps_list)-1] - steps_goal
        if steps_difference > 4000:
            tip = "Congratulations! You managed to fulfill your step goal for today by managing to take " + str(steps_list[len(steps_list)-1]) + " steps. This is much more than your goal. Would you consider increasing your goal starting from tomorrow?"
        elif 2000 < steps_difference <= 4000:
            tip = "Congratulations! You managed to fulfill your step goal for today by managing to take " + str(steps_list[len(steps_list)-1]) + " steps. This is actually more than your goal. Keep going like this and if you feel that you can achieve more, try to increase your daily steps goal."
        elif 0 <= steps_difference <= 2000:
            tip = "Congratulations! You managed to fulfill your step goal for today by managing to take " + str(steps_list[len(steps_list)-1]) + " steps. Keep going!"
        elif 0 > steps_difference:
            tip = "You haven't achieved your steps goal for today yet. But maybe it's not too late to take a relaxing walk outside and do that."
            importance_level = "High"

        generated_tip = {
                            "Type": "Daily Steps",
                            "Importance Level": importance_level,
                            "Tip": tip
                        }
        return generated_tip

    def generate_steps_goal_objective_age_tip(self):
        steps_goal = self.profile_data['Profile']['Steps Goal']

        if steps_goal <= 0 or steps_goal > 100000:
            raise ValueError("Steps Goal should be between 1 and 100000.")

        objective = self.profile_data['Profile']['Objective']

        objectives_list = ["Lose weight", "Gain muscular mass", "Improve overall health", "Improve cardiovascular health", "Increase endurance", "Maintain weight"]
        if objective not in objectives_list:
            raise ValueError("Objective not found in list.")

        age = self.profile_data['Profile']['Age']
        if age <= 0 or age > 130:
            raise ValueError("Age should be between 1 and 130.")

        tip = ""
        importance_level = "Low"
        if objective == "Lose weight":
            if steps_goal < 8000:
                tip = "In order to reach your goal and lose weight, it is recommended to set a goal of more steps per day. Of course, if you do other physical activities in addition to walking, it is very good as long as it compensates with the number of steps."
                importance_level = "High"
            else:
                tip = "A goal of " + str(steps_goal) + " steps per day is very good for your weight loss goal. Be sure to do it in as many days as possible and you will definitely notice the differences."
        else:
            if age < 60:
                if steps_goal < 8000:
                    tip = "In order to reach your goal, taking into account your age, it is recommended for you to set a goal of at least 8000 steps per day in order to enjoy the benefits of walking to the fullest."
                else:
                    tip = "Adults of your age can benefit the most from more than 8,000 steps per day. So your steps goal is just fine in order to achieve your biggest goal, just make sure you reach it daily."
            else:
                if steps_goal < 6000:
                    tip = "In order to reach your goal, taking into account your age, it is recommended for you to set a goal of at least 6000 steps per day in order to enjoy the benefits of walking to the fullest."
                else:
                    tip = "People your age can benefit the most from more than 6,000 steps per day. So your steps goal is just fine in order to achieve your biggest goal, just make sure you reach it daily."
            

        generated_tip = {
                            "Type": "Objective-Steps Goal",
                            "Importance Level": importance_level,
                            "Tip": tip
                        }
        return generated_tip

    def generate_hours_slept_last_night_tip(self):
        hours_slept_list = []
        for day in self.profile_data['Progress']:
            hours = day['HoursSlept']
            if hours <= 0 or hours > 24:
                raise ValueError("Hours should be between 1 and 24.")
            hours_slept_list.append(hours)
        
        if hours_slept_list[len(hours_slept_list) - 1] == "Not added":
            generated_tip = {
                            "Type": "Hours Slept Last Night",
                            "Importance Level": "None",
                            "Tip": "Not generated"
                        }
            return generated_tip

        tip = ""
        importance_level = "Low"
        hours_slept_last_night = hours_slept_list[len(hours_slept_list) - 1]
        if hours_slept_last_night == 0:
            tip = "You didn't sleep at all last night. If it was due to a rare occasion, such as a special event or unexpected situation, it may not be necessary to worry too much about the immediate effects. However, it's still important to prioritize healthy sleep habits moving forward to prevent any negative impacts on long-term health and productivity. "
            tip += random.choice(self.tips_data['Sleep']['No sleep last night'])
            tip += " If you continue to have trouble sleeping or notice any significant changes in your sleep patterns, consider talking to a healthcare professional for further guidance."
            importance_level = "High"
        elif 0 < hours_slept_last_night < 5:
            tip = "You slept only " + str(hours_slept_last_night) + " hours last night, so it's important to take steps to help your body and mind recover as much as possible. "
            tip += random.choice(self.tips_data['Sleep']['Less than 5 hours'])
            importance_level = "High"
        elif 5 <= hours_slept_last_night < 7:
            tip = "You slept only " + str(hours_slept_last_night) + "you may still feel somewhat tired or groggy, but you can take steps to help your body and mind recover. "
            tip += random.choice(self.tips_data['Sleep']['5 or 6 hours'])
            importance_level = "Medium"
        elif 7 <= hours_slept_last_night < 10:
            tip = "Congratulations on getting a good night's sleep! Sleeping for " + str(hours_slept_last_night) + " hours is great for your health and well-being. "
            tip += random.choice(self.tips_data['Sleep']['7 - 9 hours'])
        else:
            tip = "You slept " + str(hours_slept_last_night) + " hours last night, which is more than you would normally need. "
            tip += random.choice(self.tips_data['Sleep']['10 or more hours'])
            importance_level = "Medium"

        generated_tip = {
                            "Type": "Hours Slept Last Night",
                            "Importance Level": importance_level,
                            "Tip": tip
                        }
        return generated_tip

    def generate_hours_slept_weekly_feedback(self):
        hours_slept_list = []
        for day in self.profile_data['Progress']:
            hours = day['HoursSlept']
            if hours < 0 or hours > 24:
                raise ValueError("Hours should be between 1 and 24.")
            hours_slept_list.append(hours)

        if(verify_if_slept_hours_are_introduced(hours_slept_list) == True):
            tip = "Wouldn't you like to try entering the number of hours slept each night in the application? You could do this in the morning when you wake up and it wouldn't take more than 1 minute. In this way, you would help us analyze your situation better."
            generated_tip = {
                            "Type": "Weekly Slept Hours",
                            "Importance Level": "High",
                            "Tip": tip
                        }
            return generated_tip
        
        count_days_not_introduced = 0
        count_days_without_sleep = 0
        count_days_less_than_5_hours = 0
        count_days_5_or_6_hours = 0
        count_days_normal_sleep = 0
        count_days_oversleep = 0
        for hours_selpt in hours_slept_list:
            if hours_selpt == "Not added":
                count_days_not_introduced += 1
            elif hours_selpt == 0:
                count_days_without_sleep += 1
            elif 1 <= hours_selpt < 5:
                count_days_less_than_5_hours += 1
            elif 5 <= hours_selpt < 7:
                count_days_5_or_6_hours += 1
            elif 7 <= hours_selpt < 10:
                count_days_normal_sleep += 1
            elif 1 <= hours_selpt < 5:
                count_days_oversleep += 1

        tip = ""
        importance_level = "Low"
        if count_days_without_sleep > 1:
            tip = "You had " + str(count_days_without_sleep) + " days without sleep in the last week. It's important to prioritize your sleep and make sure you're getting at least a few hours each night to maintain your health and well-being."
            importance_level = "High"
        elif count_days_less_than_5_hours > 2:
            tip = "You had " + str(count_days_less_than_5_hours) + " days with less than 5 hours slept in the last week. Consistently getting less than 5 hours per night can have negative effects on your health and well-being. Try to increase your sleep duration if possible in the next weeks."
            importance_level = "High"
        elif count_days_5_or_6_hours > 2:
            tip = "You had " + str(count_days_5_or_6_hours) + " days with 5 or 6 hours of sleep in the last week. While it's okay to have a few nights of poor sleep, consistently getting only 5-6 hours per night can have negative effects on your health and well-being. Aim for at least 7 hours of sleep per night in the next weeks."
            importance_level = "High"
        elif count_days_oversleep > 2:
            tip = "You had " + str(count_days_oversleep) + " days with 10 or more hours of sleep in the last week. While it's great to get a lot of sleep, oversleeping can lead to feelings of grogginess and fatigue. Try to aim for 7-9 hours of sleep per night."
            importance_level = "Medium"
        else:
            tip = "You had " + str(count_days_normal_sleep) + " days with 7-9 hours of sleep in the last week. Great job on prioritizing your sleep and maintaining healthy sleep habits! Keep up the good work and continue to prioritize your sleep each night."
            importance_level = "Medium"

        generated_tip = {
                            "Type": "Weekly Slept Hours",
                            "Importance Level": importance_level,
                            "Tip": tip
                        }
        return generated_tip

    def generate_weekly_weight_objective_tip(self):
        objective = self.profile_data['Profile']['Objective']
        objectives_list = ["Lose weight", "Gain muscular mass", "Improve overall health", "Improve cardiovascular health", "Increase endurance", "Maintain weight"]
        if objective not in objectives_list:
            raise ValueError("Objective not found in list.")

        for day in reversed(self.profile_data["Progress"]):
            if day['Objective'] != objective:
                objective = day['Objective']
                break
            else:
                break
        
        weight_list = []
        for day in self.profile_data['Progress']:
            weight = day['Weight']
            if weight < 30 or weight > 200:
                raise ValueError("Weight should be between 30 and 200.")
            weight_list.append(weight)
        if(verify_if_weight_introduced(weight_list) == True):
            tip = "Wouldn't you like to weigh yourself and add your weight in the app once a week? In this way, we could better analyze your progress according to your objective. Also, by weighing yourself, you can have better control over your kilograms."
            generated_tip = {
                            "Type": "Weekly Weight Update",
                            "Importance Level": "High",
                            "Tip": tip
                        }
            return generated_tip
        
        start_weight = self.profile_data['Profile']['Weight']
        for day in reversed(self.profile_data["Progress"]):
            if day['Weight'] != "Not added":
                last_weight_added = day['Weight']
                break
        weight_change = start_weight - last_weight_added

        tip = ""
        importance_level = "Low"
        if weight_change > 0:
            tip = "You lost " + str(round(weight_change, 1)) + " kg in the last " + str(len(weight_list)) + " days. "
            if objective == "Lose weight":
                if weight_change <= 0.5:
                    tip += "Congratulations on your progress! To continue losing weight, focus on maintaining a calorie deficit by consuming fewer calories than your body needs and engaging in regular physical activity. Try to avoid sugary and processed foods and choose nutritious, whole foods instead. Remember that weight loss is a journey, so stay committed and patient as you work towards your goals."
                elif 0.5 < weight_change < 2:
                    tip += "Well done on reaching your weight loss goal! To continue losing weight, consider increasing the intensity or duration of your workouts to burn more calories. Also, pay attention to portion sizes and aim for a balanced diet with plenty of protein and fiber to help you feel full and satisfied. Finally, celebrate your progress and stay motivated by setting new goals and tracking your success."
                elif 2 <= weight_change:
                    tip += "Impressive job on achieving significant weight loss in just one week! To continue making progress, stay consistent with your healthy eating habits and exercise routine. Incorporate a variety of workouts, such as strength training and cardio, to help you build muscle and burn fat. Remember to prioritize self-care and get enough rest to support your weight loss efforts. Finally, keep up the good work and stay focused on your long-term goals."
            elif objective == "Gain muscular mass":
                if weight_change <= 0.5:
                    tip += "Losing weight while trying to gain muscle mass can be frustrating, but it may indicate that you are not consuming enough calories to support muscle growth. Consider tracking your daily calorie intake and increasing your overall caloric intake to create a surplus. Additionally, prioritize consuming high-quality protein to support muscle recovery and growth. Finally, make sure to engage in regular strength training exercises to stimulate muscle growth and hypertrophy."
                elif 0.5 < weight_change < 2:
                    tip += "Losing a significant amount of weight while trying to gain muscle mass may indicate that you are not consuming enough calories or protein to support muscle growth. Consider reviewing your diet and increasing your overall caloric intake, focusing on nutrient-dense foods like lean protein, complex carbohydrates, and healthy fats. Additionally, ensure that you are engaging in regular strength training exercises that target all major muscle groups. Remember that building muscle mass takes time and patience, so don't get discouraged and stay consistent with your efforts."
                elif 2 <= weight_change:
                    tip += "Losing a large amount of weight while trying to gain muscle mass can be discouraging, but it's important to stay committed to your goals. Consider reviewing your diet and ensuring that you are consuming enough calories and protein to support muscle growth. Additionally, focus on incorporating compound exercises like squats, deadlifts, and bench presses into your strength training routine to stimulate muscle growth. Finally, remember to prioritize rest and recovery to allow your muscles to heal and grow between workouts."        
            elif objective == "Maintain weight":
                if weight_change <= 0.5:
                    tip += "Losing weight while trying to maintain your weight can be a sign that you are not consuming enough calories to support your body's needs. Consider tracking your daily calorie intake and ensuring that you are consuming enough calories to meet your energy needs. Additionally, focus on consuming a balanced diet with plenty of whole foods like fruits, vegetables, lean protein, and healthy fats. Finally, continue engaging in regular physical activity to support overall health and well-being."
                elif 0.5 < weight_change < 2:
                    tip += "Losing a significant amount of weight while trying to maintain your weight can be concerning. Consider reviewing your diet and ensuring that you are consuming enough calories to meet your body's energy needs. Additionally, make sure to incorporate regular physical activity into your routine to support overall health and well-being. Finally, consider consulting with a healthcare professional to rule out any underlying medical conditions that may be contributing to weight loss."
                elif 2 <= weight_change:
                    tip += "Losing a large amount of weight while trying to maintain your weight may indicate that you are not consuming enough calories to support your body's needs or that there may be an underlying medical condition. Consider reviewing your diet and ensuring that you are consuming enough calories to meet your energy needs. Additionally, prioritize regular physical activity to support overall health and well-being. Finally, consider consulting with a healthcare professional to rule out any underlying medical conditions that may be contributing to weight loss."
            else:
                if weight_change <= 0.5:
                    tip += "Losing weight while trying to live a healthy lifestyle may not necessarily be a bad thing, but it's important to ensure that you are consuming enough nutrients to support overall health and well-being. Consider focusing on a balanced diet with plenty of whole foods like fruits, vegetables, lean protein, and healthy fats. Additionally, prioritize regular physical activity to support cardiovascular health, strength, and endurance. Finally, consider engaging in stress-reducing activities like meditation, yoga, or deep breathing exercises to support mental health."
                elif 0.5 < weight_change < 2:
                    tip += "Losing a significant amount of weight while trying to live a healthy lifestyle may indicate that you are not consuming enough calories to meet your body's needs. Consider tracking your daily calorie intake and ensuring that you are consuming enough calories to support your energy needs. Additionally, focus on a balanced diet with plenty of nutrient-dense foods like fruits, vegetables, lean protein, and healthy fats. Prioritize regular physical activity to support overall health and well-being, and consider consulting with a healthcare professional to rule out any underlying medical conditions that may be contributing to weight loss."
                elif 2 <= weight_change:
                    tip += "Losing a large amount of weight while trying to live a healthy lifestyle may be concerning. Consider reviewing your diet and ensuring that you are consuming enough calories and nutrients to meet your body's needs. Additionally, prioritize regular physical activity to support cardiovascular health, strength, and endurance. Finally, consider consulting with a healthcare professional to rule out any underlying medical conditions that may be contributing to weight loss and to discuss a personalized approach to living a healthy lifestyle."
        elif weight_change < 0:
            tip = "You gained " + str(abs(round(weight_change, 1))) + " kg in the last " + str(len(weight_list)) + " days. "
            if objective == "Lose weight":
                if abs(weight_change) <= 0.5:
                    tip += "Gaining weight can be frustrating, but don't let it discourage you from your weight loss goals. Take a moment to reassess your diet and exercise routine. Make sure you are consuming a balanced diet of whole foods and avoiding processed and sugary foods. Additionally, consider increasing the intensity and frequency of your workouts to burn more calories. Remember that weight loss is a journey and small setbacks are normal. Stay committed to your goals and celebrate your progress along the way."
                elif 0.5 < abs(weight_change) < 2:
                    tip += "Gaining weight can be a setback, but don't give up on your weight loss goals. Evaluate your diet and exercise routine and look for areas where you can make adjustments. Increase the intensity and frequency of your workouts and ensure that you are consuming a balanced diet with plenty of whole foods. Remember to prioritize self-care and get enough rest to support your weight loss efforts. Stay positive and committed to your goals, and celebrate your progress along the way."
                elif 2 <= abs(weight_change):
                    tip += "Gaining 2 kg can be discouraging, but don't let it derail your weight loss goals. Reassess your diet and exercise routine and make any necessary adjustments to ensure that you are in a calorie deficit. Focus on consuming a balanced diet of whole foods and avoid processed and sugary foods. Increase the intensity and frequency of your workouts to burn more calories and build muscle. Remember that weight loss is a journey and it's important to stay committed and patient as you work towards your goals. Celebrate your progress along the way and stay motivated by setting new goals for yourself."
            elif objective == "Gain muscular mass":
                if abs(weight_change) <= 0.5:
                    tip += "It's great that you have gained weight, but if your goal is to build muscle mass, it's important to ensure that the weight you are gaining is from muscle and not fat. Evaluate your diet and make sure you are consuming enough protein to support muscle growth. Additionally, focus on strength training exercises that target the major muscle groups to stimulate muscle growth. Ensure that you are getting enough rest to allow your muscles to recover and grow. Remember that building muscle mass is a gradual process and it takes time and consistency to see results. Celebrate your progress and stay committed to your goals."
                elif 0.5 < abs(weight_change) < 2:
                    tip += "Congratulations on your weight gain! To continue building muscle mass, make sure you are consuming enough protein and calories to support muscle growth. Focus on strength training exercises that target the major muscle groups and gradually increase the weight and intensity of your workouts. Remember that rest and recovery are important for muscle growth, so make sure you are getting enough sleep and allowing your muscles time to recover. Stay committed to your goals and celebrate your progress along the way."
                elif 2 <= abs(weight_change):
                    tip += "Well done on your weight gain! To continue building muscle mass, focus on consuming enough protein and calories to support muscle growth. Ensure that you are following a structured and progressive strength training program that targets the major muscle groups. Consider working with a personal trainer or fitness professional to ensure that your form is correct and that you are making progress. Remember that building muscle mass is a long-term process that requires consistency and dedication. Celebrate your progress and stay motivated by setting new goals for yourself."        
            elif objective == "Maintain weight":
                if abs(weight_change) <= 0.5:
                    tip += "Gaining weight can be a sign that you are consuming more calories than your body needs to maintain its current weight. Take a moment to reassess your diet and ensure that you are eating a balanced diet with plenty of whole foods. It can be helpful to track your food intake to get a better idea of how many calories you are consuming each day. Additionally, make sure you are engaging in regular physical activity to support your overall health and well-being. Remember that maintaining your weight is a continuous effort, so stay committed and patient as you work towards your goals."
                elif 0.5 < abs(weight_change) < 2:
                    tip += "Gaining weight can be a sign that you are consuming more calories than your body needs to maintain its current weight. Take a closer look at your diet and consider making adjustments to ensure that you are eating a balanced diet with plenty of whole foods. It can be helpful to track your food intake to get a better idea of how many calories you are consuming each day. Additionally, make sure you are engaging in regular physical activity to support your overall health and well-being. Remember that maintaining your weight is a continuous effort, so stay committed and patient as you work towards your goals."
                elif 2 <= abs(weight_change):
                    tip += "Gaining 2 kg can be a sign that you are consuming significantly more calories than your body needs to maintain its current weight. Take a closer look at your diet and make sure you are eating a balanced diet with plenty of whole foods. It can be helpful to track your food intake to get a better idea of how many calories you are consuming each day. Additionally, make sure you are engaging in regular physical activity to support your overall health and well-being. Remember that maintaining your weight is a continuous effort, so stay committed and patient as you work towards your goals. Consider making small, sustainable changes to your lifestyle to support your weight maintenance goals over the long term."
            else:
                if abs(weight_change) <= 0.5:
                    tip += "Gaining weight while trying to live a healthy lifestyle may not necessarily be a bad thing, but it's important to ensure that you are consuming enough nutrients to support overall health and well-being. Consider tracking your daily calorie intake and ensuring that you are consuming enough calories to support your energy needs. Additionally, focus on a balanced diet with plenty of nutrient-dense foods like fruits, vegetables, lean protein, and healthy fats. Finally, prioritize regular physical activity to support cardiovascular health, strength, and endurance."
                elif 0.5 < abs(weight_change) < 2:
                    tip += "Gaining a significant amount of weight while trying to live a healthy lifestyle may indicate that you are consuming more calories than your body needs. Consider tracking your daily calorie intake and ensuring that you are consuming an appropriate amount of calories to support your energy needs. Additionally, focus on a balanced diet with plenty of nutrient-dense foods like fruits, vegetables, lean protein, and healthy fats. Prioritize regular physical activity to support overall health and well-being, and consider consulting with a healthcare professional to rule out any underlying medical conditions that may be contributing to weight gain."
                elif 2 <= abs(weight_change):
                    tip += "Gaining a large amount of weight while trying to live a healthy lifestyle may be concerning. Consider reviewing your diet and ensuring that you are consuming an appropriate amount of calories to support your energy needs. Additionally, focus on a balanced diet with plenty of nutrient-dense foods like fruits, vegetables, lean protein, and healthy fats. Prioritize regular physical activity to support cardiovascular health, strength, and endurance, and consider consulting with a healthcare professional to rule out any underlying medical conditions that may be contributing to weight gain. Finally, consider working with a registered dietitian or health coach to develop a personalized approach to achieving your health goals."
        else:
            tip = "Your weight remained the same in the last " + str(len(weight_list)) + " days. "
            if objective == "Lose weight":
                tip += "While it can be frustrating not to see progress on the scale, don't get discouraged. Remember that weight loss is not always linear and can vary day to day. Evaluate your diet and exercise routine and look for areas where you can make adjustments. Make sure you are consuming a balanced diet of whole foods and avoiding processed and sugary foods. Consider increasing the intensity and frequency of your workouts to burn more calories. Finally, stay patient and consistent in your efforts and celebrate non-scale victories such as increased energy and improved fitness."
            elif objective == "Gain muscular mass":
                tip += "Keep in mind that gaining muscle mass takes time, patience, and consistency. Evaluate your diet and make sure you are consuming enough protein to support muscle growth, as well as enough overall calories to fuel your workouts and recovery. Consider adjusting your strength training routine to focus on progressive overload, which means gradually increasing the weight, reps, or sets of your exercises over time. Additionally, make sure you are getting enough rest and recovery time to allow your muscles to heal and grow. Finally, remember to celebrate small victories and stay motivated by tracking your progress and setting achievable goals."        
            elif objective == "Maintain weight":
                tip += "Congratulations on successfully maintaining your weight! Remember that weight maintenance is just as important as weight loss or gain. Continue to monitor your calorie intake and make sure you are consuming enough calories to support your energy needs while avoiding excess calories that could lead to weight gain. Focus on eating a balanced diet with plenty of whole, nutrient-dense foods, and consider incorporating regular physical activity to support your overall health and well-being. Finally, stay consistent and patient in your efforts, and celebrate your success in maintaining your weight."
            else:
                tip += "Maintaining your weight while trying to live a healthy lifestyle is a positive achievement, as it indicates that you are consuming enough calories to meet your energy needs while also engaging in regular physical activity. It's important to continue focusing on a balanced diet with plenty of whole foods like fruits, vegetables, lean protein, and healthy fats. Additionally, prioritize regular physical activity to support cardiovascular health, strength, and endurance. Finally, consider engaging in stress-reducing activities like meditation, yoga, or deep breathing exercises to support mental health."
        
        generated_tip = {
                            "Type": "Weekly Weight Update",
                            "Importance Level": importance_level,
                            "Tip": tip
                        }
        return generated_tip

    def generate_general_tip(self):
        general_tips = self.tips_data['General']
        tip = random.choice(general_tips)
        generated_tip = {
                            "Type": "General",
                            "Importance Level": "High",
                            "Tip": tip
                        }
        return generated_tip

    def generate_food_logs_weekly_feedback(self):
        food_logs_list = []
        for day in self.profile_data['Progress']:
            food = day['FoodLogs']
            food_logs_list.append(food)
        if verify_if_food_logs_introduced(food_logs_list) == True:
            tip = "Wouldn't you like to try to enter in the application what you eat every day? It would only take a few seconds after each meal and in this way you could get a real-time record of the calories you consume. Also, by doing this we will be able to offer you personalized advice on a daily basis."
            generated_tip = {
                            "Type": "Weekly Food Logs",
                            "Importance Level": "High",
                            "Tip": tip
                        }
            return generated_tip

        
        objective = self.profile_data['Profile']['Objective']
        objectives_list = ["Lose weight", "Gain muscular mass", "Improve overall health", "Improve cardiovascular health", "Increase endurance", "Maintain weight"]
        if objective not in objectives_list:
            raise ValueError("Objective not found in list.")
        for day in reversed(self.profile_data["Progress"]):
            if day['Objective'] != objective:
                objective = day['Objective']
                break
            else:
                break

        exercise_logs_list = []
        for day in self.profile_data['Progress']:
            exercise = day['ExerciseLogs']
            exercise_logs_list.append(exercise)

        calories_required = generator.calculate_amr()
        count_days_too_few_calories = 0
        count_days_too_many_calories = 0
        count_days_enough_calories = 0 

        for day in self.profile_data['Progress']:
            total_calories = sum([food['Calories'] for food in day['FoodLogs']])
            if calories_required - total_calories > 250:
                count_days_too_few_calories += 1
            elif calories_required - total_calories < -250:
                count_days_too_many_calories += 1
            else:
                count_days_enough_calories += 1

        tip = "Our recommendation is to consume a total of " + str(calories_required) + " calories per day. In the last week you had " + str(count_days_enough_calories) + " day(s) in which you consumed enough calories, " + str(count_days_too_few_calories) + " day(s) in which you consumed fewer calories and " + str(count_days_too_many_calories) + " day(s) in which you consumed more calories. "

        if objective == "Lose weight":
            if count_days_too_few_calories < 4:
                tip += "To lose weight, aim to consume 250-500 fewer calories per day than your daily calorie needs. This can result in a weight loss of 0.5-1 kilogram per week. In addition to dietary changes, aim to engage in regular physical activity for at least 30 minutes most days of the week. This can help to increase your calorie burn and promote weight loss."
            elif count_days_too_many_calories > 3:
                tip += "Experiencing setbacks is a normal part of the weight loss journey. To refocus and get back on track, aim to be mindful of your food choices and portion sizes moving forward. Try to consume a calorie deficit of 250-500 calories per day and incorporate regular physical activity into your routine. Remember to be patient and kind to yourself, and seek support from friends, family, or a healthcare professional to help you stay motivated and accountable towards your goals."
            else:
                tip += "Great job on implementing a calorie deficit of 250-500 calories per day in the last week! Consistency is key when it comes to weight loss, and it's important to remember that progress may not always be linear. Keep up the great work and remember to prioritize sustainable and gradual changes to your lifestyle for long-term success. Remember, it's a journey, and every small step counts towards your goal."
        elif objective == "Gain muscular mass":
            if count_days_too_many_calories < 4:
                tip += "If you're looking to gain muscular mass, it's important to focus on consuming a calorie surplus and engaging in strength training exercises. Aim to consume 250-500 more calories per day than your daily calorie needs and focus on consuming a balanced diet that includes plenty of protein, complex carbohydrates, and healthy fats to support muscle growth."
            elif count_days_too_few_calories > 3:
                tip += "If you want to gain muscular mass, it's important to shift your focus towards consuming a calorie surplus to support muscle growth. Aim to consume 250-500 more calories per day than your daily calorie needs and focus on consuming a balanced diet that includes plenty of protein, complex carbohydrates, and healthy fats to support muscle growth."
            else:
                if verify_if_exercise_logs_introduced(exercise_logs_list) == False:
                    tip += "Congratulations on your efforts to consume a calorie surplus and engage in strength training exercises to gain muscular mass! Keep up the great work and focus on consuming a balanced diet with plenty of protein, complex carbohydrates, and healthy fats to support muscle growth. Remember to prioritize rest and recovery and celebrate your progress along the way. With continued dedication, you can achieve your desired muscular mass."
                else:
                    tip += "Your efforts to consume a calorie surplus to gain muscular mass are good, but if you don't exercise you will only get fat. Try to exercise for at least 4 times per week and introduce your progress in the app so that we can show you feedback and more personalized tips."
        elif objective == "Maintain weight":
            if count_days_too_many_calories > 3:
                tip += "To maintain your weight, it's important to aim for consistency in your dietary habits. While it's okay to indulge in high-calorie foods occasionally, try to maintain a balanced diet and be mindful of portion sizes. To get back on track after a week of overeating, try reducing your calorie intake by 250-500 calories on some days and focus on consuming plenty of whole, nutrient-dense foods. Incorporating regular physical activity can also help to maintain your weight and support overall health."
            elif count_days_too_few_calories > 3:
                tip += "To maintain your weight, it's important to give your body the nourishment it needs. You've been consuming too few calories so increasing your intake by 250-500 calories on some days can help support your metabolism and overall health. Focus on incorporating plenty of nutrient-dense foods into your diet, including complex carbohydrates, lean proteins, and healthy fats. Don't forget to make time for regular physical activity as well, which can help to boost your metabolism and maintain a healthy weight over time."
            else:
                tip += "Great job on fueling your body with the appropriate amount of calories to support your goals! Consistently meeting your daily calorie needs is an important step in maintaining a healthy weight and overall well-being. Keep up the good work and remember to focus on consuming a balanced diet that includes plenty of nutrient-dense foods, as well as staying active with regular physical activity. You've got this!"
        else:
            tip += "To improve your overall health and well-being, paying attention to your daily calorie intake can be a helpful place to start. Aim to consume a balanced diet that includes plenty of fruits, vegetables, lean proteins, and whole grains, while limiting highly processed and high-calorie foods. It's also important to listen to your body's hunger and fullness cues, and to make sustainable changes over time that support your goals. Remember, small steps can add up to big progress over time, so start by making one or two changes today and build from there."

        generated_tip = {
                            "Type": "Weekly Food Logs",
                            "Importance Level": "High",
                            "Tip": tip
                        }
        return generated_tip

def tip(input):
    tips_file = f"{abspath}/tips.json"
    generator = TipGenerator(tips_file, input)

    return generator.generate_tips()


def test_calculate_bmr():
    generator = TipGenerator('tips.json', 'profile.json')

    generator.profile_data = {'Height': 170, 'Weight': 60, 'Age': 30, 'Sex': 'M', 'Level of activity': 'Moderately active', 'Objective': 'Maintain weight'}
    assert int(generator.calculate_bmr()) == 1539
    
    generator.profile_data = {'Height': 160, 'Weight': 70, 'Age': 25, 'Sex': 'F', 'Level of activity': 'Active', 'Objective': 'Lose weight'}
    assert int(generator.calculate_bmr()) == 1503
    
    generator.profile_data = {'Height': 180, 'Weight': 80, 'Age': 40, 'Sex': 'M', 'Level of activity': 'Sedentary', 'Objective': 'Gain muscular mass'}
    assert int(generator.calculate_bmr()) == 1796

    print('All test passed.')

def test_calculate_amr():
    generator = TipGenerator('tips.json', 'profile.json')

    generator.profile_data = {'Height': 170, 'Weight': 60, 'Age': 30, 'Sex': 'M', 'Level of activity': 'Moderately active', 'Objective': 'Maintain weight'}
    assert generator.calculate_amr() == 2409

    generator.profile_data = {'Height': 160, 'Weight': 70, 'Age': 25, 'Sex': 'F', 'Level of activity': 'Active', 'Objective': 'Lose weight'}
    assert generator.calculate_amr() == 2681
    
    generator.profile_data = {'Height': 180, 'Weight': 80, 'Age': 40, 'Sex': 'M', 'Level of activity': 'Sedentary', 'Objective': 'Gain muscular mass'}
    assert generator.calculate_amr() == 1865

    print('All test passed.')

def test_generate_amr_tip():
    generator = TipGenerator(tips_file,profile_file)
    generator.calculate_amr = MagicMock(return_value=2479)
    generator.profile_data = {'Objective': 'Lose weight', 'Level of activity': 'Moderately active'}
    expected_tip = {'tip_type': 'Daily Calories', 'tip': 'You burn 2479 calories during a typical day. To achieve your goal of losing weight, try to stay below your calorie needs and increase your activity level. However, make sure you are eating nutritious meals and not restricting your calories too much - eating too little or losing weight rapidly can be unhealthy and dangerous.'}
    assert generator.generate_amr_tip() == expected_tip

    print('All test passed.')

def test_generate_bmi_tip():
    generator = TipGenerator(tips_file,profile_file)
    generator.calculate_amr = MagicMock(return_value=2479)
    generator.profile_data = {'Objective': 'Lose weight', 'Level of activity': 'Moderately active'}
    expected_tip = {'tip_type': 'Daily Calories', 'tip': 'You burn 2479 calories during a typical day. To achieve your goal of losing weight, try to stay below your calorie needs and increase your activity level. However, make sure you are eating nutritious meals and not restricting your calories too much - eating too little or losing weight rapidly can be unhealthy and dangerous.'}
    assert generator.generate_amr_tip() == expected_tip

    print('All test passed.')

def test_generate_generate_level_of_activity_tip():
    generator = TipGenerator(tips_file,profile_file)
    generator.calculate_amr = MagicMock(return_value=2479)
    generator.profile_data = {'Objective': 'Lose weight', 'Level of activity': 'Moderately active'}
    expected_tip = {'tip_type': 'Daily Calories', 'tip': 'You burn 2479 calories during a typical day. To achieve your goal of losing weight, try to stay below your calorie needs and increase your activity level. However, make sure you are eating nutritious meals and not restricting your calories too much - eating too little or losing weight rapidly can be unhealthy and dangerous.'}
    assert generator.generate_amr_tip() == expected_tip

    print('All test passed.')


tips_file = f"{abspath}/tips.json"
profile_file = f"{abspath}/profile.json"
generator = TipGenerator(tips_file, profile_file)

# tip = generator.generate_tip()
# tip_json = json.dumps(tip)
# print(tip_json)

print(generator.generate_tips())