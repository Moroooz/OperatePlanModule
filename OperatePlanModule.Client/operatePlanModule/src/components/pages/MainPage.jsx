import { useState, useEffect } from "react";
import '@ant-design/v5-patch-for-react-19';
import { Typography, Button, Upload, Tabs, message } from 'antd';
import { UploadOutlined } from '@ant-design/icons';
import { create, ColorSet, Scrollbar, percent } from '@amcharts/amcharts4/core';
import { XYChart } from '@amcharts/amcharts4/charts';
import { DateAxis, CategoryAxis } from '@amcharts/amcharts4/charts';
import { ColumnSeries } from '@amcharts/amcharts4/charts';
import axios from "axios";
import GanttChart from "../GanttChart";
import AllTimeChart from "../AllTimeChart";
import DiffTimeChart from "../DiffTimeChart";

const { Title, Paragraph } = Typography;

const Home = () => {
    
    const [tasks, setTasks] = useState(() => {
        return JSON.parse(localStorage.getItem('matrixData')) || {};
    });
    const [tasksData, setTasksData] = useState([]);
    const [maxDate, setMaxDate] = useState(null);
    const [isVisible, setIsVisible] = useState(false);
    const [activeKey, setActiveKey] = useState('0');

    const formatDate = (dateString) => {
        const options = { year: "numeric", month: "long", day: "numeric"}
        return new Date(dateString).toLocaleDateString(undefined, options)
    }

    const generateFile = async () => {
        const response = await axios.post('http://localhost:5093/Matrix/generate/file');
        console.log(response.data.filePath);
        message.success(`Файл ${response.data.filePath} успешно создан`);
    }

    const getResult = async () => {
        try {
            console.log(tasks);
            const response = await axios.post('http://localhost:5093/Matrix/methods', tasks);
            console.log('Матрица:', response.data);

            var colorSet = new ColorSet();
            colorSet.saturation = 0.4;
            
            const formattedData = response.data.map(task => ({
                allTime: task.allTime,
                diffTime: response.data[0].allTime - task.allTime,
                methodName: task.methodName,
                keys: task.keys,
                ganttTasks: task.ganttTasks.map(item => ({
                    category: item.machine,
                    idJ: item.idJ,
                    fromDate: item.start, 
                    toDate: item.end, 
                    color: colorSet.getIndex(item.idJ).brighten(0.2),
                }))
            }));
            // console.log(new Date(response.data[response.data.length - 1].end.replace(' ', 'T')));
            setMaxDate(response.data[0].ganttTasks[response.data[0].ganttTasks.length - 1].end);
            // setTasks(formattedData);
            // localStorage.setItem('matrixData', JSON.stringify(formattedData));
            setTasksData(formattedData);
            setIsVisible(!isVisible);
        } catch (error) {
            console.error('Ошибка загрузки результата', error);
        } 
    };

    useEffect (() => {
        if (tasks) {
            console.log(tasks);
            getResult();
        }
    }, []);

    const resetData = () => {
        setTasksData([]);
        localStorage.removeItem('matrixData');
        setIsVisible(!isVisible);
        setTasks({});
        message.success('Данные очищены');
    }

    const uploadFile = async (file) => {
        const formData = new FormData();
        formData.append('file', file);
       
        try {
            const response = await axios.post('http://localhost:5093/Matrix/upload', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                },
            });
            console.log('Матрица:', response.data);
            localStorage.setItem('matrixData', JSON.stringify(response.data));
            setTasks(response.data);

            const responseResult = await axios.post('http://localhost:5093/Matrix/methods', response.data);
            console.log('Результат:', responseResult.data);

            var colorSet = new ColorSet();
            colorSet.saturation = 0.4;
            
            const formattedData = responseResult.data.map(task => ({
                allTime: task.allTime,
                diffTime: responseResult.data[0].allTime - task.allTime,
                methodName: task.methodName,
                keys: task.keys,
                ganttTasks: task.ganttTasks.map(item => ({
                    category: item.machine,
                    idJ: item.idJ,
                    fromDate: item.start, 
                    toDate: item.end, 
                    color: colorSet.getIndex(item.idJ).brighten(0.2),
                }))
            }));
            // console.log(new Date(response.data[response.data.length - 1].end.replace(' ', 'T')));
            setMaxDate(responseResult.data[0].ganttTasks[responseResult.data[0].ganttTasks.length - 1].end);
            // setTasks(formattedData);
            // localStorage.setItem('matrixData', JSON.stringify(formattedData));
            setTasksData(formattedData);

            message.success('Матрица загружена успешно');
            setIsVisible(!isVisible);
            
            // Здесь можно добавить логику для отображения диаграммы Ганта
        } catch (error) {
            message.error('Error uploading matrix');
            console.error('Error uploading matrix:', error);
        }

        return false; // Prevent automatic upload - we're handling it manually
    };

    return (
        <div style={{ textAlign: 'center', marginTop: '30px' }}>
            <Title level={1}></Title>
            <div style={{display: 'flex',  textAlign: 'center', justifyContent: 'center'}}>
                <Button onClick={generateFile} style={{marginRight: '15px'}}>Генерация нового файла</Button>
                {!isVisible && <div>
                    <Upload showUploadList={false} maxCount={1} beforeUpload={uploadFile} accept=".xlsx, .xls">
                        <Button icon={<UploadOutlined />}>Выберите файл</Button>
                    </Upload>
                </div>
                }   
                {isVisible && <Button onClick={resetData}>Очистить данные файла</Button>}
            </div>
            {isVisible && <Tabs activeKey={activeKey} onChange={setActiveKey}>
                {tasksData.map((sTasks, index) => (
                    <Tabs.TabPane tab={`Метод ${sTasks.methodName}`} key={index.toString()}>
                        <div>
                            <div>
                                <p>Порядок следования деталей: {sTasks.keys}.</p>
                                <p>Общее время обработки деталей на станках: {sTasks.allTime} час(ов).</p>
                            </div>
                        </div>
                        <GanttChart tasks={sTasks.ganttTasks} id={index} />
                    </Tabs.TabPane>
                ))}
                <Tabs.TabPane tab= {"Общее время"}>
                    <AllTimeChart tasks={tasksData} />
                </Tabs.TabPane>
                {/* <Tabs.TabPane tab= {"Сравнение"}>
                    <DiffTimeChart tasks={tasksData} />
                </Tabs.TabPane> */}
            </Tabs>}
            
        </div>
    );
};

export default Home;