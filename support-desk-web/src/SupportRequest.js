import React from 'react';

function SupportRequest({ request, onResolve, onDelete }) {
  const deadline = new Date(request.deadline);
  const now = new Date();
  const oneHour = 60 * 60 * 1000; // in milliseconds

  const style = {
    border: '1px solid #ccc',
    margin: '10px',
    padding: '10px',
    borderRadius: '5px',
    backgroundColor: deadline - now < oneHour ? 'red' : 'white'
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

  return (
    <div style={style}>
      <h2>{request.title}</h2>
      <p>{request.description}</p>
      <p>Created At: {new Date(request.createdAt).toLocaleString()}</p>
      <p>Deadline: {deadline.toLocaleString()}</p>
      {!request.resolved && <button onClick={() => onResolve(request.id)} style={buttonStyle}>Mark as Resolved</button>}
      <button onClick={() => onDelete(request.id)} style={buttonStyle}>Delete</button>
    </div>
  );
}

export default SupportRequest;