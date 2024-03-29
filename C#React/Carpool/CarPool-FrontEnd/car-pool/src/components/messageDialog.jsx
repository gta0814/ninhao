function MessageDialog(parms) {
  return (
    <div
      className="modal fade"
      id="messageDialog"
      tabIndex="-1"
      role="dialog"
      aria-labelledby="messageDialogCenterTitle"
      aria-hidden="true"
    >
      <div className="modal-dialog modal-dialog-centered" role="document">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title" id="confirmationDialogLongTitle">
              Info
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
            
            {parms.message}
            
          </div>
          <div className="modal-footer">
            <button
              type="button"
              className="btn btn-secondary"
              data-dismiss="modal"
            >
              OK
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default MessageDialog;
