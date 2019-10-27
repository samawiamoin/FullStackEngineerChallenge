import React, { Component } from 'react'
import { Button, Form, FormGroup, Label, Input, Fade } from 'reactstrap'
import styles from './Login.css';

export default class EmployeeDashboard extends Component {

    constructor(props) {
        super(props);
        this.state = { performReviews: [], loading: true };

    }

    componentDidMount() {
        fetch('api/performancereview/GetPRsRequireFeedback/' + this.props.match.params.id)
            .then(response => response.json())
            .then(data => {
                this.setState({ performReviews: data, loading: false });
            });
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
                            <td><button>Give Feedback </button></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let performanceReviews = this.state.loading
            ? <p><em>Loading...</em></p>
            : EmployeeDashboard.renderPerformanceReviewsTable(this.state.performReviews);

        return (
            <div>
                <h1>Performance Reviews Require Feedback</h1>
                {performanceReviews}
            </div>
        );
    }
}

//EmployeeDashboard.propTypes = {
//    state: this.props.state,
//    dispatch: this.props.state,
//}