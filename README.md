**# PostalCodeAPI - API Development using AWS Lambda and .NET Core**

**PostalCodeAPI** is a project that utilizes the Postcode and Geolocation API for the UK to provide a user-friendly interface for querying and managing postal codes in the UK.  

**https://postalcodeweb.s3.amazonaws.com/index.html**

**https://g8w4x6soxi.execute-api.us-east-1.amazonaws.com/Prod/LookupPostcode/ig27nh**

**https://g8w4x6soxi.execute-api.us-east-1.amazonaws.com/Prod/autocomplete/ig27n**

The project is divided into several components:

**PostalCode.API:** This is the main project and contains the core functionality for querying and managing postal codes. It is responsible for handling incoming requests, querying the database, and returning responses.

**PostalCode.API.Model:** This project contains the data models used by the PostalCode.API project. It defines the structure of the data returned by the API and provides a consistent interface for interacting with the database.

**PostalCode.API.Service:** This project contains the business logic for the PostalCode.API project. It is responsible for validating incoming requests, performing any necessary transformations on the data, and interacting with the data access layer.

**PostalCode.API.Test:** This project contains the unit tests for the PostalCode.API project. It ensures that the project is functioning as intended and that any changes to the codebase do not introduce unexpected behavior.

The PostalCode and Geolocation API for the UK provides a comprehensive dataset of all postal codes in the UK, along with their latitude and longitude coordinates. The API can be used to query specific postal codes, as well as to retrieve all postal codes within a certain radius of a given location.

To use the PostalCodeAPI, simply send a request to the appropriate endpoint with the desired parameters. The API will return a JSON response containing the requested data. Detailed documentation for the API can be found in the README file.

**Deployment Document Please refere deployment document. The deployment document provides step-by-step instructions to create build artifacts and deploy them for the PostalCodeAPI and PostalCodeWeb applications. Deployment Instruction Document.docx**

**CloudFormation Stack for Postal Code API**

This CloudFormation stack creates an AWS Lambda function that uses an ASP.NET Core application to handle API Gateway proxy requests for the PostalCodeAPI. The stack deploys the Lambda function to the US East (N. Virginia) region and stores the code in an S3 bucket with prefix "PostalCodeAPI/".

**Configuration Parameters**
The stack takes the following configuration parameters:

**profile**: The AWS CLI profile to use for deploying the stack. Default: "default".

**region**: The AWS region in which to deploy the stack. Default: "us-east-1".

**configuration**: The build configuration to use for the ASP.NET Core application. Default: "Release".

**runtime**: The .NET Core runtime version to use for the Lambda function. Default: "dotnetcore3.1".

**s3-bucket:** The name of the S3 bucket to store the Lambda function code. Required.

**s3-prefix:** The prefix to use for S3 objects created by the stack. Default: "PostalCodeAPI/".

**stack-name:** The name of the CloudFormation stack. Required.

**template:** The filename of the CloudFormation template to use. Default: "serverless.template".


**Resources Created**
The stack creates the following resources:

**Lambda Function:** An AWS Lambda function that uses the API Gateway proxy integration to handle HTTP requests. The function is created from a .NET Core 3.1 application that uses the Amazon.Lambda.AspNetCoreServer NuGet package to host an ASP.NET Core application.

**API Gateway Rest API:** An Amazon API Gateway REST API that is used to expose the Lambda function to the public/private through ip range.
 
**Deployment Instructions**
To deploy the CloudFormation stack:
Install the AWS CLI and configure it with an access key and secret key that has permissions to create CloudFormation stacks and S3 buckets.

Clone the Postcode Lookup API project and navigate to the AWS/CloudFormation directory.
**https://github.com/maringanti1/PostalCodeAPI.git**

Modify the aws-lambda-tools-defaults.json file to specify the S3 bucket name and stack name.

After the stack creation completes, note the API Gateway endpoint URL in the stack outputs. We can use this URL to access the Postcode lookup API.

**https://postalcodeweb.s3.amazonaws.com/index.html**

**https://g8w4x6soxi.execute-api.us-east-1.amazonaws.com/Prod/autocomplete/ig27n**

**Implemented Features**
**Base Controller**
A base controller has been implemented to reduce code duplication and simplify the implementation of common functionality across multiple controllers. This has improved the maintainability of the codebase and made it easier to add new endpoints.

**Limiting the API Result**
The API result has been limited with a configurable value. This has made the API more efficient and scalable.

**Interface-driven Approach**
Interfaces have been added to the API to make it easier to test and maintain. This has improved code reusability and modularity.

**Dependency Injection**
Dependency injection has been implemented to remove coupling between services and controllers. This has made it easier to unit test individual API methods.

**Middleware Integration**
Middleware has been integrated with the base controller to add common functionality such as logging and exception handling. This has improved the maintainability of the codebase and made it easier to add new middleware.

**Custom Exception Handler**
A custom exception handler has been implemented to provide a consistent error response format across all APIs. This has improved the user experience by providing clear and informative error messages.

**Mocking Framework**
Mock objects have been created to simulate behavior in a controlled way for unit testing. This has improved the reliability and accuracy of the tests.

**API Response Naming Policy**
The API response has been handled with naming policy to maintain the response format. This has made it easier to add new endpoints and maintain the consistency of the API response format.

**Service and Repository Pattern**
The service and repository pattern has been implemented to improve code reusability, maintainability, and scalability. This has helped to separate concerns and make the code easier to test and maintain.

**Lambda Function**
The API has been published through a Lambda function. This has improved the scalability and availability of the API.

**AWS S3 Bucket**
The code has been pushed to an AWS S3 bucket. This has made it easier to deploy and manage the codebase.

**Deployment Document**
Please refere deployment document. The deployment document provides step-by-step instructions to create build artifacts and deploy them for the PostalCodeAPI and PostalCodeWeb applications.
**Deployment Instruction Document.docx**

**Credits**
Postcode Lookup project is created by Laxminarsimha Maringanti.
