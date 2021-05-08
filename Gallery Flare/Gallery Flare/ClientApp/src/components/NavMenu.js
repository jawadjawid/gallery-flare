import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import DropZone from './DropZone';
import SearchByImageModal from './Search/SearchByImageModal';
import Logout from './Auth/Logout';
import Button from '@material-ui/core/Button';
import { withRouter } from 'react-router-dom';
import { BrowserRouter, Route, useHistory } from 'react-router-dom';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            open: false,
            isLoggedIn: false,
            isCalled: false,
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    async refreshNav() {
        await fetch('Authentication/User', {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        }).then((response) => {
            if (response.ok) {
                this.setState({
                    isLoggedIn: true,
                    //isCalled: false
                });
            } else {
                throw new Error('Something went wrong');
            }
        }).catch(() => {
            this.setState({
                isLoggedIn: false,
                //isCalled: false
            });
        });
    }

    componentDidMount() {
        if (!this.state.isCalled) {
            this.refreshNav();
            this.setState({
                isCalled: true
            });
        }
        //setInterval(() => this.setState({ isCalled: false }), 10000)
    }


    routeChange(context) {
        let path = '/';
        let history = useHistory();
        history.push(path);
        //this.props.history.push('/');
    }


    render() {
        let nav;
        if (this.state.isLoggedIn) {
            nav =
                <React.Fragment>
                    <NavItem>
                        <NavLink tag={Link} className="text-white" to="/">Home</NavLink>
                    </NavItem>

                    {/*  <div className="uploadButton">
                    <Button color="primary" variant="contained" onClick={this.routeChange}>
                            Home
                        </Button>
                    </div> */}


                    <NavItem>
                        <NavLink tag={Link} className="text-white" to="/gallery">My Gallery</NavLink>
                    </NavItem>

                    <NavItem>
                        <NavLink tag={Link} className="text-white" to="/public">Public Gallery</NavLink>
                    </NavItem>
                    <DropZone />

                    <SearchByImageModal />

                    <NavItem>
                        <Logout />
                    </NavItem>
                </React.Fragment>;
        } else {
            nav = <React.Fragment>
                <NavItem>
                    <NavLink tag={Link} className="text-white" to="/">Home</NavLink>
                </NavItem>

                <NavItem>
                    <NavLink tag={Link} className="text-white" to="/login">Log In</NavLink>
                </NavItem>

                <NavItem>
                    <NavLink tag={Link} className="text-white" to="/signup">Sign Up</NavLink>
                </NavItem>

                <NavItem>
                    <NavLink tag={Link} className="text-white" to="/public">Public Gallery</NavLink>
                </NavItem>

                <SearchByImageModal />

            </React.Fragment>;
        }

        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand to="/">
                            <img
                                alt=""
                                //src="./logo.svg"
                                src={require('./logo.svg')}
                                width="30"
                                height="30"
                                className="d-inline-block align-top"
                            />{' '}
                            <span className="text-white">Flare</span>
                        </NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                {nav}
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
export default withRouter(NavMenu);