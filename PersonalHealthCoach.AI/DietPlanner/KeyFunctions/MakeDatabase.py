import importlib
#import GetApiKey as gk
import requests
import random
import json
import os
from GetApiKey import UpdateKey

abspath = os.path.dirname(__file__)
import sys
sys.path.append(abspath)
gk = importlib.import_module("GetApiKey")

mealType = ["breakfast","mainCourse","sideDish","dessert","appetizer","salad","soup",
            "beverage","sauce","marinade","snack","drink"] #type
alergies = ["eggs","weat"]  #excludeIngredients
intolerances = ["dairy","egg","gluten","grain",
                "peanut","seafood","sesame","shellfish",
                "soy","treeNut","wheat"] #intolerances

dietType = ["glutenfree","vegetarian","vegan"] #diet


def getMeal():
    key = gk.LeastUsedKey().getMinKey()
    if (key == "-1"):  
        print ("Out of keys")
        
    #api = "https://api.spoonacular.com/recipes/complexSearch?number=1"+"&offset="+str(random1)+"&apiKey="+key
    baseApi = "https://api.spoonacular.com/recipes/complexSearch"
    payload={
        "type":"drink",
        "apiKey":key,
        "sort":"random",
        "addRecipeInformation":"true",
        "addRecipeNutrition":"true",
        "number":"100"
    }
    try:
        r=requests.get(baseApi,params=payload)
    except:
        return ("Connection Error")
    UpdateKey(key,r.headers["X-API-Quota-Left"])   
    result = r.json()
    
    dictList=[]
    newDict = {}
    for element in result["results"]:
        newDict = {}
        newDict["id"] = element["id"]
        newDict["title"] = element["title"]
        newDict["vegetarian"] = element["vegetarian"]
        newDict["dairyFree"] = element["dairyFree"]
        newDict["glutenFree"] = element["glutenFree"]
        newDict["vegan"] = element["vegan"]
        newDict["healthScore"] = element["healthScore"]
        newDict["image"] = element["image"]
        newDict["nutrition"]=element["nutrition"]["nutrients"]
        newList = []
        for ingredient in element["nutrition"]["ingredients"]:
            newList.append(ingredient["name"])
        newDict["ingredients"] = newList
        dictList.append(newDict)
    return dictList
    #with open (f"{abspath}/MainCourse2.json","w") as file:
    #       json.dump(dictList,file,indent=2) 
    
    
data=[]
for i in range(10):
    data.extend(getMeal())
dict = {}
for i in range(data.__len__()-2):
    for j in range(i+1,data.__len__()-1):
        try:
            if (data[i]["id"] == data[j]["id"]):
                del data[j]
                j = j-1
        except:
            print(1)

dict["meals"] = data
with open (f"{abspath}/Drink.json","w") as file:
   json.dump(dict,file,indent=2) 
