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
                <input
                    name={this.props.name}
                    onChange={this.updateInput}
                    value={this.state.input}
                    placeholder={this.props.placeholder}
                />
            </div>
            );
    }
}