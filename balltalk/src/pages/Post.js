import { Delete, Done, Edit } from "@mui/icons-material";
import { Button, ButtonGroup, Paper, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Api, authConfig, isAdmin, userId } from "../components/Api";
import CommentsList from "../components/CommentsList";
import Spinner from "../components/Spinner";

const Post = () => {
    const { topicId, postId } = useParams();
    const [post, setPost] = useState(null);
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        const getPost = async () => {
            setIsLoading(true);
            const response = await Api.get(`/topics/${topicId}/posts/${postId}`, authConfig);
            setPost(response.data);
            setIsLoading(false);
        };

        getPost();
    }, []);

    return (
        <>
            <Spinner isOpen={isLoading} />
            {post &&
                <>
                    <Paper
                        sx={{ p: 2 }}
                    >
                        <div>
                            <Typography componet="h1" variant="h3">{post.title}</Typography>
                            <Typography variant="caption">{new Date(post.posted).toLocaleString()}</Typography>
                        </div>
                        {(isAdmin() || post.userId == userId()) &&
                            <ButtonGroup>
                                {post.userId == userId() &&
                                    <Button>
                                        <Edit />
                                        Edit
                                    </Button>
                                }
                                {(isAdmin() && !post.approved) &&
                                    <Button>
                                        <Done />
                                        Approve
                                    </Button>
                                }
                                <Button sx={{ color: "#880808" }}>
                                    <Delete />
                                    Delete
                                </Button>
                            </ButtonGroup>
                        }
                        <Box
                            sx={{ mt: 1, mb: 3 }}
                        >
                            <Typography variant="body1">
                                {post.content}
                            </Typography>
                        </Box>
                        <CommentsList topicId={topicId} postId={postId} />
                    </Paper>

                </>}
        </>
    )
}

export default Post;