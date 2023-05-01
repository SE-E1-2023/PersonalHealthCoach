import TestUtils as tu
import json
import os
abspath = os.path.dirname(__file__)

def test_no_params():
    response = tu.get_response("DietPlanner", "{}")
    assert response.status_code != 500

def test_example():
    with open (f"{abspath}/../DietPlanner/RequestType/request.json","r") as file:
        inp = json.load(file)

    print(tu.get_response("DietPlanner",inp).content)

    response = tu.get_response("DietPlanner", inp) 

    print(response.status_code)

    assert response.status_code == 200

    diet = json.loads(response.content)
    

    with open (f"{abspath}/../DietPlanner/RequestType/requestResponse.json","w") as file:
        json.dump(diet,file, indent=2)

#test_example()