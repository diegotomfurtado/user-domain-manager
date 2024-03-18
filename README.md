# User Domain Management Application

Welcome to User Domain Management Application! This README provides an overview of the application, its features, and instructions on how to set it up and use it.

## Requirement from the Code Challange
## User Story

As a user, I want an application created in .NET 6 that allows me to manage user objects via a Web API. The application should store users in memory and provide functionalities such as adding, deleting, updating users, and filtering users based on certain criteria. Additionally, the application should follow Microsoft's code conventions, adhere to SOLID principles, and integrate Swashbuckle Swagger UI for API documentation.

## Features

- **Add User**: Add a new user to the storage.
- **Delete User**: Remove a user from the storage.
- **Update User**: Modify user information in the storage.
- **Get Users**: Retrieve a list of users with filters from the storage.
- **Persistence**: Business logic ensures that user modifications are stored to disk.
- **Validations**: Validate user input according to specified rules:
  - First and last names must not contain numbers.
  - Email address format must be valid.
  - Email addresses must be unique.
- **Bonus Task**:
  - **Unit Testing**: Unit tests are provided to ensure the reliability of core methods.
  - **SQLite Database**: Data is stored in an SQLite database instead of memory.

## User Object

A user object consists of the following properties:
- First Name
- Last Name
- Email Address
- Notes
- Creation Time

## Technologies Used

- .NET 6
- Swashbuckle Swagger UI
- SQLite (for bonus task)

## What I did different
- I've worked with SQL Server, from Azure, insted
- I couldn't use all SOLID principal

## Setup Instructions

1. Clone this repository to your local machine.
2. Open the project in your preferred IDE (Integrated Development Environment).
3. Install .NET 6 SDK if not already installed.
4. Build the solution to restore NuGet packages and compile the code.
5. Run the application.
6. Access the Swagger UI documentation to explore and interact with the API endpoints.

## API Endpoints

- **POST /api/user**: Add a new user.
- **DELETE /api/user/{userCode}**: Delete a user by UserCode.
- **PUT /api/user/{userCode}**: Update user information by UserCode.
- **GET /api/user/{userCode}**: Retrieve a single user..
- **GET /api/user/**: Retrieve a list of users with optional filter parameters.

## Contributing

If you'd like to contribute to this project, please follow these steps:
1. Fork the repository.
2. Create your feature branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Open a pull request.

## License

This project is licensed under the [MIT License](LICENSE).

---

Thank you for considering our User Management Application! If you have any questions or feedback, feel free to reach out. Happy managing users! 🚀
