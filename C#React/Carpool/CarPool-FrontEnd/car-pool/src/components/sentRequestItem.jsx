import Moment from "react-moment";
import url from "../config";
function SentRequestItem(prams) {
  let status = "Pending";
  let isApprovedNull = true;
 
  if (prams.status === true) {
    status = "Approved";
    isApprovedNull =false;
  } else if (prams.status === false) {
    status = "Rejected";
    isApprovedNull = false;
  }
 
  return (
    <div className="card mx-4 mt-4">
      <div className="card-header">
        <div className="row">
          <div className="col-sm-2">
            <h5 className="text-center">To</h5>

            <img
              src={url + prams.trip.vehicle.user.imageURL}
              width="100"
              height="100"
              className="rounded-circle mx-auto d-block"
              alt="dp"
            />

            <h5 className="text-center mt-4">
              {prams.trip.vehicle.user.fullName}
            </h5>
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
            <h6 className="text-center">{prams.trip.vehicle.make}</h6>
            <h6 className="text-center">{prams.trip.vehicle.model}</h6>
          </div>
          <div className="col-sm-2 border-left">
            <p className="text-right">seats requested</p>
            <h3 className="text-right text-primary">{prams.seatRequested}</h3>

            {isApprovedNull && (
              <div>
                <br></br>
                <button
                  className="btn btn-danger btn-block"
                  onClick={prams.delete}
                  id={prams.id}
                >
                  Delete
                </button>
              </div>
            ) }

          </div>
        </div>
        <br></br>
        <div className="row border-top mx-3">
          <p className="h4">
            Status: <b className="text-primary"> {status}</b>
          </p>
        </div>
      </div>
    </div>
  );
}

export default SentRequestItem;
