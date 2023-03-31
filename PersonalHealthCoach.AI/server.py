from flask import Flask, request
#from collections import OrderedDict
import json

app = Flask(__name__)

def functionWrapper(fn):
    def fun():
        incoming = request.get_data()
        print(incoming)
        if incoming != b'':
            dict = json.loads(incoming) #object_pairs_hook=OrderedDict
        else:
            dict = {}
            
        if (dict != None):
            response = fn(dict)
            return json.dumps(response)
        else :
            return "Too Bad"
    return fun

modules = ["TipGenerator", "DietPlanner", "FitnessPlanner"]

for module in modules:
    try:
        exec(f"import {module}.main")
        exec(f"app.add_url_rule(\"/{module}\", \"{module}\", functionWrapper({module}.main.main) , None, methods=['POST'])")
    except ModuleNotFoundError:
        print(f"Module {module} not imported")
        pass

if __name__ == '__main__':
    app.run(host='0.0.0.0', port='8000', debug=True)