import React, { Component } from 'react';

export default class TextInput extends Component {
    constructor() {
        super();
        this.state = {
            input: '',
        };
    }

    updateInput = (event) => {
        this.setState({ input: event.target.value });
    }

    render() {
        return (
            <div className="text-input">
                <div className="text-label">
                    <label>{this.props.label}</label>
                </div>
                <input
                    name={this.props.name}
                    onChange={this.updateInput}
                    value={this.state.input}
                    id={this.props.id}
                />
            </div>
            );
    }
}