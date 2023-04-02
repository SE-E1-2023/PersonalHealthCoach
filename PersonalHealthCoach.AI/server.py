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


import FitnessPlanner.FitnessPlanner.workout_endpoint
app.add_url_rule("/FitnessPlanner", "FitnessPlanner", FitnessPlanner.FitnessPlanner.workout_endpoint.process_data , None, methods=['POST'])
import TipGenerator.main
app.add_url_rule("/TipGenerator", "TipGenerator", functionWrapper(TipGenerator.main.tip) , None, methods=['POST'])
import DietPlanner.SendRequest
app.add_url_rule("/DietPlanner", "DietPlanner", functionWrapper(DietPlanner.SendRequest.getMeal) , None, methods=['POST'])

if __name__ == '__main__':
    app.run(host='0.0.0.0', port='8000', debug=True)