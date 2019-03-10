import React, { Component } from 'react';
import { connect } from 'react-redux';

class Home extends Component {
    constructor() {
        super();
        this.domains = ['gmail.com', 'hotmail.com']
        this.state = {
            input: '',
            filterText: '',
            FirstName: '',
            LastName: '',
            Email: '',
            VerifyEmail: '',
            PW: '',
            VerifyPW: '',
            PWMatch: '',
            EmailMatch: ''
        };
    }



    //oshit = () => {
    //    this.SendFullName();
    //    alert("Well Hello There " + this.state.FirstName + " " + this.state.LastName +"!");
    //}

    //toggleup = () => {
    //    var text = document.getElementById('test')
    //    text.style.fontSize = '100px';
    //    text.innerHTML = ('OR IS IT?!?')

    //}

    //toggledown = () => {
    //    var textsize
    //    var text = document.getElementById('test')
    //    textsize = text.style.fontSize;
    //    textsize = textsize.split("px");
    //    console.log(textsize);
    //    textsize = Number(textsize) - 10;
    //    //text.style.fontSize = textsize.concat("px");
    //    text.innerHTML = ('This is a TEST');
    //}

    TogglePW = () => {
        var PW = document.getElementById('PW');
        var VPW = document.getElementById('VerifyPW');
        if (PW.type === "password") {
            PW.type = "text";
            VPW.type = "text";
        } else {
            PW.type = "password";
            VPW.type = "password";
        }
        

    }


    onFilterChange = (event) => {
        if (event.target.name === "Email") {
            this.setState({
                Email: event.target.value
            });
        } else if (event.target.name === "VerifyEmail") {
            this.setState({
                VerifyEmail: event.target.value
            });
        } else if (event.target.name === "PW") {
            this.setState({
                PW: event.target.value
            });
        } else if (event.target.name === "VerifyPW") {
            this.setState({
                VerifyPW: event.target.value
            });
        }
    }

    VerifyInput = () => {
        if (this.state.Email === this.state.VerifyEmail) {
            var domain = this.state.Email.split("@");
            if (this.domains.includes(domain[1])) {
                if (this.state.PW === this.state.VerifyPW) {
                    this.SendNewUser();
                } else {
                    alert("Please Verify Password")
                }
            } else {
                alert("Please Enter a Valid Email")
            }
        } else {
            alert("Please Verify Email")
        }
    }

    SendNewUser =  () => {
        fetch("https://toolshed-api.azurewebsites.net/api/dispenser/sendaction",
            {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    FirstName: this.state.Email,
                    LastName: this.state.PW,
                })
            })
            .catch(error => console.error('Error:', error));

    }


    render() {
        return (<div>
            <h1>Create a Profile</h1>

            Email: <br /><input type="text" name="Email" onChange={this.onFilterChange} value={this.state.Email} /><br />

            Re-enter Email: <br /><input type="text" name="VerifyEmail" onChange={this.onFilterChange} value={this.state.VerifyEmail} /><br />

            Password: <br /><input id="PW" placeholder="password" type="password" onChange={this.onFilterChange} value={this.state.PW} required/><br />

            Re-enter Password: <br /><input id="VerifyPW" placeholder="password" type="password" onChange={this.onFilterChange} value={this.state.VerifyPW} required/><br />

            <input type="checkbox" onChange={this.TogglePW} />Show Password<br />
            <input type="submit" value="Submit" onClick={this.VerifyInput} /><br />

            <br />
            <br />
            <br />
            <br />
            <p>Welcome to your new single-page application, built with:</p>
            <ul>
                <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                <li><a href='https://facebook.github.io/react/'>React</a> and <a href='https://redux.js.org/'>Redux</a> for client-side code</li>
                <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
                <li><a href='http://reddit.com/'>Reddit</a> for a good way to waste time</li>
            </ul>
            <p>To help you get started, we've also set up:</p>
            <ul>
                <li><strong>Client-side navigation</strong>. For example, click <em>Counter</em> then <em>Back</em> to return here.</li>
                <li><strong>Development server integration</strong>. In development mode, the development server from <code>create-react-app</code> runs in the background automatically, so your client-side resources are dynamically built on demand and the page refreshes when you modify any file.</li>
                <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and your <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
            </ul>
            <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>
        </div>
        );
    }
}
export default connect()(Home);


//removed from sight!
//<h2 id="test" fontSize='24px'> This is a TEST</h2>
//<button onClick={this.oshit}>Test</button>
//<button onClick={this.toggleup}>Toggle Up</button>
//<button onClick={this.toggledown}>Toggle Down</button>