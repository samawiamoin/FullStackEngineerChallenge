import React, { Component } from 'react'
import { Button, Form, FormGroup, Label, Input, Fade } from 'reactstrap'
import _ from 'lodash'
import styles from './Login.css'; 

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
export class AddEmployee extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            fullname: '',
            jobtitle: '',
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
    addEmployee = (event) => {
        event.preventDefault();
        const username = this.state.username;
        const password = this.state.password;
        const fullname = this.state.fullname;
        const jobtitle = this.state.jobtitle;

        fetch('api/employee/AddEmployee', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Username: username,
                Password: password,
                FullName: fullname,
                JobTitle: jobtitle
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
            <Form onSubmit={this.props.onSubmit}>
                <fieldset>
                    <Label for="username">Username</Label>
                    <Input
                        type="text"
                        validations={['required']}
                        name="username"
                        onChange={this.handleChange}
                        id="username"
                        placeholder="Enter your username."
                    />
                    <Label for="password">Password</Label>
                    <Input
                        type="password"
                        validations={['required']}
                        name="password"
                        onChange={this.handleChange}
                        id="password"
                        placeholder="Enter your password."
                    />
                    <Label for="fullname">Full Name</Label>
                    <Input
                        type="text"
                        name="fullname"
                        validations={['required']}
                        onChange={this.handleChange}
                        id="fullname"
                        placeholder="Enter your full name."
                    />
                    <Label for="jobtitle">Job Title</Label>
                    <Input
                        type="text"
                        name="jobtitle"
                        validations={['required']}
                        onChange={this.handleChange}
                        id="jobtitle"
                        placeholder="Enter your job title."
                    />
                </fieldset>
                <button id="button-right" className="addemp" onClick={this.addEmployee}>
                    Add
                </button>
            </Form>
        );
    }
}