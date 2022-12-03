import { Alert, Avatar, Box, Button, Paper, TextField, Typography } from "@mui/material";
import { useEffect, useState } from "react";
import axios from "axios";
import { Api, authConfig } from "../components/Api";
import Spinner from "../components/Spinner";
import { Link, useParams } from "react-router-dom";
import { Add, Done, Edit } from "@mui/icons-material";

const EditTopic = () => {
    const [errorMessage, setErrorMessage] = useState("");
    const [success, setSuccess] = useState(false);
    const { topicId } = useParams();
    const [topic, setTopic] = useState(null);
    const [topicName, setTopicName] = useState(null);
    const [isLoading, setIsLoading] = useState(null);

    useEffect(() => {
        const getTopic = async () => {
            setIsLoading(true);
            const response = await Api.get(`/topics/${topicId}`, authConfig);
            setTopic(response.data);
            setTopicName(response.data.name)
            setIsLoading(false);
        }

        getTopic();
    }, []);

    const handleOnSubmit = async (e) => {
        setSuccess(false);
        e.preventDefault();
        setIsLoading(true);
        setErrorMessage("");
        const formData = new FormData(e.currentTarget);
        const requestData = {
            name: formData.get("name"),
        };

        try {
            debugger;
            await Api.put(`/topics/${topic.id}`, requestData, authConfig);
            setSuccess(true)
        } catch (error) {
            if (axios.isAxiosError(error)) {
                setErrorMessage("Unable to edit topic.")
            }
        }

        setIsLoading(false);
    };

    return (
        <Paper
            sx={{ m: 5, p: 5, backgroundColor: "#ddd", textAlign: "center" }}
        >
            <Spinner isOpen={isLoading} />
            <Avatar sx={{ m: "5px auto" }}>
                <Edit />
            </Avatar>
            <Typography component="h1" variant="h5">
                Edit topic
            </Typography>
            {topic &&
                <Box
                    component="form"
                    onSubmit={handleOnSubmit}
                    sx={{ mt: 1 }}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="name"
                        label="name"
                        name="name"
                        autoFocus
                        value={topicName}
                        onChange={e => setTopicName(e.target.value)}
                    />

                    {errorMessage != ""
                        ? <Alert severity="error">{errorMessage}</Alert>
                        : <></>
                    }
                    {success && <Alert severity="success">Topic edited successfully</Alert>}

                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Edit
                    </Button>
                </Box>
            }
            <Link to="/topics" >
                <Button
                    sx={{ mt: 3, mb: 2 }}
                    fullWidth
                >
                    Back
                </Button>
            </Link>
        </Paper>
    )
};

export default EditTopic;