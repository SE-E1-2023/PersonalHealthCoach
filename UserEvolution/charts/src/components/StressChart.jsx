import React, { useEffect, useRef } from 'react';
import Chart from 'chart.js/auto';
import ResizeObserver from 'resize-observer-polyfill';
import 'resize-observer-polyfill/dist/ResizeObserver.global';

const StressChart = ({ stressData }) => {
  const chartRef = useRef(null);
  useEffect(() => {
    if (stressData.length === 0) return;
    const chartData = {
      labels: stressData.map((item) => item.label),
      datasets: [
        {
          label: 'Stress',
          data: stressData.map((item) => item.value),
          fill: false,
          tension: 0.2,
          backgroundColor: 'rgba(0,0,0,0)', // set background color to transparent
          borderColor: "cyan",
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
            callbacks: {
              label: function (context) {
                const value = context.parsed.y;
                const previousValue = context.dataset.data[context.dataIndex - 1];
                const label = context.chart.data.labels[context.dataIndex];
                let legend = '';
                  
                if (previousValue !== undefined) {
                  if (value >= 50) {
                    legend = 'you are stressed';
                  } else if (value < 50) {
                    legend = 'you are relaxed';
                  } 
                } else if (value >= 50) {
                  legend = 'you are stressed';
                } else {
                  legend = 'you are relaxed';
                }

                return `${label}: ${value} (${legend})`;
              },
            },
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
  }, [stressData]);

  if (stressData.length === 0) {
    return (
      <div>
        <p>No data available for chart.</p>
      </div>
    );
  }

  return (
    <div id="chart-container-2">
      <canvas ref={chartRef} data-testid="stress-chart"></canvas>
    </div>
  );
};

export default StressChart;
