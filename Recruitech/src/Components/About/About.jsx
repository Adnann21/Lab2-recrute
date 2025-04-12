import React from 'react'
import './About.css'
import about_img from '../../assets/about.png'
import play_icon from '../../assets/play-icon.png'

const About = ({setPlayState}) => {
  return (
    <div className='about'>
      <div className="about-left">
        <img src={about_img} alt="" className='about-img'/>
        <img src={play_icon} alt="" className='play-icon' onClick={()=>{setPlayState(true)}}/>
      </div>
      <div className="about-right">
        <h3>ABOUT RECRUITECH</h3>
        <h2>Smarter Candidate Management for Recruitment Agencies</h2>
        <p>Our recruitment platform is built specifically for recruitment agencies, 
            offering powerful tools to store, organize, and manage candidate profiles efficiently. 
            With sector-based categorization, customizable tags, and detailed candidate profiles, 
            agencies can easily keep their talent pool organized and ready for any opportunity. 
            Whether you're working across multiple industries or focusing on a niche market, 
            our system adapts to your needs and streamlines your workflow.</p>
        <p>Advanced search features allow recruiters to quickly find the right candidates based on skills, 
            experience, location, and more. From initial registration to final placement, 
            our platform helps agencies save time, stay organized, 
            and deliver better results to their clients. 
            It's the smart, scalable solution for modern recruitment teams.</p>
      </div>
    </div>
  )
}

export default About
