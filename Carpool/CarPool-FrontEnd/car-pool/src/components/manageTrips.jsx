import React, { Component } from "react";
import Cookies from "universal-cookie";
import Logout from "./logout";
import url from "../config";
import Loading from "./loading";
import TripItem from "./tripItem";
import ConfirmationDialog from "./confirmationDialog";
import MessageDialog from "./messageDialog";
import cities from "../cities";
import $ from "jquery";
class ManageTrips extends Component {
  state = {
    message: "",
    destinationInput: "",
    originInput: "",
    vehicleIdInput: "",
    availableSeatsInput: "",
    leaveDateTimeInput: "",
    pricePerSeatInput: "",
    noteInput: "",
    isLoading: true,
    deleteId: 0,
    vehicleData: [],
    data: [],
  };

  Add = (e) => {
    e.preventDefault();

    if (this.state.originInput === this.state.destinationInput) {
      this.setState({ message: "Origin and destination cannot be same." });
      $("#messageDialog").modal("show");
      return;
    }
    var d = new Date(this.state.leaveDateTimeInput);
    var cd = new Date();
    if (cd > d) {
      this.setState({ message: "You cannot create a trip with past date" });
      $("#messageDialog").modal("show");
      return;
    }

    this.setState({ isLoading: true });
    const dataToSend = JSON.stringify({
      vehicleId: this.state.vehicleIdInput,
      origin: this.state.originInput,
      destination: this.state.destinationInput,
      timeLeave: this.state.leaveDateTimeInput,
      availiableSeats: this.state.availableSeatsInput,
      pricePerSeat: this.state.pricePerSeatInput,
      note: this.state.noteInput,
    });
    const cookies = new Cookies();
    var token = cookies.get("token");
    fetch(url + "/api/Trips/CreateNew", {
      method: "post",
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
          responseJson.data.vehicle = this.state.vehicleData.filter(
            (i) => i.id !== e.target.value
          )[0];
          var newData = [responseJson.data].concat(this.state.data);
          this.setState({
            isLoading: false,
            data: newData,
            message: "Data Saved!",
            destinationInput: "",
            originInput: "",
            vehicleIdInput: "",
            availableSeatsInput: "",
            leaveDateTimeInput: "",
            pricePerSeatInput: "",
            noteInput: "",
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

  onVehicleChange(e) {
    this.setState({
      [e.target.name]: e.target.value,
    });
  }

  onChange(e) {
    this.setState({
      [e.target.name]: e.target.value,
    });
  }

  componentDidMount() {
    this.setState({ isLoading: true });
    const cookies = new Cookies();
    var token = cookies.get("token");
    fetch(url + "/api/Vehicles/GetAll", {
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
            isLoading: false,
            vehicleData: responseJson.data["$values"],
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

    fetch(url + "/api/Trips/GetAll", {
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
            isLoading: false,
            data: responseJson.data["$values"],
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

  Delete = (e) => {
    this.setState({ deleteId: e.target.id });
    $("#confirmationDialog").modal("show");
  };
  DeleteConfirmed = (e) => {
    $("#confirmationDialog").modal("hide");
    this.setState({ isLoading: true });
    const cookies = new Cookies();
    var token = cookies.get("token");
    fetch(url + "/api/Trips/CancelByDriver?id=" + this.state.deleteId, {
      method: "post",
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
            data: this.state.data.filter(
              (i) => i.id.toString() !== this.state.deleteId
            ),
            isLoading: false,
            message: "Deleted",
          });
          $("#messageDialog").modal("show");
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
  };
  render() {
    const isLoading = this.state.isLoading;
    const dataCount = this.state.data.length;
    return (
      <React.Fragment>
        {isLoading && <Loading />}

        <br></br>
        <button
          type="button"
          className="btn btn-primary float-right mr-4"
          data-toggle="modal"
          data-target="#addModal"
        >
          Add New
        </button>
        <br></br>

        {dataCount > 0 ? (
          this.state.data.map((i) => (
            <TripItem
              key={i.id}
              id={i.id}
              origin={i.origin}
              destination={i.destination}
              timeLeave={i.timeLeave}
              price={i.pricePerSeat}
              seats={i.availiableSeats}
              vehicle={
                i.vehicle.make +
                ", " +
                i.vehicle.model +
                ", " +
                i.vehicle.registration
              }
              delete={this.Delete}
            />
          ))
        ) : (
          <div className="text-center text-info">
            <h3>No Data Found</h3>
          </div>
        )}

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
              <form onSubmit={this.Add}>
                <div className="modal-header">
                  <h5 className="modal-title" id="exampleModalLongTitle">
                    Create Trip
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
                      htmlFor="originInput"
                      className="col-sm-3 col-form-label"
                    >
                      Origin
                    </label>
                    <div className="col-sm-9">
                      <select
                        className="form-control"
                        aria-label="Default select example"
                        name="originInput"
                        value={this.state.originInput}
                        onChange={this.onChange.bind(this)}
                        required
                      >
                        <option key="" value=""></option>
                        {cities.map((c) => (
                          <option
                            key={c.Name + ", " + c.Province}
                            value={c.Name + ", " + c.Province}
                          >
                            {c.Name + ", " + c.Province}
                          </option>
                        ))}
                      </select>
                    </div>
                  </div>
                  <div className="form-group row">
                    <label
                      htmlFor="destinationInput"
                      className="col-sm-3 col-form-label"
                    >
                      Destination
                    </label>
                    <div className="col-sm-9">
                      <select
                        className="form-control"
                        aria-label="Default select example"
                        name="destinationInput"
                        value={this.state.destinationInput}
                        onChange={this.onChange.bind(this)}
                        required
                      >
                        <option key="" value=""></option>
                        {cities.map((c) => (
                          <option
                            key={c.Name + ", " + c.Province}
                            value={c.Name + ", " + c.Province}
                          >
                            {c.Name + ", " + c.Province}
                          </option>
                        ))}
                      </select>
                    </div>
                  </div>
                  <div className="form-group row">
                    <label
                      htmlFor="vehicleIdInput"
                      className="col-sm-3 col-form-label"
                    >
                      Vehicle
                    </label>
                    <div className="col-sm-9">
                      <select
                        className="form-control"
                        aria-label="Default select example"
                        name="vehicleIdInput"
                        value={this.state.vehicleIdInput}
                        onChange={this.onVehicleChange.bind(this)}
                        required
                      >
                        <option key="" value=""></option>
                        {this.state.vehicleData.map((i) => (
                          <option key={i.id} value={i.id}>
                            {i.make + ", " + i.model + ", " + i.registration}
                          </option>
                        ))}
                      </select>
                    </div>
                  </div>
                  <div className="form-group row">
                    <label
                      htmlFor="leaveDateTimeInput"
                      className="col-sm-3 col-form-label"
                    >
                      Time Leave (MST)
                    </label>
                    <div className="col-sm-9">
                      <input
                        type="datetime-local"
                        className="form-control"
                        id="leaveDateTimeInput"
                        name="leaveDateTimeInput"
                        value={this.state.leaveDateTimeInput}
                        onChange={this.onChange.bind(this)}
                        required
                      />
                    </div>
                  </div>

                  <div className="form-group row">
                    <label
                      htmlFor="pricePerSeatInput"
                      className="col-sm-3 col-form-label"
                    >
                      price Per Seat ($)
                    </label>
                    <div className="col-sm-9">
                      <input
                        type="number"
                        min="1"
                        className="form-control"
                        id="pricePerSeatInput"
                        step=".01"
                        name="pricePerSeatInput"
                        value={this.state.pricePerSeatInput}
                        onChange={this.onChange.bind(this)}
                        required
                      />
                    </div>
                  </div>
                  <div className="form-group row">
                    <label
                      htmlFor="seatsInput"
                      className="col-sm-3 col-form-label"
                    >
                      Seats Available
                    </label>
                    <div className="col-sm-9">
                      <input
                        type="number"
                        className="form-control"
                        id="availableSeatsInput"
                        name="availableSeatsInput"
                        max="32"
                        min="1"
                        value={this.state.availableSeatsInput}
                        onChange={this.onChange.bind(this)}
                        required
                      />
                    </div>
                  </div>
                  <div className="form-group row">
                    <label
                      htmlFor="noteInput"
                      className="col-sm-3 col-form-label"
                    >
                      Note
                    </label>
                    <div className="col-sm-9">
                      <textarea
                        type="text"
                        className="form-control"
                        id="noteInput"
                        name="noteInput"
                        value={this.state.noteInput}
                        onChange={this.onChange.bind(this)}
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
                    Add
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
        <ConfirmationDialog
          yes={this.DeleteConfirmed}
          message="Passangers will be notify through email."
          id="confirmationDialog"
        />
        <MessageDialog message={this.state.message} />
      </React.Fragment>
    );
  }
}

export default ManageTrips;
