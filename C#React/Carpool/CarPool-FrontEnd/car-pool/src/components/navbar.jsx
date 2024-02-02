import React, { Component } from "react";
import { Link } from "react-router-dom";
import url from "../config";
class NavBar extends Component {
  unreadCount = () => {
    var count = 0;
    console.log(this.props.unreadSent)
    if(this.props.unreadSent !== 0){
      count += parseInt(this.props.unreadSent);
    }
    if(this.props.unreadRecieved !== 0){
      count +=  parseInt(this.props.unreadRecieved);
    }
    if (count > 0) {
      return (
        <span className="badge badge-pill badge-primary messageBedge">
          {count}
        </span>
      );
    }
  };
  unreadSent = () => {
    if (this.props.unreadSent > 0) {
      return (
        <span className="badge badge-primary">{this.props.unreadSent}</span>
       
      );
    }
  };
  unreadRecieved = () => {
    if (this.props.unreadRecieved > 0) {
      return (
        <span className="badge badge-primary">{this.props.unreadRecieved}</span>

      );
    }
  };
  User = () => {
    return (
      <React.Fragment>
        <div className="collapse navbar-collapse" id="navbar-list-4">
          <ul className="navbar-nav ml-auto">
            
            
            <li className="nav-item dropdown">
            {this.unreadCount()}
              <a
                className="nav-link dropdown-toggle"
                id="navbarDropdownMenuLink2"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
                href="/"
              >
                Requests
              </a>
              <div
                className="dropdown-menu dropdown-menu-right"
                aria-labelledby="navbarDropdownMenuLink2"
              >
                <Link className="dropdown-item" to={"/sentRequests"}>
                Sent  {this.unreadSent()}
                </Link>
                <Link className="dropdown-item" to={"/recievedRequests"}>
                 Recieved {this.unreadRecieved()}
                </Link>
              </div>
            </li>
            <li className="nav-item dropdown">
           
              <a
                className="nav-link dropdown-toggle"
                id="navbarDropdownMenuLink1"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
                href="/"
              >
                Manage
              </a>
              <div
                className="dropdown-menu dropdown-menu-right"
                aria-labelledby="navbarDropdownMenuLink1"
              >
                <Link className="dropdown-item" to={"/manageVehicle"}>
                  Vehicle
                </Link>
                <Link className="dropdown-item" to={"/manageTrips"}>
                  Trips
                </Link>
              </div>
            </li>
            <li className="nav-item dropdown">
              <a
                className="nav-link dropdown-toggle"
                id="navbarDropdownMenuLink"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
                href="/"
              >
                <img
                  src={url + this.props.profileImage}
                  width="40"
                  height="40"
                  className="rounded-circle"
                  alt="DP"
                />
              </a>
              <div
                className="dropdown-menu dropdown-menu-right"
                aria-labelledby="navbarDropdownMenuLink"
              >
                <p className="dropdown-item">{this.props.email}</p>
                <Link className="dropdown-item" to={"/profile"}>
                  Edit Profile
                </Link>
                <Link className="dropdown-item" to={"/logout"}>
                  Logout
                </Link>
              </div>
            </li>
          </ul>
        </div>
      </React.Fragment>
    );
  };

  Guest = () => {
    return (
      <React.Fragment>
        <div className="collapse navbar-collapse" id="navbar-list-4">
          <ul className="navbar-nav ml-auto">
            <li className="nav-item">
              <Link className="nav-link" to={"/login"}>
                Login
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to={"/register"}>
                Register
              </Link>
            </li>
          </ul>
        </div>
      </React.Fragment>
    );
  };

  IsLoggedIn = () => {
    if (this.props.isLoggedIn) {
      return this.User();
    } else {
      return this.Guest();
    }
  };

  render() {
    return (
      <React.Fragment>
        <nav className="navbar navbar-dark bg-dark navbar-expand-sm">
          <a className="navbar-brand" href="/index.html">
            <img
              src="ninhao-logo.png"
              width="90"
              height="40"
              alt="logo"
            />
            CarPool
          </a>
          <button
            className="navbar-toggler"
            type="button"
            data-toggle="collapse"
            data-target="#navbar-list-4"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          {this.IsLoggedIn()}
        </nav>
      </React.Fragment>
    );
  }
}

export default NavBar;
