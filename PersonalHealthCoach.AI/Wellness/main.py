import numpy
import json
# from flask import abort
import traceback
import os
ABSPATH = os.path.dirname(__file__)
import logging
wellness_logger = logging.getLogger(__name__)
wellness_logger.setLevel(logging.DEBUG)
wellness_logger.addHandler(logging.FileHandler(ABSPATH + '/wellness.log'))
"""
Source: https://globalwellnessinstitute.org/what-is-wellness/

Physical: Nourishing a healthy body through exercise, nutrition, sleep, etc.
Mental: Engaging the world through learning, problem-solving, creativity, etc.
Emotional: Being aware of, accepting and expressing our feelings, and understanding the feelings of others.
Spiritual: Searching for meaning and higher purpose in human existence.
Social: Connecting and engaging with others and our communities in meaningful ways.
Environmental: Fostering positive interrelationships between planetary health and human actions, choices and wellbeing
"""
WELLNESS_CATEGORIES = ["Physical", "Mental",
                       "Emotional", "Spiritual", "Social", "Environmental"]

WELLNESS_ACTIONS_FILE = ABSPATH + "/data/actions.json"


class category_choice:
    def __init__(self, category_strs):
        self.scores = dict.fromkeys(category_strs, 0)

    def normalize_score(self):
        minimum = min(self.scores.values())
        maximum = max(self.scores.values())
        if (maximum - minimum) != 0:
            for i in self.scores:
                self.scores[i] =  (self.scores[i] - minimum) / (maximum - minimum)

        suma = sum(self.scores.values())
        if suma == 0:
            suma = len(self.scores)

        if all([(x == 0) for x in self.scores.values()]):
            for i in self.scores:
                self.scores[i] = 1

        for i in self.scores:
            self.scores[i] = self.scores[i] / suma
        print(self.scores)

    def sum_scores(self):
        return sum(self.scores.values())

    def select_categories(self, dictionary_input, amount=-1, prob=1):
        self.compute_priorities(dictionary_input)
        wellness_logger.debug("Scores: " + json.dumps(self.scores, indent=2))

        # numpy.random.choice(
        #        numpy.array(list(self.scores.keys())),
        #        p=numpy.array(list(self.scores.values())).astype(float),
        #        replace=False,
        #        size=len(self.scores)
        #    )

        categories = list(z for z in sorted(
            self.scores.items(), key=lambda x: x[1], reverse=True))

        # wellness_logger.debug("Ordered: ", categories)

        suma = 0
        selected = 0
        while suma < prob and selected < len(categories):
            suma += categories[selected][1]
            selected += 1

        categories = categories[:selected]

        if (amount > 0):
            categories = list(numpy.random.choice(
                numpy.array(list(i[0] for i in categories)),
                p=numpy.array(list(i[1] for i in categories)).astype(float),
                replace=False,
                size=min(amount, len(categories))
            ))  # categories[:min(amount, len(categories))]
        else:
            categories = [i[0] for i in categories]

        wellness_logger.debug("Chosen categories: " + json.dumps(categories, indent=2))
        return categories

    def compute_priorities(self, input):
        if "StressLevel" in input:
            if input["StressLevel"] > 0.7:
                self.scores["Physical"] += 2

        if "Diseases" in input:
            for disease in input["Diseases"]:
                if disease == "Osteoporosis":
                    self.scores["Physical"] -= 10
        
        self.normalize_score()


def compute_category_score(query, entry):
    score = 0
    for cat in entry['Categories']:
        if cat in query['Categories']:
            score += 1
        else:
            score -= 0.5
    return score

def compute_rule_score(query, entry):
    # all rules must pass in order to return >=0
    for rule in entry['Rules']:
        try:
            passed = eval(rule)
        except:
            #traceback.print_exc()
            wellness_logger.debug("Rule failed: " + json.dumps(rule, indent=2))
            return -float('inf')
        wellness_logger.debug("Rule: " + json.dumps(rule, indent=2) + "\nWith status: " + json.dumps(passed, indent=2) + "\n--------")
        if passed:
            continue
        else:
            return -float('inf')
    wellness_logger.debug("All rules passed")
    return len(entry['Rules'])

def compute_score(query, entry):
    return compute_category_score(query, entry) + compute_rule_score(query, entry) * 5

def choose_action(query):
    wellness_logger.debug("Query: " + json.dumps(query, indent=2))
    # query is a dict
    try:
        with open(WELLNESS_ACTIONS_FILE) as f:
            action_db = json.load(f)
    except:
        wellness_logger.debug("Couldn't open wellness action json file")
        # abort(500)
        return None

    best_matched_scores = (-float('inf'), [])

    for entry in action_db:
        wellness_logger.debug("\n==========\nEvaluating entry:" + json.dumps(entry, indent=2))

        try:
            score = compute_score(query, entry)
        except:
            wellness_logger.debug("Couldn't compute score for '" +
                  json.dumps(entry, indent=2), "' + skipping it")
            continue

        if score > best_matched_scores[0]:
            best_matched_scores = (score, [entry])
            wellness_logger.debug("Entry max updated, with score " + json.dumps(score, indent=2))
        elif score == best_matched_scores[0]:
            best_matched_scores[1].append(entry)
            wellness_logger.debug("Entry added, with score " + json.dumps(score, indent=2))
        else:
            wellness_logger.debug("Entry failed because of score: " + json.dumps(score, indent=2) + " less than current max: " + json.dumps(best_matched_scores[0], indent=2))
    action = numpy.random.choice(best_matched_scores[1])
    return format_response(action, query)

def format_response(action, query):
    rsp = action
    del rsp['Rules']
    #TODO replace {?} in text with parameter calls
    rsp["Action"].pop("Parameters", None)
    return rsp


def get(dictionary):
    wellness_scores = category_choice(WELLNESS_CATEGORIES)

    if "Categories" in dictionary:
        # load requested categories
        if isinstance(dictionary["Categories"], list) and\
                all((isinstance(v, str) and v in WELLNESS_CATEGORIES) for v in dictionary["Categories"].values()):
            categories = dictionary["Categories"]
        else:
            return None
    else:
        categories = wellness_scores.select_categories(dictionary, prob=0.7)

    # add selected Categories to query
    dictionary['Categories'] = categories

    recommendation = choose_action(dictionary)

    return recommendation
