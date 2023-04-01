from functools import wraps
from flask import Flask, request, jsonify, abort
from flask_cors import CORS
import json

# Function to load the workout data from a JSON file based on the workout_id
def load_workout(workout_id):
    filename = f"workout#{workout_id}.json"
    with open(filename, "r") as f:
        workout_data = json.load(f)
    return workout_data
    
app = Flask(__name__)
CORS(app)  # This will enable CORS for all routes

# Decorator function to enforce IP whitelisting
def ip_whitelist(allowed_ips):
    def decorator(func):
        @wraps(func)
        def wrapper(*args, **kwargs):
            client_ip = request.remote_addr
            if client_ip not in allowed_ips:
                abort(403)  # Forbidden
            return func(*args, **kwargs)
        return wrapper
    return decorator

# Replace with the IP addresses you want to whitelist, including '127.0.0.1' for localhost
allowed_ips = ['127.0.0.1']

# Route to handle requests and return workout data based on the provided workout_id
@app.route('/api', methods=['POST'])
@ip_whitelist(allowed_ips)
def process_data():
    data = request.json
    
    workout_id = data.get("workout_id")
    if not workout_id:
        abort(400)  # Bad Request
    try:
        workout_data = load_workout(workout_id)
    except FileNotFoundError:
        abort(404)  # Not Found

    # You can process the received data here and create a response
    result = {"status": "success", "message": "Workout generated", "workout": workout_data}

    return jsonify(result)

if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
