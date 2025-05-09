import React from 'react'
import { useNavigate } from 'react-router-dom'
import './Programs.css'
import program_1 from '../../assets/program-1.png'
import program_2 from '../../assets/program-2.png'
import program_3 from '../../assets/program-3.png' 
import program_icon_1 from '../../assets/program-icon-1.png' 
import program_icon_2 from '../../assets/program-icon-2.png' 
import program_icon_3 from '../../assets/program-icon-3.png' 

const Programs = () => {
  const navigate = useNavigate();

  const handleRedirect = () => {
    navigate('/pricing');
  };

  return (
    <div className='programs'>
      <div className="program" onClick={handleRedirect} style={{ cursor: 'pointer' }}>
        <img src={program_1} alt="" />
        <div className="caption">
            <img src={program_icon_1} alt="" />
            <p>Small-sized Companies</p>
        </div>
      </div>

      <div className="program" onClick={handleRedirect} style={{ cursor: 'pointer' }}>
        <img src={program_2} alt="" />
        <div className="caption">
            <img src={program_icon_2} alt="" />
            <p>Mid-sized Companies</p>
        </div>
      </div>

      <div className="program" onClick={handleRedirect} style={{ cursor: 'pointer' }}>
        <img src={program_3} alt="" />
        <div className="caption">
            <img src={program_icon_3} alt="" />
            <p>Large-sized Companies</p>
        </div>
      </div>
    </div>
  )
}

export default Programs
