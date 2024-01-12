import React, { useState, useEffect } from 'react';
import Modal from 'react-modal';

function AddSupportRequest({ onAdd }) {
  const [isOpen, setIsOpen] = useState(false);
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [deadline, setDeadline] = useState('');

  useEffect(() => {
    document.body.style.overflow = isOpen ? 'hidden' : 'auto';
  }, [isOpen]);

  const handleSubmit = (event) => {
    event.preventDefault();
    onAdd({ title, description, deadline });
    setTitle('');
    setDescription('');
    setDeadline('');
    setIsOpen(false);
  };

  const style = {
    content: {
      top: '50%',
      left: '50%',
      right: 'auto',
      bottom: 'auto',
      marginRight: '-50%',
      transform: 'translate(-50%, -50%)',
      width: '80%',
      maxWidth: '400px',
      padding: '20px',
      overflow: 'hidden',
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center'
    }
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
    <div>
      <button onClick={() => setIsOpen(true)} style={buttonStyle}>Create new</button>
      <Modal isOpen={isOpen} onRequestClose={() => setIsOpen(false)} shouldCloseOnOverlayClick={false} style={style}>
        <form onSubmit={handleSubmit}>
          <label style={{ display: 'block', marginBottom: '10px' }}>
            Title:
            <input value={title} onChange={e => setTitle(e.target.value)} required style={{ width: '100%', padding: '10px', marginTop: '5px' }} />
          </label>
          <label style={{ display: 'block', marginBottom: '10px' }}>
            Description:
            <textarea value={description} onChange={e => setDescription(e.target.value)} required style={{ width: '100%', padding: '10px', marginTop: '5px' }} />
          </label>
          <label style={{ display: 'block', marginBottom: '10px' }}>
            Deadline:
            <input type="datetime-local" value={deadline} onChange={e => setDeadline(e.target.value)} required style={{ width: '100%', padding: '10px', marginTop: '5px' }} />
          </label>
          <button type="submit" style={buttonStyle}>Add</button>
          <button onClick={() => setIsOpen(false)} style={buttonStyle}>Close</button>
        </form>
      </Modal>
    </div>
  );
}

export default AddSupportRequest;