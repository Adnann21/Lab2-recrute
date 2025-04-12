import React, { useRef } from 'react'
import './Testimonials.css'
import next_icon from '../../assets/next-icon.png'
import back_icon from '../../assets/back-icon.png'
import user_1 from '../../assets/user-1.png'
import user_2 from '../../assets/user-2.png'
import user_3 from '../../assets/user-3.png'
import user_4 from '../../assets/user-4.png'


const Testimonials = () => {

    const slider = useRef();
        let tx = 0;

    const slideForward = ()=>{
        if(tx > -50){
            tx -= 25;
        }
        slider.current.style.transform = `translateX(${tx}%)`;
    }

    const slideBackward = ()=>{
        if(tx < 0){
            tx += 25;
        }
        slider.current.style.transform = `translateX(${tx}%)`;
    }

  return (
    <div className='testimonials'>
      <img src={next_icon} alt="" className='next-btn' onClick={slideForward} />
      <img src={back_icon} alt="" className='back-btn' onClick={slideBackward}/>
      <div className="slider">
        <ul ref={slider}>
            <li>
                <div className="slide">
                    <div className="user-info">
                        <img src={user_1} alt="" />
                        <div>
                            <h3>Benjamin Hayes</h3>
                            <span>Prishtine, Kosova</span>
                            <p>"This platform has completely transformed the way 
                                we manage our candidates. It's fast, intuitive, 
                                and makes finding the right talent effortless."</p>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div className="slide">
                    <div className="user-info">
                        <img src={user_2} alt="" />
                        <div>
                            <h3>Emrys Howell</h3>
                            <span>London, UK</span>
                            <p>Thanks to this platform, we've streamlined our recruitment process and 
                                can now match candidates to roles in minutes. 
                                It's an essential tool for our team."</p>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div className="slide">
                    <div className="user-info">
                        <img src={user_3} alt="" />
                        <div>
                            <h3>Alexander Chapman</h3>
                            <span>Manchester, UK</span>
                            <p>"The search and categorization features are top-notch. 
                                We've never been more organized or efficient 
                                in managing our talent pool."</p>
                        </div>
                    </div>
                </div>
            </li>
            <li>
                <div className="slide">
                    <div className="user-info">
                        <img src={user_4} alt="" />
                        <div>
                            <h3>Michael Page</h3>
                            <span>New York, USA</span>
                            <p>"An incredibly user-friendly system thats made candidate tracking 
                                and sector organization so much easier. 
                                We couldnt imagine working without it now."</p>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
      </div>
    </div>
  )
}

export default Testimonials
