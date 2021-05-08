import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Paper from '@material-ui/core/Paper';
//import { Form, Button, FormGroup, FormControl, FormLabel } from "react-bootstrap";
import { makeStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import LoadingScreen from 'react-loading-screen';
import { NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

import '../../custom.css';


const InitialSearchModal = (props) => {

    const handleChange = (event) => {
        props.setAccessValue(event.target.value);
    }

    return (
        <div>


            <NavLink tag={Link} className="text-white" color="primary" onClick={props.handleAccessOpen}>
                Search
            </NavLink>

            <Dialog

                open={props.isOpen || props.loading}
                onClose={props.handleAccessOpen}
            >
                <DialogTitle>
                    Search
                </DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Please use one word to have good results
                    </DialogContentText>

                    <div>
                        <TextField onChange={handleChange} id="standard-basic" label="Image Tag" />
                    </div>
                    <br />
                    <span onClick={props.searchByImage} className="fake-link">Search by image?</span>
                </DialogContent>
                <DialogActions>
                    <Button variant="contained" onClick={props.handleAccessOpen}>
                        Cancel
                    </Button>
                    <Button variant="contained" onClick={props.searchByText} color="primary">
                        Search
                 </Button>


                    <LoadingScreen
                        loading={props.loading}
                        bgColor='#f1f1f1'
                        spinnerColor='#9ee5f8'
                        textColor='#676767'
                        children=''
                    >
                    </LoadingScreen>

                </DialogActions>
            </Dialog>
        </div>
    );
}

export default InitialSearchModal;