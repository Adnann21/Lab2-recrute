import React, { useState } from 'react';
import SidebarLayout from './SidebarLayout/SidebarLayout';
import './AdminDashboard.css';
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
} from 'recharts';
import { FaUsers, FaShoppingCart, FaChartLine, FaClipboardList } from 'react-icons/fa';

const AdminDashboard = () => {
  const [transactions] = useState([
    { name: 'Company A', amount: 1200, date: '2025-05-01' },
    { name: 'Company B', amount: 1500, date: '2025-05-02' },
    { name: 'Company C', amount: 900, date: '2025-05-02' },
    { name: 'Company D', amount: 2000, date: '2025-05-03' },
    { name: 'Company E', amount: 1300, date: '2025-05-03' },
    { name: 'Company F', amount: 1700, date: '2025-05-04' },
    { name: 'Company G', amount: 1900, date: '2025-05-04' },
    { name: 'Company H', amount: 1100, date: '2025-05-05' },
    { name: 'Company I', amount: 1400, date: '2025-05-05' },
    { name: 'Company J', amount: 1000, date: '2025-05-06' },
  ]);

  const totalRevenue = transactions.reduce(
    (acc, transaction) => acc + transaction.amount,
    0
  );

  return (
    <SidebarLayout>
      <div className="admin-dashboard">
        <div className="dashboard-header">
          <h1>Dashboard</h1>
          <p>Welcome to your Dashboard</p>
        </div>

        {/* Top Cards */}
        <div className="dashboard-top-cards">
          <div className="dashboard-card small-card">
            <FaUsers className="card-icon" />
            <h3>1,245</h3>
            <p>New Clients</p>
          </div>
          <div className="dashboard-card small-card">
            <FaShoppingCart className="card-icon" />
            <h3>3,768</h3>
            <p>Sales Obtained</p>
          </div>
          <div className="dashboard-card small-card">
            <FaChartLine className="card-icon" />
            <h3>24,503</h3>
            <p>Traffic Received</p>
          </div>
          <div className="dashboard-card small-card">
            <FaClipboardList className="card-icon" />
            <h3>712</h3>
            <p>Demo Requests</p>
          </div>
        </div>

        <div className="dashboard-main">
          {/* Revenue + Chart */}
          <div className="dashboard-card graph">
            <h2>Revenue Generated</h2>
            <p className="total-revenue">${totalRevenue.toLocaleString()}</p>
            <ResponsiveContainer width="98%" height={300}>
              <LineChart data={transactions}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
                <Line
                  type="monotone"
                  dataKey="amount"
                  stroke="#4cceac"
                  strokeWidth={2}
                />
              </LineChart>
            </ResponsiveContainer>
          </div>

          {/* Recent Transactions */}
          <div className="dashboard-card transactions">
            <h2>Recent Transactions</h2>
            <div className="transaction-scroll-container">
              <div className="transaction-header">
                <span>Company Name</span>
                <span>Date</span>
                <span>Amount ($)</span>
              </div>
              <div className="transaction-rows">
                {transactions.map((transaction, index) => (
                  <div className="transaction-row" key={index}>
                    <span>{transaction.name}</span>
                    <span>{transaction.date}</span>
                    <span>{transaction.amount.toLocaleString()}</span>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>
      </div>
    </SidebarLayout>
  );
};

export default AdminDashboard;
