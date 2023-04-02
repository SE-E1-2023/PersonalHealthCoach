import requests
import json

class ResetAccounts:
    def __init__(self):
        with open ("accountsReset.json","r") as file:
            data = json.load(file)
        with open ("accounts.json","w") as file:
            json.dump(data,file,indent=2)

class LeastUsedKey:
    def __init__(self):
        self
    def getMinKey(self):
        with open ("accounts.json","r") as file:
            data = json.load(file)

        min = data["accounts"][0]["requests"]
        for row in data["accounts"]:
            if row["requests"] < min:
                min = row["requests"]
        for row in data["accounts"]:
            if row["requests"] == min:
                row["requests"] += 1
                with open ("accounts.json","w") as file:
                    json.dump(data,file,indent=2)
                return (row["key"])
                
class Request:
    def __init__(self,type,restrictions):
        self.type = type
        self.restrictions = restrictions
