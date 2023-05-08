import os
import json
abspath = os.path.dirname(__file__)
class modifyAccount:
    def __init__(self,id):
        self
        self.id = id
        with open ( os.path.join(abspath, "AccountManager.py"),"r") as file:
            data = json.load(file)
    