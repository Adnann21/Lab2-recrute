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

function App() {
  const [playState, setPlayState] = useState(false);
  const location = useLocation();

  const isLoginPage = location.pathname === '/login';

  return (
    <div className={isLoginPage ? 'login-body' : 'normal-body'}>
      {!isLoginPage && <Navbar />}

      <Routes>
        <Route path="/login" element={<LoginRegister />} />
        <Route path="/" element={
          <>
            <Hero />
            <div className="container">
              <Title subTitle='OUR PRICING' title='What We Offer' />
              <Programs />
              <About setPlayState={setPlayState} />
              <Title subTitle='TESTIMONIALS' title='What Customer Says' />
              <Testimonials />
              <Title subTitle='Contact Us' title='Get In Touch' />
              <Contact />
              <Footer />
            </div>
            <VideoPlayer playState={playState} setPlayState={setPlayState} />
          </>
        } />
      </Routes>
    </div>
  );
}

export default App;