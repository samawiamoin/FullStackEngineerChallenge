import React, { Component } from 'react'
import { Button, Form, FormGroup, Label, Input, Fade } from 'reactstrap'
import _ from 'lodash'
import styles from './Login.css';
//import Select from "react-dropdown-select";

const validationMethods = {
    required: (field, value) => {
        if (!value.toString().trim().length) {
            return `This ${field} field is required.`
        }
    }
}

const validateForm = (form) => {
    const loginForm = document.getElementById(form)
    return loginForm.querySelectorAll('[validations]');
}

const runValidationRules = (element, errors) => {
    const target = element;
    const field = target.name;
    const value = target.value
    let validations = element.getAttribute('validations');
    validations = validations.split(',')

    for (let validation of validations) {
        validation = validation.split(':');
        const rule = validation[0];
        const error = validationMethods[rule](field, value);
        errors[field] = errors[field] || {};
        if (error) {
            errors[field][rule] = error;
        } else {
            if (_.isEmpty(errors[field])) {
                delete errors[field];
            } else {
                delete errors[field][rule];
            }
        }
    }

    return errors;
}
export class AddPerformanceReview extends Component {
    constructor(props) {
        super(props);
        this.state = {
            communication: '',
            timemanagement: '',
            leadership: '',
            ownership: '',
            overallPerformance: '',
            employees: [],
            selectedEmpId: '',
            errors: []
        }
    }

    handleChange = (event) => {
        const target = event.target;
        const field = target.name;
        const value = target.value

        const errors = runValidationRules(target, this.state.errors);

        this.setState({
            errors: errors
        });

        this.setState({
            [field]: value
        });
    }

    componentDidMount() {
        fetch('api/employee/GetEmployees')
            .then(response => response.json())
            .then(data => {
                this.setState({ employees: data, loading: false });
            })
            .catch(error => console.log(error.response));
    }

    addPerforamnceReview = (event) => {
        event.preventDefault();

        const communication = this.state.communication;
        const timemanagement = this.state.timemanagement;
        const technical = this.state.technical;
        const leadership = this.state.leadership;
        const ownership = this.state.ownership;
        const overallPerformance = this.state.overallPerformance;

        fetch('api/performancereview/AddPerformanceReview', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                EmployeeId: this.state.selectedEmpId,
                Communication: communication,
                Technical: technical,
                TimeManagement: timemanagement,
                Leadership: leadership,
                Ownership: ownership,
                OverallPerformance: overallPerformance
            })
        }).then(response => {
            response.json().then(function (data) {
                if (data > 0) {
                    document.location.assign('/admin-dashboard');
                    //this.props.history.push('/admin-dashboard');
                    //<AdminDashboard employeeId={data.id} />
                }
            });
        });
    }
    render() {
        return (  
            <div className="container">
                <Label> Select Employee </Label>

                <select name="selectedEmpId" onChange={event => this.setState({ selectedEmpId: event.target.value })}>
                    {this.state.employees.map(emp => {
                        return <option key={emp.id} value={emp.id}>{emp.fullName}</option>;
                    })}
                </select>
            <Form onSubmit={this.props.onSubmit}>
                <fieldset>
                    <Label for="communication">Communication</Label>
                    <Input
                        type="text"
                        validations={['required']}
                        name="communication"
                        onChange={this.handleChange}
                        id="communication"
                        placeholder="Enter Communication."
                    />
                    <Label for="technical">Technical</Label>
                    <Input
                        type="technical"
                        validations={['required']}
                        name="technical"
                        onChange={this.handleChange}
                        id="technical"
                        placeholder="Enter Technical."
                    />
                    <Label for="timemanagement">Time Management</Label>
                    <Input
                        type="timemanagement"
                        validations={['required']}
                        name="timemanagement"
                        onChange={this.handleChange}
                        id="timemanagement"
                        placeholder="Enter Time Management."
                    />
                    <Label for="leadership">Leadership</Label>
                    <Input
                        type="text"
                        name="leadership"
                        validations={['required']}
                        onChange={this.handleChange}
                        id="leadership"
                        placeholder="Enter Leadership."
                    />
                    <Label for="ownership">Ownership</Label>
                    <Input
                        type="text"
                        name="ownership"
                        validations={['required']}
                        onChange={this.handleChange}
                        id="ownership"
                        placeholder="Enter Ownership."
                    />
                    <Label for="overallPerformance">Overall Performance</Label>
                    <Input
                        type="text"
                        name="overallPerformance"
                        validations={['required']}
                        onChange={this.handleChange}
                        id="overallPerformance"
                        placeholder="Enter Overall Performance."
                    />
                </fieldset>
                <button id="button-right" className="addemp" onClick={this.addPerforamnceReview}>
                    Add
                </button>
                </Form>
            </div>
        );
    }
}