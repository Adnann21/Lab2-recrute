import React, { useEffect, useState } from 'react'
import './Navbar.css'
import logo from '../../assets/logo.png'
import menu_icon from '../../assets/menu-icon.png'
import { Link } from 'react-scroll';
import { useNavigate } from 'react-router-dom';

const Navbar = () => {
  const [sticky, setSticky] = useState(false);
  const navigate = useNavigate();
  const [mobileMenu, setMobileMenu] = useState(false);

  useEffect(() => {
    window.addEventListener('scroll', () => {
      window.scrollY > 500 ? setSticky(true) : setSticky(false);
    });
  }, []);

  const toggleMenu = () => {
    setMobileMenu(!mobileMenu);
  };

  return (
    <nav className={`container ${sticky ? 'dark-nav' : ''}`}>
      <img src={logo} alt="Logo" className="logo" />

      <div className={`nav-links ${mobileMenu ? '' : 'hide-mobile-menu'}`}>
        <ul className="center-nav">
          <li><Link to='hero' smooth={true} offset={0} duration={500}>Home</Link></li>
          <li><Link to='program' smooth={true} offset={-260} duration={500}>Pricing</Link></li>
          <li><Link to='about' smooth={true} offset={-150} duration={500}>About Us</Link></li>
          <li><Link to='testimonials' smooth={true} offset={-260} duration={500}>Testimonials</Link></li>
          <li><Link to='contact' smooth={true} offset={-260} duration={500}>Contact Us</Link></li>
        </ul>
        <div className="login-btn">
          <button onClick={() => navigate('/login')} className='btn'>Log In</button>
        </div>
      </div>

      <img src={menu_icon} alt="Menu" className="menu-icon" onClick={toggleMenu} />
    </nav>
  )
}

export default Navbar;
