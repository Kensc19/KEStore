"use client"; //Para utilizar el cliente en lugar del servidor
import { useState, useEffect } from 'react';
import Link from 'next/link';
import { Chart } from 'react-google-charts';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

export default function Init() {
    const [selectedOption, setSelectedOption] = useState(null);
    const [selectedDay, setSelectedDay] = useState(new Date());
    const [weeklySalesData, setWeeklySalesData] = useState([['Day', 'Total']]);

    useEffect(() => {
        fetchData();
    }, [selectedDay]); // Asegúrate de llamar a fetchData cuando selectedDay cambie

    const fetchData = async () => {
        try {
            const response = await fetch(`https://localhost:7067/api/Sale`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(selectedDay)
            });
            if (!response.ok) {
                throw new Error('Failed to fetch data');
            }
            const data = await response.json();
            const newData = [['Day', 'Total']];
            for (const item of data) {
                newData.push([item.day, item.total]);
            }
            setWeeklySalesData(newData);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    const handleDayChange = (selectedDay) => {
        setSelectedDay(selectedDay);
    };

    return (
        <div>
            <div className="header">
                <Link href="/">
                    <h1>Tienda</h1>
                </Link>
            </div>

            <div className="body">
                <div className="sidebar">
                    <h2>Menu</h2>
                    <div>
                        <button className="Button" onClick={() => setSelectedOption("Products")}>Products</button>
                        <button className="Button" onClick={() => setSelectedOption("Reports")}>Reports</button>
                    </div>
                </div>

                <div className="content">
                    {selectedOption === "Products" && (
                        <div>
                            <h2>Products</h2>
                        </div>
                    )}
                    {selectedOption === "Reports" && (
                        <div>
                            <h2>Reports</h2>
                            <DatePicker
                                label="Select a day"
                                selected={selectedDay}
                                onChange={handleDayChange}
                                renderInput={(params) => <input {...params} />}
                            />

                            <div className="chartContainer">
                                <Chart
                                    width={"100%"}
                                    height={"300px"}
                                    chartType="Table"
                                    loader={<div>Loading Chart</div>}
                                    data={weeklySalesData}
                                    options={{
                                        title: "Weekly Sales"
                                    }}
                                />

                                <Chart
                                    width={"100%"}
                                    height={"300px"}
                                    chartType="PieChart"
                                    loader={<div>Loading Chart</div>}
                                    data={weeklySalesData}
                                    options={{
                                        title: "Weekly Sales",
                                    }}
                                />
                                
                            </div>
                        </div>
                    )}
                </div>
            </div>

            <div className="footer">
                <h2>Tienda.com</h2>
            </div>
        </div>
    );
}