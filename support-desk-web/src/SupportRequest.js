import React, { useEffect, useState } from 'react';
import Modal from 'react-modal';

function SupportRequest({ request, onResolve, onDelete }) {
    const deadline = new Date(request.deadline);
    const now = new Date();
    const oneHour = 60 * 60 * 1000; // in milliseconds

    const style = {
        border: '1px solid #ccc',
        margin: '10px',
        padding: '10px',
        borderRadius: '5px',
        backgroundColor: deadline - now < oneHour ? '#ff2f00' : 'white'
    };

    const modalStyles = {
        content: {
        top: '50%',
        left: '50%',
        right: 'auto',
        bottom: 'auto',
        marginRight: '-50%',
        transform: 'translate(-50%, -50%)',
        width: '80%', 
        maxHeight: '90%',
        overflow: 'auto'
        },
    };

    const buttonStyle = {
        padding: '10px 20px',
        fontSize: '16px',
        backgroundColor: '#007BFF',
        color: 'white',
        border: 'none',
        borderRadius: '5px',
        cursor: 'pointer',
        margin: '5px'
    };  
  
    const [timeLeft, setTimeLeft] = useState('');
    const [modalIsOpen, setModalIsOpen] = useState(false);

    const calculateTimeLeft = () => {
        const now = new Date();
        const deadline = new Date(request.deadline);
        const diff = deadline - now;
    
        if (diff < 0) {
            setTimeLeft('Overdue');
        } else {
            const hours = Math.floor(diff / (1000 * 60 * 60));
            const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
            setTimeLeft(`${hours} hours ${minutes} minutes left`);
        }
    };
  
    useEffect(() => {
        calculateTimeLeft(); // Calculate time left immediately
        const timer = setInterval(calculateTimeLeft, 1000); // Then update every second
        return () => clearInterval(timer);
    }, [request.deadline]);

    const openModal = () => {
        setModalIsOpen(true);
    };
    
    const closeModal = () => {
        setModalIsOpen(false);
    };

    return (
        <div style={style} onClick={openModal}>
        <h2>{request.title}</h2>
        <p>Time left: {timeLeft}</p>
    
        <Modal
            isOpen={modalIsOpen}
            onRequestClose={closeModal}
            shouldCloseOnOverlayClick={true}
            style={modalStyles}
            contentLabel="Request Details"
        >
            <h2>{request.title}</h2>
            <p>Description: {request.description}</p>
            <p>Created At: {new Date(request.createdAt).toLocaleString()}</p>
            <p>Deadline: {new Date(request.deadline).toLocaleString()}</p>
            <p>Time left: {timeLeft}</p>
            {!request.resolved && <button onClick={() => onResolve(request.id)} style={buttonStyle}>Mark as Resolved</button>}
            <button onClick={() => onDelete(request.id)} style={buttonStyle}>Delete</button>
            <button onClick={(e) => { e.stopPropagation(); closeModal(); }} style={buttonStyle}>Close</button>
        </Modal>
        </div>
    );
}

export default SupportRequest;