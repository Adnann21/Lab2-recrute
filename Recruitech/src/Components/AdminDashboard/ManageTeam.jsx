import React, { useEffect, useState } from 'react';
import SidebarLayout from './SidebarLayout/SidebarLayout';
import './ManageTeam.css';
import { FaUserShield, FaBuilding, FaUser } from 'react-icons/fa';

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
  const [users, setUsers] = useState([]);
  const [error, setError] = useState('');
  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchUsers = async () => {
      setLoading(true);
      try {
        const res = await fetch('https://localhost:7159/api/Auth/Users'); // Kontrollo URL-në sipas backend-it tënd
        if (!res.ok) throw new Error('Failed to fetch users');
        const data = await res.json();

        // Nëse rolet janë numra, mund t'i konvertojmë këtu në string (p.sh.)
        data.forEach(u => {
          if (typeof u.role === 'number') {
            if (u.role === 1) u.role = 'User';
            else if (u.role === 2) u.role = 'Company';
            else if (u.role === 3) u.role = 'Admin';
          }
        });

        setUsers(data);
        setError('');
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchUsers();
  }, []);

  const totalPages = Math.ceil(users.length / USERS_PER_PAGE);
  const startIndex = (currentPage - 1) * USERS_PER_PAGE;
  const currentUsers = users.slice(startIndex, startIndex + USERS_PER_PAGE);

  if (loading) return <SidebarLayout><p>Loading users...</p></SidebarLayout>;

  return (
    <SidebarLayout>
      <div className="manage-team">
        <div className="team-header">
          <h1>TEAM</h1>
          <p>Managing the Team Members</p>
        </div>

        {error && <p style={{ color: 'red' }}>{error}</p>}

        <div className="team-table">
          <div className="team-header-row">
            <span>Name</span>
            <span>Email</span>
            <span>Role</span>
          </div>

          {currentUsers.length === 0 ? (
            <p>No users found.</p>
          ) : (
            currentUsers.map((user, index) => {
              const role = user.role ? user.role.toLowerCase() : 'user';
              return (
                <div className="team-row" key={index}>
                  <span>{user.name || user.username || 'No name'}</span>
                  <span>{user.email || 'No email'}</span>
                  <span className={`role-badge ${role}`}>
                    {getRoleIcon(user.role)}
                    {user.role || 'User'}
                  </span>
                </div>
              );
            })
          )}
        </div>

        <div className="pagination-controls">
          <button onClick={() => setCurrentPage(1)} disabled={currentPage === 1}>First Page</button>
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
          <button onClick={() => setCurrentPage(totalPages)} disabled={currentPage === totalPages}>Last Page</button>
        </div>
      </div>
    </SidebarLayout>
  );
};

export default ManageTeam;
