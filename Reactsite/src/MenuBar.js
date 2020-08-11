import React, { Component } from 'react';
import { Menu, Icon } from 'antd';

class MenuBar extends Component {
    state = {
        theme: 'dark',
        current: "1",
    };

    handleClick = e => {
        console.log('click ', e);
        this.setState({
            current: e.key,
        });
    };
    render() {
        return (
            <div>
                <Menu
                    theme={this.state.theme}
                    mode="horizontal"
                    onClick={this.handleClick}
                    style={{ lineHeight: '64px' }}
                >
                    <Menu.Item key="1">

                        <a href="../">
                            <Icon type="appstore" />
                            Trips
                </a>
                    </Menu.Item>
                    <Menu.Item key="2">
                        <a href="./Create" rel="noopener noreferrer">
                            发布
                </a>
                    </Menu.Item>
                </Menu>
            </div>
        );
    }
}

export default MenuBar;