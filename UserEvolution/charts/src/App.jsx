import React, { useEffect, useState } from 'react';
import StepsChart from './components/StepsChart';
import LineChart from './components/LineChart';
import WeightChart from './components/WeightChart';
import SleepChart from './components/SleepChart';
import StressChart from './components/StressChart';
import BpmChart from './components/BpmChart';
import SpO2Chart from './components/SpO2Chart';
import filljson from './components/filljson.jsx';
import './App.css';

const App = () => {
  const [stepsData, setStepsData] = useState([]);
  const [activityData, setActivityData] = useState([]);
  const [weightData, setWeightData] = useState([]);
  const [sleepData, setSleepData] = useState([]);
  const[stressData,setStressData] = useState([]);
  const[bpmData,setBpmData] = useState([]);
  const[spo2Data,setSpO2Data] = useState([]);


  useEffect(() => {
    const fetchStepsData = async () => {
      try {
        const response = await fetch('./src/components/steps.json');
        const data = await response.json();
        console.log('data:', data); // add console log to check data
        setStepsData(data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchStepsData();
    
    const fetchActivityData = async () => {
      try {
        const response = await fetch('./src/components/activity.json');
        const data = await response.json();
        console.log('data:', data); // add console log to check data
        setActivityData(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchActivityData();
    filljson();
    const fetchWeightData = async () => {
      try {
        const response = await fetch('./src/components/weight.json');
        const data = await response.json();
        console.log('data:', data); // add console log to check data
        setWeightData(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchWeightData();

    const fetchSleepData = async () => {
      try {
        const response = await fetch('./src/components/sleep.json');
        const data = await response.json();
        console.log('data:', data); // add console log to check data
        setSleepData(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchSleepData();

    const fetchStressData = async () => {
      try {
        const response = await fetch('./src/components/stress.json');
        const data = await response.json();
        console.log('data:', data); // add console log to check data
        setStressData(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchStressData();

    const fetchBpmData = async () => {
      try {
        const response = await fetch('./src/components/bpm.json');
        const data = await response.json();
        console.log('data:', data); // add console log to check data
        setBpmData(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchBpmData();

    const fetchSpO2Data = async () => {
      try {
        const response = await fetch('./src/components/spo2.json');
        const data = await response.json();
        console.log('data:', data); // add console log to check data
        setSpO2Data(data);
      } catch (error) {
        console.error(error);
      }
    };
    fetchSpO2Data();
  }, []);

  return (
    <div>
      <div>
        <h2>Steps Chart</h2>
        {stepsData.length > 0 ? (
          <StepsChart stepsData={stepsData} />
        ) : (
          <p class="bar">Loading...</p>
        )}
      </div>
      <div>
      <h2>Activity Chart</h2>
      {activityData.length > 0 ? (
        <LineChart activityData={activityData} />
      ) : (
        <p class="bar">Loading...</p>
      )}
    </div>
    <div>
      <h2>Weight Chart</h2>
      {weightData.length > 0 ? (
        <WeightChart weightData={weightData} />
      ) : (
        <p class="bar">Loading...</p>
      )}
    </div>
    <div>
      <h2>Sleep Chart</h2>
      {sleepData.length > 0 ? (
        <SleepChart sleepData={sleepData} />
      ) : (
        <p class="bar">Loading...</p>
      )}
    </div>
    <div>
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
    </div>
  </div>
  );
};

export default App;
