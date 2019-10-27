import React, { Component } from 'react'
import { Button, Form, FormGroup, Label, Input, Fade } from 'reactstrap'
import styles from './Login.css'; 

export default class AdminDashboard extends Component {

    constructor(props) {
        super(props);
        this.state = { employees: [], performReviews: [], loading: true };
        
    }

    componentDidMount() {
        fetch('api/employee/GetEmployees')
            .then(response => response.json())
            .then(data => {
                this.setState({ employees: data, loading: false });
            });

        fetch('api/performancereview/GetPerformanceReviews')
            .then(response => response.json())
            .then(data => {
                this.setState({ performReviews: data, loading: false });
            });
    }

    addEmp = (event) => {
        document.location.assign('/add-employee');
    }

    addPerformRev = (event) => {
        document.location.assign('/add-performancereview');
    }

    static renderEmployeesTable(employees) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Full Name</th>
                        <th>Job Title</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map(employee =>
                        <tr key={employee.id}>
                            <td>{employee.id}</td>
                            <td>{employee.fullName}</td>
                            <td>{employee.jobTitle}</td>
                            <td>Edit</td>
                            <td>Delete</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    static renderPerformanceReviewsTable(performReviews) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Emp Id</th>
                        <th>Communication</th>
                        <th>Technical</th>
                        <th>TimeManagement</th>
                        <th>Leadership</th>
                        <th>Ownership</th>
                        <th>OverallPerformance</th>
                        <th>RequireFeedback</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {performReviews.map(performReview =>
                        <tr key={performReview.id}>
                            <td>{performReview.employeeId}</td>
                            <td>{performReview.communication}</td>
                            <td>{performReview.technical}</td>
                            <td>{performReview.timeManagement}</td>
                            <td>{performReview.leadership}</td>
                            <td>{performReview.ownership}</td>
                            <td>{performReview.overallPerformance}</td>
                            <td>{performReview.requireFeedback}</td>
                            <td>Edit</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
            let contents = this.state.loading
                ? <p><em>Loading...</em></p>
            : AdminDashboard.renderEmployeesTable(this.state.employees);

        let performanceReviews = this.state.loading
            ? <p><em>Loading...</em></p>
            : AdminDashboard.renderPerformanceReviewsTable(this.state.performReviews);

        return (
            <div>
                <h1>Employees</h1><button className="empassignbutton">Assign Emlployees</button><button className="empbutton" onClick={this.addEmp}>Add Employee </button>
                {contents}
                <h1>Performance Reviews</h1>
                <button className="empbutton" onClick={this.addPerformRev}>Add Performance Review </button>
                {performanceReviews}
            </div>
        );
    }
}