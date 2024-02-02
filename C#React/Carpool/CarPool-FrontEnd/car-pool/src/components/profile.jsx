import React, { Component } from "react";
import Cookies from "universal-cookie";
import Logout from "./logout";
import url from "../config";
import MessageDialog from "./messageDialog";
import $ from "jquery";
import Loading from "./loading";

class Profile extends Component {
  state = {
    isLoading: false,
    fullName: "",
    email: "",
    phoneNumber: "",
    dateOfBirth: "",
    address: "",
    gender: "",
    imageUrl: "",
    message: "",
   
  };

  onChangeButtonClick = (e) => {
    this.setState({ isLoading: true });
    const cookies = new Cookies();
    var token = cookies.get("token");

    let file = new FormData();
    file.append("file", e.target.files[0]);

    fetch(url + "/api/Users/UploadImage", {
      method: "post",
      headers: { Authorization: "Bearer " + token },
      body: file,
    })
      .then(function (response) {
        if (response.status === 401) {
          Logout();
        }
        return response.json();
      })
      .then((responseJson) => {
     
        if (responseJson.success) {
          this.setState({
            message: "Profile picture updated",
            imageUrl: url + responseJson.data,
            isLoading: false
          });
          cookies.set("profilePicture", responseJson.data, { path: "/" });
          this.props.updateLoginInfo();
    
        } else {
          this.setState({ message: responseJson.Message,
          isLoading: false });
          
        }
        $("#messageDialog").modal("show");
      })
      .catch((error) => {
        this.setState({ message: "An Error Occure!", isLoading: false });
        console.error(error);
        $("#messageDialog").modal("show");
      });
    document.getElementById("file").value = "";
  };

  componentDidMount() {
   
    this.setState({ isLoading: true });
    const cookies = new Cookies();
    var token = cookies.get("token");

    if (token) {
      fetch(url + "/api/Users/GetUser", {
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
            this.setState({
              email: responseJson.data.email,
              fullName: responseJson.data.fullName,
              phoneNumber: responseJson.data.phoneNumber,
              dateOfBirth: responseJson.data.dateOfBirth,
              gender: responseJson.data.gender === null ? null : responseJson.data.gender? "male": "female",
              imageUrl: url + responseJson.data.imageURL,
              address: responseJson.data.address,
              isLoading: false
            });
          } else {
            this.setState({ message: responseJson.message, isLoading: false });
            $("#messageDialog").modal("show");
          }
        })
        .catch((error) => {
          this.setState({ message: "An Error Occure!", isLoading: false });
          console.error(error);
          $("#messageDialog").modal("show");
        });
    } else {
      window.location.href = "/";
    }
  }

  
  handleSubmit = (e) => {
    e.preventDefault();
    this.setState({ isLoading: true });
    const cookies = new Cookies();
    var token = cookies.get("token");
    const dataToSend = JSON.stringify({
      email: this.state.email,
      fullName: this.state.fullName,
      phoneNumber: this.state.phoneNumber,
      dateOfBirth: this.state.dateOfBirth,
      gender: this.state.gender === "male"? true: false,
      address: this.state.address,
    });
    fetch(url + "/api/Users/Edit", {
      method: "put",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
      body: dataToSend,
    })
      .then(function (response) {
        if (response.status === 401) {
          Logout();
        }
        return response.json();
      })
      .then((responseJson) => {
      
     
        if (responseJson.success) {
          this.setState({ message: "Profile saved!", isLoading: false });
        } else {
          this.setState({ message: responseJson.message, isLoading: false });
        }
        $("#messageDialog").modal("show");
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
    return (
      <React.Fragment>
        <br></br>
        <br></br>

        <div>
          <div className="col-md-4 mx-auto">
            <h3 className="text-center">Profile</h3>

            <form onSubmit={this.handleSubmit}>
              <div>
                <div className="text-center">
                  <img
                    src={this.state.imageUrl}
                    className="rounded-circle"
                    height="150px"
                    width="150px"
                    alt="..."
                  />

                  <div className="custom-file">
                    <input
                      type="file"
                      accept="image/x-png,image/jpg,image/jpeg"
                      className="custom-file-input"
                      id="file"
                      onChange={this.onChangeButtonClick.bind(this)}
                    />
                    <label className="custom-file-label" htmlFor="file">
                      Choose file
                    </label>
                  </div>
                </div>
              </div>
              <br></br>
              <div className="form-group">
                <label htmlFor="name">Full Name</label>
                <input
                  type="text"
                  className="form-control my-input"
                  placeholder="Name"
                  id="fullName"
                  name="fullName"
                  value={this.state.fullName}
                  onChange={this.onChange.bind(this)}
                  required
                />
              </div>
              <div className="form-group">
                <label htmlFor="email">Email address</label>
                <input
                  type="email"
                  id="email"
                  name="email"
                  className="form-control my-input"
                  placeholder="Email"
                  value={this.state.email}
                  readOnly
                />
              </div>
              <div className="form-group">
                <label htmlFor="phoneNumber">Phone Number</label>
                <input
                  type="tel"
                  name="phoneNumber"
                  id="phoneNumber"
                  value={this.state.phoneNumber}
                  onChange={this.onChange.bind(this)}
                  className="form-control"
                  placeholder="Phone Number"
                  required
                />
              </div>

              <div className="form-group">
                <label htmlFor="dateOfBirth">Date of Birth</label>
                <input
                  value={this.state.dateOfBirth}
                  onChange={this.onChange.bind(this)}
                  type="date"
                  id="dateOfBirth"
                  name="dateOfBirth"
                  className="form-control"
                  placeholder="Date of Birth"
                  required
                />
              </div>
              <div className="form-group">
                <label htmlFor="address">Address</label>
                <textarea
                  value={this.state.address || ""}
                  onChange={this.onChange.bind(this)}
                  id="address"
                  name="address"
                  className="form-control"
                  placeholder="address"
                  required
                />
              </div>
              <div className="form-check form-check-inline">
                <input
                  onChange={this.onChange.bind(this)}
                  className="form-check-input"
                  type="radio"
                  value="male"
                  name="gender"
                  id="inlineRadio1"
                  checked={this.state.gender === "male"}
                  required
                />
                <label className="form-check-label" htmlFor="inlineRadio1">
                  Male
                </label>
              </div>
              <div className="form-check form-check-inline">
                <input
                  onChange={this.onChange.bind(this)}
                  className="form-check-input"
                  type="radio"
                  value="female"
                  name="gender"
                  id="inlineRadio2"
                
                  checked={this.state.gender === "female"}
                  required
                />
                <label className="form-check-label" htmlFor="inlineRadio2">
                  Female
                </label>
              </div>

              <br></br>
              <p className="text-center text-danger">{this.state.message}</p>
              <div className="text-center ">
                <button
                  type="submit"
                  className=" btn btn-block btn-primary tx-tfm"
                >
                  Save
                </button>
              </div>
              <br></br>
            </form>
          </div>
        </div>
        {isLoading && <Loading />}
        <MessageDialog message={this.state.message} />
      </React.Fragment>
    );
  }
}
export default Profile;
