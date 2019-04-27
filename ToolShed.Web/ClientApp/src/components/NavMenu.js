import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export default class NavMenu extends Component {
    constructor(props) {
        super(props);

        this.toggle = this.toggle.bind(this);
        this.state = {
            isOpen: false,
            nav: true
        };
    }

    showSomething = () => {
        this.setState({
            nav: !this.state.nav
        });
    }

    toggle () {
    this.setState({
        isOpen: !this.state.isOpen
    });
    }

    render () {
    return (
        <header>
        <div className="navbar-container" >
            <div className="site-branding">
                Tool Shed
            </div>
            <div className="navbar-toggle" onClick={this.toggle} />
                <div className="navbar-links" isOpen={this.state.isOpen}>
                <a onMouseOver={this.showSomething} href="/" className="navbar-link">Rent</a>
                <a onMouseOver={this.showSomething} href="/" className="navbar-link">Buy</a>
                <a onMouseOver={this.showSomething} href="/" className="navbar-link">Own</a>
                <a onMouseOver={this.showSomething} href="/rental/start" className="navbar-link">Rental</a>
                <a onMouseOver={this.showSomething} href="/login" className="navbar-link">Login</a>
                <i></i>
                <i></i>
                <i></i>
            </div>
            <div className="navbar-sublinks">
                <div hidden={this.state.nav} className="thing">asdfasdf</div>
            </div>
        </div>
        </header>
    );
    }
}
