import React from 'react';
import BpmChart from './BpmChart';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import 'resize-observer-polyfill/dist/ResizeObserver.global';

test('renders activity chart with data', () => {
  const bpmData = [
    { label: 'Day 1', steps: 100 },
    { label: 'Day 2', steps: 80 },
    { label: 'Day 3', steps: 120 },
  ];
  const { getByTestId } = render(<BpmChart bpmData={bpmData} />);
  const chartElement = getByTestId('bpm-chart');
  expect(chartElement).toBeInTheDocument();
});

test('renders no data message when there is no data', () => {
  const bpmData = [];
  const { getByText } = render(<BpmChart bpmData={bpmData} />);
  const noDataMessage = getByText('No data available for chart.');
  expect(noDataMessage).toBeInTheDocument();
});


