import { useParams } from "react-router-dom";

const Post = () => {
    const { topicId, postId } = useParams();
    return <h2>Topic {topicId} Post {postId}</h2>
}

export default Post;