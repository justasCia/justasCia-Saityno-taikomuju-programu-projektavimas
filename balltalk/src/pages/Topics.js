import { Link, Route, Switch, useRouteMatch } from "react-router-dom"
import Post from "./Post";
import Topic from "./Topic";

const Topics = () => {
    const match = useRouteMatch();
    return (
        <div>
            <Switch>
                <Route exact path={`${match.path}/:topicId`}>
                    <Topic />
                </Route>
                <Route exact path={match.path}>
                    <h2>Topics</h2>
                    <ul>
                        <li>
                            <Link to="/topics/1">Topic 1</Link>
                        </li>
                        <li>
                            <Link to="/topics/2">Topic 2</Link>
                        </li>
                    </ul>
                </Route>
                <Route exact path={`${match.path}/:topicId/posts/:postId`}>
                    <Post />
                </Route>
            </Switch>
        </div>
    );
}

export default Topics;