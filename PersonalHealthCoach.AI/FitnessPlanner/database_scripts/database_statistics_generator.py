import json
from collections import defaultdict


def calculate_exercise_distribution(category_dict, key):
    distribution = defaultdict(lambda: defaultdict(int))
    for category, exercises in category_dict.items():
        for exercise in exercises:
            distribution[category][exercise[key]] += 1
    return distribution


def ensure_all_fields(data, fields):
    for field in fields:
        if field not in data:
            data[field] = 0
    return data


with open('exercise_database.json', 'r') as f:
    exercises = json.load(f)

type_dict = defaultdict(list)
main_muscle_dict = defaultdict(list)
equipment_dict = defaultdict(list)
level_dict = defaultdict(list)

for exercise in exercises:
    type_dict[exercise['type']].append(exercise)
    main_muscle_dict[exercise['main_muscle']].append(exercise)
    equipment_dict[exercise['equipment']].append(exercise)
    level_dict[exercise['level']].append(exercise)

other_dicts_main_muscle = {
    'score_distribution': calculate_exercise_distribution(main_muscle_dict, 'level'),
    'equipment_distribution': calculate_exercise_distribution(main_muscle_dict, 'equipment'),
    'type_distribution': calculate_exercise_distribution(main_muscle_dict, 'type')
}

fields_level = ['Beginner', 'Intermediate', 'Expert']
fields_equipment = ['Other', 'Body Only', 'Dumbbell', 'Cable', 'Medicine Ball', 'Barbell', 'Exercise Ball', 'None', 'Kettlebells', 'Machine', 'Bands']

def calculate_statistics(category_dict, other_dicts=None):
    stats = {}
    for category, exercises in category_dict.items():
        total_exercises = len(exercises)
        total_score = sum(exercise['score'] for exercise in exercises)
        min_score = min(exercise['score'] for exercise in exercises)
        max_score = max(exercise['score'] for exercise in exercises)
        avg_score = total_score / total_exercises
        above_threshold_count = sum(1 for exercise in exercises if exercise['score'] > 9.0)

        stats[category] = {
            'total_exercises': total_exercises,
            'score_distribution': {
                'min_score': min_score,
                'max_score': max_score,
                'average_score': avg_score,
                'exercises_above_threshold': above_threshold_count
            }
        }
        if other_dicts:
            for other_key, other_dict in other_dicts.items():
                if other_key == 'difficulty_distribution':
                    stats[category][other_key] = ensure_all_fields(other_dict[category], fields_level)
                elif other_key == 'equipment_distribution':
                    stats[category][other_key] = ensure_all_fields(other_dict[category], fields_equipment)
                elif other_key == 'muscle_group_distribution':
                    stats[category][other_key] = ensure_all_fields(other_dict[category], main_muscle_dict.keys())
                elif other_key == 'type_distribution':
                    stats[category][other_key] = ensure_all_fields(other_dict[category], type_dict.keys())

    return stats

other_dicts_main_muscle = {
    'difficulty_distribution': calculate_exercise_distribution(main_muscle_dict, 'level'),
    'equipment_distribution': calculate_exercise_distribution(main_muscle_dict, 'equipment'),
    'type_distribution': calculate_exercise_distribution(main_muscle_dict, 'type')
}

other_dicts_level = {
    'muscle_group_distribution': calculate_exercise_distribution(level_dict, 'main_muscle'),
    'type_distribution': calculate_exercise_distribution(level_dict, 'type'),
    'equipment_distribution': calculate_exercise_distribution(level_dict, 'equipment')
}

other_dicts_type = {
    'muscle_group_distribution': calculate_exercise_distribution(type_dict, 'main_muscle'),
    'equipment_distribution': calculate_exercise_distribution(type_dict, 'equipment'),
    'difficulty_distribution': calculate_exercise_distribution(type_dict, 'level')
}
other_dicts_equipment = {
    'muscle_group_distribution': calculate_exercise_distribution(equipment_dict, 'main_muscle'),
    'type_distribution': calculate_exercise_distribution(equipment_dict, 'type'),
    'difficulty_distribution': calculate_exercise_distribution(equipment_dict, 'level')
}
type_stats = calculate_statistics(type_dict, other_dicts_type)
main_muscle_stats = calculate_statistics(main_muscle_dict, other_dicts_main_muscle)
equipment_stats = calculate_statistics(equipment_dict, other_dicts_equipment)
level_stats = calculate_statistics(level_dict, other_dicts_level)


for level, exercises in level_dict.items():
    level_stats[level]['score_distribution']['average_score'] = sum(exercise['score'] for exercise in exercises) / len(exercises)

import os

with open(os.path.join(os.path.join(os.path.join('.',"Data"), "Statistics"), 'type_statistics.json'), 'w') as f:
    json.dump(type_stats, f, indent=2)

with open(os.path.join(os.path.join(os.path.join('.',"Data"), "Statistics"), 'main_muscle_statistics.json'), 'w') as f:
    json.dump(main_muscle_stats, f, indent=2)

with open(os.path.join(os.path.join(os.path.join('.',"Data"), "Statistics"), 'level_statistics.json'), 'w') as f:
    json.dump(level_stats, f, indent=2)

with open(os.path.join(os.path.join(os.path.join('.',"Data"), "Statistics"), 'equipment_statistics.json'), 'w') as f:
    json.dump(equipment_stats, f, indent=2)



