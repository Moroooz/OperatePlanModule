import React, { useEffect } from 'react';
import { create, ColorSet, Scrollbar, percent, color } from '@amcharts/amcharts4/core';
import { XYChart } from '@amcharts/amcharts4/charts';
import { DateAxis, CategoryAxis, ValueAxis } from '@amcharts/amcharts4/charts';
import { ColumnSeries } from '@amcharts/amcharts4/charts';
// import am4themes_animated from '@amcharts/amcharts4/themes/animated';
// import '@amcharts/amcharts4/themes/animated.css';


const DiffTimeChart = ({ tasks }) => {
    useEffect(() => {
            // Создаем экземпляр диаграммы
            let chart = create(`chartdivDiff`, XYChart);
            chart.hiddenState.properties.opacity = 0; // this creates initial fade-in
            
            chart.paddingRight = 30;
            
            chart.data = tasks;
              
              // Create axes
              let categoryAxis = chart.xAxes.push(new CategoryAxis());
              categoryAxis.dataFields.category = "methodName";
              categoryAxis.title.text = "Общее время с использованием различных методов";
              
              let valueAxis = chart.yAxes.push(new ValueAxis());
              valueAxis.title.text = "Общее время (часов)";
              
              // Create series
              var series = chart.series.push(new ColumnSeries());
              series.dataFields.valueY = "diffTime";
              series.dataFields.categoryX = "methodName";
              series.name = "Общее время";
              series.columns.template.tooltipText = "Метод: {categoryX}\nВремя: {valueY}";
              series.columns.template.fill = color("#AADAB9"); // fill

            return () => {
                chart.dispose();
            };
    }, [tasks]);

    return <div id={`chartdivDiff`} style={{ width: '100%', height: '500px' }} />;
};

export default DiffTimeChart;