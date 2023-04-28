import TestUtils as tu

def test_no_params():
    response = tu.get_response("Wellness", "{}")
    assert response.status_code != 500