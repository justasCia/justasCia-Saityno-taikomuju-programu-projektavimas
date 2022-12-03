import { AppBar, Button, Toolbar, Typography } from "@mui/material";
import React from "react";
import { Link } from "react-router-dom";

const NavBar = () => {
    return (
        <AppBar
            position="static"
            color="default"
            elevation={0}
        >
            <Toolbar sx={{ flexWrap: 'wrap' }}>
                <Typography variant="h5" color="inherit" noWrap sx={{ flexGrow: 1 }}>
                    BallTalk
                </Typography>
                <nav>
                    <Link to="/" style={{ textDecoration: 'none' }}>
                        <NavBarButton name="Home" />
                    </Link>
                    <Link to="/topics" style={{ textDecoration: 'none' }}>
                        <NavBarButton name="Topics" />
                    </Link>
                    <Link to="/login" style={{ textDecoration: 'none' }}>
                        <NavBarButtonOutlined name="Login" />
                    </Link>
                    <Link to="/register" style={{ textDecoration: 'none' }}>
                        <NavBarButtonOutlined name="Register" />
                    </Link>
                </nav>
            </Toolbar>
        </AppBar>);
}

const NavBarButton = ({ name }) => {
    return (
        <Button
        >
            {name}
        </Button>
    )
}

const NavBarButtonOutlined = ({ name }) => {
    return (
        <Button
            variant="outlined"
            sx={{ mx: 1 }}
        >
            {name}
        </Button>
    )
}

export default NavBar;