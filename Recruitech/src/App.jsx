import React, { useState } from 'react';
import { Routes, Route, useLocation } from 'react-router-dom';
import Navbar from './Components/Navbar/Navbar';
import Hero from './Components/Hero/Hero';
import Programs from './Components/Programs/Programs';
import Title from './Components/Title/Title';
import About from './Components/About/About';
import Testimonials from './Components/Testimonials/Testimonials';
import Contact from './Components/Contact/Contact';
import Footer from './Components/Footer/Footer';
import VideoPlayer from './Components/VideoPlayer/VideoPlayer';
import LoginRegister from './Components/LoginRegister/LoginRegister';
import PricingPage from './Components/PricingPage/PricingPage';
import CheckoutPage from './Components/CheckoutPage/CheckoutPage';
import ChangePassword from './Components/ChangePassword/ChangePassword';

function App() {
  const [playState, setPlayState] = useState(false);
  const location = useLocation();

  const isLoginPage = location.pathname === '/login';
  const isPricingPage = location.pathname === '/pricing';
  const isCheckoutPage = location.pathname === '/checkout';
  const isChangePassword = location.pathname === '/changepassword';

  const getBodyClass = () => {
    if (isLoginPage || isCheckoutPage) return 'login-body';
    if (isPricingPage) return 'pricing-body';
    if (isChangePassword) return 'cp-body'; // 👈 Add your custom class here
    return 'normal-body';
  };

  return (
    <div className={getBodyClass()}>
      {!(isLoginPage || isPricingPage || isCheckoutPage || isChangePassword) && <Navbar />}

      <Routes>
        <Route path="/login" element={<LoginRegister />} />
        <Route path="/pricing" element={<PricingPage />} />
        <Route path="/checkout" element={<CheckoutPage />} />
        <Route path="/changepassword" element={<ChangePassword />} />
        <Route
          path="/"
          element={
            <>
              <Hero />
              <div className="container">
                <Title subTitle="OUR PRICING" title="What We Offer" />
                <Programs />
                <About setPlayState={setPlayState} />
                <Title subTitle="TESTIMONIALS" title="What Customer Says" />
                <Testimonials />
                <Title subTitle="Contact Us" title="Get In Touch" />
                <Contact />
                <Footer />
              </div>
              <VideoPlayer playState={playState} setPlayState={setPlayState} />
            </>
          }
        />
      </Routes>
    </div>
  );
}

export default App;
