import requests
import json

import os
abspath = os.path.dirname(__file__)

def matchingDiet(diet):
    with open (f"{abspath}/Databases/Diets.json","r") as file:
        data = json.load(file)
    usefulData = []
    for d in data["diets"]:
        if diet in d["tag"]:
            usefulData.append(d)
    return usefulData

def newDiet(dietType,id):
    dietData = matchingDiet(dietType)
    for diet in dietData:
        if diet["id"] != id:
            return diet
    return dietData[0]

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
    users["users"][userIndex]["idDiet"] = response["diet"]["id"]
    with open(f"{abspath}/Databases/Users.json","w") as file:
        json.dump(users,file,indent=2)
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
    knownUser(input)


def getDiet(input):
    with open (f"{abspath}/Databases/Users.json","r") as file:
        users = json.load(file)
    good = 0
    returndRequest={}
    for user in users["users"]:
        if input["idClient"] == user["idUser"]:
            returnedRequest = knownUser(input)
            good = 1
            break
    if good == 0:
        returnedRequest = unknownUser(input)
    
    return returnedRequest



# astea is pentru debughing, nu au traba cu programul mare
with open (f"{abspath}/RequestType/request.json","r") as file:
    inp = json.load(file)

diet = getDiet(inp)

with open (f"{abspath}/RequestType/requestResponse.json","w") as file:
    json.dump(diet,file, indent=2)
