import { Typography } from "@mui/material";
import { isAdmin, isLoggedIn } from "../components/Api";
import PostsList from "../components/PostsList";
import Login from "./Login"

const Home = () => {
    return (
        <>
            {
                isLoggedIn()
                ? 
                <>
                    <Typography component="h1" variant="h3">Welcome, {sessionStorage.getItem("username")}!</Typography>
                    {isAdmin() && <PostsList showOnlyPending={true} />}
                </>
                : <Login />
            }
        </>
    )
}

export default Home;