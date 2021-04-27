import React, { Component } from "react";
import cities from "../cities";
import HomeItem from "./homeItem";
import $ from "jquery";
import MessageDialog from "./messageDialog";
import Loading from "./loading";
import url from "../config";
import Logout from "./logout";
import ReactPaginate from "react-paginate";
class Home extends Component {
  state = {
    message: "",
    isLoading: true,
    data: [],
    pageIndex: "",
    totalPages: "",
    origin: "",
    destination: "",
    leaveDate: "",
  };


Clear = (e) => {

  this.setState({origin: '', destination: '', leaveDate: ''});
}
  Search = (e) => {
    e.preventDefault();
    if (this.state.origin === this.state.destination) {
      if (this.state.origin !== "") {
        this.setState({ message: "Origin and destination cannot be same." });
        $("#messageDialog").modal("show");
        return;
      }
    }
    this.setState({ isLoading: true, pageIndex: null });
    fetch(
      url +
        "/api/Home/GetTrips?origin=" +
        this.state.origin +
        "&destination=" +
        this.state.destination +
        "&leaveDate=" +
        this.state.leaveDate,
      {
        method: "get",
        headers: {
          "Content-Type": "application/json",
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
            isLoading: false,
            data: responseJson.data.items["$values"],
            totalPages: responseJson.data.totalPages,
            pageIndex: responseJson.data.pageIndex,
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
        console.error(error.status);
        $("#messageDialog").modal("show");
      });
  };

  componentDidMount() {
    this.setState({ isLoading: true });
    fetch(
      url +
        "/api/Home/GetTrips?origin=" +
        this.state.origin +
        "&destination=" +
        this.state.destination +
        "&leaveDate=" +
        this.state.leaveDate +
        "&pageIndex=" +
        this.state.pageIndex,
      {
        method: "get",
        headers: {
          "Content-Type": "application/json",
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
            isLoading: false,
            data: responseJson.data.items["$values"],
            totalPages: responseJson.data.totalPages,
            pageIndex: responseJson.data.pageIndex,
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
        console.error(error.status);
        $("#messageDialog").modal("show");
      });
  }
  handlePageClick = (data) => {
    this.setState({ pageIndex: data.selected + 1 }, function () {
      this.componentDidMount();
    });
  };

  onChange(e) {
    this.setState({
      [e.target.name]: e.target.value,
    });
  }
  render() {
    const isLoading = this.state.isLoading;
    const dataCount = this.state.data.length;
    return (
      <React.Fragment>
        {isLoading && <Loading />}
        <form onSubmit={this.Search}>
          <img
            className="img-fluid background-img"
            src="background.jpg"
            alt=""
            hidden
          />
          <br></br>
          <br></br>
          <br></br>
          <br></br>

          <div className="form-row mx-2">
            <div className="col-md-2"></div>

            <div className="col-md-2">
              <label className="text-light">Origin</label>
              <select
                className="form-control"
                aria-label="Default select example"
                onChange={this.onChange.bind(this)}
                name="origin"
                id="origin"
                value={this.state.origin}
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
            <div className="col-md-2">
              <label className="text-light">Destination</label>
              <select
                className="form-control"
                aria-label="Default select example"
                onChange={this.onChange.bind(this)}
                name="destination"
                id="destination"
                value={this.state.destination}
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
            <div className="col-md-2">
              <label className="text-light">Leave Date</label>
              <input
                type="date"
                className="form-control"
                name="leaveDate"
                id="leaveDate"
                onChange={this.onChange.bind(this)}
                value={this.state.leaveDate}
              />
            </div>
            <div className="col-md-2">
              <button
                type="submit"
                className="btn btn-primary btn-block"
                style={{ marginTop: "2rem" }}
              >
                Search
              </button>
              <button  type="button" className="btn btn-link btn-sm btn-block" onClick={this.Clear}>Clear</button>
            </div>
          </div>
          <div className="col-md-2"></div>
        </form>
        <br></br>

        {dataCount > 0 && (
          <h6 className="mx-4">
            Showing Page {this.state.pageIndex} of {this.state.totalPages}
          </h6>
        )}
        {dataCount > 0 ? (
          this.state.data.map((i) => (
            <HomeItem
              key={i.id}
              id={i.id}
              image={url + i.vehicle.user.imageURL}
              user={i.vehicle.user}
              origin={i.origin}
              destination={i.destination}
              timeLeave={i.timeLeave}
              createTime={i.createTime}
              price={i.pricePerSeat}
              seats={i.remainingAvailiableSeats}
              vehicle={i.vehicle}
              delete={this.Delete}
              note={i.note}
            />
          ))
        ) : (
          <div className="text-center text-info">
            <h3>No Data Found</h3>
          </div>
        )}

        {dataCount > 0 && (
          <div className="d-flex justify-content-center my-5">
            <ReactPaginate
              previousLabel={"Previous"}
              nextLabel={"Next"}
              breakLabel={"..."}
              breakClassName={"page-item"}
              breakLinkClassName={"page-link"}
              pageCount={this.state.totalPages}
              marginPagesDisplayed={2}
              pageRangeDisplayed={5}
              onPageChange={this.handlePageClick}
              containerClassName={"pagination mx-auto text-center"}
              subContainerClassName={"pages pagination"}
              activeClassName={"active"}
              pageClassName={"page-item"}
              pageLinkClassName={"page-link"}
              nextClassName={"page-item"}
              nextLinkClassName={"page-link"}
              previousClassName={"page-item"}
              previousLinkClassName={"page-link"}
            />
          </div>
        )}

        <MessageDialog message={this.state.message} />
      </React.Fragment>
    );
  }
}

export default Home;
