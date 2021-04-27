

function TripItem(prams) {
    return <div className="card my-3 mx-4">
    <div className="card-body">
      <div className="row">
        <div className="col-sm-2">
          <h6>Origin</h6>
          <h5>{prams.origin}</h5>
        </div>
        <div className="col-sm-2">
          <h6>Destination</h6>
          <h5>{prams.destination}</h5>
        </div>
        <div className="col-sm-2">
          <h6>Time Leave</h6>
          <h5>{prams.timeLeave}</h5>
        </div>
        <div className="col-sm-2">
          <h6>Price Per Seat</h6>
          <h5>{prams.price} $</h5>
        </div>
        <div className="col-sm-2">
          <h6>Seats Available</h6>
          <h5>{prams.seats}</h5>
        </div>
        <div className="col-sm-2">
          <h6>Vehicle</h6>
          <h5>{prams.vehicle}</h5>
        </div>
      </div>
      <div className="float-right mt-4">

      <button onClick={prams.delete} id={prams.id} className="btn btn-outline-danger">Delete</button>

      </div>
    </div>
  </div>

}


export default TripItem