function Loading() {
  return (
    <div
      className="modal fade"
      style={{ display: "block", opacity: "1", zIndex: "9999999" }}
      tabIndex="-1"
      role="dialog"
      aria-labelledby="exampleModalCenterTitle"
      aria-hidden="true"
    >
      <div className="modal-dialog modal-dialog-centered" role="document">
        <div
          className="modal-content"
          style={{ backgroundColor: "rgba(0, 0, 0, 0.8)", height: "200px" }}
        >
          <img
            src="loading.gif"
            width="150px"
            height="150px"
            className="img-fluid mx-auto my-auto"
            alt="Paris"
          />
        </div>
      </div>
    </div>
  );
}

export default Loading;
