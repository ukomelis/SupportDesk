import React, { useEffect, useState } from 'react';
import SupportRequest from './SupportRequest';
import AddSupportRequest from './AddSupportRequest';

function SupportRequestList() {
  const [requests, setRequests] = useState([]);

  useEffect(() => {
    fetchUnresolvedRequests();
  }, []);

  const fetchUnresolvedRequests = () => {
    fetch('http://localhost:5001/supportrequests')
      .then(response => response.json())
      .then(data => setRequests(data.filter(request => !request.resolved)));
  };

  const handleResolve = (id) => {
    fetch(`http://localhost:5001/supportrequests/${id}/resolve`, { method: 'PUT' })
      .then(() => fetchUnresolvedRequests());
  };

  const handleDelete = (id) => {
    fetch(`http://localhost:5001/supportrequests/${id}`, { method: 'DELETE' })
      .then(() => fetchUnresolvedRequests());
  };

  const handleAdd = (request) => {
    fetch('http://localhost:5001/supportrequests', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(request)
    })
      .then(() => fetchUnresolvedRequests());
  };

  return (
    <div style={{ maxWidth: '600px', margin: '0 auto' }}>        
      <h1 style={{ textAlign: 'center' }}>Support Requests</h1>
      <AddSupportRequest  onAdd={handleAdd} />
      {requests.map(request => (
        <SupportRequest 
          key={request.id} 
          request={request} 
          onResolve={handleResolve} 
          onDelete={handleDelete} 
          style={{ border: '1px solid #ddd', borderRadius: '5px', padding: '10px', marginBottom: '10px' }}
        />
      ))}
    </div>
  );
}

export default SupportRequestList;