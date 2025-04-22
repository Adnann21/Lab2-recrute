import React, { useState } from 'react';
import { useLocation, Link } from 'react-router-dom';
import './CheckoutPage.css';
import logo from '../../assets/logo.png';

const CheckoutPage = () => {
  const location = useLocation();
  const { planName, userCount, totalPrice } = location.state || {};

  const [cardNumber, setCardNumber] = useState('');
  const [cardholderName, setCardholderName] = useState('');
  const [expiryDate, setExpiryDate] = useState('');
  const [cvv, setCvv] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const formatCardNumber = (value) => {
    const digits = value.replace(/\D/g, '').slice(0, 16);
    const formatted = digits.match(/.{1,4}/g);
    return formatted ? formatted.join(' ') : '';
  };

  const handleCardNumberChange = (e) => {
    const input = e.target.value;
    const formatted = formatCardNumber(input);
    setCardNumber(formatted);
  };

  const handleCardholderNameChange = (e) => {
    let value = e.target.value;
    value = value.replace(/[^a-zA-Z\s]/g, '').replace(/\s+/g, ' ');
    const capitalizedName = value
      .split(' ')
      .map(word => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase())
      .join(' ');
    if (capitalizedName.length <= 20) {
      setCardholderName(capitalizedName);
    }
  };

  const handleExpiryChange = (e) => {
    let value = e.target.value.replace(/\D/g, '');

    if (value.length === 0) {
      setExpiryDate('');
      return;
    }

    if (value.length === 1) {
      if (parseInt(value, 10) > 1) {
        value = '0' + value;
      }
      setExpiryDate(value);
      return;
    }

    if (value.length === 2) {
      const month = parseInt(value, 10);
      if (month < 1 || month > 12) return;
      setExpiryDate(value + '/');
      return;
    }

    if (value.length > 2 && value.length <= 4) {
      const month = value.slice(0, 2);
      const year = value.slice(2);
      setExpiryDate(`${month}/${year}`);
    }
  };

  const handleCvvChange = (e) => {
    const value = e.target.value.replace(/\D/g, '');
    setCvv(value.slice(0, 3));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');

<<<<<<< HEAD
    // Prepare the card data object
    const cardData = {
      planName,               // Plan name (e.g., Companies Plan)
      userCount,              // Number of users
      totalPrice,             // Total price
      cardNumber: cardNumber.replace(/\s+/g, ''), // Clean card number (remove spaces)
      cardholderName,         // Cardholder's name
      expiryDate,             // Expiry date (MM/YY)
      cvv,                    // CVV
    };

    try {
      // Sending POST request to backend API (adjust the URL if necessary)
=======
    const cardData = {
      planName,
      userCount,
      totalPrice,
      cardNumber: cardNumber.replace(/\s+/g, ''),
      cardholderName,
      expiryDate,
      cvv,
    };

    try {
>>>>>>> ac367585d8dd748d3dded7bf4bd305c9e201992e
      const response = await fetch('https://localhost:7159/Payment', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
<<<<<<< HEAD
        body: JSON.stringify(cardData),  // Send the cardData object in the request body
      });

      // Check if the response is successful
=======
        body: JSON.stringify(cardData),
      });

>>>>>>> ac367585d8dd748d3dded7bf4bd305c9e201992e
      if (!response.ok) {
        throw new Error('Failed to submit payment details');
      }

<<<<<<< HEAD
      // Assuming the backend sends a success message, you can process it here
      const result = await response.json();

      alert('✅ Purchase completed! (Fake payment for demo)');
      // Optionally, redirect or clear form data after successful payment

    } catch (error) {
      // Handle errors (e.g., failed fetch, network issues)
      setError('Error submitting payment details. Please try again later.');
    } finally {
      setLoading(false);  // Reset loading state
=======
      const result = await response.json();

      alert('✅ Purchase completed! (Fake payment for demo)');
    } catch (error) {
      setError('Error submitting payment details. Please try again later.');
    } finally {
      setLoading(false);
>>>>>>> ac367585d8dd748d3dded7bf4bd305c9e201992e
    }
  };

  if (!planName || !userCount || !totalPrice) {
    return (
      <div className="checkout-container">
        <h2 className="checkout-heading">Missing checkout information.</h2>
        <Link to="/pricing" className="checkout-form">
          <button>Back to Pricing</button>
        </Link>
      </div>
    );
  }

  return (
    <div className="checkout-container">
      <Link to="/">
        <img src={logo} alt="Logo" className="checkout-logo" />
      </Link>

      <h2 className="checkout-heading">Checkout</h2>

      <div className="checkout-summary">
        <p>
          You’re buying the <strong>{planName} Companies Plan</strong> with <strong>{userCount} users</strong> for a total of <strong>${totalPrice.toLocaleString()}</strong>.
        </p>
      </div>

      <form className="checkout-form" onSubmit={handleSubmit}>
        <label htmlFor="card-number">Card Number</label>
        <input
          type="text"
          id="card-number"
          placeholder="1234 5678 9012 3456"
          value={cardNumber}
          onChange={handleCardNumberChange}
          required
        />

        <label htmlFor="card-name">Cardholder Name</label>
        <input
          type="text"
          id="card-name"
          placeholder="John Doe"
          value={cardholderName}
          onChange={handleCardholderNameChange}
          required
        />

        <label htmlFor="expiry">Expiry Date (MM/YY)</label>
        <input
          type="text"
          id="expiry"
          placeholder="MM/YY"
          value={expiryDate}
          onChange={handleExpiryChange}
          required
        />

        <label htmlFor="cvv">CVV</label>
        <input
          type="text"
          id="cvv"
          placeholder="123"
          value={cvv}
          onChange={handleCvvChange}
          required
        />

        {error && <div className="error-message">{error}</div>}

        <button type="submit" disabled={loading}>
          {loading ? 'Processing...' : 'Confirm Purchase'}
        </button>

        <Link to="/pricing">
          <button type="button">Cancel</button>
        </Link>
      </form>
    </div>
  );
};

export default CheckoutPage;
