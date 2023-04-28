import random
import numpy
"""
Source: https://globalwellnessinstitute.org/what-is-wellness/

Physical: Nourishing a healthy body through exercise, nutrition, sleep, etc.
Mental: Engaging the world through learning, problem-solving, creativity, etc.
Emotional: Being aware of, accepting and expressing our feelings, and understanding the feelings of others.
Spiritual: Searching for meaning and higher purpose in human existence.
Social: Connecting and engaging with others and our communities in meaningful ways.
Environmental: Fostering positive interrelationships between planetary health and human actions, choices and wellbeing
"""
wellness_categories = ["Physical", "Mental", "Emotional", "Spiritual", "Social", "Environmental"]

def normalize_dict(d):
    suma = sum(d.values())
    for i in d:
        d[i] = d[i] / suma

class category_choice:
    def __init__(self, category_strs):
        self.scores = dict.fromkeys(category_strs, 1)
        self.multiplier = dict.fromkeys(category_strs, 1)

    def inc(self, category, score):
        if category in self.scores:
            self.scores[category] += score * self.multiplier[category]
        else:
            raise Exception(category.__str__() + " not in category list")
        
    def sum_scores(self):
        return sum(self.scores.values())

    def select_categories(self, amount):
        normalize_dict(self.scores)
        print("Scores: ", self.scores)

        categories = numpy.random.choice(
                            numpy.array(list(self.scores.keys())), 
                            p=numpy.array(list(self.scores.values())).astype(float), 
                            replace = False, 
                            size = amount
                    )
        print("Chosen categories: ", categories)
        return list(categories)


def get(dictionary):
    wellness_scores = category_choice(wellness_categories)
    
    if "Multipliers" in dictionary:
        for i in wellness_categories:
            if i in dictionary["Multipliers"]:
                wellness_scores.multiplier[i] = dictionary["Multipliers"][i]

    if "Categories" in dictionary:
        if dictionary["Categories"] in wellness_categories:
            categories = dictionary["Categories"]
        else:
            return None
    else:
        if "StressLevel" in dictionary:
            if dictionary["StressLevel"] > 0.7:
                wellness_scores.inc("Physical", 2)
        
        wellness_scores.inc("Mental", 1)
        wellness_scores.inc("Emotional", 2)
        wellness_scores.inc("Physical", 2)

        categories = wellness_scores.select_categories(3)
    
    category = categories[0]

    recommendation = {"Category": category, "Bolster": " ".join(["Be more", category])}

    return recommendation
    