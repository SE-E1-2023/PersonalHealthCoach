import React from 'react';
import WeightChart from './WeightChart';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import 'resize-observer-polyfill/dist/ResizeObserver.global';

test('renders weight chart with data', () => {
  const weightData = [
    { label: 'Day 1', steps: 10000 },
    { label: 'Day 2', steps: 8000 },
    { label: 'Day 3', steps: 12000 },
  ];
  const { getByTestId } = render(<WeightChart weightData={weightData} />);
  const chartElement = getByTestId('weight-chart');
  expect(chartElement).toBeInTheDocument();
});

test('renders no data message when there is no data', () => {
  const weightData = [];
  const { getByText } = render(<WeightChart weightData={weightData} />);
  const noDataMessage = getByText('No data available for chart.');
  expect(noDataMessage).toBeInTheDocument();
});


