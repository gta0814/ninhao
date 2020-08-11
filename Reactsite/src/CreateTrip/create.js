import React from 'react';
import { Form } from 'antd';
import Demo from './createForm';

class CreatePage extends React.Component {
  callCreateAPI = (values) => {
    console.log('Calling api with values', values);
  }
  render() {
    const WrappedDemo = Form.create({ name: 'validate_other' })(Demo);
    console.log('Wrapped Demo', WrappedDemo);
    return (
      <WrappedDemo submitCreateForm={this.callCreateAPI} />
    );
  }
}

export default CreatePage;
