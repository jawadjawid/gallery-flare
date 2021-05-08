import React from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Link from '@material-ui/core/Link';
import Paper from '@material-ui/core/Paper';
import Box from '@material-ui/core/Box';
import Grid from '@material-ui/core/Grid';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';
import { useLocation } from "react-router-dom";

function Copyright() {
    return (
        <Typography variant="body2" color="textSecondary" align="center">
            {'Copyright © '}
            <Link color="inherit" href="#">
                Gallery Flare. Jawad Jawid
      </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

const useStyles = makeStyles((theme) => ({
    root: {
        height: '100vh',
    },
    image: {
        backgroundImage: 'url(https://source.unsplash.com/random)',
        backgroundRepeat: 'no-repeat',
        backgroundColor:
            theme.palette.type === 'light' ? theme.palette.grey[50] : theme.palette.grey[900],
        backgroundSize: 'cover',
        backgroundPosition: 'center',
    },
    paper: {
        margin: theme.spacing(8, 4),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));

export default function SignInSide(props) {
    const classes = useStyles();
    const [username, setUsername] = React.useState("");
    const [password, setPassword] = React.useState("");
    const [notificationSuccessOpen, setNotificationSuccessOpen] = React.useState(false);
    const [notificationFailOpen, setNotificationFailOpen] = React.useState(false);
    const [failMsg, setFailMsg] = React.useState("Invalid Credentials");
    const [loggedInUserName, setLoggedInUserName] = React.useState("");

    const handleUserNameChange = (event) => {

        setUsername(event.target.value);
    }

    const handlePasswordChange = (event) => {
        setPassword(event.target.value);

    }

    const handleNotifacationSuccessClose = () => {
        setNotificationSuccessOpen(false)
    }

    const handleNotifacationFailClose = () => {
        setNotificationFailOpen(false)
    }

    const handleNav = () => {
        // let location = useLocation();
        //console.log(location.navBarLoggedIn);

        //props.history.push({ pathname: '/gallery' });
        // window.location.reload();
        window.location.href = "/gallery"

    }

    const submit = async (e) => {
        e.preventDefault();

        if (String(username) == "" || String(password) == "") {
            setNotificationFailOpen(true)
            setFailMsg("User Name and Password are mandatory!");
            return;
        }

        fetch('Authentication/Login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ username: String(username), password: String(password) }),
            'dataType': 'json',
        }).then((response) => {
            if (response.ok) {
                //setNotificationSuccessOpen(true)
                //setLoggedInUserName("Welcome");
                //handleNav();
                response.json().then(msg => {
                    setNotificationSuccessOpen(true)
                    setLoggedInUserName(msg);

                }).then(() => { handleNav(); });

            } else {

                throw new Error('Something went wrong');
            }
        }).catch(() => {

            setFailMsg("Invalid Credentials");
            setNotificationFailOpen(true)
        })
    }

    return (

        <Grid container component="main" className={classes.root}>
            {props.location.navBarLoggedIn}
            <Snackbar open={notificationSuccessOpen} autoHideDuration={6000} anchorOrigin={{ vertical: 'top', horizontal: 'right' }} onClose={handleNotifacationSuccessClose}>
                <MuiAlert elevation={6} variant="filled" onClose={handleNotifacationSuccessClose} severity="success">
                    Welcome {loggedInUserName}!
                    </MuiAlert>
            </Snackbar>

            <Snackbar open={notificationFailOpen} autoHideDuration={6000} anchorOrigin={{ vertical: 'top', horizontal: 'right' }} onClose={handleNotifacationFailClose}>
                <MuiAlert elevation={6} variant="filled" onClose={handleNotifacationFailClose} severity="error">
                    {failMsg}
                </MuiAlert>
            </Snackbar>

            <CssBaseline />
            <Grid item xs={false} sm={4} md={7} className={classes.image} />
            <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
                <div className={classes.paper}>
                    <Avatar className={classes.avatar}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Sign in
          </Typography>
                    <form className={classes.form} noValidate>
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            onChange={handleUserNameChange}
                            id="username"
                            label="User Name"
                            name="username"
                            autoComplete="username"
                            autoFocus
                        />
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            onChange={handlePasswordChange}
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                        />

                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={classes.submit}
                            onClick={submit}

                        >
                            Sign In
                        </Button>
                        <Grid container>

                            <Grid item>
                                <Link href="/signup" variant="body2">
                                    {"Don't have an account? Sign Up"}
                                </Link>
                            </Grid>
                        </Grid>
                        <Box mt={5}>
                            <Copyright />
                        </Box>
                    </form>
                </div>
            </Grid>
        </Grid>
    );
}