import React from 'react';
import { useNavigate } from 'react-router-dom';
import './SideBarCLayout.css';
import { FaTachometerAlt, FaUsers, FaDollarSign } from 'react-icons/fa'; // Importing icons from react-icons
import logo from "../../../assets/logo.png";  // Correct path

const SideBarCLayout = ({ children }) => {
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
          <p>Company</p>
        </div>


        {/* Sidebar links */}
        <div className="sidebar-links">
          {/* Dashboard Section */}
          <div className="sidebar-link" onClick={() => navigate('/company')}>
            <FaTachometerAlt className="sidebar-icon" />
            <span>Dashboard</span>
          </div>

          {/* Data Group (without toggle) */}
          <div className="sidebar-group">
            <div className="sidebar-link group-title">
              <span>Data</span>
            </div>

             <div className="sidebar-link sub-link" onClick={() => navigate('/company/candidates')}>
                          <FaUsers className="sidebar-icon" />
                          <span>Candidate</span>
                        </div>

            {/* Manage Team Section */}
            <div className="sidebar-link sub-link" onClick={() => navigate('/company/workers')}>
              <FaUsers className="sidebar-icon" />
              <span>Manage Workers</span>
            </div>
            <div className="sidebar-link sub-link" onClick={() => navigate('/company/placements')}>
              <FaDollarSign className="sidebar-icon" />
              <span>Placements</span>
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

export default SideBarCLayout;