import json
import random

import os
abspath = os.path.dirname(__file__)

class TipGenerator:
    def __init__(self, tips_file, profile_json):
        with open(tips_file) as f:
            self.tips_data = json.load(f)
        
        """with open(profile_file) as f:
            self.profile_data = json.load(f)"""
        self.profile_data = profile_json
    
    def calculate_bmi(self):
        height = self.profile_data['Height']
        weight = self.profile_data['Weight']

        height = height / 100
        bmi = weight / (height * height)
        bmi = round(bmi, 1)

        with open('bmi_categories.json') as f:
            bmi_categories = json.load(f)['categories']

        for category in bmi_categories:
            if category['upper_bound'] is None and bmi >= category['lower_bound']:
                return category['name'],bmi
            elif bmi >= category['lower_bound'] and bmi <= category['upper_bound']:
                return category['name'],bmi
            
    def generate_tip(self, tip_type=None):
        objective = self.profile_data['Objective']

        if objective == 'Lose weight':
            general_tips = self.tips_data['Lose weight']['general']
            diet_tips = self.tips_data['Lose weight']['diet']
            fitness_tips = self.tips_data['Lose weight']['fitness']
        elif objective == 'Gain muscular mass':
            general_tips = self.tips_data['Gain muscular mass']['general']
            diet_tips = self.tips_data['Gain muscular mass']['diet']
            fitness_tips = self.tips_data['Gain muscular mass']['fitness']
        elif objective == 'Improve overall health':
            general_tips = self.tips_data['Improve overall health']['general']
            diet_tips = self.tips_data['Improve overall health']['diet']
            fitness_tips = self.tips_data['Improve overall health']['fitness']
        elif objective == 'Improve cardiovascular health':
            general_tips = self.tips_data['Improve cardiovascular health']['general']
            diet_tips = self.tips_data['Improve cardiovascular health']['diet']
            fitness_tips = self.tips_data['Improve cardiovascular health']['fitness']
        elif objective == 'Increase strength':
            general_tips = self.tips_data['Increase strength']['general']
            diet_tips = self.tips_data['Increase strength']['diet']
            fitness_tips = self.tips_data['Increase strength']['fitness']
        elif objective == 'Increase endurance':
            general_tips = self.tips_data['Increase endurance']['general']
            diet_tips = self.tips_data['Increase endurance']['diet']
            fitness_tips = self.tips_data['Increase endurance']['fitness']
        elif objective == 'Maintain weigth':
            general_tips = self.tips_data['Maintain weigth']['general']
            diet_tips = self.tips_data['Maintain weigth']['diet']
            fitness_tips = self.tips_data['Maintain weigth']['fitness']
        else:
            objective = 'Maintain weigth'
            general_tips = self.tips_data['Maintain weigth']['general']
            diet_tips = self.tips_data['Maintain weigth']['diet']
            fitness_tips = self.tips_data['Maintain weigth']['fitness']
        
        if tip_type is None:
            tip_type = random.choice(['general','diet', 'fitness'])
        
        if tip_type == 'general':
            tip = random.choice(general_tips)
        elif tip_type == 'diet':
            tip = random.choice(diet_tips)
        else:
            tip = random.choice(fitness_tips)
        
        tip_dict = {'tip_type': tip_type, 'tip': tip}

        # first_time_tip = self.tips_data['First time in app']
        # tip = random.choice(first_time_tip)
        # tip_dict = {'tip_type': 'First time in app', 'tip': tip}
        
        return tip_dict
    
def tip(input):
    tips_file = f"{abspath}/tips.json"
    generator = TipGenerator(tips_file, input)

    return generator.generate_tip()

"""    
tips_file = 'tips.json'
profile_file = 'profile.json'
generator = TipGenerator(tips_file, profile_file)

tip = generator.generate_tip()
tip_json = json.dumps(tip)
print(tip_json)

bmi_category = generator.calculate_bmi()
print(bmi_category)"""
    
    

    



