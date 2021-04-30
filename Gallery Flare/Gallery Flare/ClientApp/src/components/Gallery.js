import React, { Component } from 'react';

import { makeStyles } from '@material-ui/core/styles';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import InfoIcon from '@material-ui/icons/Delete';
import tileData from './tileData';
import Button from '@material-ui/core/Button';

const useStyles = makeStyles((theme) => ({
    root: {
        //display: 'flex',
        //flexWrap: 'wrap',
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


/**
 * The example data is structured as follows:
 *
 * import image from 'path/to/image.jpg';
 * [etc...]
 *
 * const tileData = [
 *   {
 *     img: image,
 *     title: 'Image',
 *     author: 'author',
 *   },
 *   {
 *     [etc...]
 *   },
 * ];
 */


const Gallery = () => {
    const [forecasts, setForecasts] = React.useState([]);
    const [loading, setLoading] = React.useState(true);
    const [hasLoaded, setHasLoaded] = React.useState(false);

    const classes = useStyles();

    const populateWeatherData = async () => {
        const response = await fetch('weatherforecast');
        const data = await response.json();

        setForecasts(data);
        setLoading(false);
    }

    if (!hasLoaded) {
        populateWeatherData();
        setHasLoaded(true);
    }

    React.useEffect(() => console.log(forecasts.length), [forecasts]);
    let data;

    if (forecasts.length != 0) {
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
    } else {
        data = <h1>Loading Images...</h1>;
    }

    return (
        <div >
            <GridList cellHeight={180} >
                <GridListTile key="Subheader" cols={2} style={{ height: 'auto' }}>
                    <ListSubheader component="div">Gallery</ListSubheader>
                </GridListTile>
                {data}
            </GridList>
        </div>
    );
}

export default Gallery;
