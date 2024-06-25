'use client';
import React, { useEffect, useState } from 'react';
import * as signalR from "@microsoft/signalr";
import '../ui/globals.css';
const URL = process.env.NEXT_PUBLIC_API;

function Chat() {
    const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
    const [messages, setMessages] = useState<string[]>([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchCampaigns = async () => {
            const response = await fetch(URL + '/api/Campannas');
            if (response.ok) {
                const data = await response.json();
                const campaigns = data.map((campanna: any) => `Nueva Campaña: ${campanna.contenidoHtml}`);
                setMessages(campaigns.slice(-3)); // Only keep the last 3 messages
            } else {
                setError('Error al obtener las campañas.');
            }
        };

        fetchCampaigns();

        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl(URL + "/StoreHub", {
                withCredentials: true
            })
            .withAutomaticReconnect()
            .configureLogging(signalR.LogLevel.Information)
            .build();

        const startConnection = () => {
            newConnection.start()
                .then(() => {
                    setConnection(newConnection);
                    setError(null);
                })
                .catch(() => {
                    setError('Error al conectar con el servidor. Intentando reconectar...');
                    setTimeout(() => startConnection(), 5000);
                });
        };

        newConnection.onclose(() => {
            setError('Conexión cerrada. Intentando reconectar...');
            setTimeout(() => startConnection(), 5000);
        });

        startConnection();

        return () => {
            if (newConnection) {
                newConnection.stop();
            }
        };
    }, []);

    useEffect(() => {
        if (connection) {
            const handleReceiveMessage = (user: string, message: string) => {
                setMessages(prevMessages => {
                    const newMessages = [...prevMessages, `${user}: ${message}`];
                    return newMessages.slice(-3); // Only keep the last 3 messages
                });
            };

            const handleUpdateCampaigns = (contenidoHtml: string, estado: boolean) => {
                setMessages(prevMessages => {
                    let newMessages;
                    if (estado) {
                        // If the status is true, add the message
                        newMessages = [...prevMessages, `Nueva Campaña: ${contenidoHtml}`];
                    } else {
                        // If the status is false, remove the corresponding message
                        newMessages = prevMessages.filter(msg => msg !== `Nueva Campaña: ${contenidoHtml}`);
                    }
                    return newMessages.slice(-3); // Only keep the last 3 messages
                });
            };

            connection.on("ReceiveMessage", handleReceiveMessage);
            connection.on("UpdateCampaigns", handleUpdateCampaigns);

            return () => {
                connection.off("ReceiveMessage", handleReceiveMessage);
                connection.off("UpdateCampaigns", handleUpdateCampaigns);
            };
        }
    }, [connection]);

    return (
        <div className="chat-container">
            <h1 className="chat-title">Mensajes Recibidos</h1>
            {error && <div className="error-message">{error}</div>}
            <ul className="messages-list">
                {messages.map((message, index) => (
                    <li key={index} className="message-item" dangerouslySetInnerHTML={{ __html: message }}></li>
                ))}
            </ul>
        </div>
    );
}

export default Chat;
