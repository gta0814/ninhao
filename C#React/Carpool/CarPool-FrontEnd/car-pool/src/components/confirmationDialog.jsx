function ConfirmationDialog(prams) {
  return (
    <div
      className="modal fade"
      id={prams.id}
      tabIndex="-1"
      role="dialog"
      aria-labelledby="confirmationDialogCenterTitle"
      aria-hidden="true"
    >
      <div className="modal-dialog modal-dialog-centered" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title" id="confirmationDialogLongTitle">
              Confirmation
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
            You cannot undo this action.
            <br></br>
            {prams.message}
            <br></br>
            Do you want to continue?
          </div>
          <div className="modal-footer">
            <button
              type="button"
              className="btn btn-secondary"
              data-dismiss="modal"
            >
              No
            </button>
            <button
              type="button"
              onClick={prams.yes}
              className="btn btn-danger"
            >
              Yes
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ConfirmationDialog;
