import React from 'react';
import {
  Form,
  Select,
  Button,
  Icon,
  Input,
  Tooltip,
} from 'antd';

const { Option } = Select;

class SignupForm extends React.Component {

    handleSubmit = e => {
        e.preventDefault();
        this.props.form.validateFields((err, values) => {
          if (!err) {
            //Call API
            console.log('Received values of form: ', values);
          } else {
              //Show validation errors
          }
        });
      };
    

  render() {
    const { getFieldDecorator } = this.props.form;
    const formItemLayout = {
      labelCol: { span: 6 },
      wrapperCol: { span: 14 },
    };
    return (
      <Form {...formItemLayout} onSubmit={this.handleSubmit}>
        <Form.Item>
          <span className="ant-form-text">Sign Up</span>
        </Form.Item>
        <Form.Item label="Username" hasFeedback>
          {getFieldDecorator('username', {
            rules: [{ required: true, message: 'Please enter a username' }],
          })(
            <Input
                placeholder="Enter your username"
            />
          )}
        </Form.Item>

        <Form.Item label="Password" hasFeedback>
          {getFieldDecorator('password', {
            rules: [{ required: true, message: 'Please enter a username' }],
          })(
            <Input.Password
                placeholder="Enter a password"
                suffix={
                    <Tooltip title="Password must be at least 6 characters">
                    <Icon type="info-circle" style={{ color: 'rgba(0,0,0,.45)' }} />
                    </Tooltip>
                }
            />
          )}
        </Form.Item>

        

        <Form.Item wrapperCol={{ span: 12, offset: 6 }}>
          <Button type="primary" htmlType="submit">
            Submit
          </Button>
        </Form.Item>
      </Form>
    );
}
}
export default SignupForm;