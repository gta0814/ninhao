import Moment from "react-moment";
import url from "../config";
function RecievedRequestItem(prams) {
  let isApprovedNull = true;
  if (prams.isApproved !== null) {
    isApprovedNull = false;
  }

  return (
    <div className="card mx-4 mt-4">
      <div className="card-header">
        <div className="row">
          <div className="col-sm-2">
            <h6 className="text-center">FROM</h6>
            <img
              src={url + prams.user.imageURL}
              width="100"
              height="100"
              className="rounded-circle mx-auto d-block"
              alt="dp"
            />

            <h5 className="text-center mt-4">{prams.user.fullName}</h5>
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
                <h6>{prams.trip.origin}</h6>
                <h6>{prams.trip.destination}</h6>
                <br></br>
                <br></br>

                <h6>
                  <Moment format="DD MMMM, YYYY">{prams.trip.timeLeave}</Moment>
                </h6>
                <h6>
                  <Moment format="hh:mm A">{prams.trip.timeLeave}</Moment> (MST)
                </h6>
              </div>
            </div>
          </div>

          <div className="col-sm-2 border-left">
            <p className="text-center">Vehicle</p>
            <br></br>
            <h6 className="text-center">{prams.vehicle.make}</h6>
            <h6 className="text-center">{prams.vehicle.model}</h6>
          </div>
          <div className="col-sm-2 border-left">
            <p className="text-right">seats requested</p>
            <h3 className="text-right text-primary">{prams.seatRequested}</h3>

            {isApprovedNull? (
              <div>
                <br></br>
                <button
                  className="btn btn-danger mr-3"
                  onClick={prams.reject}
                  id={prams.id}
                >
                  Reject
                </button>
                <button onClick={prams.accept} id={prams.id} className="btn btn-primary">Accept</button>
              </div>
            ) : prams.isApproved? <h5 className="text-center text-success">Approved</h5> : <h5 className="text-center text-danger">Rejected</h5>}
          </div>
        </div>
      </div>
    </div>
  );
}

export default RecievedRequestItem;
