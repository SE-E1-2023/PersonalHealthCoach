# Import required libraries
import time
import json
import re
from bs4 import BeautifulSoup
from selenium import webdriver
from selenium.common.exceptions import NoSuchElementException, TimeoutException
from selenium.webdriver.firefox.service import Service
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

# Define target URL for scraping exercise information
url = "https://www.bodybuilding.com/exercises/finder"

# Change this to the absolute path of geckodriver.exe on your machine
geckodriver_path = r"C:\Users\Koroem\Desktop\geckodriver.exe"

def save_to_json(data, filename):

    with open(filename, 'w') as f:
        json.dump(data, f, indent=2)

def load_from_json(filename):

    with open(filename, 'r') as f:
        return json.load(f)

def get_exercise_urls(soup):

    exercise_urls = []
    exercise_rows = soup.find_all('div', {'class': 'ExResult-row'})
    for row in exercise_rows:
        exercise_link = row.find('a')['href']
        exercise_url = "https://www.bodybuilding.com" + exercise_link
        exercise_urls.append(exercise_url)
    return exercise_urls

def extract_exercise_info(url, driver):

    driver.get(url)
    try:
        WebDriverWait(driver, 10).until(EC.presence_of_element_located((By.XPATH, "//div[contains(@class, 'grid-3 grid-12-s grid-8-m')]")))
    except TimeoutException:
        print(f"Timeout waiting for elements in {url}")
        return None

    soup = BeautifulSoup(driver.page_source, 'html.parser')

    exercise_info = {"url": url}

    def find_attribute_element(soup, search_string):
        return soup.find("li", string=re.compile(search_string))

    info_div = soup.find("div", class_="grid-3 grid-12-s grid-8-m")
    if info_div:
        info_list = info_div.find("ul", class_="bb-list--plain")
        if info_list:
            for li in info_list.find_all("li"):
                li_text = li.text.strip()
                if "Type:" in li_text:
                    exercise_info["type"] = li_text.replace("Type:", "").strip()
                elif "Main Muscle Worked:" in li_text:
                    exercise_info["main_muscle"] = li_text.replace("Main Muscle Worked:", "").strip()
                elif "Equipment:" in li_text:
                    exercise_info["equipment"] = li_text.replace("Equipment:", "").strip()
        else:
            exercise_info["type"] = "Not found"
            exercise_info["main_muscle"] = "Not found"
            exercise_info["equipment"] = "Not found"
            print(f"List element not found in {url}")
    else:
        exercise_info["type"] = "Not found"
        exercise_info["main_muscle"] = "Not found"
        exercise_info["equipment"] = "Not found"
        print(f"Div element not found in {url}")

    level_element = find_attribute_element(soup, "Level:")
    if level_element:
        exercise_info["level"] = level_element.text.replace("Level:", "").strip()
    else:
        exercise_info["level"] = "Not found"
        print(f"Level element not found in {url}")

    exercise_info["score"] = float(soup.find("div", class_="ExRating-badge").text.strip())

    images = soup.find_all("img", class_="ExImg ExDetail-img js-ex-enlarge")
    exercise_info["images"] = [img["src"] for img in images]

    instructions_section = soup.find("div", itemprop="description")
    instructions = [li.text.strip() for li in instructions_section.find_all("li")]
    exercise_info["instructions"] = instructions

    return exercise_info

def extract_all_exercise_info(exercise_urls, driver):
    exercise_data = []
    for i, url in enumerate(exercise_urls, start=1):
        print(f"Processing exercise {i}/{len(exercise_urls)}: {url}")
        exercise_info = extract_exercise_info(url, driver)
        exercise_data.append(exercise_info)
        time.sleep(2)

    return exercise_data

def main():
    with open('exercises_html.html', 'r') as f:
        html = f.read()

    soup = BeautifulSoup(html, 'html.parser')
    exercise_urls = get_exercise_urls(soup)

    with open('exercises_urls.json', 'w') as f:
        json.dump(exercise_urls, f, indent=2)

    driver = webdriver.Firefox(executable_path=geckodriver_path)

    exercise_urls = load_from_json('exercises_urls.json')

    exercise_data = extract_all_exercise_info(exercise_urls, driver)

    driver.quit()

    save_to_json(exercise_data, 'exercises_database.json')

if __name__ == "__main__":
    main()
            