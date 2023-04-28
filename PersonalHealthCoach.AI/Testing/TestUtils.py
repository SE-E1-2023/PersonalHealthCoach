import requests

URL = 'http://localhost:8000'

def get_response(endpoint, json_to_send):
    new_url = URL + "/" + endpoint
    try:
        response = requests.post(new_url, json = json_to_send)
        return response
    except: 
        print("Error posting request\n")
        return None

def get_response_data(endpoint, data_to_send):
    new_url = URL + "/" + endpoint
    try:
        response = requests.post(new_url, data_to_send)
        return response
    except: 
        print("Error posting request\n")
        return None
