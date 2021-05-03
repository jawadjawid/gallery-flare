import React, { Component } from 'react'
import { DropzoneDialog } from 'material-ui-dropzone'
import Button from '@material-ui/core/Button';
import InitialSearchModal from './InitialSearchModal'
import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';
import { makeStyles } from '@material-ui/core/styles';
import { Redirect } from "react-router";
import { withRouter } from 'react-router-dom';
import CircularProgress from '@material-ui/core/CircularProgress';

import LoadingScreen from 'react-loading-screen';

class SearchByImageModal extends Component {
    constructor(props) {
        super(props);
        this.searchByImage = this.searchByImage.bind(this)
        this.handleAccessOpen = this.handleAccessOpen.bind(this)
        this.setAccessValue = this.setAccessValue.bind(this)
        this.handleNotifacationSuccessClose = this.handleNotifacationSuccessClose.bind(this)
        this.handleNotifacationFailClose = this.handleNotifacationFailClose.bind(this)
        this.searchByText = this.searchByText.bind(this)
      
        this.state = {
            open: false,
            files: [],
            accessOpen: false,
            searchQuery: "",
            notificationSuccessOpen: false,
            notificationFailOpen: false,
            failMsg: "",
            doneSearch: false,
            data: [],
            loading: false,
        };
    }

    handleClose() {
        this.setState({
            open: false,
        });
    }

    setAccessValue(value) {
        this.setState({
            searchQuery: value
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

            const res = await fetch('Search', {
                method: 'POST',
                body: formData
            }).then((response) => {
                if (response.ok) {
                    numSuccess++;
                    response.json().then(json => {
                        this.props.history.push({ pathname: '/gallery', state: { startedLoading: true, data: json, hasLoaded: true } });
                    });
                    this.setState({
                        loading: false,
                    });

                } else {
                    throw new Error('Something went wrong');
                }
            }).catch(() => {
                failedFilesArray.push(files[i].name);
                failedFiles += failedFilesArray.join(", ");
            })
        }

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

    searchByImage() {
        this.setState({
            accessOpen: false,
            open: true,
        });
    }

    async searchByText() {
        //let { history } = this.props

         const response = await fetch('Search/' + this.state.searchQuery);
         const data = await response.json();
         this.setState({
             data: data,
             accessOpen: false,
         });
        this.props.history.push({ pathname: '/gallery', state: { startedLoading: true, data: this.state.data, hasLoaded: true } });
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

                <InitialSearchModal loading={this.state.loading } searchByImage={this.searchByImage} isOpen={this.state.accessOpen} handleAccessOpen={this.handleAccessOpen} setAccessValue={this.setAccessValue} searchQuery={this.searchQuery} searchByText={this.searchByText}/>

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

export default withRouter(SearchByImageModal);
