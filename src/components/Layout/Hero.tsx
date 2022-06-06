import React, { useEffect, useState, useContext } from "react";
import { UserContext } from '../contexts/UserContext';
import { Blog } from '../Blog/BlogsList';
import { Link } from "react-router-dom";

export default function Hero() {
  const {token} = useContext(UserContext);
  const [isLoading, setIsLoading] = useState(true);
  const [blogs, setBlogs] = useState<Blog[]>([]);

  var [currentIndex, setCurrentIndex] = useState(0);

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch(process.env.REACT_APP_API_URL + 'Blog/GetNewestBlogs', {
        method: 'GET',
        headers: token != null ? {'Authorization': token} : {}
      });
      const data = await response.json();

      setBlogs(data.result as Blog[]);
      setIsLoading(false);
    }

    setIsLoading(true);
    fetchData();
  },[])

  const scrollTop = () => window['scrollTo']({ top: 0, behavior: 'smooth' });
  
  return (
    <div className="center-text">
            <div id="userSettingsForm">
                <div className="container" style={{border: '10px solid rgb(204 202 204)', paddingRight: '0px', paddingLeft: '0px'}}>
                    <div className="hero-row row" style={{background: 'white'}}>
                        <div className="leftPanelHero col-12 col-lg-8 panelBox">
                            <div>
                                <h2 style={{paddingTop: '10px'}}>Most Recent</h2>
                                
                            </div>
                            <div style={{margin: '35px', backgroundColor: 'rgba(159, 169, 156, 0.442)'}}>
                                <img src={blogs[currentIndex]?.imageUrl} className="disabled-link" style={{width: '100%'}}/>
                            </div>
                            <div style={{padding: '20px'}}>
                              <button className="blogButton" onClick={() => setCurrentIndex(currentIndex = 0)}></button>
                              <button className="blogButton" onClick={() => setCurrentIndex(currentIndex = 1)}></button>
                              <button className="blogButton" onClick={() => setCurrentIndex(currentIndex = 2)}></button>
                            </div>
                            
                            <div className="panelBox">
                            </div>  
                        </div>
                            
                        <div className="rightPanelHero col-12 col-lg-4 panelBox">
                            <h2>{blogs[currentIndex]?.title}</h2>
                            <div style={{textAlign: 'left'}}>
                              <h5>{blogs[currentIndex]?.content.substring(0, 521)} ...</h5>
                            </div>
                            <Link to={`/blog/${blogs[currentIndex]?.id}`}><button className="heroButton" onClick={() => scrollTop()} >Read more</button></Link>
                        </div>
                        
                    </div>
                </div>

              <div></div>

            </div>
        </div>
  );
}
