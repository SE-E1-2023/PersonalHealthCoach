import requests
import json

import os
import random
abspath = os.path.dirname(__file__)

def matchingDiet(diet):
    with open (f"{abspath}/Databases/Diets.json","r") as file:
        data = json.load(file)
    usefulData = []
    for d in data["diets"]:
       
        check = True
        for element in diet:
            if element not in d["tag"]:
                check = False
        if check:
            usefulData.append(d)
    return usefulData

def newDiet(dietType,id):
    dietData = matchingDiet(dietType)
    for diet in dietData:
        if diet["id"] != id:
            return diet
    if len(dietData) != 0 :
        return dietData[0]
    return {}
def knownUser(input):
    with open (f"{abspath}/Databases/Users.json","r") as file:
        users = json.load(file)
    userIndex = -1
    for usr in users["users"]:
        userIndex += 1
        if usr["idUser"] == input["idClient"]:
            break
    response = {}
    response["diet"]= newDiet(input["dietType"],users["users"][userIndex]["idDiet"])
    if len(response["diet"]) == 0:
        r={}
        r["NOP"] = 1   
        return r
    users["users"][userIndex]["idDiet"] = response["diet"]["id"]
    response["breakfast"] = getMeal(input,users["users"][userIndex]["idBreakfast"],"Breakfast.json")
    response["drink"] = getMeal(input,users["users"][userIndex]["idDrink"],"Drink.json")
    response["mainCourse"] = getMeal(input,users["users"][userIndex]["idMainCourse"],"MainCourse.json")
    response["sideDish"] = getMeal(input,users["users"][userIndex]["idSideDish"],"SideDish.json")
    response["soup"] = getMeal(input,users["users"][userIndex]["idSnack"],"Soup.json")
    response["snack"] = getMeal(input,users["users"][userIndex]["idSoup"],"Snack.json")
    users["users"][userIndex]["idBreakfast"] = response["breakfast"]["id"]
    users["users"][userIndex]["idDrink"] = response["drink"]["id"]
    users["users"][userIndex]["idMainCourse"] = response["mainCourse"]["id"]
    users["users"][userIndex]["idSideDish"] = response["sideDish"]["id"]
    users["users"][userIndex]["idSnack"] = response["soup"]["id"]
    users["users"][userIndex]["idSoup"] = response["snack"]["id"]
    with open (f"{abspath}/Databases/Users.json","w") as file:
        json.dump(users,file,indent=2) 
    response["NOP"] = 0
    return response


def unknownUser(input):
    userData={"idUser":input["idClient"]}
    with open (f"{abspath}/Databases/Users.json","r") as file:
        u = json.load(file)
    userData["idDiet"]=-1
    userData["idBreakfast"]=-1
    userData["idDrink"]=-1
    userData["idMainCourse"]=-1
    userData["idSideDish"]=-1
    userData["idSnack"]=-1
    userData["idSoup"]=-1
    u["users"].append(userData)
    with open (f"{abspath}/Databases/Users.json","w") as file:
        json.dump(u,file,indent=2)
    return knownUser(input)


def getDiet(input):
    with open (f"{abspath}/Databases/Users.json","r") as file:
        users = json.load(file)
    good = 0
    returnedRequest={}
    for user in users["users"]:
        if input["idClient"] == user["idUser"]:
            returnedRequest = knownUser(input)
            good = 1
            break
    if good == 0:
        returnedRequest = unknownUser(input)
    return returnedRequest

def getMeal(info,idMeal,path):
    with open (f"{abspath}/Databases/{path}","r") as file:
        d = json.load(file)
    minHealthiness = 0
    maxHealthiness = 100
    if (info["goal"] == "weight loss"):
        minHealthiness = 20
    if (info["goal"] == "weight gain"):
        maxHealthiness = 20
    data = d["meals"]
    goodData = []
    good = True
    for meal in data:
        good = True
        for problem in info["dietType"]:
            if meal[problem] == False:
                good = False
        for alergi in info["alergies"]:
            if alergi in meal["ingredients"] : 
                good = False
        if meal["healthScore"] < minHealthiness or meal["healthScore"] > maxHealthiness :
            good = False
        if good:
            goodData.append(meal)
    dataReturn={}
    if (len(goodData)) == 1 :
        dataReturn["id"] = goodData[0]["id"]
        dataReturn["title"] = goodData[0]["title"]
        dataReturn["image"] = goodData[0]["image"]
        dataReturn["ingredients"] = goodData[0]["ingredients"]
        dataReturn["kcal"] = goodData[0]["nutrition"][0]["amount"]
        return dataReturn
    while True :
        index = random.randint(0,len(goodData)-1)
        if (goodData[index]["id"] != idMeal):
            dataReturn["id"] = goodData[index]["id"]
            dataReturn["title"] = goodData[index]["title"]
            dataReturn["image"] = goodData[index]["image"]
            dataReturn["ingredients"] = goodData[index]["ingredients"]
            dataReturn["kcal"] = goodData[index]["nutrition"][0]["amount"]
            return dataReturn
        
        

def testFunction():
    # astea is pentru debughing, nu au traba cu programul mare
    with open (f"{abspath}/RequestType/request.json","r") as file:
        inp = json.load(file)

    diet = getDiet(inp)

    with open (f"{abspath}/RequestType/requestResponse.json","w") as file:
        json.dump(diet,file, indent=2)

#testFunction()

