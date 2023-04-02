import importlib
#import GetApiKey as gk
import requests
import random

import os
abspath = os.path.dirname(__file__)
import sys
sys.path.append(abspath)
gk = importlib.import_module("GetApiKey")


def getMeal(input):
    mealTypes=["breakfast","launch","diner"]
    key = gk.LeastUsedKey().getMinKey()
    random1 = random.randint(0,999)
    api = "https://api.spoonacular.com/recipes/complexSearch?number=1"+"&offset="+str(random1)+"&apiKey="+key
    r=requests.get(api)
    result = r.json()
    return result["results"][0]["title"]