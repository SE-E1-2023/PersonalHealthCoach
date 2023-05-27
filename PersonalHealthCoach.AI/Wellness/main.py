#!/usr/bin/env python
import numpy
import json
import random
# from flask import abort
import traceback
import os
ABSPATH = os.path.dirname(__file__)
import logging
wellness_logger = logging.getLogger(__name__)
wellness_logger.setLevel(logging.DEBUG)
wellness_logger.addHandler(logging.FileHandler(os.path.join(ABSPATH, 'wellness.log')))
"""
Source: https://globalwellnessinstitute.org/what-is-wellness/

Physical: Nourishing a healthy body through exercise, nutrition, sleep, etc.
Mental: Engaging the world through learning, problem-solving, creativity, etc.
Emotional: Being aware of, accepting and expressing our feelings, and understanding the feelings of others.
Spiritual: Searching for meaning and higher purpose in human existence.
Social: Connecting and engaging with others and our communities in meaningful ways.
Environmental: Fostering positive interrelationships between planetary health and human actions, choices and wellbeing
"""
WELLNESS_CATEGORIES = ["Physical", "Mental", "Emotional", "Spiritual", "Social", "Environmental"]

WELLNESS_ACTIONS_FILE = os.path.join(os.path.join(ABSPATH, 'data'), 'actions.json')
WELLNESS_PHYS_DISEASES_FILE = os.path.join(os.path.join(ABSPATH, 'data'), 'phys_diseases.json')


class category_choice:
    def __init__(self, category_strs):
        self.scores = dict.fromkeys(category_strs, 0)

    def compute_score(self, topic):
        score = 0
        for category in topic['Categories']:
            score += self.scores[category]
        return score

    def compute_priorities(self, input):
        if "StressLevel" in input:
            if input["StressLevel"] > 0.7:
                self.scores["Physical"] += 2

        if "Diseases" in input:
            try:
                with open(WELLNESS_PHYS_DISEASES_FILE) as f:
                    disease_db = json.load(f)

                for disease in input["Diseases"]:
                    if disease in disease_db:
                        self.scores["Physical"] -= 100
            except:
                wellness_logger.debug("Couldn't open phys_disease json file")

            
    def choose_topic(self, query):
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
        #epsilon = 5

        for entry in action_db:
            wellness_logger.debug("\n==========\nEvaluating entry:" + json.dumps(entry, indent=2))

            try:
                score = self.compute_score(entry)
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

        return format_response(action)

def format_response(action):
    rsp = {}

    rsp['Categories'] = action['Categories']
    if 'Subchoice' not in action['Action']:
        rsp['Action'] = {}
        rsp['Action']['Title'] = action['Action']['Title']
        rsp['Action']['Description'] = action['Action']['Description']
    else:
        rsp['Action'] = random.choices(action["Action"]["Subchoice"]["Choices"])[0]

    return rsp

def get(dictionary):
    wellness_scores = category_choice(WELLNESS_CATEGORIES)
    wellness_scores.compute_priorities(dictionary)
    recommendation = wellness_scores.choose_topic(dictionary)
    return recommendation