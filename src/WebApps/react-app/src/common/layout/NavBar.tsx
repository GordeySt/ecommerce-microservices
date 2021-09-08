﻿import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Typography from '@material-ui/core/Typography';
import { Container, IconButton, Menu, MenuItem, Toolbar } from '@material-ui/core';
import { useState } from 'react';
import MenuIcon from '@material-ui/icons/Menu';
import { signoutRedirect } from '../auth/userService';
import { getAccessToken, getIdToken } from '../auth/authHeaders';
import { useHistory } from 'react-router';
import { CatalogRoutes, CommonRoutes } from '../constants/routeConstants';

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
    })
);

const NavBar = () => {
    const classes = useStyles();
    const isUserLoggedIn = getAccessToken();
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const history = useHistory();

    const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const handleMenuItemClick = (route: string) => {
        history.push(route);
        handleClose();
    };

    return (
        <div className={classes.root}>
            <AppBar position="static" style={{ marginBottom: '20px' }}>
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
                                <MenuIcon />
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
                                <MenuItem onClick={() => handleMenuItemClick(CatalogRoutes.catalogPageRoute)}>
                                    Catalog
                                </MenuItem>
                                <MenuItem onClick={() => handleMenuItemClick(CommonRoutes.welcomePageRoute)}>
                                    Welcome Page
                                </MenuItem>
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
