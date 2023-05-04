import requests
import json

import os
abspath = os.path.dirname(__file__)

class ResetAccounts:
    def __init__(self):
        with open (f"{abspath}/accountsReset.json","r") as file:
            data = json.load(file)
        with open (f"{abspath}/accounts.json","w") as file:
            json.dump(data,file,indent=2)

class LeastUsedKey:
    def __init__(self):
        self
    def getMinKey(self):
        with open (f"{abspath}/accounts.json","r") as file:
            data = json.load(file)

        min = data["accounts"][0]["requests"]
        for row in data["accounts"]:
            if row["requests"] > min:
                min = row["requests"]
        if min < 5:
            return "-1"
        for row in data["accounts"]:
            if row["requests"] == min:
                return (row["key"])
class UpdateKey:
    def __init__(self,key,usedData):
        self
        self.key = key     
        self.used = float(usedData)
        with open (f"{abspath}/accounts.json","r") as file:
            data = json.load(file)
        for row in data["accounts"]:
            if row["key"] == key:
                row["requests"] = self.used
        with open (f"{abspath}/accounts.json","w") as file:
            json.dump(data,file,indent=2)        
