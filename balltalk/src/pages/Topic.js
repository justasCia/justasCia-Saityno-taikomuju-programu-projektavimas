import { Link, useParams, useRouteMatch } from "react-router-dom"

const Topic = () => {
    const match = useRouteMatch();
    const { topicId } = useParams();
    return (
        <div>
            <h2>Topic {topicId}</h2>
            <ul>
                <li>
                    <Link to={`${match.url}/posts/1`}>Post 1</Link>
                </li>
                <li>
                    <Link to={`${match.url}/posts/2`}>Post 2</Link>
                </li>
            </ul>
        </div>
    );
}

export default Topic;