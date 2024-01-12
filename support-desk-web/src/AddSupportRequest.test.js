import { render, fireEvent } from '@testing-library/react';
import AddSupportRequest from './AddSupportRequest';

test('renders AddSupportRequest and checks modal open/close', () => {
  const onAdd = jest.fn(); // Create a mock function for onAdd

  const { getByText } = render(<AddSupportRequest onAdd={onAdd} />); // Pass the mock function as the onAdd prop
  
  // Check that the modal is not open initially
  expect(getByText('Create new')).toBeInTheDocument();
  
  // Click the 'Create new' button and check that the modal opens
  fireEvent.click(getByText('Create new'));
  expect(getByText('Title:')).toBeInTheDocument();
  
  // Click the 'Close' button and check that the modal closes
  fireEvent.click(getByText('Close'));
  expect(getByText('Create new')).toBeInTheDocument();
});