function VehicleItem(prams) {
  return (
    <div className="card my-3 mx-4">
      <div className="card-body">
        <div className="row">
          <div className="col-sm-2">
            <h6>Make</h6>
            <h3>{prams.make}</h3>
          </div>
          <div className="col-sm-2">
            <h6>Model</h6>
            <h3>{prams.model}</h3>
          </div>
          <div className="col-sm-2">
            <h6>Type</h6>
            <h3>{prams.type}</h3>
          </div>
          <div className="col-sm-2">
            <h6>Color</h6>
            <h3>{prams.color}</h3>
          </div>
          <div className="col-sm-2">
            <h6>Registration</h6>
            <h3>{prams.registration}</h3>
          </div>
          <div className="col-sm-2">
            <button
              onClick={prams.delete}
              id={prams.id}
              className="btn btn-outline-danger"
            >
              Delete
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default VehicleItem;
