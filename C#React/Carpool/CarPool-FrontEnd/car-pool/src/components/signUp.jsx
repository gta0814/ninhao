import React, { Component } from "react";
import Cookies from "universal-cookie";
import { Redirect } from "react-router";
import MessageDialog from "./messageDialog";
import Loading from "./loading";
import $ from "jquery";
import url from "../config";
class SignUp extends Component {
  state = {
    isLoading: false,
    name: "",
    email: "",
    password: "",
    confirm_password: "",
    message: "",
    isLoggedIn: false,
  };
  componentDidMount() {
    const cookies = new Cookies();
    var token = cookies.get("token");

    if (token) {
      this.setState({ isLoggedIn: true });
    }
  }
  handleSubmit = (e) => {
    e.preventDefault();
    if (this.state.password !== this.state.confirm_password) {
      this.setState({
        message: "Password does not match",
        password: "",
        confirm_password: "",
      });
      $("#messageDialog").modal("show");
    } else {
      this.setState({ isLoading: true });
      const dataToSend = JSON.stringify({
        email: this.state.email,
        password: this.state.password,
        fullName: this.state.name,
      });
      fetch(url+"/api/Authenticate/SignUp", {
        method: "post",
        headers: { "Content-Type": "application/json" },
        body: dataToSend,
      })
        .then((response) => response.json())
        .then((responseJson) => {
          this.setState({ isLoading: false });
          if (responseJson.success) {
            this.setState({ message: "Account created kindly login now!" });
            $("#messageDialog").modal("show");
            this.props.history.push('/login');
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
    }
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
              <h3 className="text-center">Registration</h3>
              <br></br>
              <div className="form-group">
                <input
                  type="text"
                  className="form-control my-input"
                  placeholder="Name"
                  name="name"
                  value={this.state.name}
                  onChange={this.onChange.bind(this)}
                  required
                />
              </div>
              <div className="form-group">
                <input
                  type="email"
                  name="email"
                  className="form-control my-input"
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
                  className="form-control my-input"
                  placeholder="Password"
                  required
                />
              </div>
              <div className="form-group">
                <input
                  value={this.state.confirm_password}
                  onChange={this.onChange.bind(this)}
                  type="password"
                  name="confirm_password"
                  className="form-control my-input"
                  placeholder="Confirm Password"
                />
              </div>
              <div className="text-center ">
                <button
                  type="submit"
                  className=" btn btn-block btn-primary tx-tfm"
                >
                  Create Your Free Account
                </button>
              </div>
              <div className="col-md-12 ">
                <hr></hr>
                <br></br>
              </div>
              <p className="text-center">Or</p>
              <div className="form-group">
                <a className="btn btn-block btn-secondary" href="/GoogleLogin">
                  <i className="fa fa-google"></i> Sign up with Google
                </a>
              </div>
              <p className="small mt-3">
                By signing up, you are indicating that you have read and agree
                to the Terms of Use and Privacy Policy.
              </p>
            </form>
          </div>
        </div>
        {isLoading && <Loading />}
        <MessageDialog message={this.state.message} />
      </React.Fragment>
    );
  }
}
export default SignUp;
