import { Alert, Avatar, Box, Button, Paper, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import axios from "axios";
import { Api, authConfig } from "../components/Api";
import Spinner from "../components/Spinner";
import { Link, useParams } from "react-router-dom";
import { Add, Done } from "@mui/icons-material";

const AddPost = () => {
    const { topicId } = useParams();
    const [errorMessage, setErrorMessage] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const [success, setSuccess] = useState(false);

    const handleOnSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true);
        setErrorMessage("");
        const formData = new FormData(e.currentTarget);
        const requestData = {
            title: formData.get("title"),
            content: formData.get("content"),
        };

        try {
            await Api.post(`/topics/${topicId}/posts`, requestData, authConfig);
            setSuccess(true)
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setErrorMessage("Unable to add post.")
            }
        }

        setIsLoading(false);
    };

    return success
        ? (
            <Paper
                sx={{ m: 5, p: 5, backgroundColor: "#ddd", textAlign: "center" }}
            >
                <Avatar sx={{ m: "5px auto" }}>
                    <Done />
                </Avatar>
                <Typography variant="h4">
                    {"Post added, please wait till an admin approves it."}
                    <br/>
                    {"Back to "}
                    <Link style={{ textDecoration: 'none' }} to={`/topics/${topicId}`}>Topic</Link>
                </Typography >
            </Paper>
        ) : (
            <Paper
                sx={{ m: 5, p: 5, backgroundColor: "#ddd", textAlign: "center" }}
            >
                <Spinner isOpen={isLoading} />
                <Avatar sx={{ m: "5px auto" }}>
                    <Add />
                </Avatar>
                <Typography component="h1" variant="h5">
                    Add post
                </Typography>
                <Box
                    component="form"
                    onSubmit={handleOnSubmit}
                    sx={{ mt: 1 }}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="title"
                        label="title"
                        name="title"
                        autoFocus
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="content"
                        label="content"
                        name="content"
                        autofocus
                        multiline
                        rows={4}

                    />
                    {errorMessage != ""
                        ? <Alert severity="error">{errorMessage}</Alert>
                        : <></>
                    }

                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Add
                    </Button>
                    <Link to={`/topics/${topicId}`}>
                        <Button
                            sx={{ mt: 3, mb: 2 }}
                            fullWidth
                        >
                            Back
                        </Button>
                    </Link>
                </Box>
            </Paper>
        )
};

export default AddPost;