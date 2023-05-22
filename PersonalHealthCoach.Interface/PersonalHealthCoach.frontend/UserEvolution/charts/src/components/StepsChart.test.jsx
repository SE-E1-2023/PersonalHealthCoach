import React from 'react';
import StepsChart from './StepsChart';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import 'resize-observer-polyfill/dist/ResizeObserver.global';

test('renders steps chart with data', () => {
  const stepsData = [
    { label: 'Day 1', steps: 10000 },
    { label: 'Day 2', steps: 8000 },
    { label: 'Day 3', steps: 12000 },
  ];
  const { getByTestId } = render(<StepsChart stepsData={stepsData} />);
  const chartElement = getByTestId('steps-chart');
  expect(chartElement).toBeInTheDocument();
});

test('renders no data message when there is no data', () => {
  const stepsData = [];
  const { getByText } = render(<StepsChart stepsData={stepsData} />);
  const noDataMessage = getByText('No data available for chart.');
  expect(noDataMessage).toBeInTheDocument();
});


