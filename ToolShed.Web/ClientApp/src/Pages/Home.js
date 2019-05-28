import React, { Component } from 'react';
import { connect } from 'react-redux';
import '../Styles/App.css';

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

    SendFullName = () => {
        fetch("https://toolshed-api.azurewebsites.net/api/dispenser/sendaction",
            {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    FirstName: this.state.FirstName,
                    LastName: this.state.LastName,
                })
            })
            .catch(error => console.error('Error:', error));

    }

    goToSearchMapsPage = () => {
        window.location.href = "/maps/search";
    }

    render() {
        return (
            <div className="home-container">
                <div className="hero-container">
                    <div className="hero-header">
                        <span id="hero-header-text">Build Faster, Sooner<br /></span>
                        <span id="header-header-subtext">See what items are being sold and rented in your area.<br /></span>
                        <button onClick={this.goToSearchMapsPage}>Search your area</button>
                    </div>
                    <div className="hero-image">
                        <img src={require("../images/bridge.jpeg")} alt="home image" />
                    </div>
                </div>
            </div>
        );
    }
}
export default connect()(Home);
