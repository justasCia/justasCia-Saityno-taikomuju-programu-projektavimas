import { Delete, Edit } from "@mui/icons-material";
import { Button, ButtonGroup, Grid, Typography } from "@mui/material";
import { useState } from "react";
import { Link } from "react-router-dom";
import { Api, authConfig, isAdmin } from "./Api";
import ComfirmationModal from "./ComfirmationModal";
import Spinner from "./Spinner";

const TopicListItem = ({ name, id }) => {
    const [deleteModalOpen, setDeleteModalOpen] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    const handleOpenDeleteModal = () => {
        setDeleteModalOpen(true);
    };
    const handleCloseDeleteModal = () => {
        setDeleteModalOpen(false);
    };

    const deleteTopic = async () => {
        setIsLoading(true);
        await Api.delete(`/topics/${id}`, authConfig);
        setIsLoading(false);
        window.location.href = `/topics`;
    }

    return (
        <>
            <Spinner isOpen={isLoading} />
            <Grid container>
                <Grid xs={12} md={8} item>
                    <Link to={`/topics/${id}`} style={{ textDecoration: "none" }}>
                        <Button>
                            <Typography component="h1" variant="h6">{name}</Typography>
                        </Button>
                    </Link>

                </Grid>
                {isAdmin() &&
                    <Grid xs={12} md={4} item>
                        <ButtonGroup>
                            <Button
                                onClick={() => window.location.href = `/topics/${id}/edit`}
                            >
                                <Edit />
                                Edit
                            </Button>
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
                message="Are you sure you want to delete this topic?"
                handleClose={handleCloseDeleteModal}
                handleComfirm={deleteTopic}
            />
        </>
    )
};

export default TopicListItem;