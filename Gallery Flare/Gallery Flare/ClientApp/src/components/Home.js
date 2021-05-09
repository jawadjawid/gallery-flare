import React, { Component } from 'react';
import LoadingScreen from 'react-loading-screen';
import { Collapse, Container, Row, Col, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import '../custom.css';
import search from './media/search.png';
import stoarge from './media/stoarge.png';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>

                <br />
                <br />
         

                <div className ="headlinecontainer">
                    <h className="headline"> Upload </h>
                </div>
                <br />

                <Container>
                    <Row>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/secure-png-.png"} className="photo" alt="Logo" />
                            <p>Upload unlimited images securely to Microsoft servers all around the world! </p>
                        </Col>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/49665-200.png"} className="photo" alt="Logo" />
                            <p>Upload multiple images at once with the drag and drop functionality for more efficiency! </p>
                        </Col>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/access-pngrepo-com.png"} className="photo" alt="Logo" />
                            <p>Decide if other users can see your images by making them public or private</p>
                        </Col>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/Hey_Machine_Learning_Logo.png"} className="photo" alt="Logo" />
                            <p>Each image is processed with smart APIs and given tags according to its characteristics for faster search functionality! </p>
                        </Col>
                    </Row>
                    <br />
                    <div className="headlinecontainer">
                        <h className="headline"> Search </h>
                    </div>
                    <br />
                    <br />
                    <Row>
                        <Col className="contain2"><img src={"https://galleryflare.blob.core.windows.net/flare/5c4cb57c426969027569a273.png"} className="photo" alt="Logo" />
                            <p>Search from all public images with very fast processing! </p>
                        </Col>
                        <Col className="contain2"><img src={"https://galleryflare.blob.core.windows.net/flare/advantage_accuracy-512.png"} className="photo" alt="Logo" />
                            <p>Search by tags and get all corelated images with high accurecy! </p>
                        </Col>
                        <Col className="contain2"><img src={"https://galleryflare.blob.core.windows.net/flare/smart-search.png"} className="photo" alt="Logo" />
                            <p> Search by image and get all the images that are similar to it with no time! </p>
                        </Col>
                        <Col className="contain2"><img src={"https://galleryflare.blob.core.windows.net/flare/easy.png"} className="photo" alt="Logo" />
                            <p>You can search and view all Public images without creating an account! </p>
                        </Col>
                    </Row>

                    <br />
                    <div className="headlinecontainer">
                        <h className="headline"> Contact </h>
                    </div>
                    <br />
                    <br />
                    <Row>
                        <Col xs lg="2" md="auto" ></Col>

                        <Col onClick={() => window.open("http://www.linkedin.com/in/jawad-jawid")} xs lg="3" md="auto" className="contact"><img src={"https://galleryflare.blob.core.windows.net/flare/174857.png"} className="photo" alt="Logo" />
                            <p>Connect with me through LinkedIn! </p>
                        </Col>
                        <Col xs lg="2" md="auto" ></Col>
                        <Col onClick={() => window.open("https://github.com/jawadjawid")} xs lg="3" md="auto" className="contact"><img src={"https://galleryflare.blob.core.windows.net/flare/Octocat.png"} className="photo" alt="Logo" />
                            <p>Checkout my GitHub for more amazing projects like this one! </p>
                        </Col>
                    </Row>
                    <br />
                    <br />
                    <br />
                    <br />

                </Container>


            </div>
        );
    }
}
