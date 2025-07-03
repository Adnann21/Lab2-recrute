import React, { useState } from 'react';
import SideBarCLayout from './SidebarCLayout/SidebarCLayout';
import './Placements.css';

// Mock Payment Data
const mockPayments = [
  { name: 'Alice Johnson', email: 'alice@example.com', date: '2025-04-01', amount: '$1000' },
  { name: 'Bob Smith', email: 'bob@example.com', date: '2025-04-03', amount: '$950' },
  { name: 'Charlie Brown', email: 'charlie@example.com', date: '2025-04-05', amount: '$800' },
  { name: 'David Lee', email: 'david@example.com', date: '2025-04-07', amount: '$950' },
  // Add more mock data as needed
];

const PAYMENTS_PER_PAGE = 10;

const ManagePlacements = () => {
  const [currentPage, setCurrentPage] = useState(1);

  const totalPages = Math.ceil(mockPayments.length / PAYMENTS_PER_PAGE);

  const startIndex = (currentPage - 1) * PAYMENTS_PER_PAGE;
  const currentPayments = mockPayments.slice(startIndex, startIndex + PAYMENTS_PER_PAGE);

  return (
    <SideBarCLayout>
      <div className="manage-payments">
        <div className="payments-header">
          <h1>Placements</h1>
          <p>See the Placements Records</p>
        </div>

        <div className="payments-table">
          <div className="payments-header-row">
            <span>Name</span>
            <span>Email</span>
            <span>Date</span>
            <span>Amount</span>
          </div>

          {currentPayments.map((payment, index) => (
            <div className="payments-row" key={index}>
              <span>{payment.name}</span>
              <span>{payment.email}</span>
              <span>{payment.date}</span>
              <span>{payment.amount}</span>
            </div>
          ))}
        </div>

        <div className="pagination-controls">
  <button onClick={() => setCurrentPage(1)}>First Page</button>
  {Array.from({ length: Math.min(10, totalPages) }, (_, i) => {
    const offset = currentPage > 5 ? currentPage - 5 : 0;
    const pageNumber = i + 1 + offset;
    if (pageNumber > totalPages) return null;
    return (
      <button
        key={pageNumber}
        className={currentPage === pageNumber ? 'active' : ''}
        onClick={() => setCurrentPage(pageNumber)}
      >
        {pageNumber}
      </button>
    );
  })}
  <button onClick={() => setCurrentPage(totalPages)}>Last Page</button>
</div>
      </div>
    </SideBarCLayout>
  );
};

export default ManagePlacements;