# Employee Performance Review Challenge
Design a web application that allows employees to submit feedback toward each other's performance review.
Have a look at the [Challenge Requirements](https://github.com/samawiamoin/FullStackEngineerChallenge/blob/master/README.md)

## Assumptions
- Employees can see the performance review to update that are already added by admin 
- Employee can only update the performance reviews of other employees
- Employees can update more that one performance reviews of employees 
- Employees can add more than one feedback of on employee's performance review
- Admin will be assigning multiple employees to one employee
- Only admin can add/edit/delete employee
- Only admin can add performance review of each employee

## Proposed Software Stack

Below is a list of the software stack I used to create this application:

| Tech/Lib | Purpose |
| ------ | ------ |
| [ReactJS](https://reactjs.org/) | Front end client |
| [dotnetcore 2](https://github.com/dotnet/core) | Server side processing |
| [SQL Server](https://www.tutorialspoint.com/ms_sql_server/ms_sql_server_create_database.htm) | Database |


## Server API:

Under the **/authenticate** path:

- POST:

  - /login
    authenticate user and if admin return to admin dashboard
    if employee return to employee dashboard

Under the **/employee** path:

- POST:

  - /AddEmployee
    Add an employee
  - /UpdateEmployee
    Update an employee

- GET:

  - /GetEmployees
    get the list of all employees
  - /GetEmployee/{employeeId}
    get employee by id
  - /DeleteEmployee/{employeeId}
    remove the employee and all its performance reviews
    
Under the **/performancereview** path:

- POST:

  - /AddPerformanceReview
    add a performance review
  - /UpdatePerformanceReview
    update a performance review
    
- GET:

  - /GetPerformanceReviews
    get the list of all performance reviews
  - /GetPerformanceReview/{prId}
    get performance review by id
  - /GetPRsRequireFeedback/{selfEmployeeId}
  get list of performance reviews require feedback
  of all employees except the current employee
  

Under the **/feedback** path:

- POST:

  - /AddFeedback
    add a feedback on performance review
    
### Admin view

- Admin login page
- View employees
- Add employees
- View performance reviews
- Add performance reviews

### Employee view

- Employee login page
- List of performance reviews requiring feedback

## Areas of strengths

- Compiling & composing database architecture 
- Backend API development
- Frontend API calling 

## Installation and execution

- Run the visual studio solution, this will run react server itself
- Login as admin: Username: admin, Password: admin
