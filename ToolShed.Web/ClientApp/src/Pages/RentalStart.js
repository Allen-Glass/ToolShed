import React, { Component } from 'react';
import TextInput from '../components/TextInput';
import { PlaceRental, StartRental } from '../Services/RentalService';

class RentalStart extends Component {
    constructor(props) {
        super(props);

        this.state = {
            codeInput: ''
        };
    }

    placeRental = () => {
        var rental = {
            "DispenserId": "",
            "ItemRentalDetails": {
                "LockerNumber": ""
            }
        }
        PlaceRental(rental);
    }

    startRental = () => {
        var lockerCode = document.getElementById("LockerCode").value;
        var rental = {
            "rentalId": "",
            "LockerCode": lockerCode          
        }
        StartRental(rental);
    }

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

export default RentalStart;