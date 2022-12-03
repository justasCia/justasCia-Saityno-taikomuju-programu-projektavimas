import { Delete, Edit } from "@mui/icons-material";
import { Button, ButtonGroup, Grid, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { isAdmin } from "./Api";

const TopicListItem = ({ name, id }) => {
    return (
        <>
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
                                onClick={() => window.location.href=`/topics/${id}/edit`}
                            >
                                <Edit />
                                Edit
                            </Button>
                            <Button sx={{ color: "#880808" }}>
                                <Delete />
                                Delete
                            </Button>
                        </ButtonGroup>
                    </Grid>
                }
            </Grid>
        </>
    )
};

export default TopicListItem;