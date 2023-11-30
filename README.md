# Backend Developer Assessment

## Recommended IDE Setup
This project provides a secure User Authentication System and Task Management Endpoints using a .NET-based API.

## Getting Started

### Prerequisites

Ensure you have the [6.0 .NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed.


## Getting Started

### Prerequisites

Ensure you have the following software installed:

- [.NET SDK](https://dotnet.microsoft.com/download)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/kervin00/backend-assessment.git

2. Navigate to the project folder:

```bash
cd your-project
```

3. Restore dependencies:

```bash
dotnet restore
```
4. Build the project:

```bash
dotnet build

```

Configuration
1. Open the appsettings.json file and configure the database connection string and any other necessary settings.

```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "your_database_connection_string"
  },
  // Other settings...
}
```

Usage
Run the project:

```bash
dotnet run
```



### API Endpoints
## User Authentication
## POST /api/auth/register

Description: Register a new user.
Request: JSON object with user data.
Response: JSON object of the registered user.


### POST /api/auth/login

Description: Authenticate and generate a JWT token.
Request: JSON object with user credentials.
Response: JSON object with a JWT token.


### POST /api/auth/logout

Description: Invalidate the user's JWT token.
Request: Authorization header with Bearer token.
Response: Success message.

### Task Management
### GET /api/tasks

Description: Retrieve a list of tasks.
Request: None
Response: JSON array of tasks.

### POST /api/tasks

Description: Create a new task.
Request: JSON object with task data.
Response: JSON object of the created task.

### PUT /api/tasks/{id}

Description: Update an existing task.
Request: JSON object with updated task data.
Response: JSON object of the updated task.

### DELETE /api/tasks/{id}

Description: Delete a task by ID.
Request: None
Response: Success message.

### Authentication
User authentication is handled using JWT tokens. Include the token in the Authorization header as a Bearer token for secured endpoints.
