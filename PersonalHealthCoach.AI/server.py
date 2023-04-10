from flask import Flask, request, abort
#from collections import OrderedDict
import json

app = Flask(__name__)

def functionWrapper(fn):
    def fun():
        incoming = request.get_data()
        print(incoming)
        if incoming != b'':
            try:
                dict = json.loads(incoming) #object_pairs_hook=OrderedDict
            except json.decoder.JSONDecodeError:
                abort(400)
        else:
            dict = {}
            
        if (dict != None):
            response = fn(dict)
            if response != None:
                return json.dumps(response)
            else :
                abort(400)
        else :
            abort(400)
    return fun


import FitnessPlanner.workout_endpoint

import TipGenerator.main
app.add_url_rule("/TipGenerator", "TipGenerator", functionWrapper(TipGenerator.main.tip) , None, methods=['POST'])
import DietPlanner.SendRequest
app.add_url_rule("/DietPlanner", "DietPlanner", functionWrapper(DietPlanner.SendRequest.getMeal) , None, methods=['POST'])

if __name__ == '__main__':
    app.run(host='0.0.0.0', port='8000', debug=True)