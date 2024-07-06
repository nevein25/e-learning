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




## Getting Started

To get started with Learn'It, follow these steps:

1. **Clone the Repository:**

    ```bash
    git clone https://github.com/nevein25/e-learning
    ```

2. **Backend Setup:**

    - Navigate to the API project folder and restore the necessary packages:
    
        ```bash
        cd API
        dotnet restore
        ```

    - Update the `appsettings.json` file with your SQL Server, Stripe and Cloudinary credentials.
    
    - Run the API:
    
        ```bash
        dotnet run
        ```

3. **Frontend Setup:**

    - Navigate to the Angular project folder:
    
        ```bash
        cd client
        npm install
        ```

    - Update the environment configuration files with your API endpoints.
    
    - Run the Angular application:
    
        ```bash
        ng serve
        ```


## Technologies Used

- **Backend:** ASP.NET API
- **Frontend:** Angular
- **Database:** SQL Server
- **Media Storage:** Cloudinary
- **Payment Processing:** Stripe



