import React, { Component } from "react";
import $ from "jquery";
import MessageDialog from "./messageDialog";
import Loading from "./loading";
import url from "../config";
import Moment from "react-moment";
import { Link } from "react-router-dom";
import Cookies from "universal-cookie";
import Logout from "./logout";
class HomeItemDetail extends Component {
  state = {
    message: "",
    isLoading: false,
    isLogin: false,
    isRequested: false,
    isUserTrip: false,
    data: [],
    user: [],
    riders: [],
    vehicle: [],
    seatsInput: "",
  };

  Request = (e) => {
    e.preventDefault();
    this.setState({ isLoading: true });

    const cookies = new Cookies();
    var token = cookies.get("token");
    fetch(
      url +
        "/api/RideRequests/Send?id=" +
        this.state.data.id +
        "&requestedSeats=" +
        this.state.seatsInput,
      {
        method: "get",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + token,
        },
      }
    )
      .then(function (response) {
        if (response.status === 401) {
          Logout();
        }
        return response.json();
      })
      .then((responseJson) => {
        if (responseJson.success) {
          this.setState({
            isRequested: true,
            isLoading: false,
            message: "Request sent.",
          });
          $("#addModal").modal("hide");
          $("#messageDialog").modal("show");
        } else {
          this.setState({
            message: responseJson.message,
            isLoading: false,
          });
          $("#addModal").modal("hide");
          $("#messageDialog").modal("show");
        }
      })
      .catch((error) => {
        this.setState({ message: "An Error Occure!", isLoading: false });
        console.error(error);
        $("#addModal").modal("hide");
        $("#messageDialog").modal("show");
      });
  };

  onChange(e) {
    this.setState({
      [e.target.name]: e.target.value,
    });
  }
  componentDidMount() {
    const id = this.props.location.id;
    if (!id) {
      this.props.history.push("/");
      return;
    }

    this.setState({ isLoading: true });
    const cookies = new Cookies();
    var token = cookies.get("token");
    fetch(url + "/api/Home/GetTripById?id=" + id, {
      method: "get",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + token,
      },
    })
      .then(function (response) {
        return response.json();
      })
      .then((responseJson) => {
        if (responseJson.success) {
          this.setState({
            isLoading: false,
            data: responseJson.data.detail,
            vehicle: responseJson.data.detail.vehicle,
            user: responseJson.data.detail.vehicle.user,
            isLogin: responseJson.data.isLogin,
            isRequested: responseJson.data.isRequested,
            isUserTrip: responseJson.data.isUserTrip,
            riders: responseJson.data.detail.riders["$values"],
          });
        } else {
          this.setState({
            isLoading: false,
            message: "Error: " + responseJson.message,
          });
          $("#messageDialog").modal("show");
        }
      })
      .catch((error) => {
        this.setState({ isLoading: false, message: "An Error Occure!" });
        console.error(error);
        $("#messageDialog").modal("show");
      });
  }

  render() {
    const isLoading = this.state.isLoading;
    const riderCount = this.state.riders.length;
    const isLogin = this.state.isLogin;
    const isRequested = this.state.isRequested;
    const isUserTrip = this.state.isUserTrip;
    const dob = this.state.user.dateOfBirth? true: false;
    return (
      <React.Fragment>
        {isLoading && <Loading />}
        <div className="card mx-4 mt-4">
          <div className="card-body">
            <div className="row">
              <div className="col-sm-2">
                <img
                  src={url + this.state.user.imageURL}
                  width="100"
                  height="100"
                  className="rounded-circle mx-auto d-block"
                  alt="dp"
                />
                <br></br>
                <h4 className="text-center text-primary">
                  {this.state.user.fullName}
                </h4>
              </div>
              <div className="col-sm-6 border-left">
                <div className="row">
                  <div className="col-3">
                    <h6>Origin:</h6>
                    <h6>Destination:</h6>
                    <br></br>
                    <br></br>

                    <h6>Leaving Date:</h6>
                    <h6>Leaving Time:</h6>
                  </div>

                  <div className="col-9">
                    <h6>{this.state.data.origin}</h6>
                    <h6>{this.state.data.destination}</h6>
                    <br></br>
                    <br></br>

                    <h6>
                      <Moment format="DD MMMM, YYYY">
                        {this.state.data.timeLeave}
                      </Moment>
                    </h6>
                    <h6>
                      <Moment format="hh:mm A">
                        {this.state.data.timeLeave}
                      </Moment>{" "}
                      (MST)
                    </h6>
                  </div>
                </div>
              </div>

              <div className="col-sm-2 border-left">
                <p className="text-center">Vehicle</p>
                <br></br>
                <h6 className="text-center">{this.state.vehicle.make}</h6>
                <h6 className="text-center">{this.state.vehicle.model}</h6>
              </div>
              <div className="col-sm-2 border-left">
                <p className="text-right">
                  <b>{this.state.data.remainingAvailiableSeats}</b> seats left
                </p>
                <h3 className="text-right text-success">
                  {this.state.data.pricePerSeat} $
                </h3>
                <p className="text-right">per seat</p>
              </div>
            </div>
            <div className="row">
              <div className="col-sm-2"></div>

              <div className="col-sm-6">
                <div className="row">
                  <div className="col-3">
                    <h6>Note: </h6>
                  </div>
                  <div className="col-9">
                    <p className="text-left">{this.state.data.note}</p>
                  </div>
                </div>
              </div>
            </div>

            {isLogin ? (
              <div className="row border-top">
                <div className="col-md-4 border-right">
                  <br></br>
                  <h5>Driver Details</h5>
                  <br></br>
                  <div className="row">
                    <div className="col-4">
                      <h6>Full Name:</h6>
                      <h6>Age:</h6>
                      <h6>Phone #:</h6>
                      <h6>Email:</h6>
                      <h6>Address:</h6>
                    </div>
                    <div className="col-8">
                      <h6>{this.state.user.fullName}</h6>
                     
                        {dob? 
                        <h6>
                        <Moment
                          diff={this.state.user.dateOfBirth}
                          unit="years"
                        /> Years</h6>
                        : <h6>Not provided</h6> }
                     
                      <h6>{this.state.user.phoneNumber}.</h6>
                      <h6>{this.state.user.email}</h6>
                      <h6>{this.state.user.address}</h6>
                    </div>
                  </div>
                </div>
                <div className="col-md-4 border-right">
                  <br></br>
                  <h5>Vehicle Details</h5>
                  <br></br>
                  <div className="row">
                    <div className="col-4">
                      <h6>Maker:</h6>
                      <h6>Model:</h6>
                      <h6>Type:</h6>
                      <h6>Color:</h6>
                      <h6>Registration:</h6>
                    </div>
                    <div className="col-8">
                      <h6>{this.state.vehicle.make}</h6>
                      <h6>{this.state.vehicle.model}</h6>
                      <h6>{this.state.vehicle.type}</h6>
                      <h6>{this.state.vehicle.color}</h6>
                      <h6>{this.state.vehicle.registration}</h6>
                    </div>
                  </div>
                </div>
                <div className="col-md-4">
                  <br></br>
                  <h5>Booked By</h5>
                  <br></br>

                  {riderCount > 0 ? (
                    this.state.riders.map((i) => (
                      <div className="row">
                        <div className="col-3">
                          <img
                            src={url + i.user.imageURL}
                            width="50"
                            height="50"
                            className="rounded-circle"
                            alt="dp"
                          />
                        </div>
                        <div className="col-6">
                          <h6>
                            <span class="align-middle">{i.user.fullName}</span>
                          </h6>
                        </div>
                        <div className="col-3  align-middle">
                          <h6>
                            <span class="align-middle">
                              {i.seatBooked} Seats
                            </span>
                          </h6>
                        </div>
                      </div>
                    ))
                  ) : (
                    <div className="text-center text-info">
                      <h3>No one has booked yet</h3>
                    </div>
                  )}
                </div>
              </div>
            ) : (
              <div className="text-center text-info">
                <Link to="/login">
                  {" "}
                  <h3>Login to view full details</h3>
                </Link>
              </div>
            )}
            <br></br>
            {!isUserTrip && (
              <div className="row">
                <div className="col-md-6 offset-md-3">
                  <button
                    className="btn btn-primary btn-block"
                    data-toggle="modal"
                    data-target="#addModal"
                    disabled={isLogin ? (isRequested ? true : false) : true}
                  >
                    {isLogin
                      ? isRequested
                        ? "Already Requested"
                        : "Request to Book"
                      : "Login in to Request Booking"}
                  </button>
                </div>
              </div>
            )}
          </div>
        </div>

        <div
          className="modal fade"
          id="addModal"
          tabIndex="-1"
          role="dialog"
          aria-labelledby="exampleModalCenterTitle"
          aria-hidden="true"
        >
          <div className="modal-dialog modal-dialog-centered" role="document">
            <div className="modal-content">
              <form onSubmit={this.Request}>
                <div className="modal-header">
                  <h5 className="modal-title" id="exampleModalLongTitle">
                    Send Request
                  </h5>
                  <button
                    type="button"
                    className="close"
                    data-dismiss="modal"
                    aria-label="Close"
                  >
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div className="modal-body">
                  <div className="form-group row">
                    <label
                      htmlFor="seatsInput"
                      className="col-sm-3 col-form-label"
                      required
                    >
                      Requested Seats
                    </label>
                    <div className="col-sm-9">
                      <input
                        type="number"
                        min="1"
                        max={this.state.data.remainingAvailiableSeats}
                        className="form-control"
                        id="seatsInput"
                        name="seatsInput"
                        placeholder="Seats"
                        value={this.state.seatsInput}
                        onChange={this.onChange.bind(this)}
                        required
                      />
                    </div>
                  </div>
                </div>
                <div className="modal-footer">
                  <button
                    type="button"
                    className="btn btn-secondary"
                    data-dismiss="modal"
                  >
                    Close
                  </button>
                  <button
                    type="submit"
                    data-toggle="modal"
                    data-target="#exampleModal"
                    className="btn btn-primary"
                    disabled={this.state.isAdding}
                  >
                    Request
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
        <MessageDialog message={this.state.message} />
      </React.Fragment>
    );
  }
}

export default HomeItemDetail;
