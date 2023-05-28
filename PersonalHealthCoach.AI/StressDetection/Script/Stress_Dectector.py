#Stress Dectector 
import time 
import random
import linecache

#Function that colect data about heart rate
def get_heart_rate():
    filename="heart_rates.txt"
    num_lines = sum(1 for _ in open(filename))
    random_line_number = random.randint(1, num_lines)
    random_line = linecache.getline(filename, random_line_number)
    heart_rate = int(random_line)
    return heart_rate

# Function to simulate breathing rate data
def get_breathing_rate():
    filename="breathing_rates.txt"
    num_lines = sum(1 for _ in open(filename))
    random_line_number = random.randint(1, num_lines)
    random_line = linecache.getline(filename, random_line_number)
    breathing_rate = int(random_line)
    return breathing_rate

# Function to simulate sleeping hours data
def get_sleeping_hours():
    filename="sleeping_hours.txt"
    num_lines = sum(1 for _ in open(filename))
    random_line_number = random.randint(1, num_lines)
    random_line = linecache.getline(filename, random_line_number)
    sleeping_hours = float(random_line)
    return sleeping_hours

#Detection of stress level
def detect_stress(heart_rate, breathing_rate , sleeping_hours):
    if heart_rate > 100 or breathing_rate > 20 or sleeping_hours < 5.5:
        return True
    else:
        return False

while True:
    # Get heart rate
    heart_rate = get_heart_rate()
    
    # Get breathing rate
    breathing_rate = get_breathing_rate()

    # Get sleeping hours
    sleeping_hours = get_sleeping_hours()

    # Detect stress
    if detect_stress(heart_rate, breathing_rate, sleeping_hours):
        print("Stress detected!")
    else:
        print("No stress detected.")
    
    # Wait for a moment before checking again
    time.sleep(1)

