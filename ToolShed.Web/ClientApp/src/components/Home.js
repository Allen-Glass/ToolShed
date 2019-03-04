import React, { Component } from 'react';
import { connect } from 'react-redux';

class Home extends Component {
    constructor() {
        super();
        this.state = {
            input: '',
            filterText: '',
            FirstName: '',
            LastName: '',
            countryList: null
        };
    }

    oshit = () => {
        this.SendFullName();
        alert("Well Hello There " + this.state.FirstName + " " + this.state.LastName +"!");
    }

    toggleup = () => {
        var text = document.getElementById('test')
        text.style.fontSize = '100px';
        text.innerHTML = ('OR IS IT?!?')

    }

    toggledown = () => {
        var text = document.getElementById('test')
        text.style.fontSize = '12px';
        text.innerHTML = ('This is a TEST')
    }

    onFilterChange = (event) => {
        console.log(event.target.value);
        console.log(event.target.name);
        if (event.target.name === "FirstName") {
            this.setState({
                FirstName: event.target.value
            });
        } else if (event.target.name === "LastName") {
            this.setState({
                LastName: event.target.value
            });}
    }

    SendFullName =  () => {
        fetch("https://localhost:44325/api/dispenser/",
            {
                method: 'GET',
                headers: {
                    "Content-Type": "application/json"
                },
            })
            .catch(error => console.error('Error:', error));

    }


    render() {
        return (<div>
            <h1>Hello, world!</h1>
            <h2 id="test"> This is a TEST</h2>
            First name: <input type="text" name="FirstName" onChange={this.onFilterChange} value={this.state.FirstName} /><br />
            Last name: <input type="text" name="LastName" onChange={this.onFilterChange} value={this.state.LastName} /><br />
            <input type="submit" value="Submit" onClick={this.oshit}/><br />
            <button onClick={this.oshit}>Test</button>
            <button onClick={this.toggleup}>Toggle Up</button>
            <button onClick={this.toggledown}>Toggle Down</button>
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
