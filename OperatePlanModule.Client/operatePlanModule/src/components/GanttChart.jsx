import React, { useEffect, useState } from 'react';
import { create, ColorSet, Scrollbar, percent, color } from '@amcharts/amcharts4/core';
import { XYChart } from '@amcharts/amcharts4/charts';
import { DateAxis, CategoryAxis } from '@amcharts/amcharts4/charts';
import { ColumnSeries } from '@amcharts/amcharts4/charts';
import '@ant-design/v5-patch-for-react-19';
import { Spin } from 'antd';

const GanttChart = ({ tasks, id }) => {
    useEffect(() => {
            
            // Создаем экземпляр диаграммы
            let chart = create(`chartdiv-${id}`, XYChart);
            chart.hiddenState.properties.opacity = 0; 
            let maxDate= tasks[tasks.length - 1].end;
            chart.paddingRight = 30;
            chart.dateFormatter.inputDateFormat = "yyyy-MM-dd HH:mm";
            
            var colorSet = new ColorSet();
            colorSet.saturation = 0.4;
            // Настраиваем оси
            let dateAxis = chart.xAxes.push(new DateAxis());
            dateAxis.dateFormatter.dateFormat = "yyyy-MM-dd HH:mm";
            dateAxis.max = new Date(maxDate).getTime();
            dateAxis.renderer.tooltipLocation = 0;
            dateAxis.strictMinMax = false; // Установите минимальное расстояние для сетки

            let range = dateAxis.axisRanges.create();
            range.value = 300;
            range.endValue = 1100;

            let valueAxis = chart.yAxes.push(new CategoryAxis());
            valueAxis.dataFields.category = "category";
            valueAxis.renderer.grid.template.location = 0;
            valueAxis.renderer.inversed = true;

            // Добавляем данные в диаграмму
            chart.data = tasks;
            let series = chart.series.push(new ColumnSeries());
            // series.columns.template.width = percent(80);
            series.dataFields.openDateX = 'fromDate';
            series.dataFields.dateX = 'toDate';
            series.dataFields.categoryY = 'category'; // Используем поле category для оси Y
            series.columns.template.tooltipText = '{categoryY}: {openDateX} - {dateX}';
            series.columns.template.propertyFields.fill = "color";
            series.columns.template.propertyFields.stroke = "color";
            series.columns.template.strokeOpacity = 1;
            
            // Устанавливаем данные для серии
            chart.scrollbarX = new Scrollbar();
            chart.scrollbarY = new Scrollbar();
            // Удаляем диаграмму при размонтировании компонента
            
            return () => {
                chart.dispose();
            };
    }, [tasks]);
    
    return <div id={`chartdiv-${id}`} style={{ width: '100%', height: '750px' }} />;
};

export default GanttChart;