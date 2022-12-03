import { Grid } from "@mui/material";
import { useEffect, useState } from "react";
import { Link, Route, Switch, useRouteMatch } from "react-router-dom"
import { Api, authConfig } from "../components/Api";
import TopicsList from "../components/TopicsList";
import AddPost from "./AddPost";
import AddTopic from "./AddTopic";
import EditPost from "./EditPost";
import EditTopic from "./EditTopic";
import Post from "./Post";
import Topic from "./Topic";

const Topics = () => {
    const match = useRouteMatch();

    return (
        <>
            <Switch>
                <Route exact path={`${match.path}/add`}>
                    <AddTopic />
                </Route>
                <Route exact path={`${match.path}/:topicId`}>
                    <Topic />
                </Route>
                <Route exact path={`${match.path}/:topicId/edit`}>
                    <EditTopic />
                </Route>
                <Route exact path={match.path}>
                    <TopicsList />
                </Route>
                <Route exact path={`${match.path}/:topicId/posts/add`}>
                    <AddPost />
                </Route>
                <Route exact path={`${match.path}/:topicId/posts/:postId`}>
                    <Post />
                </Route>
                <Route exact path={`${match.path}/:topicId/posts/:postId/edit`}>
                    <EditPost />
                </Route>
            </Switch>
        </>
    );
}

export default Topics;