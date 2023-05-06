from flask import Flask, jsonify, request, abort, make_response
#from collections import OrderedDict
import json
import traceback

app = Flask(__name__)

def functionWrapper(fn):
    def fun():
        dictionary = None

        if not request.is_json:
            print("not JSON MIME type")
            abort(400)

        incoming = request.json
        print("\n=====\nIncoming:\n" + incoming.__str__())

        try:
            if type(incoming) is not dict:
                dictionary = json.loads(incoming)
            else:
                dictionary = incoming
        except json.decoder.JSONDecodeError as e:
            print(e.pos, e.msg)
            print("JSON decode error")
            abort(400)

        if dictionary is None:
            print("Dict is none")
            abort(503)
        
        try:
            response = fn(dictionary)
        except Exception as e:
            print("Internal exception: ", e )
            traceback.print_exc()
            abort(500)
        
        if response is None:
            print("No response")
            abort(400)

        if '__message__' in response:
            if '__status__' in response:
                return make_response(response['__message__'], response['__status__'])
            else:
                return make_response(response['__message__'], 400)
        elif isinstance(response, str):
            return make_response(response, 400)

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