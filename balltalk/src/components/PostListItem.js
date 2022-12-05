import { Approval, Delete, Done, Edit } from "@mui/icons-material";
import { Button, ButtonGroup, Grid, Typography } from "@mui/material";
import { useState } from "react";
import { Link } from "react-router-dom";
import { Api, authConfig, isAdmin, userId } from "./Api";
import ComfirmationModal from "./ComfirmationModal";
import Spinner from "./Spinner";

const PostListItem = ({ name, postId, topicId, needsApproval, postOwnerId }) => {
    const [deleteModalOpen, setDeleteModalOpen] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    const handleOpenDeleteModal = () => {
        setDeleteModalOpen(true);
    };
    const handleCloseDeleteModal = () => {
        setDeleteModalOpen(false);
    };

    const deletePost = async () => {
        setIsLoading(true);
        await Api.delete(`/topics/${topicId}/posts/${postId}`, authConfig);
        handleCloseDeleteModal();
        setIsLoading(false);
        window.location.href = `/topics/${topicId}`;
    }
    const approvePost = async () => {
        setIsLoading(true);
        await Api.post(`/topics/${topicId}/posts/${postId}/approve`, {}, authConfig);
        setIsLoading(false);
        window.location.href = `/`;
    }
    return (
        <>
            <Spinner open={isLoading} />
            <Grid container>
                <Grid item xs={12} md={8}>
                    <Link to={`/topics/${topicId}/posts/${postId}`} style={{ textDecoration: "none" }}>
                        <Button>
                            <Typography component="h1" variant="h6">{name}</Typography>
                        </Button>
                    </Link>
                </Grid>
                {(isAdmin() || postOwnerId == userId()) &&
                    <Grid item xs={12} md={4}>
                        <ButtonGroup>
                            {postOwnerId == userId() &&
                                <Button>
                                    <Edit />
                                    Edit
                                </Button>
                            }
                            {(needsApproval && isAdmin()) &&
                                <Button onClick={approvePost}>
                                    <Done />
                                    Approve
                                </Button>
                            }
                            <Button
                                sx={{ color: "#880808" }}
                                onClick={handleOpenDeleteModal}
                            >
                                <Delete />
                                Delete
                            </Button>
                        </ButtonGroup>
                    </Grid>
                }
            </Grid>
            <ComfirmationModal
                open={deleteModalOpen}
                message="Are you sure you want to delete this post?"
                handleClose={handleCloseDeleteModal}
                handleComfirm={deletePost}
            />
        </>
    )
};

export default PostListItem;