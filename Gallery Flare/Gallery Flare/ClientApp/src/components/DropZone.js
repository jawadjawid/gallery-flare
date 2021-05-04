import React, { Component } from 'react'
import { DropzoneDialog } from 'material-ui-dropzone'
import Button from '@material-ui/core/Button';
import Access from './Access.js';
import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';
import { makeStyles } from '@material-ui/core/styles';

export default class DropZone extends Component {
    constructor(props) {
        super(props);
        this.accessDone = this.accessDone.bind(this)
        this.handleAccessOpen = this.handleAccessOpen.bind(this)
        this.setAccessValue = this.setAccessValue.bind(this)
        this.handleNotifacationSuccessClose = this.handleNotifacationSuccessClose.bind(this)
        this.handleNotifacationFailClose = this.handleNotifacationFailClose.bind(this)

        this.state = {
            open: false,
            files: [],
            accessOpen: false,
            accessValue: "public",
            notificationSuccessOpen: false,
            notificationFailOpen: false,
            failMsg: "",
            loading: false
        };
    }

    handleClose() {
        this.setState({
            open: false
        });
    }

    setAccessValue(value) {
        this.setState({
            accessValue: value
        });
    }

    async handleSave(files) {
        this.setState({
            files: files,
            open: false,
            loading: true
        });

        let failedFilesArray = [];
        let numSuccess = 0;
        let failedFiles = "";
        for (var i = 0; i < files.length; i++) {
            var formData = new FormData();
            formData.append('file', files[i]);
            formData.append('access', String(this.state.accessValue));

            await fetch('Upload', {
                method: 'POST',
                body: formData
            }).then((response) => {
                if (response.ok) {
                    numSuccess++;
          
                } else {
                    throw new Error('Something went wrong');
                }
            }).catch(() => {
                failedFilesArray.push(files[i].name);                
                failedFiles += failedFilesArray.join(", ");
            }) 
        }

        this.setState({
            loading: false,
        });

        if (numSuccess == files.length) {
            this.setState({
                notificationSuccessOpen: true
            });
        } else {
            this.setState({
                notificationFailOpen: true,
                failMsg: failedFiles,
            });
        }

    }

    handleOpen() {
        this.setState({
            open: true,
        });
    }

    handleAccessOpen() {
        this.setState({
            accessOpen: !this.state.accessOpen,
        });
    }

    accessDone() {
        this.setState({
            accessOpen: false,
            open: true,
        });
    }

    handleNotifacationSuccessClose() {
        this.setState({
            notificationSuccessOpen: false 
        });
    }

    handleNotifacationFailClose() {
        this.setState({
            notificationFailOpen: false
        });
    }

    render() {
        return (
            <div>
                <Snackbar open={this.state.notificationSuccessOpen} autoHideDuration={6000} anchorOrigin={{ vertical: 'top', horizontal: 'right' }} onClose={this.handleNotifacationSuccessClose}>
                    <MuiAlert elevation={6} variant="filled" onClose={this.handleNotifacationSuccessClose} severity="success">
                        All files uploaded successfully!
                    </MuiAlert>
                </Snackbar>

                <Snackbar open={this.state.notificationFailOpen} autoHideDuration={6000} anchorOrigin={{ vertical: 'top', horizontal: 'right' }} onClose={this.handleNotifacationFailClose}>
                    <MuiAlert elevation={6} variant="filled" onClose={this.handleNotifacationFailClose} severity="error">
                        {this.state.failMsg + " failed to upload"}
                    </MuiAlert>
                </Snackbar>

                <Access loading={this.state.loading} accessDone={this.accessDone} isOpen={this.state.accessOpen} handleAccessOpen={this.handleAccessOpen} setAccessValue={this.setAccessValue} accessValue={this.accessValue} />

                <DropzoneDialog
                    open={this.state.open}
                    onSave={this.handleSave.bind(this)}
                    acceptedFiles={['image/jpeg', 'image/png', 'image/bmp']}
                    showPreviews={true}
                    maxFileSize={5000000}
                    onClose={this.handleClose.bind(this)}
                    filesLimit={9999999999}
                />
            </div>
        );
    }
}