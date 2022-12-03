import { Add, Delete, Edit } from "@mui/icons-material";
import { Button, ButtonGroup, Grid, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Api, authConfig, isAdmin, userId } from "./Api";
import Spinner from "./Spinner";

const CommentsList = ({ topicId, postId }) => {
    const [comments, setComments] = useState([]);
    const [isLoading, setIsLoading] = useState([]);

    useEffect(() => {
        const getComments = async () => {
            setIsLoading(true);
            const response = await Api.get(`/topics/${topicId}/posts/${postId}/comments`, authConfig);
            setComments(response.data);
            setIsLoading(false);
        }
        getComments();
    }, [])

    return (
        <>
            <Spinner isOpen={isLoading} />
            <Typography component="h1" variant="h5" sx={{ mb: 1 }}>Comments:</Typography>
            <Link
                style={{ textDecoration: 'none' }}
                to="/addPost"
            >
                <Button variant="contained" sx={{ mb: 2 }}>
                    <Add color="#000"></Add>
                    Add
                </Button>
            </Link>
            <Grid
                container
                rowSpacing={{ md: 2, xs: 1 }}
                alignItems="stretch"
            >
                {comments?.map((comment, index) => (
                    <>
                        <Grid item xs={12} md={7} key={index}>
                            <Typography variant="body2">
                                {comment.content}
                            </Typography>
                        </Grid>
                        <Grid item xs={7} md={3} key={index}>
                            <Typography variant="caption" color="#bbb">
                                {new Date(comment.posted).toLocaleString()}
                            </Typography>
                        </Grid>
                        {(isAdmin() || comment.userId == userId()) &&
                            <Grid item xs={5} md={2} key={index}>
                                <ButtonGroup>
                                    {comment.userId == userId() &&
                                        <Button>
                                            <Edit />
                                        </Button>
                                    }
                                    <Button>
                                        <Delete />
                                    </Button>
                                </ButtonGroup>
                            </Grid>
                        }
                    </>
                ))}
            </Grid>
        </>
    )
};

export default CommentsList;