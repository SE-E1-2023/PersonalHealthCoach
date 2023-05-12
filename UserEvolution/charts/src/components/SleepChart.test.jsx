import React from 'react';
import SleepChart from './SleepChart';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import 'resize-observer-polyfill/dist/ResizeObserver.global';

test('renders activity chart with data', () => {
  const sleepData = [
    { label: 'Day 1', steps: 5 },
    { label: 'Day 2', steps: 8 },
    { label: 'Day 3', steps: 6 },
  ];
  const { getByTestId } = render(<SleepChart sleepData={sleepData} />);
  const chartElement = getByTestId('sleep-chart');
  expect(chartElement).toBeInTheDocument();
});

test('renders no data message when there is no data', () => {
  const sleepData = [];
  const { getByText } = render(<SleepChart sleepData={sleepData} />);
  const noDataMessage = getByText('No data available for chart.');
  expect(noDataMessage).toBeInTheDocument();
});


