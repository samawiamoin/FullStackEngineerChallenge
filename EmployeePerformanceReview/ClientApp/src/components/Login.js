import React, { Component } from 'react'
import { Redirect } from 'react-router-dom'
import { Button, Form, FormGroup, Label, Input, Fade } from 'reactstrap'
import AdminDashboard from './AdminDashboard'
import _ from 'lodash'
import EmployeeDashboard from './EmployeeDashboard';

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


export class Login extends Component {

    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            errors: []
        }
    }

    login = (event) => {

        event.preventDefault();

        const formElements = validateForm("loginForm");

        formElements.forEach(element => {
            const errors = runValidationRules(element, this.state.errors);
            this.setState({
                errors: errors
            });
        })

        const username = this.state.username;
        const password = this.state.password;
        let isadmin = false;
        const errors = this.state.errors;
        if (username === "" || username === undefined || password === "" || password === undefined) {
            alert("Enter all fields");
            return;
        }
        if (username === "admin" && password === "admin") {
            isadmin = true;
        }

        fetch('api/authenticate/login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Username: username,
                Password: password,
                IsAdmin: isadmin
            })
        }).then(response => {
            response.json().then(function (data) {
                if (data.authenticateUser.isAdmin) {
                    document.location.assign('/admin-dashboard');
                } else {
                    console.log('fetch returned ok');
                    console.log(data);
                    document.location.assign('/employee-dashboard/'+ data.id);
                }              
            });
        });
        console.log(username, password, errors);
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

    render() {
        return (
            <div className="container">
                <h3>Login as Admin</h3>
                <p> Username: admin</p>
                <p> Password: admin</p>
                <p> ---------------------------------- </p>
                <h3>Login as Employee</h3>
                <p>First add employee by log in as Admin. Then login with employee's credentials.</p>
                <p> ---------------------------------- </p>
                <Form id="loginForm" method="post" onSubmit={this.login}>
                    <FormGroup>
                        <Label for="username">Username</Label>
                        <Input
                            type="text"
                            validations={['required']}
                            name="username"
                            value={this.state.username}
                            onChange={this.handleChange}
                            id="username"
                            placeholder="Enter your username."
                        />
                        <FromValidationError field={this.state.errors.username} />
                    </FormGroup>
                    <FormGroup>
                        <Label for="password">Password</Label>
                        <Input
                            type="password"
                            validations={['required']}
                            name="password"
                            value={this.state.password}
                            onChange={this.handleChange}
                            id="password"
                            placeholder="Enter your password."
                        />
                        <FromValidationError field={this.state.errors.password} />
                    </FormGroup>
                    <Button>Login</Button>
                </Form>
            </div>
        );
    }
}

const FromValidationError = props => (
    <Fade in={Boolean(props.field)} tag="p" className="error">
        {props.field ? Object.values(props.field).shift() : ''}
    </Fade>
);