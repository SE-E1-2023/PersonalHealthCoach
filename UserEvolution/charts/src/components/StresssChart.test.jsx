import React from 'react';
import StressChart from './StressChart';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import 'resize-observer-polyfill/dist/ResizeObserver.global';

test('renders activity chart with data', () => {
  const stressData = [
    { label: 'Day 1', steps: 50 },
    { label: 'Day 2', steps: 80},
    { label: 'Day 3', steps: 60 },
  ];
  const { getByTestId } = render(<StressChart stressData={stressData} />);
  const chartElement = getByTestId('stress-chart');
  expect(chartElement).toBeInTheDocument();
});

test('renders no data message when there is no data', () => {
  const stressData = [];
  const { getByText } = render(<StressChart stressData={stressData} />);
  const noDataMessage = getByText('No data available for chart.');
  expect(noDataMessage).toBeInTheDocument();
});


