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
        
        self.assertEqual(result,"no idClient provided" )
    def test_negative2(self):
        input = {
        "idClient" : "4",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        
        self.assertEqual(result, "no requestType provided")
    def test_negative3(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        
        self.assertEqual(result, "no alergies provided")
    def test_negative4(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        
        self.assertEqual(result, "no dietType provided")
    def test_negative5(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        }
        result = getDiet(input)
        
        self.assertEqual(result, "no goal provided")
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
        self.assertEqual(result,"unknown diet")
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
        "requestType" : "soup",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan","vegetarian","dairyFree","glutenFree"],
        "goal" : "weight loss"
        }       
        result = getDiet(input)
        self.assertEqual(result["NOP"],0)
    def test_negative8(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegn"],
        "goal" : "weight loss"
        }       
        result = getDiet(input)
        self.assertEqual(result,"unknown diet")
    def test_negative9(self):
        input = {
        "idClient" : "4",
        "requestType" : "sooop",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        self.assertEqual(result,"unknown food type")
    def test_positive3(self):
        input = {
        "idClient" : "4",
        "requestType" : "soup",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        self.assertEqual(result["NOP"],0)
    
    def test_pozitive39(self):
        input = {
        "idClient" : "4",
        "requestType" : "soup",
        "alergies" : ["nut","milk"],
        "dietType" : ["dairyFree"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        self.assertEqual(result["NOP"],0)
    def test_unknownUser1(self):
        input = {
        "idClient" : "4",
        "requestType" : "soup",
        "alergies" : ["nut","milk"],
        "dietType" : ["dairyFree"],
        "goal" : "weight loss"
        }
        result = getDiet(input)
        self.assertEqual(result["NOP"],0)



def autoTest(): 
    if __name__ == '__main__':
        unittest.main()

def testFunction():
    # astea is pentru debughing, nu au traba cu programul mare
    with open (os.path.join(os.path.join(abspath,"RequestType"),"request.json"),"r") as file:
        inp = json.load(file)

    diet = getDiet(inp)

    with open (os.path.join(os.path.join(abspath,"RequestType"),"requestResponse.json"),"w") as file:
        json.dump(diet,file, indent=2)


#testFunction()
autoTest()


