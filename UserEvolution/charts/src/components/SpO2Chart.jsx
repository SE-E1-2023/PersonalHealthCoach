import React, { useEffect, useRef } from 'react';
import Chart from 'chart.js/auto';
import ResizeObserver from 'resize-observer-polyfill';
import 'resize-observer-polyfill/dist/ResizeObserver.global';

const SpO2Chart = ({ spo2Data }) => {
  const chartRef = useRef(null);

  useEffect(() => {
    if (spo2Data.length === 0) return;
    const chartData = {
      labels: spo2Data.map((item) => item.label),
      datasets: [
        {
          label: 'Bpm Average',
          data: spo2Data.map((item) => item.value),
          fill: false,
          tension: 0.2,
          backgroundColor: 'rgba(0,0,0,0)', // set background color to transparent
          borderColor: 'pink',
          borderWidth:2,
          pointRadius: 0, // hide the points
        },
      ],
    };

    const chartConfig = {
      type: 'line',
      data: chartData,
      options: {
        plugins: {
          tooltip: {
            enabled: true,
            mode: 'nearest',
            position: 'nearest',
            intersect: false,
          },
          legend: {
            display: false,
          },
        },
        scales: {
          y: {
            beginAtZero: false,
          },
        },
      },
    };

    if(window.outerWidth>1024){
      Chart.defaults.font.size=15;
      }else Chart.defaults.font.size=7;
    const chart = new Chart(chartRef.current, chartConfig);

    const resizeObserver = new ResizeObserver(() => {
      chart.resize();
    });
    if (chartRef.current && chartRef.current.parentNode) {
      resizeObserver.observe(chartRef.current.parentNode);
    }
  
    return () => {
      chart.destroy();
      if (chartRef.current && chartRef.current.parentNode) {
        resizeObserver.unobserve(chartRef.current.parentNode);
      }
    };
  }, [spo2Data]);

  if (spo2Data.length === 0) {
    return (
      <div>
        <p>No data available for chart.</p>
      </div>
    );
  }

  return (
    <div id="chart-container-3">
      <canvas ref={chartRef} data-testid="spo2-chart"></canvas>
    </div>
  );
};

export default SpO2Chart;
