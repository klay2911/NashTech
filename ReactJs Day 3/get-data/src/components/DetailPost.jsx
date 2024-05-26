const DetailPost = ( props ) => {
    return (
    <div>
        <div>id: {props?.data?.id}</div>
        <div>Title: {props?.data?.title}</div>
        <div>Body: {props?.data?.body}</div>    
    </div>
    );
}
export default DetailPost;