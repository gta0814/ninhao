import React from 'react';
import { Form } from 'antd';
import SignupForm from './signupForm';

class Signup extends React.Component {
  render() {
    const WrappedDemo = Form.create({ name: 'validate_other' })(SignupForm);
    return (
      <WrappedDemo />
    );
  }
}

export default Signup;
