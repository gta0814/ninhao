import React, { Component } from "react";
import Cookies from "universal-cookie";
import Logout from "./logout";
import url from "../config";
import Loading from "./loading";
import VehicleItem from "./vehicleItem";
import ConfirmationDialog from "./confirmationDialog";
import MessageDialog from "./messageDialog";
import $ from "jquery";
class ManageVehicle extends Component {
  state = {
    message: "",
    makerInput: "",
    modelInput: "",
    typeInput: "",
    colorInput: "",
    registrationInput: "",
    isLoading: true,
    deleteId: 0,
    data: [],
  };

  Add = (e) => {
    e.preventDefault();
    this.setState({ isLoading: true });
    const dataToSend = JSON.stringify({
      make: this.state.makerInput,
      model: this.state.modelInput,
      type: this.state.typeInput,
      color: this.state.colorInput,
      registration: this.state.registrationInput,
   
    });
    const cookies = new Cookies();
    var token = cookies.get("token");
    fetch(url + "/api/Vehicles/AddNew", {
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
          var newData = [responseJson.data].concat(this.state.data);
          this.setState({
            isLoading: false,
            data: newData,
            message: "Data Saved!",
            makerInput: "",
            modelInput: "",
            typeInput: "",
            registrationInput: "",
            colorInput: "",
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
    fetch(url + "/api/Vehicles/Deactive?id=" + this.state.deleteId, {
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

        {dataCount>0?this.state.data.map((c) => (
          <VehicleItem
            key={c.id}
            id={c.id}
            delete={this.Delete}
            make={c.make}
            model={c.model}
            type={c.type}
            color={c.color}
           
            registration={c.registration}
          />
        )): <div className="text-center text-info"><h3>No Data Found</h3></div> }

     
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
                    Add Vehicle
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
                      htmlFor="makerInput"
                      className="col-sm-3 col-form-label"
                    >
                      Maker
                    </label>
                    <div className="col-sm-9">
                      <input
                        type="text"
                        className="form-control"
                        id="makerInput"
                        name="makerInput"
                        placeholder="Maker"
                        value={this.state.makerInput}
                        onChange={this.onChange.bind(this)}
                        required
                      />
                    </div>
                  </div>
                  <div className="form-group row">
                    <label
                      htmlFor="modelInput"
                      className="col-sm-3 col-form-label"
                    >
                      Model
                    </label>
                    <div className="col-sm-9">
                      <input
                        type="text"
                        className="form-control"
                        id="modelInput"
                        name="modelInput"
                        placeholder="Model"
                        value={this.state.modelInput}
                        onChange={this.onChange.bind(this)}
                        required
                      />
                    </div>
                  </div>
                  <div className="form-group row">
                    <label
                      htmlFor="colorInput"
                      className="col-sm-3 col-form-label"
                    >
                      Color
                    </label>
                    <div className="col-sm-9">
                      <input
                        type="text"
                        className="form-control"
                        id="colorInput"
                        name="colorInput"
                        placeholder="Color"
                        value={this.state.colorInput}
                        onChange={this.onChange.bind(this)}
                       
                      />
                    </div>
                  </div>
                  <div className="form-group row">
                    <label
                      htmlFor="typeInput"
                      className="col-sm-3 col-form-label"
                    >
                      Type
                    </label>
                    <div className="col-sm-9">
                      <input
                        type="text"
                        className="form-control"
                        id="typeInput"
                        name="typeInput"
                        placeholder="Type"
                        value={this.state.typeInput}
                        onChange={this.onChange.bind(this)}
                       
                      />
                    </div>
                  </div>
                 
                  <div className="form-group row">
                    <label
                      htmlFor="registrationInput"
                      className="col-sm-3 col-form-label"
                    >
                      Registration
                    </label>
                    <div className="col-sm-9">
                      <input
                        type="text"
                        className="form-control"
                        id="registrationInput"
                        name="registrationInput"
                        placeholder="Registration No"
                        value={this.state.registrationInput}
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
          message="Active trips with vehicle will not be effected."
          id="confirmationDialog"
        />
        <MessageDialog message={this.state.message} />
      </React.Fragment>
    );
  }
}

export default ManageVehicle;
