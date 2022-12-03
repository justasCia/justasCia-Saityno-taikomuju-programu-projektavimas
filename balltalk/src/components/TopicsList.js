import { Add } from "@mui/icons-material";
import { Button, Grid, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Api, authConfig, isAdmin } from "./Api";
import Spinner from "./Spinner";
import TopicListItem from "./TopicListItem";

const TopicsList = () => {
    const [topics, setTopics] = useState(null);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const getTopics = async () => {
            setIsLoading(true);
            const response = await Api.get("/topics", authConfig);
            setTopics(response.data);
            setIsLoading(false);
        }

        getTopics();
    }, []);
    return (
        <>
            <Spinner isOpen={isLoading} />
            <Typography component="h1" variant="h4" sx={{ mb: 1 }}>Topics</Typography>
            {isAdmin() &&
                <Link
                    style={{ textDecoration: 'none' }}
                    to="/bybys"
                >
                    <Button variant="contained" sx={{ mb: 2 }}>
                        <Add color="#000"></Add>
                        Add
                    </Button>
                </Link>
            }
            <Grid
                container
                spacing={{ xs: 2, md: 3 }}
                columns={1}
            >
                {topics?.map((topic, index) => (
                    <Grid item xs={2} sm={4} md={4} key={index}>
                        <TopicListItem name={topic.name} id={topic.id} />
                    </Grid>
                ))}
            </Grid>
        </>
    )
}

export default TopicsList;