import React from 'react';
//import Button from '@material-ui/core/Button';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Paper from '@material-ui/core/Paper';
import { Form, Button, FormGroup, FormControl, FormLabel } from "react-bootstrap";
import { RadioGroup } from '@material-ui/core';
import { Radio } from '@material-ui/core';

import { FormControlLabel } from '@material-ui/core';



const Access = (props) => {
    //const [accessValue, setaccessValue] = React.useState("public")

    const handleChange = (event) => {
        props.setAccessValue(event.target.value);
    }

    //React.useEffect(() => { console.log(accessValue) }, [accessValue]);

    return (
        <div>
            <Button className="btn btn-primary btn-sm" color="primary" onClick={props.handleAccessOpen}>
                Upload
            </Button>
            <Dialog
                open={props.isOpen}
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
                    <Button className="btn btn-light btn-sm" onClick={ props.handleAccessOpen}>
                        Cancel
                </Button>
                    <Button className="btn btn-primary btn-sm" onClick={props.accessDone} color="primary">
                        Next
                 </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}

export default Access;