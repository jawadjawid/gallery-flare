﻿import React, { Component } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import InfoIcon from '@material-ui/icons/Delete';
import { common } from '@material-ui/core/colors';

const useStyles = makeStyles((theme) => ({
    root: {
        justifyContent: 'space-around',
        overflow: 'hidden',
        backgroundColor: theme.palette.background.paper,
    },
    gridList: {
        width: 900,
        height: 450,
    },
    icon: {
        color: 'rgba(255, 255, 255, 0.54)',
    },
}));


const Gallery = (props) => {
    const [forecasts, setForecasts] = React.useState([]);
    const [hasLoaded, setHasLoaded] = React.useState(false);
    const [startedLoading, setstartedLoading] = React.useState(false);
    const [loggedUser, setLoggedUser] = React.useState("UserName");

    const classes = useStyles();

    const populateGallery = async () => {
        setstartedLoading(true);
        const images = await fetch('Gallery/personal');
        const data = await images.json();
        setForecasts(data);
        await fetch('Authentication/User', {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }}).then((response) => {
            if (response.ok) {
                response.json().then(msg => {
                    setLoggedUser(msg);
                    console.log(loggedUser);
                });
            } else { 
                throw new Error('Something went wrong');
            }
        }).catch(() => {
        }) ;
        setHasLoaded(true);
    }

    if (!startedLoading && props.location.state == undefined ) {
        populateGallery();
    }

    let data;

    if (forecasts.length != 0 && props.location.state == undefined) {
        data = forecasts.map((tile) => (
            <GridListTile key={tile.img}>
                <img src={tile.img} alt={tile.title} />
                <GridListTileBar
                    title={tile.title}
                    subtitle={<span>by: {tile.author}</span>}
                    actionIcon={
                        <IconButton aria-label={`info about ${tile.title}`} className={classes.icon}>
                            <InfoIcon />
                        </IconButton>
                    }
                />
            </GridListTile>
        ));
    } else if (props.location.state != undefined && props.location.state.data.length != 0) {
        data = props.location.state.data.map((tile) => (
            <GridListTile key={tile.img}>
                <img src={tile.img} alt={tile.title} />
                <GridListTileBar
                    title={tile.title}
                    subtitle={<span>by: {tile.author}</span>}
                    actionIcon={
                        <IconButton aria-label={`info about ${tile.title}`} className={classes.icon}>
                            <InfoIcon />
                        </IconButton>
                    }
                />
            </GridListTile>
        ));
        window.history.replaceState(null, '');
    }
    else if (hasLoaded || props.location.state != undefined) {
        data = <h1>No Images...</h1>;
    } else {
        data = <h1>Loading Images...</h1>;
    }

    return (
        <div >
            <h4>{loggedUser}'s Gallery</h4>
            <GridList cellHeight={180} >
                <GridListTile key="Subheader" cols={2} style={{ height: 'auto' }}>
                </GridListTile>
                {data}
            </GridList>
        </div>
    );
}

export default Gallery;
