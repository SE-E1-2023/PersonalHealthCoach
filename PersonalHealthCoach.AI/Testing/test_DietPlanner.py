import TestUtils as tu
import json
import os
import requests
import unittest
from unittest.mock import patch, mock_open



abspath = os.path.dirname(__file__)


def test_no_params():
    response = tu.get_response("DietPlanner", "{}")
    assert response.status_code != 500

def test_example():
    with open (f"{abspath}/../DietPlanner/RequestType/request.json","r") as file:
        inp = json.load(file)

    print(tu.get_response("DietPlanner",inp).content)

    response = tu.get_response("DietPlanner", inp) 

    print(response.status_code)

    assert response.status_code == 200

    diet = json.loads(response.content)
    

    with open (f"{abspath}/../DietPlanner/RequestType/requestResponse.json","w") as file:
        json.dump(diet,file, indent=2)

#test_example()

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
        result = tu.get_response("DietPlanner",input).json()
        
        self.assertEqual(result["NOP"], 10)
    def test_negative2(self):
        input = {
        "idClient" : "4",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = tu.get_response("DietPlanner",input).json()
        
        self.assertEqual(result["NOP"], 11)
    def test_negative3(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = tu.get_response("DietPlanner",input).json()
        
        self.assertEqual(result["NOP"], 12)
    def test_negative4(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "goal" : "weight loss"
        }
        result = tu.get_response("DietPlanner",input).json()
        
        self.assertEqual(result["NOP"], 13)
    def test_negative5(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        }
        result = tu.get_response("DietPlanner",input).json()
        
        self.assertEqual(result["NOP"], 14)
    def test_pozitive(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = tu.get_response("DietPlanner",input).json()
        self.assertEqual(result["NOP"], 0)
    def test_negative6(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan","vegetarian","diaryFree"],
        "goal" : "weight loss"
        }
        result = tu.get_response("DietPlanner",input).json()
        self.assertEqual(result["NOP"], 1)
    def test_pozitive2(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan"],
        "goal" : "weight loss"
        }
        result = tu.get_response("DietPlanner",input).json()
        self.assertEqual(result["NOP"],0)
    def test_negative7(self):
        input = {
        "idClient" : "4",
        "requestType" : "diet",
        "alergies" : ["nut","milk"],
        "dietType" : ["vegan","vegetarian","dieryFree","glutenFree"],
        "goal" : "weight loss"
        }       
        result = tu.get_response("DietPlanner",input).json()
        self.assertEqual(result["NOP"],1)

def autoTest(): 
    if __name__ == '__main__':
        unittest.main()

def testFunction():
    # astea is pentru debughing, nu au traba cu programul mare
    with open (f"{abspath}/RequestType/request.json","r") as file:
        inp = json.load(file)

    diet = tu.get_response("DietPlanner",inp).json()

    with open (f"{abspath}/RequestType/requestResponse.json","w") as file:
        json.dump(diet,file, indent=2)


#testFunction()
#autoTest()


