import os
import json
abspath = os.path.dirname(__file__)
class modifyAccount:
    def __init__(self,id):
        self
        self.id = id
        with open (f"{abspath}/AccountManager.py","r") as file:
            data = json.load(file)
    