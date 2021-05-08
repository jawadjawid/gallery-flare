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
                            <p>Upload unlimitted images that are stored in secure Microsoft servers all around the world! </p>
                        </Col>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/49665-200.png"} className="photo" alt="Logo" />
                            <p>Upload a bulk of images with drag and drop to make everything faster! </p>
                        </Col>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/access-pngrepo-com.png"} className="photo" alt="Logo" />
                            <p>Accsess modfier to decide who can see your images! Where each user has a profile </p>
                        </Col>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/Hey_Machine_Learning_Logo.png"} className="photo" alt="Logo" />
                            <p>After upload, each image is processed with smart api's and given tags according to its charcerstics! </p>
                        </Col>
                    </Row>
                    <br />
                    <div className="headlinecontainer">
                        <h className="headline"> Search </h>
                    </div>
                    <br />
                    <br />
                    <Row>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/5c4cb57c426969027569a273.png"} className="photo" alt="Logo" />
                            <p>Search from all public images with very fast processing! </p>
                        </Col>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/advantage_accuracy-512.png"} className="photo" alt="Logo" />
                            <p>Search by tags and get all images with that tag with high accurecy! </p>
                        </Col>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/smart-search.png"} className="photo" alt="Logo" />
                            <p>Search by image and get all the images that are simmilar to it with no time! </p>
                        </Col>
                        <Col className="contain"><img src={"https://galleryflare.blob.core.windows.net/flare/easy.png"} className="photo" alt="Logo" />
                            <p>You can serach for alll the Public images without even creating an account! </p>
                        </Col>
                    </Row>
                </Container>
            </div>
        );
    }
}
