import React from 'react';
import { BrowserRouter as Router, Route } from "react-router-dom";
import './App.css';
import { Layout } from 'antd';
import { Row } from 'antd';

import MenuBar from './MenuBar'
import Search from './Trips/Search'
import Trip from './Trips/Trip';
import CreatePage from './CreateTrip/create';
import Signup from './signup';

import 'antd/dist/antd.css';;
const { Header, Footer, Content, Sider } = Layout;

function App() {
  return (
    <Router>
      <Layout>
        <Header className="header">
          <div className="logo" />
          <MenuBar />
        </Header>
        <Layout>
        <Sider>广告</Sider>
        <Content>

          <Route exact path="/" component={() => {return(
            <div>
              <Row gutter={24} type="flex" justify="center">
                <Search />
              </Row>
              <Row gutter={24} type="flex" justify="space-around" align="middle">
                <Trip />
                <Trip />
                <Trip />
                <Trip />
              </Row>
            </div>
          )}

          } />

          <Route path="/create" component={CreatePage} />
          <Route path="/signup" component={Signup} />
          <Route path="/help" component={() => {return (<h2>This is help page</h2>);}} />

        </Content>
        <Sider>广告</Sider>
        </Layout>
        <Footer 
        style={{
          textAlign: 'center',
          position: 'bottom',
          height: '10vh' }}>Footer</Footer>
      </Layout>
    </Router>
  );
}

export default App;


//index
//user
//user/create
//user/view
//user/delete
//help
