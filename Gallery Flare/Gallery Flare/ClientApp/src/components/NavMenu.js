import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import DropZone from './DropZone';
import InitialSearchModal from './Search/InitialSearchModal';
import SearchByImageModal from './Search/SearchByImageModal';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            open: false
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                       
                        <NavbarBrand to="/">
                            <img
                                alt=""
                                src="../../../logo.svg"
                                width="30"
                                height="30"
                                className="d-inline-block align-top"
                            />{' '}
                          Gallery Flare
                        </NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                                </NavItem>
                     
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/gallery">My Gallery</NavLink>
                                </NavItem>

                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/login">Log In</NavLink>
                                </NavItem>

                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/signup">Sign Up</NavLink>
                                </NavItem>

                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/gallery">Public Gallery</NavLink>
                                </NavItem>
                                <div className="uploadButton">
                                    <DropZone/>
                                </div> 
                                <div className="uploadButton">
                                    <SearchByImageModal />
                                </div>  


                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
