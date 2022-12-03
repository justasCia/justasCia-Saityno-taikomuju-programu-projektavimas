import { Grid } from "@mui/material";
import { useEffect, useState } from "react";
import { Link, useParams, useRouteMatch } from "react-router-dom"
import { Api, authConfig } from "../components/Api";
import PostsList from "../components/PostsList";
import Spinner from "../components/Spinner";

const Topic = () => {
    const { topicId } = useParams();
    const [topic, setTopic] = useState(null);
    const [isLoading, setIsLoading] = useState(null);

    useEffect(() => {
        const getTopic = async () => {
            setIsLoading(true);
            const response = await Api.get(`/topics/${topicId}`, authConfig);
            setTopic(response.data);
            setIsLoading(false);
        }

        getTopic();
    }, []);

    if (topic) {
        return (
            <>
                <Spinner isOpen={isLoading} />
                <PostsList topicName={topic.name} topicId={topicId} />
            </>
        );
    }

}

export default Topic;