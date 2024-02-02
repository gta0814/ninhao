import React, { Component } from "react";
import $ from "jquery";
import MessageDialog from "./messageDialog";
import Loading from "./loading";
import url from "../config";
import Cookies from "universal-cookie";
import Logout from "./logout";
import SentRequestItem from './sentRequestItem';
import ConfirmationDialog from "./confirmationDialog";

class SentRequest extends Component{
    state = {
        message: "",        
        isLoading: true,
        deleteId: 0,
        data: [],
      };
    
      componentDidMount() {
        this.setState({ isLoading: true });
        const cookies = new Cookies();
        var token = cookies.get("token");
        fetch(url + "/api/RideRequests/GetAllSent", {
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
        fetch(url + "/api/RideRequests/CancelSent?id=" + this.state.deleteId, {
          method: "put",
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
            
    
            {dataCount>0?this.state.data.map((c) => (
              <SentRequestItem
                key={c.id}
                id={c.id}
                delete={this.Delete}
                trip = {c.trip}
                status= {c.isApproved}
                user= {c.user}
                seats={c.totalSeats}
                seatRequested = {c.seatRequested}
              />
            )): <div className="text-center text-info"><h3>No Data Found</h3></div> }
    
         
           
            <ConfirmationDialog
              yes={this.DeleteConfirmed}
              message="Only pending requests can be deleted."
              id="confirmationDialog"
            />
            <MessageDialog message={this.state.message} />
          </React.Fragment>
        );
      }



}
export default SentRequest