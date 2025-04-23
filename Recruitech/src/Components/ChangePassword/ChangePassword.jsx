import React, { useState } from 'react';
import './ChangePassword.css';
import { FaLock } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';
import logo from '../../assets/logo.png';

const ChangePassword = () => {
  const [OldPassword, setOldPassword] = useState('');
  const [NewPassword, setNewPassword] = useState('');
  const [ConfirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setSuccess('');

    const storedUser = JSON.parse(localStorage.getItem('user'));
    const token = localStorage.getItem('token');

    if (!storedUser || !token) {
      setError('You must be logged in to change password.');
      return;
    }

    if (NewPassword !== ConfirmPassword) {
      setError('New password and confirm password do not match!');
      return;
    }

    try {
      const response = await fetch('https://localhost:7159/Password', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify({
          username: storedUser.username,
          OldPassword,
          NewPassword,
          ConfirmPassword,
        }),
      });

      if (response.ok) {
        setSuccess('Password successfully changed!');
        setOldPassword('');
        setNewPassword('');
        setConfirmPassword('');
      } else {
        const errorData = await response.json();
        setError(errorData.message || 'Failed to change password.');
      }
    } catch (err) {
      console.error(err);
      setError('Something went wrong. Please try again.');
    }
  };

  const goBackToHome = () => navigate('/');

  return (
    <div>
      <img
        src={logo}
        alt="Back to Home"
        className="cp-back-icon"
        onClick={goBackToHome}
      />
      <div className="cp-wrapper">
        <div className="cp-form-box">
          <form onSubmit={handleSubmit}>
            <h1 className="cp-title">Change Password</h1>

            <div className="cp-input-box">
              <input
                type="password"
                placeholder="Old Password"
                value={OldPassword}
                onChange={(e) => setOldPassword(e.target.value)}
                required
              />
              <FaLock className="cp-icon" />
            </div>

            <div className="cp-input-box">
              <input
                type="password"
                placeholder="New Password"
                value={NewPassword}
                onChange={(e) => setNewPassword(e.target.value)}
                required
              />
              <FaLock className="cp-icon" />
            </div>

            <div className="cp-input-box">
              <input
                type="password"
                placeholder="Confirm New Password"
                value={ConfirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
                required
              />
              <FaLock className="cp-icon" />
            </div>

            {error && <p className="cp-error">{error}</p>}
            {success && <p className="cp-success">{success}</p>}

            <button type="submit" className="cp-button">Change Password</button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ChangePassword;
