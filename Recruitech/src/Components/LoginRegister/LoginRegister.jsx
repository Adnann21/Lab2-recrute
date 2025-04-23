import React, { useState } from 'react';
import './LoginRegister.css';
import { FaUser, FaLock, FaEnvelope } from "react-icons/fa";
import { useNavigate } from 'react-router-dom';
import logo from '../../assets/logo.png';

const LoginRegister = () => {
    const [action, setAction] = useState('');
    const [loginData, setLoginData] = useState({ username: '', password: '' });
    const [registerData, setRegisterData] = useState({ username: '', email: '', password: '', termsAccepted: false });
    const [loginError, setLoginError] = useState('');
    const [registerError, setRegisterError] = useState('');
    const navigate = useNavigate();

    const registerLink = () => setAction(' active');
    const loginLink = () => setAction('');
    const goBackToHome = () => navigate('/');

    const handleLogin = async (e) => {
        e.preventDefault();
        setLoginError('');

        if (!loginData.username || !loginData.password) {
            setLoginError('Username and Password are required!');
            return;
        }

        try {
            const encodedUsername = encodeURIComponent(loginData.username);
            const encodedPassword = encodeURIComponent(loginData.password);
            const apiUrl = `https://localhost:7159/login?Username=${encodedUsername}&password=${encodedPassword}`;

            const res = await fetch(apiUrl, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
            });

            if (res.ok) {
                const data = await res.json();
                localStorage.setItem('token', data.token);
                localStorage.setItem('user', JSON.stringify({ username: loginData.username })); // Save username
                navigate('/pricing');
            } else {
                const errorData = await res.json();
                setLoginError(errorData.message || 'Login failed');
            }
        } catch (error) {
            console.error('Login error:', error);
            setLoginError('An error occurred during login.');
        }
    };

    const handleRegister = async (e) => {
        e.preventDefault();
        setRegisterError('');

        if (!registerData.username || !registerData.email || !registerData.password) {
            setRegisterError('All fields are required!');
            return;
        }

        if (!registerData.termsAccepted) {
            setRegisterError('You must agree to the terms & conditions.');
            return;
        }

        try {
            const res = await fetch('https://localhost:7159/signup', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(registerData),
            });

            if (res.ok) {
                alert('Registration successful. You can now log in.');
                loginLink();
            } else {
                const errorData = await res.json();
                setRegisterError(errorData.message || 'Registration failed');
            }
        } catch (error) {
            console.error('Register error:', error);
            setRegisterError('An error occurred during registration.');
        }
    };

    const handleCheckboxChange = (e) => {
        setRegisterData({
            ...registerData,
            termsAccepted: e.target.checked,
        });
    };

    return (
        <div>
            <img
                src={logo}
                alt="Back to Home"
                className="back-icon"
                onClick={goBackToHome}
            />
            <div className={`wrapper${action}`}>
                <div className="form-box login">
                    <form onSubmit={handleLogin}>
                        <h1>Login</h1>
                        <div className="input-box">
                            <input
                                type="text"
                                placeholder="Username / Company Name"
                                required
                                value={loginData.username}
                                onChange={(e) => setLoginData({ ...loginData, username: e.target.value })}
                            />
                            <FaUser className="icon" />
                        </div>
                        <div className="input-box">
                            <input
                                type="password"
                                placeholder="Password"
                                required
                                value={loginData.password}
                                onChange={(e) => setLoginData({ ...loginData, password: e.target.value })}
                            />
                            <FaLock className="icon" />
                        </div>

                        <div className="remember-forgot">
                            <label>
                                <input type="checkbox" /> Remember me
                            </label>
                            <a href="#">Forgot password?</a>
                        </div>

                        <button type="submit">Login</button>

                        <div className="register-link">
                            <p>Don't have an account?<a href="#" onClick={registerLink}> Register</a></p>
                        </div>

                        {loginError && <p className="error">{loginError}</p>}
                    </form>
                </div>

                <div className="form-box register">
                    <form onSubmit={handleRegister}>
                        <h1>Sign Up</h1>
                        <div className="input-box">
                            <input
                                type="text"
                                placeholder="Username / Company Name"
                                required
                                value={registerData.username}
                                onChange={(e) => setRegisterData({ ...registerData, username: e.target.value })}
                            />
                            <FaUser className="icon" />
                        </div>
                        <div className="input-box">
                            <input
                                type="email"
                                placeholder="Email"
                                required
                                value={registerData.email}
                                onChange={(e) => setRegisterData({ ...registerData, email: e.target.value })}
                            />
                            <FaEnvelope className="icon" />
                        </div>
                        <div className="input-box">
                            <input
                                type="password"
                                placeholder="Password"
                                required
                                value={registerData.password}
                                onChange={(e) => setRegisterData({ ...registerData, password: e.target.value })}
                            />
                            <FaLock className="icon" />
                        </div>

                        <div className="remember-forgot">
                            <label>
                                <input
                                    type="checkbox"
                                    checked={registerData.termsAccepted}
                                    onChange={handleCheckboxChange}
                                /> I agree to the terms & conditions
                            </label>
                        </div>

                        <button type="submit">Sign Up</button>

                        <div className="register-link">
                            <p>Already have an account?<a href="#" onClick={loginLink}> Login</a></p>
                        </div>

                        {registerError && <p className="error">{registerError}</p>}
                    </form>
                </div>
            </div>
        </div>
    );
};

export default LoginRegister;
