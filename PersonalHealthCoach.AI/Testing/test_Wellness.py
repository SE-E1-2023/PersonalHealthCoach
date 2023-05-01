import TestUtils as tu
import json

def test_no_params():
    response = tu.get_response("Wellness", "{}")
    assert response.status_code != 500
    wellness_nice_print(response.json())

def wellness_nice_print(d):
    print("Categories: ",d['Categories'],'\n')
    print(d['Action']['Title'])
    print(d['Action']['Description'], '\n')
    print("Multipliers: ", d['Multipliers'])

def test_disease_params():
    query = {"Diseases": ['Osteoporosis']}
    response = tu.get_response("Wellness", json.dumps(query))
    assert response.status_code != 500
    wellness_nice_print(response.json())

def test_invalid_data():
    query = {"Categories": ['Moado', "vksapDFS", "{}dsf", "Mental"]}
    response = tu.get_response("Wellness", json.dumps(query))
    assert response.status_code == 422
#test_no_params()