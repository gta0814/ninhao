import React, { Component } from "react";
import Cookies from "universal-cookie";
import { Redirect } from "react-router";
import { Link } from "react-router-dom";
import MessageDialog from "./messageDialog";
import Loading from "./loading";
import $ from "jquery";
import url from "../config";
class Login extends Component {
  componentDidMount() {
    const cookies = new Cookies();
    var token = cookies.get("token");
    if (token) {
      this.setState({ isLoggedIn: true });
    }
  }
  state = { email: "", password: "", isLoading: false, isLoggedIn: false };

  handleSubmit = (e) => {
    e.preventDefault();

    this.setState({ isLoading: true });
    const dataToSend = JSON.stringify({
      email: this.state.email,
      password: this.state.password,
    });
    fetch(url+"/api/Authenticate/SignIn", {
      method: "post",
      headers: { "Content-Type": "application/json" },
      body: dataToSend,
    })
      .then((response) => response.json())
      .then((responseJson) => {
        this.setState({ isLoading: false });
        if (responseJson.success) {
          const cookies = new Cookies();
          cookies.set("token", responseJson.data.token, { path: "/" });
          cookies.set("email", responseJson.data.email, { path: "/" });
          cookies.set("name", responseJson.data.name, { path: "/" });
          cookies.set("profilePicture", responseJson.data.imageURL, {
            path: "/",
          });
          cookies.set("unreadReceived", responseJson.data.unreadReceived, {
            path: "/",
          });
          cookies.set("unreadSent", responseJson.data.unreadSent, {
            path: "/",
          });
          window.location.reload();
        } else {
          this.setState({ message: responseJson.message });
          $("#messageDialog").modal("show");
        }
      })
      .catch((error) => {
        this.setState({ message: "An Error Occure!", isLoading: false });
        console.error(error);
        $("#messageDialog").modal("show");
      });
  };

  onChange(e) {
    this.setState({
      [e.target.name]: e.target.value,
    });
  }
  render() {
    const isLoading = this.state.isLoading;
    return this.state.isLoggedIn ? (
      <Redirect to="/" />
    ) : (
      <React.Fragment>
        <br></br>
        <br></br>
        <div>
          <div className="col-md-4 mx-auto">
            <form onSubmit={this.handleSubmit}>
              <h3 className="text-center">Login</h3>
              <br></br>
              <div className="form-group">
                <input
                  type="email"
                  name="email"
                  className="form-control"
                  placeholder="Email"
                  value={this.state.email}
                  onChange={this.onChange.bind(this)}
                  required
                />
              </div>
              <div className="form-group">
                <input
                  type="password"
                  name="password"
                  value={this.state.password}
                  onChange={this.onChange.bind(this)}
                  className="form-control"
                  placeholder="Password"
                  required
                />
              </div>
              <div className="text-center">
                <button
                  type="submit"
                  className=" btn btn-block btn-primary tx-tfm"
                >
                  Login
                </button>
              </div>
              <Link className="" to={"/forgotPassword"}>
               Forgot Password?
                </Link>
              <div className="col-md-12 ">
                <hr></hr>
                <br></br>
              </div>
              <p className="text-center">Or</p>
              <div className="form-group">
                <a className="btn btn-block btn-secondary" href="/GoogleLogin">
                  <i className="fa fa-google"></i> Login with Google
                </a>
              </div>
            </form>
          </div>
        </div>
        {isLoading && <Loading />}
        <MessageDialog message={this.state.message} />
      </React.Fragment>
    );
  }
}
export default Login;
