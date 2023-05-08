import json
from urllib.parse import urlparse
from collections import OrderedDict

def to_title_case(s):
    words = s.split(' ')
    return ' '.join(word.capitalize() for word in words)

with open("exercise_database.json", "r") as f:
    exercise_database = json.load(f)

updated_exercise_database = []
for exercise in exercise_database:
    ordered_exercise = OrderedDict()
    url = exercise["url"]
    parsed_url = urlparse(url)
    exercise_name = parsed_url.path.split("/")[-1].replace("-", " ")
    title_case_name = to_title_case(exercise_name)
   
    ordered_exercise["url"] = url
    ordered_exercise["name"] = title_case_name
    for key, value in exercise.items():
        if key != "url":
            ordered_exercise[key] = value
            
    updated_exercise_database.append(ordered_exercise)

with open("exercise_database_updated.json", "w") as f:
    json.dump(updated_exercise_database, f, indent=2)
