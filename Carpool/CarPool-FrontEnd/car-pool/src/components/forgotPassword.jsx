import React, { Component } from "react";
import Cookies from "universal-cookie";
import { Redirect } from "react-router";
import MessageDialog from "./messageDialog";
import Loading from "./loading";
import $ from "jquery";
import url from "../config";
class ForgotPassword extends Component {
  componentDidMount() {
    const cookies = new Cookies();
    var token = cookies.get("token");
    if (token) {
      this.setState({ isLoggedIn: true });
    }
  }
  state = { email: "", isLoading: false, isLoggedIn: false };

  handleSubmit = (e) => {
    e.preventDefault();

    this.setState({ isLoading: true });
 
    fetch(url+"/api/Authenticate/PasswordForget?email="+this.state.email, {
      method: "post",
      headers: { "Content-Type": "application/json" },
     
    })
      .then((response) => response.json())
      .then((responseJson) => {
        this.setState({ isLoading: false });
        if (responseJson.success) {
            this.setState({ message: "Check Your Email" });
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
              <h3 className="text-center">Forgot Password</h3>
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
              <div className="text-center">
                <button
                  type="submit"
                  className=" btn btn-block btn-primary tx-tfm"
                >
                  Get Email
                </button>
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
export default ForgotPassword;
