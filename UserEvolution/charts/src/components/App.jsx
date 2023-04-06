import React, { useEffect, useState } from 'react';
import StepsChart from './components/StepsChart';
import LineChart from './components/LineChart';
import WeightChart from './components/WeightChart';
import './App.css';

const App = () => {
  const [stepsData, setStepsData] = useState([]);
  const [activityData, setActivityData] = useState([]);
  const [weightData, setWeightData] = useState([]);
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
  }, []);

  return (
    <div>
      <div>
        <h2>Steps Chart</h2>
        {stepsData.length > 0 ? (
          <StepsChart stepsData={stepsData} />
        ) : (
          <p>Loading...</p>
        )}
      </div>
      <div>
      <h2>Activity Chart</h2>
      {activityData.length > 0 ? (
        <LineChart activityData={activityData} />
      ) : (
        <p>Loading...</p>
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
  </div>
  );
};

export default App;
