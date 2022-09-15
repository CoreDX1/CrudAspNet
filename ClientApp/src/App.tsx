import "./App.css";
import { useEffect, useState } from "react";

interface IData {
  content: string;
  postId: number;
  title: string;
}

interface AppState {
  list: Array<IData>;
}

function App() {
  const [post, setPost] = useState<AppState["list"]>();

  const getPost = async () => {
    const data = await fetch("https://localhost:7228/get-all-post");
    const user = await data.json();
    setPost(user);
  };

  useEffect(() => {
    getPost();
  }, []);

  function renderPostTable() {
    return (
      <div className="table-responsive mt-5">
        <table className="table table-bordered border-dark">
          <thead>
            <tr>
              <th scope="col">PostId</th>
              <th scope="col">Title</th>
              <th scope="col">Content</th>
              <th scope="col">Crud Operations</th>
            </tr>
          </thead>
          <tbody>
            {post?.map((post) => (
              <tr>
                <th scope="col">{post.postId}</th>
                <td>{post.title}</td>
                <td>{post.content}</td>
                <td>
                  <button className="btn btn-dark btn-lg mx-3 my-3">
                    Update
                  </button>
                  <button className="btn btn-secondary btn-lg">Dalete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }
  return (
    <>
      <div className="container">
        <div className="row min-vh-100">
          <div className="col d-flex flex-column justify-content-center align-items-center">
            <div>
              <h1>Asp.Net Core React</h1>
              <div className="mt-5">
                <button onClick={getPost} className="btn btn-dark btn-lg w-100">
                  Get Post from server
                </button>
                <button
                  onClick={() => {}}
                  className="btn btn-secondary btn-lg w-100 mt-4"
                >
                  Create New Post
                </button>
              </div>
            </div>
            {renderPostTable()}
          </div>
        </div>
      </div>
    </>
  );
}

export default App;
