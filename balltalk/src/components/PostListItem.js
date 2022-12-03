import { Approval, Delete, Done, Edit } from "@mui/icons-material";
import { Button, ButtonGroup, Grid, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { isAdmin, userId } from "./Api";

const PostListItem = ({ name, postId, topicId, needsApproval, postOwnerId }) => {
    return (
        <>
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
                                <Button>
                                    <Done />
                                    Approve
                                </Button>
                            }
                            <Button sx={{ color: "#880808" }}>
                                <Delete/>
                                Delete
                            </Button>
                        </ButtonGroup>
                    </Grid>
                }
            </Grid>
        </>
    )
};

export default PostListItem;