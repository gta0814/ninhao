import Moment from "react-moment";
import { Link } from "react-router-dom";

function HomeItem(prams) {
  return (
    <Link
      to={{pathname: "/detail", id: prams.id}}
      style={{ color: "inherit", textDecoration: "none" }}
    >
      <div className="card mx-4 mt-4">
        <div className="card-header">
          <div className="row">
            <div className="col-sm-2">
              <img
                src={prams.image}
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
                  <h6>{prams.origin}</h6>
                  <h6>{prams.destination}</h6>
                  <br></br>
                  <br></br>

                  <h6>
                    <Moment format="DD MMMM, YYYY">{prams.timeLeave}</Moment>
                  </h6>
                  <h6>
                    <Moment format="hh:mm A">{prams.timeLeave}</Moment> (MST)
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
              <p className="text-right">
                <b>{prams.seats}</b> seats left
              </p>
              <h3 className="text-right text-success">{prams.price} $</h3>
              <p className="text-right">per seat</p>
            </div>
          </div>
        </div>
      </div>
    </Link>
  );
}

export default HomeItem;
