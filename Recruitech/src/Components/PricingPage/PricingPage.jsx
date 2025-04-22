import React, { useState } from 'react';
import './PricingPage.css';
import { useNavigate, Link } from 'react-router-dom';
import logo from '../../assets/logo.png';

const PricingPage = () => {
  const [userCounts, setUserCounts] = useState({ Small: 2, Mid: 11, Large: 101 });
  const navigate = useNavigate();

  const plans = [
    { name: 'Small', price: 1000, users: '2–10 users', min: 2, max: 10 },
    { name: 'Mid', price: 950, users: '11–100 users', min: 11, max: 100 },
    { name: 'Large', price: 800, users: '101–300 users', min: 101, max: 300 },
  ];

  const handleUserCountChange = (planName, value) => {
    if (value.length > 3) return; // Limit to 3 digits

    const intVal = parseInt(value, 10);
    if (!isNaN(intVal)) {
      setUserCounts({ ...userCounts, [planName]: intVal });
    } else if (value === '') {
      setUserCounts({ ...userCounts, [planName]: '' });
    }
  };

  const handleUserCountBlur = (planName) => {
    const intVal = parseInt(userCounts[planName], 10);
    const plan = plans.find(plan => plan.name === planName);

    if (isNaN(intVal) || intVal < plan.min) {
      setUserCounts({ ...userCounts, [planName]: plan.min });
    } else if (intVal > plan.max) {
      setUserCounts({ ...userCounts, [planName]: plan.max });
    }
  };

  const handleUserCountKeyDown = (e, planName) => {
    if (e.key === 'Enter') {
      handleUserCountBlur(planName);
    }
  };

  const handleCheckout = (plan) => {
    const userCount = userCounts[plan.name];
    if (userCount < plan.min || userCount > plan.max) {
      alert(`Please select between ${plan.min} and ${plan.max} users for the ${plan.name} plan.`);
      return;
    }

    const totalPrice = plan.price * userCount;

    navigate('/checkout', {
      state: {
        planName: plan.name,
        userCount: userCount,
        pricePerUser: plan.price,
        totalPrice: totalPrice,
      }
    });
  };

  return (
    <div className="price-container">
      <Link to="/">
        <img src={logo} alt="Logo" className="pricing-logo" />
      </Link>
      <h2>Choose your plan</h2>

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
                  onBlur={() => handleUserCountBlur(plan.name)}
                  onKeyDown={(e) => handleUserCountKeyDown(e, plan.name)}
                />
                <span className="user-range">({plan.users})</span>
              </label>

              <ul>
                <li>${plan.price} Per User</li>
                <li>Access To All Tools</li>
                <li>Free Email Address</li>
                <li>Contact Support Available</li>
                <li>Security Guaranteed</li>
                <li>Tutorial For Tools</li>
              </ul>

              <button onClick={() => handleCheckout(plan)}>
                Buy Now
              </button>
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default PricingPage;
