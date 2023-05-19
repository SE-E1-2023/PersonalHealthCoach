import React, { useEffect, useState } from "react";
import StepsChart from "./components/StepsChart";
import LineChart from "./components/LineChart";
import WeightChart from "./components/WeightChart";
import SleepChart from "./components/SleepChart";
import StressChart from "./components/StressChart";
import BpmChart from "./components/BpmChart";
import SpO2Chart from "./components/SpO2Chart";
import filljson from "./components/filljson.jsx";

import PersonalData from "./models/PersonalData";

import "./App.css";

import Cookies from "js-cookie";

const App = () => {
  const [stepsData, setStepsData] = useState([]);
  const [activityData, setActivityData] = useState([]);
  const [weightData, setWeightData] = useState([]);
  const [sleepData, setSleepData] = useState([]);
  const [stressData, setStressData] = useState([]);
  const [bpmData, setBpmData] = useState([]);
  const [spo2Data, setSpO2Data] = useState([]);
  const [personalDataList, setPersonalDataList] = useState([]);

  useEffect(() => {
    const fetchPersonalData = async () => {
      const userId = "036d5276-5ca2-43ab-9635-d8b14965ca51";
      const url = `http://localhost:7071/api/v1/users/${userId}/data/personal`;

      try {
        const response = await fetch(url);

        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }

        const data = await response.json();
        const personalDataObjects = data.map(item => new PersonalData(item));
        setPersonalDataList(personalDataObjects);
        console.log("PersonalData:", personalDataObjects); // add console log to check data
      } catch (error) {
        console.error('Error fetching personal data:', error);
      }
    };


    fetchPersonalData();

    const fetchStepsData = () => {
      try {
        // Assuming personalDataList has been fetched and set correctly
        const stepsData = personalDataList.map((personalData) => ({
          Date: personalData.CreatedAt, // Assuming 'createdAt' is the date you want to use
          Steps: personalData.DailySteps, // Make sure this property name matches the one in the JSON data
        }));

        setStepsData(stepsData);
      } catch (error) {
        console.error(error);
      }
    };
    fetchStepsData();

    const fetchActivityData = async () => {
      try {
        const response = await fetch("./src/components/activity.json");
        const data = await response.json();
        // console.log("data:", data); // add console log to check data
        setActivityData(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchActivityData();

    const fetchWeightData = () => {
      try {
        // Assuming personalDataList has been fetched and set correctly
        const weightData = personalDataList.map((personalData) => ({
          Date: personalData.CreatedAt, // Assuming 'createdAt' is the date you want to use
          Weight: personalData.Weight, // Make sure this property name matches the one in the JSON data
        }));

        setWeightData(weightData);
      } catch (error) {
        console.error(error);
      }
    };
    fetchWeightData();

    const fetchSleepData = async () => {
      try {
        const sleepData = personalDataList.map((personalData) => ({
          Date: personalData.CreatedAt, // Assuming 'createdAt' is the date you want to use
          HoursOfSleep: personalData.HoursOfSleep, // Make sure this property name matches the one in the JSON data
        }));
        setSleepData(sleepData);
      } catch (error) {
        console.error(error);
      }
    };
    fetchSleepData();

    const fetchStressData = async () => {
      try {
        const response = await fetch("./src/components/stress.json");
        const data = await response.json();
        // console.log("data:", data); // add console log to check data
        setStressData(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchStressData();

    const fetchBpmData = async () => {
      try {
        const response = await fetch("./src/components/bpm.json");
        const data = await response.json();
        // console.log("data:", data); // add console log to check data
        setBpmData(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchBpmData();

    const fetchSpO2Data = async () => {
      try {
        const response = await fetch("./src/components/spo2.json");
        const data = await response.json();
        // console.log("data:", data); // add console log to check data
        setSpO2Data(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchSpO2Data();
  }, []);

  // this one is used to ensure the proper fetching and mapping of the weight data
  useEffect(() => {
    if (personalDataList.length === 0) return;

    const weightData = personalDataList.map(personalData => ({
      CreatedAt: personalData.CreatedAt,
      Weight: personalData.Weight
    }));

    setWeightData(weightData);
  }, [personalDataList]);

  // this one is used for steps data
  useEffect(() => {
    if (personalDataList.length === 0) return;

    const stepsData = personalDataList.map(personalData => ({
      CreatedAt: personalData.CreatedAt,
      Steps: personalData.DailySteps
    }));

    setStepsData(stepsData);
  }, [personalDataList]);

  // this one is used for sleep data
  useEffect(() => {
    if (personalDataList.length === 0) return;

    const sleepData = personalDataList.map(personalData => ({
      CreatedAt: personalData.CreatedAt,
      HoursOfSleep: personalData.HoursOfSleep
    }));

    setSleepData(sleepData);
  }, [personalDataList]);

  return (
    <div>
      <div>
        <h2>Steps Chart</h2>
        {stepsData.length > 0 ? (
          <StepsChart stepsData={stepsData} />
        ) : (
          <p className="bar">Loading...</p>
        )}
      </div>
      {/* <div>
                <h2>Activity Chart</h2>
                {activityData.length > 0 ? (
                    <LineChart activityData={activityData} />
                ) : (
                    <p class="bar">Loading...</p>
                )}
            </div> */}
      <div>
        <h2>Weight Chart</h2>
        {weightData.length > 0 ? (
          <WeightChart weightData={weightData} />
        ) : (
          <p className="bar">Loading...</p>
        )}
      </div>
      <div>
        <h2>Sleep Chart</h2>
        {sleepData.length > 0 ? (
          <SleepChart sleepData={sleepData} />
        ) : (
          <p className="bar">Loading...</p>
        )}
      </div>
      {/* <div>
                <h2>Stress Average Chart</h2>
                {stressData.length > 0 ? (
                    <StressChart stressData={stressData} />
                ) : (
                    <p class="bar">Loading...</p>
                )}
            </div>
            <div>
                <h2>BPM Average Chart</h2>
                {bpmData.length > 0 ? (
                    <BpmChart bpmData={bpmData} />
                ) : (
                    <p class="bar">Loading...</p>
                )}
            </div>
            <div>
                <h2>SpO2 Average Chart</h2>
                {spo2Data.length > 0 ? (
                    <SpO2Chart spo2Data={spo2Data} />
                ) : (
                    <p class="bar">Loading...</p>
                )}
            </div> */}
    </div>
  );
};

export default App;
