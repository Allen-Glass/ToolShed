import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/Counter';

class Counter extends Component {
    constructor(props) {
        super(props);
        this.state = {
            input: '',
            filterText: '',
            FirstName: '',
            LastName: '',
            countryList: null
        };
    }

    render() {
        return (
            <div className="hero-image">
                <h1>Counter</h1>

                <p>This is a simple example of a React component.</p>

                <p>Current count: <strong></strong></p>

                <button className="btn btn-primary">Increment</button>
            </div>
            );
    }
}
export default connect(
  state => state.counter,
  dispatch => bindActionCreators(actionCreators, dispatch)
)(Counter);
