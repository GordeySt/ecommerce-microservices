import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Typography from '@material-ui/core/Typography';
import { Container, IconButton, Menu, MenuItem, Toolbar } from '@material-ui/core';
import { useState } from 'react';
import AccountCircleIcon from '@material-ui/icons/AccountCircle';
import { signoutRedirect } from '../auth/userService';
import { getAccessToken, getIdToken } from '../auth/authHeaders';

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {
            flexGrow: 1,
        },
        menuButton: {
            marginRight: theme.spacing(2),
        },
        title: {
            flexGrow: 1,
        },
        appBar: {
            marginBottom: '20px',
        },
    })
);

const NavBar = () => {
    const classes = useStyles();
    const isUserLoggedIn = getAccessToken();
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };
    return (
        <div className={classes.root}>
            <AppBar position="static" className={classes.appBar}>
                <Container maxWidth="lg">
                    <Toolbar>
                        <Typography variant="h6" className={classes.title}>
                            App
                        </Typography>
                        <div>
                            <IconButton
                                aria-label="account of current user"
                                aria-controls="menu-appbar"
                                aria-haspopup="true"
                                onClick={handleMenu}
                                color="inherit"
                            >
                                <AccountCircleIcon />
                            </IconButton>
                            <Menu
                                id="menu-appbar"
                                anchorEl={anchorEl}
                                anchorOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                keepMounted
                                transformOrigin={{
                                    vertical: 'top',
                                    horizontal: 'right',
                                }}
                                open={open}
                                onClose={handleClose}
                            >
                                {isUserLoggedIn && (
                                    <MenuItem onClick={() => signoutRedirect({ id_token_hint: getIdToken() })}>
                                        Logout
                                    </MenuItem>
                                )}
                            </Menu>
                        </div>
                    </Toolbar>
                </Container>
            </AppBar>
        </div>
    );
};

export default NavBar;
