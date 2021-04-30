import React, { Component } from 'react'
import { DropzoneDialog } from 'material-ui-dropzone'
import Button from '@material-ui/core/Button';
import Access from './Access.js';

export default class DropZone extends Component {
    constructor(props) {
        super(props);
        this.accessDone = this.accessDone.bind(this)
        this.handleAccessOpen = this.handleAccessOpen.bind(this)
        this.setAccessValue = this.setAccessValue.bind(this)

        this.state = {
            open: false,
            files: [],
            accessOpen: false,
            accessValue: "public"
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

    handleSave(files) {
        this.setState({
            files: files,
            open: false
        });

        for (var i = 0; i < files.length; i++) {
            var formData = new FormData();
            formData.append('file', files[i]);
            formData.append('access', String(this.state.accessValue));
            console.log()
            fetch('Upload', {
                method: 'POST',
                body: formData
            })
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

    render() {
        return (
            <div>
                <Access accessDone={this.accessDone} isOpen={this.state.accessOpen} handleAccessOpen={this.handleAccessOpen} setAccessValue={this.setAccessValue} accessValue={this.accessValue} />

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