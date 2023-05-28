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
        self.stress_data = stress_json
    

    def generate_advice_for_heart_rate(self):
        toReturn = {}
        heart_rate_list = []
        for day in self.stress_data['days']:
            heart_rate = day['heartrate']
            heart_rate_list.append(heart_rate)
        msg=""
        advice=""

        for day in self.stress_data['days']:
            heart_rate = day['heartrate']
            day_no=day['day']
            if heart_rate>100:
               advice="Your heart rate was too high in the day:" + str(day_no)+ ". If you have made physical effort, you must reduce it for your health or take more breaks."
               toReturn["heart"+str(day["day"])] = advice
            elif heart_rate<60:
                advice="Your heart rate was too low in the day:" + str(day_no)+ ". Due to the low pulse, you present symptoms similar to Bradycardia, you should visit a doctor as soon as possible."
                toReturn["heart"+str(day["day"])] = advice
        return toReturn

    def generate_advice_for_breathing_rate(self):
        toReturn = {}
        breathing_rate_list = []
        for day in self.stress_data['days']:
            breathing_rate = day['breathingrate']
            breathing_rate_list.append(breathing_rate)

        for day in self.stress_data['days']:
            breathing_rate = day['breathingrate']
            day_no=day['day']
            

            if breathing_rate>16:
               advice="Your breathing rate was too high in the day:" + str(day_no)+ ". You seemed stressed. Maybe a breathing exercise will help you:"
               toReturn["breathing"+str(day["day"])] = advice
               exercises = [exercise["ex"] for exercise in self.stress_data["breathing_ex"] if exercise["ex"]]
               random_exercise = random.choice(exercises)
               toReturn["randomExercise"+str(day["day"])] = random_exercise


            elif breathing_rate<12:
                advice="Your breathing rate was too low in the day:" + str(day_no)+ ". Due to the low breathing rate, you may have some problembs about lungs breathing, you should visit a doctor as soon as possible."
                toReturn["breathing"+str(day["day"])] = advice
        return toReturn

    def generate_advice_for_sleeping_hours(self):
        toReturn = {}
        sleeping_hours_list = []
        for day in self.stress_data['days']:
            sleeping_hours = day['sleepinhours']
            sleeping_hours_list.append(sleeping_hours)
        msg=""
        advice=""

        for day in self.stress_data['days']:
            sleeping_hours = day['sleepinhours']
            day_no=day['day']
            

            if sleeping_hours>9:
               advice="You have exceeded the normal range of sleeping hours in the day: " + str(day_no)+ ". This can lead to disruption of normal sleep and the appearance of insomnia."
               toReturn["sleeping"+str(day["day"])] = advice


            elif sleeping_hours<6:
                advice="You slept too little during the day: " + str(day_no)+ ". Here are some tips for increasing and improving sleep:"
                toReturn["sleeping"+ str(day["day"])] = advice
                exercises = [exercise["ex"] for exercise in self.stress_data["sleeping_methods"] if exercise["ex"]]
                random_exercise = random.choice(exercises)
                toReturn["zexercise"+str(day["day"])] = random_exercise

        return toReturn
            

def tip(input):
    tips_file = f"{abspath}\exemplu.json"
    q = {}
    with open(tips_file, 'r', encoding='utf-8') as f:
            q = json.load(f)
    generator = Stress_detec(q)
    ex1 = generator.generate_advice_for_sleeping_hours()

    return ex1




def getStress(input):
    tips_file = f"{abspath}\exemplu.json"
    q = {}
    with open(tips_file, 'r', encoding='utf-8') as f:
        q = json.load(f)
    q["days"] = input["days"]
    generator = Stress_detec(q)
    toReturn = {}
    toReturn["breathing"] =generator.generate_advice_for_breathing_rate()
    toReturn["sleeping"] = generator.generate_advice_for_sleeping_hours()
    toReturn["hearth"] = generator.generate_advice_for_heart_rate()
    
    return toReturn

tips_file = f"{abspath}\InputData.json"
with open(tips_file, 'r', encoding='utf-8') as f:
        input = json.load(f)





