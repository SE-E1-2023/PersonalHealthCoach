import sys
import os
abspath = os.path.dirname(__file__)
sys.path.insert(0, abspath)

import TestUtils as tu

#import unittest
import pytest

ENDPOINTS = ["Wellness", "TipGenerator", "FitnessPlanner", "DietPlanner"]

@pytest.mark.parametrize("endpoint", ENDPOINTS)
def test_no_json_mimetype(endpoint):
    response = tu.get_response_data(endpoint, "sa")
    assert response.status_code == 400

@pytest.mark.parametrize("endpoint", ENDPOINTS)
def test_invalid_json(endpoint):
    response = tu.get_response(endpoint, "dsao,.d[]")
    assert response.status_code == 400

    response = tu.get_response(endpoint, " ")
    assert response.status_code == 400

    response = tu.get_response(endpoint, "")
    assert response.status_code == 400

@pytest.mark.parametrize("endpoint", ENDPOINTS)
def test_no_params(endpoint):
    response = tu.get_response(endpoint, "{}")
    assert response.status_code != 500
