import React, { Component } from 'react';
import TextInput from '../components/TextInput';
import { StartRental } from '../Services/RentalService';

class RentalStart extends Component {
    constructor(props) {
        super(props);

        this.state = {
            codeInput: ''
        };
    }

    sendRentalStart = (heading) => {
        this.setState({ codeInput: heading });
        var rental = {
            "rentalId": "",
            "LockerCode": "123456"           
        }
        console.log(heading.target);
    }

    render() {
        return (
            <div className="form-container">
                <TextInput
                    placeholder="Enter Code"
                    value=""
                />
                <button
                    onClick={this.sendRentalStart}
                >
                    <span>Begin</span>
                </button>
            </div>
        );
    }
}

export default RentalStart;