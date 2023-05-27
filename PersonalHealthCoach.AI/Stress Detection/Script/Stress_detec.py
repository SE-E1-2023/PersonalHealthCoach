import json
import random
import os
abspath = os.path.dirname(__file__)
import sys
sys.path.append(abspath)

def verify_if_heartrate_are_introduced(heartrate_list):
        for heartrate_per_day in heartrate_list:
            if heartrate_per_day == None:
                return False;
        return True;

def verify_if_breathing_are_introduced(breathing_rate_list):
        for breathing_rate_per_day in breathing_rate_list:
            if breathing_rate_per_day == None:
                return False;
        return True;

def verify_if_sleeping_hours_are_introduced(sleeping_hours_list):
        for sleeping_hours_per_day in sleeping_hours_list:
            if sleeping_hours_per_day == None:
                return False;
        return True;

class Stress_detec:
    def __init__(self,stress_json):
        with open(stress_json, 'r', encoding='utf-8') as f:
            self.stress_data = json.load(f)

    def generate_advice_for_heart_rate(self):
        heart_rate_list = []
        for day in self.stress_data['days']:
            heart_rate = day['heartrate']
            heart_rate_list.append(heart_rate)
        msg=""
        advice=""
        if verify_if_heartrate_are_introduced(heart_rate_list)==True:
            msg="There are data about user heart rates. We are analyzing them now... "
            print(msg)
            print("\n")
        for day in self.stress_data['days']:
            heart_rate = day['heartrate']
            day_no=day['day']
            if heart_rate>100:
               advice="Your heart rate was too high in the day:" + str(day_no)+ ". If you have made physical effort, you must reduce it for your health or take more breaks."
               print(advice)
            elif heart_rate<60:
                advice="Your heart rate was too low in the day:" + str(day_no)+ ". Due to the low pulse, you present symptoms similar to Bradycardia, you should visit a doctor as soon as possible."
                print(advice)
    
    def generate_advice_for_breathing_rate(self):
        breathing_rate_list = []
        for day in self.stress_data['days']:
            breathing_rate = day['breathingrate']
            breathing_rate_list.append(breathing_rate)
        msg=""
        advice=""

        if verify_if_breathing_are_introduced(breathing_rate_list)==True:
            msg="There are data about user breathing rates. We are analyzing them now..."
            print(msg)
            print("\n")
        for day in self.stress_data['days']:
            breathing_rate = day['breathingrate']
            day_no=day['day']
            

            if breathing_rate>16:
               advice="Your breathing rate was too high in the day:" + str(day_no)+ ". You seemed stressed. Maybe a breathing exercise will help you:"
               print(advice)
               exercises = [exercise["ex"] for exercise in self.stress_data["breathing_ex"] if exercise["ex"]]
               random_exercise = random.choice(exercises)
               print(random_exercise)
               print("\n")

            elif breathing_rate<12:
                advice="Your breathing rate was too low in the day:" + str(day_no)+ ". Due to the low breathing rate, you may have some problembs about lungs breathing, you should visit a doctor as soon as possible."
                print(advice)
    def generate_advice_for_sleeping_hours(self):
        sleeping_hours_list = []
        for day in self.stress_data['days']:
            sleeping_hours = day['sleepinhours']
            sleeping_hours_list.append(sleeping_hours)
        msg=""
        advice=""

        if verify_if_sleeping_hours_are_introduced(sleeping_hours_list)==True:
            msg="There are data about user sleeping hours. We are analyzing them now..."
            print(msg)
            print("\n")
        for day in self.stress_data['days']:
            sleeping_hours = day['sleepinhours']
            day_no=day['day']
            

            if sleeping_hours>9:
               advice="You have exceeded the normal range of sleeping hours in the day: " + str(day_no)+ ". This can lead to disruption of normal sleep and the appearance of insomnia."
               print(advice)
               print("\n")

            elif sleeping_hours<6:
                advice="You slept too little during the day: " + str(day_no)+ ". Here are some tips for increasing and improving sleep:"
                print(advice)
                exercises = [exercise["ex"] for exercise in self.stress_data["sleeping_methods"] if exercise["ex"]]
                random_exercise = random.choice(exercises)
                print(random_exercise)
                print("\n")
            
        
def tip(input):
    tips_file = f"{abspath}/exemplu.json"
    generator = Stress_detec(tips_file)
    ex1 = generator.generate_advice_for_sleeping_hours()
    print(ex1)
    return ex1

stress_json = f"{abspath}/exemplu.json"
tip(input)
