import React from 'react';
import { useNavigate } from 'react-router-dom';
import './SidebarLayout.css';
import { FaTachometerAlt, FaUsers, FaDollarSign } from 'react-icons/fa'; // Importing icons from react-icons
import logo from "../../../assets/logo.png";  // Correct path

const SidebarLayout = ({ children }) => {
  const navigate = useNavigate();
  const storedUser = localStorage.getItem('user');
  const username = storedUser ? JSON.parse(storedUser).username : '';


  return (
    <div className="sidebar-layout">
      <div className="sidebar">
        {/* Logo in the middle of the sidebar */}
        <div className="sidebar-logo" onClick={() => navigate('/')}>
          <img src={logo} alt="Logo" />
        </div>

        <div className="sidebar-user">
          <h1>{username}</h1>
          <p>Admin</p>
        </div>


        {/* Sidebar links */}
        <div className="sidebar-links">
          {/* Dashboard Section */}
          <div className="sidebar-link" onClick={() => navigate('/admin')}>
            <FaTachometerAlt className="sidebar-icon" />
            <span>Dashboard</span>
          </div>

          {/* Data Group (without toggle) */}
          <div className="sidebar-group">
            <div className="sidebar-link group-title">
              <span>Data</span>
            </div>

            {/* Manage Team Section */}
            <div className="sidebar-link sub-link" onClick={() => navigate('/admin/team')}>
              <FaUsers className="sidebar-icon" />
              <span>Manage Team</span>
            </div>
            <div className="sidebar-link sub-link" onClick={() => navigate('/admin/payments')}>
              <FaDollarSign className="sidebar-icon" />
              <span>Payments</span>
            </div>
          </div>
        </div>
      </div>

      <div className="main-content">
        {children}
      </div>
    </div>
  );
};

export default SidebarLayout;
