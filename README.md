# Learn'It (E-Learning Web Application)


## Description

Learn'It is an E-Learning Web Application built using ASP.NET API, Angular, SQL Server, and Cloudinary. It's an online learning platform which allows students and instructors to engage in a rich educational experience.



## Main Features

- Course Upload:
Instructors can easily upload and manage courses. Video lectures are stored securely using **Cloudinary**.

- Payment Features:
The application supports secure payment processing for course purchases via **Stripe**.

- Certifications:
Users can earn **certificates** upon completing courses.

- Course Rating and Review: 
Students can rate and review courses, helping others make informed decisions about their learning paths.

- Course Structure:
Courses are well-organized into modules and lessons.

- Search and Discovery:
Users can search for courses by categories, course name, and prices, making it easy to find the right course to meet their needs.

- Login/Signup:
Secure authentication for both students and instructors.


## Technologies Used

- **Backend:** ASP.NET API
- **Frontend:** Angular
- **Database:** SQL Server
- **Media Storage:** Cloudinary
- **Payment Processing:** Stripe

## Getting Started



### Installation

1. **Clone the repository:**

    ```bash
    git clone https://github.com/nevein25/e-learning
    cd e-learning
    ```

2. **Navigate to the `src` directory:**

    ```bash
    cd src
    ```

3. **Set up the .NET backend:**

    - Navigate to the `e-learning.API` directory:

      ```bash
      cd e-learning.API
      ```

    - Restore .NET dependencies:

      ```bash
      dotnet restore
      ```

    - Update the `appsettings.json` file with your database connection string and other configurations (e.g., SQL Server, Cloudinary settings, Stripe settings).

4. **Set up the Angular frontend:**

    - Navigate to the `client` directory:

      ```bash
      cd ../client
      ```

    - Install Angular dependencies:

      ```bash
      npm install
      ```

### Running the Application

1. **Run the .NET backend:**

    - Navigate to the `e-learning.API` directory:

      ```bash
      cd ../e-learning.API
      ```


    - Run the application:

      ```bash
      dotnet run
      ```

3. **Run the Angular frontend:**

    - Navigate to the `client` directory:

      ```bash
      cd ../client
      ```

    - Build and serve the Angular application:

      ```bash
      npm start
      ```

    The Angular application will be available at `http://localhost:4200` by default.

