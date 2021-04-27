import React, { Component } from "react";
import $ from "jquery";
import MessageDialog from "./messageDialog";
import Loading from "./loading";
import url from "../config";
import Cookies from "universal-cookie";
import Logout from "./logout";
import RecievedRequestItem from './recievedRequestItem';
import ConfirmationDialog from "./confirmationDialog";

class RecievedRequest extends Component{
    state = {
        message: "",        
        isLoading: true,
        rejectId: 0,
        approveId: 0,
        data: [],
      };
    
    
      componentDidMount() {
       
        this.setState({ isLoading: true });
        const cookies = new Cookies();
        var token = cookies.get("token");
        fetch(url + "/api/RideRequests/GetAllReceived", {
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
    
    
    
      Reject = (e) => {
        this.setState({ rejectId: e.target.id });
        $("#RejectConfirmationDialog").modal("show");
      };
      RejectConfirmed = (e) => {
        $("#RejectConfirmationDialog").modal("hide");
        this.setState({ isLoading: true });
        const cookies = new Cookies();
        var token = cookies.get("token");
        fetch(url + "/api/RideRequests/RejectReceived?id=" + this.state.rejectId, {
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
              
                isLoading: false,
                message: "Rejected",
              });
              let r =  cookies.get("unreadReceived");
              r = r-1;
              cookies.set("unreadReceived",r);
         
              $("#messageDialog").modal("show");
              window.location.reload();
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
        
      Accept = (e) => {
        this.setState({ approveId: e.target.id });
        $("#AcceptConfirmationDialog").modal("show");
      };
      AcceptConfirmed = (e) => {
        $("#AcceptConfirmationDialog").modal("hide");
        this.setState({ isLoading: true });
        const cookies = new Cookies();
        var token = cookies.get("token");
        fetch(url + "/api/RideRequests/ApproveReceived?id=" + this.state.approveId, {
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
               
                isLoading: false,
                message: "Accepted",
              });
              let r =  cookies.get("unreadReceived");
              r = r-1;
              cookies.set("unreadReceived",r);
     
              $("#messageDialog").modal("show");
              window.location.reload();

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
              <RecievedRequestItem
                key={c.id}
                id={c.id}
                reject={this.Reject}
                accept={this.Accept}
                trip = {c.trip}
                vehicle= {c.trip.vehicle}
                user= {c.user}
                seats={c.totalSeats}
                seatRequested = {c.seatRequested}
                isApproved = {c.isApproved}
              />
            )): <div className="text-center text-info"><h3>No Data Found</h3></div> }
    
         
           
            <ConfirmationDialog
              yes={this.RejectConfirmed}
              message="Sender will be notify through email."
              id="RejectConfirmationDialog"
            />
              <ConfirmationDialog
              yes={this.AcceptConfirmed}
              message="Sender will be notify through email."
              id="AcceptConfirmationDialog"
            />
            <MessageDialog message={this.state.message} />
          </React.Fragment>
        );
      }



}
export default RecievedRequest