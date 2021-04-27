import React from 'react';
import { Card, Col, Row, Checkbox, Icon } from 'antd';

const driver = (
  <div className="trip-item-profile-right">
    <div className="trip-item-profile-name">
      Cris
    </div>
    <div className="xxs-spacer"></div>
    <div className="trip-item-profile-driven">
      24 driven
    </div>
    <div className="trip-item-profile-reviews">
      <div className="rating-summary">
        <div className="rating">
          5.0
        </div>
        <div className="divider-dot"></div>
        <div className="review-count">
          <a href="/user/u_Ia9TsRSYdNYtS4ypSi3FmINp/reviews" className="link-grey">14 reviews</a>
        </div>
        <div className="clear"></div>
      </div>
    </div>
  </div>
);

const trip = (
  <div className="trip-item-details-locations ">
    <Row>
      <Col span={6}>
        <span>Calgary</span>
      </Col>
      <Col span={12}>
        <div>Leaving： Monday at 6:30am</div>
        <Icon type="swap-right" />

      </Col>
      <Col span={6}>
        <span>Edmonton</span>
      </Col>
    </Row>
  </div>
);

class Trip extends React.Component {
  render() {
    return (
      <Col span={24}>
        <Card bordered={true}>
          <Row>
            <Col span={4}>
              <Row>
                <Col span={12}>
                  <img height="100px" width="100px" alt="" src="https://images1.livehindustan.com/uploadimage/library/2018/10/07/16_9/16_9_1/google_map_Symbolic_Image__1538892270.jpg"></img>
                </Col>
                {/* <Col span={6}>
                  {driver}
                </Col> */}
              </Row>

            </Col>
            <Col span={8}>
              {trip}
            </Col>
            <Col span={2}>
              <Checkbox indeterminate={true}>接</Checkbox>
              <Checkbox checked={false}>送</Checkbox>
              <Checkbox checked={true} >Test</Checkbox>
            </Col>
            <Col span={4}>
              <img height="100px" width="100px" alt="" src="https://hitchplanet.s3.amazonaws.com/images/vehicles/thumbs/ne05DNfq9ym1bRRkkcs7_200x150.jpg"></img>
            </Col>
            <Col span={2}>
              <div className="trip-item-cta-price">$40</div>
              <div className="trip-item-cta-per-seat">per seat</div>
            </Col>
            <Col span={2}>
              <div className="trip-contact">7809648915</div>
            </Col>
            <Col span={2}>
              <div className="trip-contact">gta0814</div>
            </Col>
          </Row>
        </Card>
      </Col>
    );
  }
}

export default Trip;
