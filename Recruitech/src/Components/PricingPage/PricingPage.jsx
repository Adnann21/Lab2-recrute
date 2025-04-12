import React, { useState } from 'react';
import './PricingPage.css';
import { loadStripe } from '@stripe/stripe-js';
import { useNavigate, Link } from 'react-router-dom';
import logo from '../../assets/logo.png';

const stripePromise = loadStripe('pk_test_51OWe0KCl1DKyJcVcTPuad4uMinERoYHViKJyJeU80fl2KTlSAYus958iL0TZlq9iC9aJr75xm0pkUhAuhZ65hopV001gQItJiL');

const PricingPage = () => {
  const [selectedPlan, setSelectedPlan] = useState(null);
  const [userCounts, setUserCounts] = useState({ Small: 2, Mid: 11, Large: 101 });
  const [loading, setLoading] = useState(false);
  const [paymentSuccess, setPaymentSuccess] = useState(false);
  const navigate = useNavigate();

  const plans = [
    { name: 'Small', price: 1000, users: '2–10 users', min: 2, max: 10 },
    { name: 'Mid', price: 950, users: '11–100 users', min: 11, max: 100 },
    { name: 'Large', price: 800, users: '101–300 users', min: 101, max: 300 },
  ];

  const handleUserCountChange = (planName, value) => {
    const intVal = parseInt(value, 10);
    if (!isNaN(intVal) && value.length <= 3) {
      setUserCounts({ ...userCounts, [planName]: intVal });
    }
  };

  const handleUserCountKeyPress = (e, planName, value) => {
    if (e.key === 'Enter') {
      const intVal = parseInt(value, 10);
      const plan = plans.find(plan => plan.name === planName);

      if (intVal < plan.min) {
        setUserCounts({ ...userCounts, [planName]: plan.min });
      } else if (intVal > plan.max) {
        setUserCounts({ ...userCounts, [planName]: plan.max });
      } else {
        setUserCounts({ ...userCounts, [planName]: intVal });
      }
    }
  };

  const handleUserCountBlur = (planName, value) => {
    const intVal = parseInt(value, 10);
    const plan = plans.find(plan => plan.name === planName);

    if (intVal < plan.min) {
      setUserCounts({ ...userCounts, [planName]: plan.min });
    } else if (intVal > plan.max) {
      setUserCounts({ ...userCounts, [planName]: plan.max });
    } else {
      setUserCounts({ ...userCounts, [planName]: intVal });
    }
  };

  const handleCheckout = async (plan) => {
    const userCount = userCounts[plan.name];
    if (userCount < plan.min || userCount > plan.max) {
      alert(`Please select between ${plan.min} and ${plan.max} users for ${plan.name} plan.`);
      return;
    }

    setLoading(true);
    setPaymentSuccess(false);

    try {
      const response = await fetch('https://your-backend-api.com/create-checkout-session', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          plan: plan.name,
          userCount,
          amount: plan.price * userCount,
        }),
      });
      const session = await response.json();

      const stripe = await stripePromise;
      const { error } = await stripe.redirectToCheckout({
        sessionId: session.id,
      });

      if (error) {
        console.error('Stripe redirect error:', error);
        alert('There was an error redirecting to Stripe.');
      } else {
        setPaymentSuccess(true);
      }
    } catch (err) {
      console.error('Checkout error:', err);
      alert('Something went wrong during checkout.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="price-container">
      <Link to="/">
        <img src={logo} alt="Logo" className="pricing-logo" />
      </Link>
      <h2>Choose your plan</h2>

      {paymentSuccess && <p className="payment-success">✅ Payment is finished.</p>}

      <div className="price-row">
        {plans.map(plan => {
          const userCount = userCounts[plan.name];
          const total = plan.price * userCount;

          return (
            <div key={plan.name} className="price-col">
              <p>{plan.name}-sized Companies</p>
              <h3>${total.toLocaleString()} <span>per year</span></h3>

              <label>
                Users:
                <input
                  type="number"
                  value={userCount}
                  min={plan.min}
                  max={plan.max}
                  onChange={(e) => handleUserCountChange(plan.name, e.target.value)}
                  onKeyDown={(e) => handleUserCountKeyPress(e, plan.name, e.target.value)}
                  onBlur={(e) => handleUserCountBlur(plan.name, e.target.value)}
                  maxLength={3}
                />
                <span className="user-range">({plan.users})</span>
              </label>

              <ul>
                <li>{plan.price}$ Per User</li>
                <li>Access To All Tools</li>
                <li>Free Email Address</li>
                <li>Contact Support Available</li>
                <li>Security Guaranteed</li>
                <li>Tutorial For Tools</li>
              </ul>

              <button onClick={() => handleCheckout(plan)} disabled={loading}>
                {loading ? 'Processing...' : 'Buy Now'}
              </button>
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default PricingPage;
