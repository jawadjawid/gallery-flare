import React from 'react';
import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Paper from '@material-ui/core/Paper';
//import { Form, Button, FormGroup, FormControl, FormLabel } from "react-bootstrap";
import { RadioGroup } from '@material-ui/core';
import { Radio } from '@material-ui/core';
import LoadingScreen from 'react-loading-screen';

import { FormControlLabel } from '@material-ui/core';



const Access = (props) => {
    //const [accessValue, setaccessValue] = React.useState("public")

    const handleChange = (event) => {
        props.setAccessValue(event.target.value);
    }

    return (
        <div>
            <Button variant="contained" color="primary" onClick={props.handleAccessOpen}>
                Upload
            </Button>
            <Dialog
                open={props.isOpen || props.loading}
                onClose={props.handleAccessOpen}
            >
                <DialogTitle>
                    Access to photos
                </DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Here you can decide who can see your pictures!
                    </DialogContentText>
                   
                    <RadioGroup defaultValue="public" aria-label="access-value" name="customized-radios" value={props.accessValue} onChange={handleChange}>
                        <FormControlLabel value="public" control={<Radio />} label="Public" />
                        <FormControlLabel value="private" control={<Radio />} label="Private" />
                    </RadioGroup>
           
                </DialogContent>
                <DialogActions>
                    <Button variant="contained" onClick={ props.handleAccessOpen}>
                        Cancel
                </Button>
                    <Button variant="contained" onClick={props.accessDone} color="primary">
                        Next
                 </Button>
                </DialogActions>
                <LoadingScreen
                    loading={props.loading}
                    bgColor='#f1f1f1'
                    spinnerColor='#9ee5f8'
                    textColor='#676767'
                    children= ''
                >
                </LoadingScreen>
            </Dialog>
        </div>
    );
}

export default Access;