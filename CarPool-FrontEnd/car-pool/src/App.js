import React, { Component } from "react";
import { Route, Switch } from "react-router-dom";
import NavBar from "./components/navbar";
import "./App.css";
import Home from "./components/home";
import SignUp from "./components/signUp";
import Profile from "./components/profile";
import Login from "./components/login";
import Logout from "./components/logout";
import ManageVehicle from "./components/manageVehicles";
import Cookies from "universal-cookie";
import ManageTrips from "./components/manageTrips";
import HomeItemDetail from "./components/homeItemDetail";
import RecievedRequest from "./components/recievedRequest";
import SentRequest from "./components/sentRequest";
import ForgotPassword from "./components/forgotPassword";
import url from "./config";
class App extends Component {
  updateLoginInfo = () => {
    const cookies = new Cookies();
    var token = cookies.get("token");
    if (token) {
      fetch(url + "/api/Authenticate/GetUserData", {
        method: "get",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      })
        .then(function (response) {
          if (response.status === 401) {
            Logout();
          }
          return response.json();
        })
        .then((responseJson) => {
          if (responseJson.success) {
            const cookies = new Cookies();
            cookies.set("unreadReceived", responseJson.data.unreadReceived, {
              path: "/",
            });
            cookies.set("unreadSent", responseJson.data.unreadSent, {
              path: "/",
            });
          } else {
            console.error("error:", responseJson);
          }
        })
        .catch((error) => {
          console.error(error);
        });

      this.setState({
        isLoggedIn: true,
        unreadSent: cookies.get("unreadSent"),
        unreadRecieved: cookies.get("unreadReceived"),
        profileImage: cookies.get("profilePicture"),
        email: cookies.get("email"),
      });
    }
  };
  handleUnreadRecieved = () => {
    this.setState({
      unreadRecieved: this.state.unreadRecieved - 1,
    });
  };

  handleLoginInfo = (params) => {
    this.setState({
      isLoggedIn: true,
      unreadRecieved: params.data.unreadReceivedRequests,
      unreadSent: params.data.unreadSentRequests,
      email: params.data.email,
      profileImage: params.data.imageURL,
    });
  };

  componentDidMount() {
    this.updateLoginInfo();
  }
  state = {
    isLoggedIn: false,
    unreadSent: 0,
    unreadRecieved: 0,
    profileImage: "",
    email: "",
  };
  render() {
    return (
      <React.Fragment>
        <NavBar
          isLoggedIn={this.state.isLoggedIn}
          unreadSent={this.state.unreadSent}
          unreadRecieved={this.state.unreadRecieved}
          email={this.state.email}
          profileImage={this.state.profileImage}
        />
        <main>
          <Switch>
            <Route path="/" component={Home} exact />
            <Route path="/register" component={SignUp} />
            <Route path="/login" component={Login} />
            <Route path="/logout" component={Logout} />
            <Route path="/manageVehicle" component={ManageVehicle} />
            <Route path="/manageTrips" component={ManageTrips} />
            <Route path="/sentRequests" component={SentRequest} />
            <Route path="/forgotPassword" component={ForgotPassword} />

            <Route
              path="/recievedRequests"
              render={(props) => (
                <RecievedRequest setRecieved={this.handleUnreadRecieved}
                />
              )}
            />
            <Route
              path="/profile"
              render={(props) => (
                <Profile updateLoginInfo={this.updateLoginInfo} />
              )}
            />
            <Route name="/detail/:id" component={HomeItemDetail} />
          </Switch>
        </main>
      </React.Fragment>
    );
  }
}

export default App;
