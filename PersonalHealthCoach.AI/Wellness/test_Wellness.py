import main

import os
import unittest

abspath = os.path.dirname(__file__)

class TestWellnessResponse(unittest.TestCase):
    def test_no_params(self):
        response = main.get({})
        self.assertNotEqual(response, None)

    def test_disease_params(self):
        query = {"Diseases": ['Osteoporosis']}
        response = main.get(query)
        self.assertNotEqual(response, None)

    def test_invalid_data(self):
        query = {"Categories": ['Moado', "vksapDFS", "{}dsf", "Mental"]}
        response = main.get(query)
        self.assertTrue(response == None or isinstance(response, str))