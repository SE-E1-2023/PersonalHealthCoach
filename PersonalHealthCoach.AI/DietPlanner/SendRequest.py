import importlib
#import GetApiKey as gk
import requests
import random

import os
#from KeyFunctions.GetApiKey import *

abspath = os.path.dirname(__file__)
import sys
sys.path.append(abspath)
gk = importlib.import_module("KeyFunctions.GetApiKey")

def getMeal(input):
    
    key = gk.LeastUsedKey().getMinKey()
    if (key == "-1"):  
        print ("Out of keys")
        exit()
    #api = "https://api.spoonacular.com/recipes/complexSearch?number=1"+"&offset="+str(random1)+"&apiKey="+key
    baseApi = "https://api.spoonacular.com/recipes/complexSearch?apiKey="+key
    try:
        r=requests.get(baseApi)
    except:
        return ("Connection Error")
    gk.UpdateKey(key,r.headers["X-API-Quota-Left"])   
    result = r.json()
    print (result)
    return result["results"][0]["title"]










