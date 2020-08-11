import React from 'react';
import { Col, Input, Select, DatePicker, Icon } from 'antd';

const InputGroup = Input.Group;
const { Option } = Select;
const reloadGrid = e => {
  console.log(e);
};
const cities = {};

class Search extends React.Component{
  render(){
    return(
        
        <Col>
          <br />
          <InputGroup compact>
            <Col span={2}>
              <Select defaultValue="单程">
                  <Option value="">往返</Option>
                  <Option value="">单程</Option>
              </Select>
            </Col>
            <Col span={7}>
            <Input style={{ width: '100%' }} defaultValue="Calgary" placeholder="从哪儿" allowClear />
            </Col>
            <Col span={1}>
            <Icon style={{ width: '10%', height: '32px' }} type="rise" />{/*不能居中？！*/}
            </Col>
            <Col span={7}>
            <Input style={{ width: '100%' }} defaultValue="" placeholder="到哪儿" allowClear />
            </Col>
            <Col span={6}>
            <DatePicker style={{ width: '100%', textAlign:'left' }} placeholder="日期" onChange={reloadGrid}/>
            </Col>
          </InputGroup>
          <br />
        </Col>
        
    );
  }
}

export default Search;
