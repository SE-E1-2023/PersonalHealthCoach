import unittest
from unittest.mock import patch
import json
from Stress_detec import Stress_detec, verify_if_heartrate_are_introduced, verify_if_breathing_are_introduced, verify_if_sleeping_hours_are_introduced

class TestStressDetection(unittest.TestCase):
    def setUp(self):
       
        with open('exemplu.json', 'r') as f:
            self.test_data = json.load(f)
    
    def test_verify_if_heartrate_are_introduced(self):
        # Test when all heartrate values are introduced
        heartrate_list = [80, 90, 95]
        self.assertTrue(verify_if_heartrate_are_introduced(heartrate_list))
        
        # Test when at least one heartrate value is None
        heartrate_list = [80, None, 95]
        self.assertFalse(verify_if_heartrate_are_introduced(heartrate_list))

    def test_verify_if_breathing_are_introduced(self):
        # Test when all breathing rate values are introduced
        breathing_rate_list = [14, 16, 15]
        self.assertTrue(verify_if_breathing_are_introduced(breathing_rate_list))
        
        # Test when at least one breathing rate value is None
        breathing_rate_list = [14, None, 15]
        self.assertFalse(verify_if_breathing_are_introduced(breathing_rate_list))

    def test_verify_if_sleeping_hours_are_introduced(self):
        # Test when all sleeping hours values are introduced
        sleeping_hours_list = [7, 8, 6.5]
        self.assertTrue(verify_if_sleeping_hours_are_introduced(sleeping_hours_list))
        
        # Test when at least one sleeping hours value is None
        sleeping_hours_list = [7, None, 6.5]
        self.assertFalse(verify_if_sleeping_hours_are_introduced(sleeping_hours_list))

    def test_generate_advice_for_heart_rate(self):
        # Mock the print function to capture the printed output
        with patch('builtins.print') as mock_print:
            stress_detec = Stress_detec('exemplu.json')
            stress_detec.generate_advice_for_heart_rate()

            # Verify the printed output based on the test data
            expected_output = [
                "There are data about user heart rates. We are analyzing them now... ",
                "",
                "Your heart rate was too high in the day:1. If you have made physical effort, you must reduce it for your health or take more breaks.",
                "Your heart rate was too low in the day:2. Due to the low pulse, you present symptoms similar to Bradycardia, you should visit a doctor as soon as possible."
            ]
            self.assertEqual(mock_print.call_args_list, [unittest.mock.call(output) for output in expected_output])
    
    def test_generate_advice_for_breathing_rate(self):
        # Mock the print function to capture the printed output
        with patch('builtins.print') as mock_print:
            stress_detec = Stress_detec('exemplu.json')
            stress_detec.generate_advice_for_breathing_rate()

            # Verify the printed output based on the test data
            expected_output = [
                "There are data about user breathing rates. We are analyzing them now...",
                "",
                "Your breathing rate was too high in the day:1. You seemed stressed. Maybe a breathing exercise will help you:",
                "Deep Breathing: Most people take short, shallow breaths into their chest. It can make you feel anxious and zap your energy. With this technique, you'll learn how to take bigger breaths, all the way into your belly. Get comfortable. You can lie on your back in bed or on the floor with a pillow under your head and knees. Or you can sit in a chair with your shoulders, head, and neck supported against the back of the chair. Breathe in through your nose. Let your belly fill with air. Breathe out through your nose. Place one hand on your belly. Place the other hand on your chest. As you breathe in, feel your belly rise. As you breathe out, feel your belly lower. The hand on your belly should move more than the one that's on your chest. Take three more full, deep breaths. Breathe fully into your belly as it rises and falls with your breath.",
                "",
                "Your breathing rate was too low in the day:2. Due to the low breathing rate, you may have some problems with lung breathing. You should visit a doctor as soon as possible."
            ]
            self.assertEqual(mock_print.call_args_list, [unittest.mock.call(output) for output in expected_output])
    def test_generate_advice_for_sleeping_hours(self):
    # Mock the print function to capture the printed output
      with patch('builtins.print') as mock_print:
        stress_detec = Stress_detec('exemplu.json')
        stress_detec.generate_advice_for_sleeping_hours()

        # Verify the printed output based on the test data
        expected_output = [
            "There are data about user sleeping hours. We are analyzing them now...",
            "",
            "You have exceeded the normal range of sleeping hours in the day: 1. This can lead to disruption of normal sleep and the appearance of insomnia.",
            "",
            "You slept too little during the day: 2. Here are some tips for increasing and improving sleep:",
            "Make sure your bedroom is quiet, dark, relaxing, and at a comfortable temperature."
        ]
        self.assertEqual(mock_print.call_args_list, [unittest.mock.call(output) for output in expected_output])

if __name__ == '__main__':
    unittest.main()