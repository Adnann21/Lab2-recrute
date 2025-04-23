import React, { useState, useEffect } from 'react';
import './Navbar.css';
import logo from '../../assets/logo.png';
import menu_icon from '../../assets/menu-icon.png';
import { Link } from 'react-scroll';
import { useNavigate } from 'react-router-dom';

const Navbar = () => {
  const [sticky, setSticky] = useState(false);
  const [mobileMenu, setMobileMenu] = useState(false);
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    const handleScroll = () => {
      setSticky(window.scrollY > 500);
    };
    window.addEventListener('scroll', handleScroll);

    // Restore user from localStorage
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }

    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  const toggleMenu = () => {
    setMobileMenu(!mobileMenu);
  };

  const toggleDropdown = () => {
    setDropdownOpen(!dropdownOpen);
  };

  const handleLogout = () => {
    localStorage.removeItem('user');
    localStorage.removeItem('token'); // If you store token
    setUser(null); // Update state
    setDropdownOpen(false);
    navigate('/'); // Optional redirect after logout
  };

  const handleChangePassword = () => {
    setDropdownOpen(false);
    navigate('/changepassword');
  };

  return (
    <nav className={`container ${sticky ? 'dark-nav' : ''}`}>
      <img src={logo} alt="Logo" className="logo" />

      <div className={`nav-links ${mobileMenu ? '' : 'hide-mobile-menu'}`}>
        <ul className="center-nav">
          <li><Link to="hero" smooth={true} offset={0} duration={500}>Home</Link></li>
          <li><Link to="program" smooth={true} offset={-260} duration={500}>Pricing</Link></li>
          <li><Link to="about" smooth={true} offset={-150} duration={500}>About Us</Link></li>
          <li><Link to="testimonials" smooth={true} offset={-260} duration={500}>Testimonials</Link></li>
          <li><Link to="contact" smooth={true} offset={-260} duration={500}>Contact Us</Link></li>
        </ul>

        {user ? (
          <div className="user-dropdown">
            <button className="username-btn" onClick={toggleDropdown}>
              Hi, {user.username}
              <span className={`arrow ${dropdownOpen ? 'up' : 'down'}`}>&#x2193;</span>
            </button>
            {dropdownOpen && (
              <div className="dropdown-menu">
                <button onClick={handleChangePassword}>Change Password</button>
                <button onClick={handleLogout}>Log Out</button>
              </div>
            )}
          </div>
        ) : (
          <div className="login-btn">
            <button onClick={() => navigate('/login')} className="btn">Log In</button>
          </div>
        )}
      </div>

      <img src={menu_icon} alt="Menu" className="menu-icon" onClick={toggleMenu} />
    </nav>
  );
};

export default Navbar;
