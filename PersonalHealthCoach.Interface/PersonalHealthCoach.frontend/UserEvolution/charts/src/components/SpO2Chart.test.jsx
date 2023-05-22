import React from 'react';
import SpO2Chart from './SpO2Chart';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import 'resize-observer-polyfill/dist/ResizeObserver.global';

test('renders activity chart with data', () => {
  const spo2Data = [
    { label: 'Day 1', steps: 100 },
    { label: 'Day 2', steps: 80 },
    { label: 'Day 3', steps: 99 },
  ];
  const { getByTestId } = render(<SpO2Chart spo2Data={spo2Data} />);
  const chartElement = getByTestId('spo2-chart');
  expect(chartElement).toBeInTheDocument();
});

test('renders no data message when there is no data', () => {
  const spo2Data = [];
  const { getByText } = render(<SpO2Chart spo2Data={spo2Data} />);
  const noDataMessage = getByText('No data available for chart.');
  expect(noDataMessage).toBeInTheDocument();
});


