import React from 'react';
import {
  Form,
  Select,
  InputNumber,
  Radio,
  Slider,
  Button,
  Input,
} from 'antd';

const { Option } = Select;

class CreateForm extends React.Component {
    state = {
      value: 0,
    };

    onChange = e => {
      console.log('radio checked', e.target.value);
      this.setState({
        value: e.target.value,
      });
    };
    handleSubmit = e => {
        e.preventDefault();
        this.props.form.validateFields((err, values) => {
          if (!err) {
            //Call API
            this.props.submitCreate(values);
            console.log('Received values of form: ', values);
          } else {
              //Show validation errors
          }
        });
      };

      normFile = e => {
        console.log('Upload event:', e);
        if (Array.isArray(e)) {
          return e;
        }
        return e && e.fileList;
      };

  render() {
    const { getFieldDecorator } = this.props.form;
    const formItemLayout = {
      labelCol: { span: 6 },
      wrapperCol: { span: 14 },
    };
    return (
      <Form {...formItemLayout} onSubmit={this.handleSubmit}>

        <Form.Item label="出发" hasFeedback>
          {getFieldDecorator('from', {
            rules: [{ required: true, message: '从哪出发啊？' }],
          })(
            <Select placeholder="请选择一个城市">
              <Option value="china">China</Option>
              <Option value="usa">U.S.A</Option>
            </Select>,
          )}
        </Form.Item>
        <Form.Item label="到达" hasFeedback>
          {getFieldDecorator('to', {
            rules: [{ required: true, message: '去哪啊？' }],
          })(
            <Select placeholder="请选择一个城市">
              <Option value="china">China</Option>
              <Option value="usa">U.S.A</Option>
            </Select>,
          )}
        </Form.Item>

        <Form.Item label="座位">
          {getFieldDecorator('seats', { initialValue: 3 })(<InputNumber min={1} max={10} />)}
          <span className="ant-form-text"> 空闲座位</span>
        </Form.Item>

        {/*<Form.Item label="Switch">
          {getFieldDecorator('switch', { valuePropName: 'checked' })(<Switch />)}
        </Form.Item>*/}

        <Form.Item label="接送偏好">
          {getFieldDecorator('slider')(
            <Slider
              marks={{
                5: '打死不管',
                20: '最好不用',
                40: '一般不管',
                60: '可以考虑',
                80: '好商量',
                100: '没问题',
              }}
            />,
          )}
        </Form.Item>

        <Form.Item label="价格">
          {getFieldDecorator('price')(
            <Radio.Group allignment="left" onChange={this.onChange} value={this.state.value}> 
              <Radio value={1}>
                一口价
                {this.state.value === 1 ? <Input style={{ width: 100, marginLeft: 10 }} /> : null}
              </Radio><br/>
              
              <Radio value={2}>私聊</Radio><br/>
              <Radio value={3}>免费陪我走一程</Radio>
            </Radio.Group>
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
export default CreateForm;
