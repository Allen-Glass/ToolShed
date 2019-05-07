import React, { Component } from 'react';
import TextInput from '../components/TextInput';

class Dispenser extends Component {
    render() {
        return (
            <div className="form-container">
                <TextInput
                    id="DispenserId"
                    label="Dispenser Id"
                />
                <TextInput
                    id="LockerNumber"
                    label="Locker Number"
                />
                <button
                    onClick={this.startRental}
                >
                    <span>Place</span>
                </button>
                <TextInput
                    id="LockerCode"
                    label="Enter Code"
                />
                <button
                    onClick={this.startRental}
                >
                    <span>Begin</span>
                </button>
            </div>
            );
    }
}

export default Dispenser;