import React, { useEffect } from 'react';
import { create, ColorSet, Scrollbar, percent, color } from '@amcharts/amcharts4/core';
import { XYChart } from '@amcharts/amcharts4/charts';
import { DateAxis, CategoryAxis, ValueAxis } from '@amcharts/amcharts4/charts';
import { ColumnSeries } from '@amcharts/amcharts4/charts';
// import am4themes_animated from '@amcharts/amcharts4/themes/animated';
// import '@amcharts/amcharts4/themes/animated.css';


const AllTimeChart = ({ tasks }) => {
    useEffect(() => {
        // Создаем экземпляр диаграммы
        let chart = create(`chartdivAll`, XYChart);
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
        series.dataFields.valueY = "allTime";
        series.dataFields.categoryX = "methodName";
        series.name = "Общее время";
        series.columns.template.tooltipText = "Метод: {categoryX}\nВремя: {valueY}";
        series.columns.template.fill = color("#AADAB9"); // fill

        let diffChart = create(`chartdivDiff`, XYChart);
        diffChart.hiddenState.properties.opacity = 0; // this creates initial fade-in
        
        diffChart.paddingRight = 30;
        
        diffChart.data = tasks;
        
        // Create axes
        let diffCategoryAxis = diffChart.xAxes.push(new CategoryAxis());
        diffCategoryAxis.dataFields.category = "methodName";
        diffCategoryAxis.title.text = "Эффективность работы методов";
        
        let diffValueAxis = diffChart.yAxes.push(new ValueAxis());
        diffValueAxis.title.text = "Количество съэкономленных часов";
        
        // Create series
        var diffSeries = diffChart.series.push(new ColumnSeries());
        diffSeries.dataFields.valueY = "diffTime";
        diffSeries.dataFields.categoryX = "methodName";
        diffSeries.name = "Общее время";
        diffSeries.columns.template.tooltipText = "Метод: {categoryX}\nВремя: {valueY}";
        diffSeries.columns.template.fill = color("#AADAB9"); // fill

        return () => {
            chart.dispose();
            diffChart.dispose();
        };
}, [tasks]);

    return (
        <div style={{marginTop: '75px'}}>
            <div id={`chartdivAll`} style={{ width: '99%', height: '350px' }} />
            <div id={`chartdivDiff`} style={{ width: '99%', height: '350px', marginTop: '45px' }} />
        </div>
    )
   
    
};

export default AllTimeChart;