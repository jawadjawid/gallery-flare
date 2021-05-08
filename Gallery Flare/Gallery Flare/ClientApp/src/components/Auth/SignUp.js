import React from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Link from '@material-ui/core/Link';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';

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
    paper: {
        marginTop: theme.spacing(8),
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
        marginTop: theme.spacing(3),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));

export default function SignUp(props) {
    const classes = useStyles();

    const [username, setUsername] = React.useState("");
    const [password, setPassword] = React.useState("");
    const [notificationSuccessOpen, setNotificationSuccessOpen] = React.useState(false);
    const [notificationFailOpen, setNotificationFailOpen] = React.useState(false);
    const [failMsg, setFailMsg] = React.useState("Fail. Please Try again!");


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

    const submit = (e) => {
        e.preventDefault();
        if (String(username) == "" || String(password) == "") {
            setNotificationFailOpen(true)
            setFailMsg("User Name and Password are mandatory!");
            return;
        }

        fetch('Authentication/SignUp', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ username: String(username), password: String(password) }),
            'dataType': 'json',
        }).then((response) => {
            if (response.ok) {
                //props.history.push({ pathname: '/login' });
                setNotificationSuccessOpen(true)

            } else {
                response.json().then(msg => {
                    if (msg == "User name is already taken!") {
                        setFailMsg(msg);
                    }
                });
                throw new Error('Something went wrong');
            }
        }).catch(() => {
            setFailMsg("Fail. Please Try again!");
            setNotificationFailOpen(true)
        })
    }

    return (
        //<div>

        //</div>

        <Container component="main" maxWidth="xs">
            <Snackbar open={notificationSuccessOpen} autoHideDuration={6000} anchorOrigin={{ vertical: 'top', horizontal: 'right' }} onClose={handleNotifacationSuccessClose}>
                <MuiAlert elevation={6} variant="filled" onClose={handleNotifacationSuccessClose} severity="success">
                    Signup success!
                    </MuiAlert>
            </Snackbar>

            <Snackbar open={notificationFailOpen} autoHideDuration={6000} anchorOrigin={{ vertical: 'top', horizontal: 'right' }} onClose={handleNotifacationFailClose}>
                <MuiAlert elevation={6} variant="filled" onClose={handleNotifacationFailClose} severity="error">
                    {failMsg}
                </MuiAlert>
            </Snackbar>

            <CssBaseline />
            <div className={classes.paper}>
                <Avatar className={classes.avatar}>
                    <LockOutlinedIcon />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Sign up
        </Typography>
                <form className={classes.form} noValidate>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                required
                                fullWidth
                                id="username"
                                onChange={handleUserNameChange}
                                label="User Name"
                                name="username"
                                autoComplete="username"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                variant="outlined"
                                required
                                fullWidth
                                onChange={handlePasswordChange}
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="current-password"
                            />
                        </Grid>

                    </Grid>
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={submit}
                        className={classes.submit}
                    >
                        Sign Up
                </Button>
                    <Grid container justify="flex-end">
                        <Grid item>
                            <Link href="/login" variant="body2">
                                Already have an account? Sign in
                            </Link>
                        </Grid>
                    </Grid>
                </form>
            </div>
            <Box mt={5}>
                <Copyright />
            </Box>
        </Container>
    );
}