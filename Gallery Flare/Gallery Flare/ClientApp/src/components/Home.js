import React, { Component } from 'react';
import LoadingScreen from 'react-loading-screen';
import { Collapse, Container, Row, Col, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import '../custom.css';
import search from './media/search.png';
import stoarge from './media/stoarge.png';

export class Home extends Component {
  static displayName = Home.name;

  render () {
      return (
          <div>
              <Container>
                  <Row>
                      <Col className="contain"><img src={search} className="photo" alt="Logo" /> <p>Welcome to your new single-page application, built with:</p></Col>
                      <Col className="contain"><img src={stoarge} className="photo" alt="Logo" /> <p>Welcome to your new single-page application, built with:</p></Col>
                      <Col className="contain"><img src={search} className="photo" alt="Logo" /> <p>Welcome to your new single-page application, built with:</p></Col>
                      <Col className="contain"><img src={stoarge} className="photo" alt="Logo" /> <p>Welcome to your new single-page application, built with:</p></Col>
                  </Row>
                  <br/>
                  <Row>
                      <Col className="contain"><img src={search} className="photo" alt="Logo" /> <p>Welcome to your new single-page application, built with:</p></Col>
                      <Col className="contain"><img src={stoarge} className="photo" alt="Logo" /> <p>Welcome to your new single-page application, built with:</p></Col>
                      <Col className="contain"><img src={search} className="photo" alt="Logo" /> <p>Welcome to your new single-page application, built with:</p></Col>
                      <Col className="contain"><img src={stoarge} className="photo" alt="Logo" /> <p>Welcome to your new single-page application, built with:</p></Col>
                  </Row>
              </Container>
          </div>
    );
  }
}
