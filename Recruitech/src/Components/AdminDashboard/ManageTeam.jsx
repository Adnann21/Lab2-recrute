import React, { useState } from 'react';
import SidebarLayout from './SidebarLayout/SidebarLayout';
import './ManageTeam.css';
import { FaUserShield, FaBuilding, FaUser } from 'react-icons/fa';

const mockUsers = [
  { name: 'Alice Johnson', phone: '123-456-7890', email: 'alice@example.com', role: 'Admin' },
  { name: 'Bob Smith', phone: '987-654-3210', email: 'bob@example.com', role: 'User' },
  { name: 'Charlie Brown', phone: '555-123-4567', email: 'charlie@example.com', role: 'Company' },
];

const getRoleIcon = (role) => {
  switch (role) {
    case 'Admin':
      return <FaUserShield />;
    case 'Company':
      return <FaBuilding />;
    case 'User':
      return <FaUser />;
    default:
      return null;
  }
};

const USERS_PER_PAGE = 10;

const ManageTeam = () => {
  const [currentPage, setCurrentPage] = useState(1);

  const totalPages = Math.ceil(mockUsers.length / USERS_PER_PAGE);

  const startIndex = (currentPage - 1) * USERS_PER_PAGE;
  const currentUsers = mockUsers.slice(startIndex, startIndex + USERS_PER_PAGE);

  return (
    <SidebarLayout>
      <div className="manage-team">
        <div className="team-header">
          <h1>TEAM</h1>
          <p>Managing the Team Members</p>
        </div>

        <div className="team-table">
          <div className="team-header-row">
            <span>Name</span>
            <span>Phone</span>
            <span>Email</span>
            <span>Role</span>
          </div>

          {currentUsers.map((user, index) => (
            <div className="team-row" key={index}>
              <span>{user.name}</span>
              <span>{user.phone}</span>
              <span>{user.email}</span>
              <span className={`role-badge ${user.role.toLowerCase()}`}>
                {getRoleIcon(user.role)}
                {user.role}
              </span>
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
    </SidebarLayout>
  );
};

export default ManageTeam;
