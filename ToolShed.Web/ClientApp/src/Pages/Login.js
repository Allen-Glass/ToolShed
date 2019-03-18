import React, { Component } from 'react';
import { createUserAccount } from '../Services/UserService';
import GoogleLogin from 'react-google-login';
import TextInput from '../components/TextInput';
import FacebookLogin from 'react-facebook-login';

class Login extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isOpen: false,
            nav: true
        };
    }

    createUserAccount = () => {
        createUserAccount();
    }

    responseGoogle = (response) => {
        console.log(response);
    }

    responseFacebook = (response) => {
        console.log(response);
    }

    render() {
        return (
            <div>
                <GoogleLogin
                    clientId="580066842012-091fuh06s2kg8ukunrbtjskrobgmuaim.apps.googleusercontent.com"
                    buttonText="Login"
                    onSuccess={this.responseGoogle}
                    onFailure={this.responseGoogle}
                />
                <FacebookLogin
                    appId=""
                    autoLoad={true}
                    fields="name, email, picture"
                    callback={this.responseFacebook}
                />
                <TextInput
                    placeholder="test"
                />
            </div>
            );
    }
}

export default Login;