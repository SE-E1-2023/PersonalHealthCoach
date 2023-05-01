import requests
import json

import os
import random
import unittest
import DietRequest
from unittest.mock import patch, mock_open
from DietRequest import getDiet

abspath = os.path.dirname(__file__)

class TestStringMethods(unittest.TestCase):
    # test function to test equality of two value
    def test_negative1(self):
        input = {
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        
        self.assertEqual(result["NOP"], 10)
    def test_negative2(self):
        input = {
        "idClient" : "4",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        
        self.assertEqual(result["NOP"], 11)
    def test_negative3(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        
        self.assertEqual(result["NOP"], 12)
    def test_negative4(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        
        self.assertEqual(result["NOP"], 13)
    def test_negative5(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        }
        result = getDiet(input)
        
        self.assertEqual(result["NOP"], 14)
    def test_pozitive(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        self.assertEqual(result["NOP"], 0)
    def test_negative6(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan","vegetarian","diaryFree"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        self.assertEqual(result["NOP"], 1)
    def test_pozitive2(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        self.assertEqual(result["NOP"],0)
    def test_negative7(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan","vegetarian","dieryFree","glutenFree"],
        "goal" : "weight loss"
        }       
        result = getDiet(input)
        self.assertEqual(result["NOP"],1)

def autoTest(): 
    if __name__ == '__main__':
        unittest.main()

def testFunction():
    # astea is pentru debughing, nu au traba cu programul mare
    with open (f"{abspath}/RequestType/request.json","r") as file:
        inp = json.load(file)

    diet = getDiet(inp)

    with open (f"{abspath}/RequestType/requestResponse.json","w") as file:
        json.dump(diet,file, indent=2)


#testFunction()
autoTest()

