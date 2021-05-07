import React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { withRouter } from 'react-router-dom';
import { BrowserRouter, Link, Route } from 'react-router-dom';

const Logout = (props) => {

    const handleLogOut = async (context) => {
        await fetch('Authentication/Logout').then((response) => {
            if (response.ok) {
                //props.history.push('/');
                window.location.href = "/"
            } else {
                throw new Error('Something went wrong');
            }
        }).catch(() => {
        });
    }

    return (
        <BrowserRouter>
            <div>
                <NavLink tag={Link} className="text-dark" color="primary" onClick={handleLogOut}>
                    Logout
                </NavLink>
            </div>
        </BrowserRouter>
    );
}

export default withRouter(Logout);