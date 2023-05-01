import json
import random

import os
abspath = os.path.dirname(__file__)
import sys
sys.path.append(abspath)
from unittest.mock import MagicMock


class TipGenerator:
    def __init__(self, tips_file, profile_json):
        with open(tips_file) as f:
            self.tips_data = json.load(f)
        
        """with open(profile_file) as f:
            self.profile_data = json.load(f)"""
        self.profile_data = profile_json
    
    def calculate_bmr(self):
        
#test if these fields are present in our dictionary
        if {'Height', 'Weight', 'Age', 'Sex'} <= self.profile_data.keys():
            return None
        
        height = self.profile_data['Height']
        weight = self.profile_data['Weight']
        age = self.profile_data['Age']
        gender = self.profile_data['Sex']

        if gender == 'F':
            bmr = 655.1 + (9.563 * weight) + (1.850 * height) - (4.676 * age)
        else:
            bmr = 66.47 + (13.75 *weight) + (5.003 * height) - (6.755 * age)
        return bmr
    
    def calculate_amr(self):

#test if these fields are present in our dictionary
        if {'Level of activity'} <= self.profile_data.keys():
            return None

        bmr = generator.calculate_bmr()
        level_of_activity = self.profile_data['Level of activity']

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
        
#test if these fields are present in our dictionary
        if {'Objective'} <= self.profile_data.keys():
            return None

        objective = self.profile_data['Objective']
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

        tip_dict = {'tip_type': 'Daily Calories', 'tip': tip}
        return tip_dict

    def generate_bmi_tip(self):

#test if these fields are present in our dictionary
        if {'Height', 'Weight', 'Objective'} <= self.profile_data.keys():
            return None
        
        height = self.profile_data['Height']
        weight = self.profile_data['Weight']
        objective = self.profile_data['Objective']

        height = height / 100
        bmi = weight / (height * height)
        bmi = round(bmi, 1)

        with open('bmi_categories.json') as f:
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
        elif 0 < diff_weight < 2:
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
        elif diff_weight > 2:
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
        elif -2 < diff_weight < 0:
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
        elif diff_weight < -2:
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

        tip_dict = {'tip_type': tip_type, 'tip': tip}
        return tip_dict

    def generate_level_of_activity_tip(self):

#test if these fields are present in our dictionary
        if {'Level of activity', 'Objective'} <= self.profile_data.keys():
            return None

        level_of_activity = self.profile_data['Level of activity']
        objective = self.profile_data['Objective']

        tip = self.tips_data[objective]['level of activity'][level_of_activity]

        tip_dict = {'tip_type': 'Objective-Level of activity', 'tip': tip}
        return tip_dict

    def generate_tip(self, tip_type=None):

#test if these fields are present in our dictionary
        if {'Objective'} <= self.profile_data.keys():
            return None

        objective = self.profile_data['Objective']

        general_tips = self.tips_data[objective]['general']
        diet_tips = self.tips_data[objective]['diet']
        fitness_tips = self.tips_data[objective]['fitness']

#Cosmin start
        disease = self.profile_data['Disease']
        for diseases in  disease: 
            general_tips.extend(self.tips_data['Diseases'][disease]['general'])
            diet_tips.extend(self.tips_data['Diseases'][disease]['diet'])
            fitness_tips.extend(self.tips_data['Diseases'][disease]['fitness'])
        
#Cosmin finnish
        

        if tip_type is None:
            tip_type = random.choice(['general','diet', 'fitness'])
        
        if tip_type == 'general':
            tip = random.choice(general_tips)
        elif tip_type == 'diet':
            tip = random.choice(diet_tips)
        else:
            tip = random.choice(fitness_tips)
        
        tip_dict = {'tip_type': tip_type, 'tip': tip}
        return tip_dict   
    

def tip(input):
    tips_file = f"{abspath}/tips.json"
    generator = TipGenerator(tips_file, input)

    return generator.generate_tip()

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
    # create an instance of the class and set the profile data
    generator = TipGenerator(tips_file,profile_file)

    # create a mock function for calculate_amr
    generator.calculate_amr = MagicMock(return_value=2479)

    generator.profile_data = {'Objective': 'Lose weight', 'Level of activity': 'Moderately active'}
    
    # call the generate_amr_tip method and check the return value
    expected_tip = {'tip_type': 'Daily Calories', 'tip': 'You burn 2479 calories during a typical day. To achieve your goal of losing weight, try to stay below your calorie needs and increase your activity level. However, make sure you are eating nutritious meals and not restricting your calories too much - eating too little or losing weight rapidly can be unhealthy and dangerous.'}
    assert generator.generate_amr_tip() == expected_tip

    print('All test passed.')

tips_file = f'{abspath}\\tips.json'
profile_file = f'{abspath}\\profile.json'
generator = TipGenerator(tips_file, profile_file)

"""    
tip = generator.generate_tip()
tip_json = json.dumps(tip)
print(tip_json)

bmi_category = generator.calculate_bmi()
print(bmi_category)"""
    
    

    



