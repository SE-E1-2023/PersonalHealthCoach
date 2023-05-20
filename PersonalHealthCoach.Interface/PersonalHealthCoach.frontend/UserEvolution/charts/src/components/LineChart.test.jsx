import React from 'react';
import LineChart from './LineChart';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import 'resize-observer-polyfill/dist/ResizeObserver.global';

test('renders activity chart with data', () => {
  const activityData = [
    { label: 'Day 1', steps: 10000 },
    { label: 'Day 2', steps: 8000 },
    { label: 'Day 3', steps: 12000 },
  ];
  const { getByTestId } = render(<LineChart activityData={activityData} />);
  const chartElement = getByTestId('activity-chart');
  expect(chartElement).toBeInTheDocument();
});

test('renders no data message when there is no data', () => {
  const activityData = [];
  const { getByText } = render(<LineChart activityData={activityData} />);
  const noDataMessage = getByText('No data available for chart.');
  expect(noDataMessage).toBeInTheDocument();
});


