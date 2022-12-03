import { Grid } from "@mui/material";
import { useEffect, useState } from "react";
import { Link, Route, Switch, useRouteMatch } from "react-router-dom"
import { Api, authConfig } from "../components/Api";
import TopicsList from "../components/TopicsList";
import Post from "./Post";
import Topic from "./Topic";

const Topics = () => {
    const match = useRouteMatch();

    return (
        <>
            <Switch>
                <Route exact path={`${match.path}/:topicId`}>
                    <Topic />
                </Route>
                <Route exact path={match.path}>
                    <TopicsList />
                </Route>
                <Route exact path={`${match.path}/:topicId/posts/:postId`}>
                    <Post />
                </Route>
            </Switch>
        </>
    );
}

export default Topics;