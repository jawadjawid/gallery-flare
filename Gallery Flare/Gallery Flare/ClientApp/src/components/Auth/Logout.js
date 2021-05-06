import React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { withRouter } from 'react-router-dom';


const Logout = (props) => {

    const handleLogOut = async (props) => {
        await fetch('Authentication/Logout').then((response) => {
            if (response.ok) {
                console.log("3");
                console.log(props.history);
                //props.history.push({ pathname: '/' });
                console.log("3");

            } else {
                throw new Error('Something went wrong');
            }
        }).catch(() => {
        });
    }

    return (
        <div>
            <NavLink className="btn btn-primary btn-sm" color="primary" onClick={handleLogOut}>
                Logout
            </NavLink>
        </div>
    );
}

export default withRouter(Logout);