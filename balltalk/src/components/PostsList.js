import { Add } from "@mui/icons-material";
import { Button, Grid, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Api, authConfig, isAdmin } from "./Api";
import PostListItem from "./PostListItem";
import Spinner from "./Spinner";
import TopicListItem from "./TopicListItem";

const PostsList = ({ topicName, topicId, showOnlyPending }) => {
    const [posts, setPosts] = useState(null);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const getPosts = async () => {
            setIsLoading(true);
            const url = showOnlyPending ? `/posts/pending` : `/topics/${topicId}/posts`;
            const response = await Api.get(url, authConfig);
            setPosts(response.data);
            setIsLoading(false);
        }

        getPosts();
    }, []);
    return (
        <>
            <Spinner isOpen={isLoading} />
            <Typography component="h1" variant="h4" sx={{ mb: 1 }}>{topicName}</Typography>
            {!showOnlyPending ?
                <Link
                    style={{ textDecoration: 'none' }}
                    to="/addPost"
                >
                    <Button variant="contained" sx={{ mb: 2 }}>
                        <Add color="#000"></Add>
                        Add
                    </Button>
                </Link>
                :
                <Typography component="h1" variant="h6">Posts waiting for approval:</Typography>
            }
            <Grid
                container
                spacing={{ xs: 2, md: 3 }}
                columns={1}
            >
                {posts?.map((post, index) => (
                    <Grid item xs={2} sm={4} md={4} key={index}>
                        {showOnlyPending &&
                            <PostListItem name={post.title} topicId={post.topicId} postId={post.id} needsApproval={showOnlyPending} postOwnerId={post.userId} />
                        }
                        {(post.approved && !showOnlyPending) &&
                            <PostListItem name={post.title} topicId={topicId} postId={post.id} postOwnerId={post.userId} />
                        }
                    </Grid>
                ))}
            </Grid>
        </>
    )
}

export default PostsList;