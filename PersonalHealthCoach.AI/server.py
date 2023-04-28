from flask import Flask, jsonify, request, abort
#from collections import OrderedDict
import json

app = Flask(__name__)

def functionWrapper(fn):
    def fun():
        if not request.is_json:
            print("not JSON MIME type")
            abort(400)

        incoming = request.json
        print("\n=====\nIncoming:\n" + incoming.__str__())

        try:
            dictionary = json.loads(incoming)
        except json.decoder.JSONDecodeError as e:
            print(e.pos, e.msg)
            print("JSON decode error")
            abort(400)

        if dictionary is None:
            print("Dict is none")
            abort(503)
        
        response = fn(dictionary)
        if response is None:
            print("No response")
            abort(422)

        return jsonify(response)
    
    return fun


import FitnessPlanner.workout_endpoint
app.add_url_rule("/FitnessPlanner", "FitnessPlanner", functionWrapper(FitnessPlanner.workout_endpoint.generate_workout_endpoint) , None, methods=['POST'])
import TipGenerator.main
app.add_url_rule("/TipGenerator", "TipGenerator", functionWrapper(TipGenerator.main.tip) , None, methods=['POST'])
import DietPlanner.DietRequest
app.add_url_rule("/DietPlanner", "DietPlanner", functionWrapper(DietPlanner.DietRequest.getDiet) , None, methods=['POST'])
import Wellness.main
app.add_url_rule("/Wellness", "Wellness", functionWrapper(Wellness.main.get) , None, methods=['POST'])

if __name__ == '__main__':
    app.run(host='0.0.0.0', port='8000', debug=True)