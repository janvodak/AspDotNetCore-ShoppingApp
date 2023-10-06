# AspnetMicroservices

In this project, there are implemented a set of microservices that cover various e-commerce modules, including Product, Basket, Discount, and Ordering. These microservices leverage a combination of NoSQL databases (MongoDB, Redis) and Relational databases (PostgreSQL, SQL Server) for data storage.

## Communication between Microservices:

To facilitate communication between these microservices, we have adopted a RabbitMQ-based Event Driven Communication approach. This allows seamless interaction and data exchange between the different modules.

## API Gateway Integration:

Additionally, we have integrated Ocelot API Gateway into our architecture to manage and route incoming requests to the appropriate microservices, providing a unified entry point for external clients.

This combination of microservices, databases, event-driven communication, and API gateway integration forms the foundation of our e-commerce solution.

**Refer the main repository -> https://github.com/janvodak/AspnetMicroservices**

## Dependencies
You will need the following tools:

* [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
* [.Net Core 7 or later](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) 
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Setup Git
If you are working on **Windows** or Mac, **BEFORE** cloning the repository, you need to set up Git!  
If you cloned the repository first, you need to remove it and clone it again. Otherwise, you will encounter issues with line endings conversion.

For your Local Development Environment (Windows/Linux/macOS) you will need following:

* [GIT](https://git-scm.com/downloads)
	* Set correct commit author and email for all your commits (there is validation on GitLab for it)
		```
		git config --global user.name "John Doe"
		git config --global user.email "john.doe@email.com"
		```
	* On Windows and macOS set Git line endings to `LF`
		```
		git config --global core.autocrlf false
		git config --global core.eol lf
		```
	* Set Git to automatically rebase your changes when you pull
		```
		git config --global pull.rebase true
		```
* [Docker](https://www.docker.com/get-started) and [Docker Compose](https://docs.docker.com/compose/install)

## Setup application
1. Clone the repository: `git clone `.
   
   **Windows users:** If you use Docker on Windows with WSL2 enabled, make sure you clone it directly into the linux partition (e.g.,`\\wsl$\Ubuntu...`).
1. Go into the project directory
1. Once Docker for Windows is installed, go to the **Settings > Advanced option**, from the Docker icon in the system tray, to configure the minimum amount of memory and CPU like so:
	* **Memory: 4 GB**
	* **CPU: 2**
1. Run
	* With Portainer (WITH_PORTAINER=1):
		```
		WITH_PORTAINER=1 bash ./scripts/dc.sh up --detach --build
		```
		This command is used to start and manage Docker containers for a specific project or application. The WITH_PORTAINER=1 environment variable is set to enable Portainer, a container management tool, which allows for an easier and more visual way to manage Docker containers.
	* Without Portainer (WITH_PORTAINER=0 or unset):
		```
		bash ./scripts/dc.sh up --detach --build
		```
		This command is used to start and manage Docker containers for a specific project or application without using Portainer. You can use this bash script for the manipulation with `docker-compose` command in terminal.
1. You can **launch microservices** as below urls:

* **Catalog API -> http://host.docker.internal:8000/swagger/index.html**
* **Basket API -> http://host.docker.internal:8001/swagger/index.html**
* **Discount API -> http://host.docker.internal:8002/swagger/index.html**
* **Portainer -> http://host.docker.internal:9000**   -- admin/admin1234
* **pgAdmin PostgreSQL -> http://host.docker.internal:5050**   -- admin@aspnetrun.com/admin1234


## Repository Settings and Best Practices

1. Git Strategy - Streamline Workflow:
In this repository, we follow the Streamline Workflow as our Git strategy. This approach encourages a linear and simplified version control process, making it easier to manage changes and releases.
1. Signed Commits:
To maintain a high level of code integrity and traceability, all commits made to this repository must be signed. This ensures that each change is associated with a verified author.
1. Pull Requests and Code Reviews:
We adhere to a strict pull request-based development process. Every change, whether it's a bug fix, feature, or hotfix, must be submitted as a pull request (PR). Before merging, each PR undergoes a thorough code review by one or more team members to maintain code quality and catch potential issues.
1. Require Conversation Resolution Before Merging:
To promote effective communication and collaboration, we require that all conversations (comments, discussions, and feedback) within a PR must be resolved before the PR can be merged. This ensures that no important feedback or concerns are left unaddressed.
1. Require Status Checks to Pass Before Merging:
Before a PR can be merged, it must pass a set of defined status checks. These checks may include automated tests, code quality checks, and other validation processes. This ensures that changes meet the defined quality standards and do not introduce regressions.
1. Branch Management:
To keep our repository organized and maintain a clear branching strategy, we only allow specific branches:
	* **feature**: Feature branches are used for developing new features or enhancements. They are created based on the `main` branch.
	* **hotfix**: Hotfix branches are used for addressing critical issues or bugs in production. They are created based on the `main` branch.
	* **release**: Release branches are created when preparing for a new release. They are based on the `main` branch and serve as a stabilization phase before deployment.
	* **main**: The `main` branch represents the stable production-ready codebase. All changes flow into this branch through pull requests.
By following these GitHub settings and best practices, we aim to maintain a structured and collaborative development process that ensures code quality, security, and a smooth release cycle for our project.

## Using Entity Framework in Your .NET Project
Entity Framework is a powerful Object-Relational Mapping (ORM) tool for .NET that allows you to interact with your database using .NET objects. In this project, we've integrated Entity Framework to simplify database operations.

To get started with Entity Framework in your .NET project, follow these steps:

### Step 1: Install the dotnet-ef Tool (if not already installed)

* If you haven't already installed the dotnet-ef tool, you can do so globally using the following command:
	```
	dotnet tool install -g dotnet-ef
	```
* This tool is required to manage Entity Framework migrations and perform database updates.

### Step 2: Create Initial Migration

* To set up the initial database schema, we need to create a migration. Run the following command, specifying the desired migration name and context:
	```
	dotnet ef migrations add InitialCreate --context DiscountContext
	```
* Replace InitialCreate with a suitable name for your migration if needed. The DiscountContext should match the name of your DbContext class.

### Step 3: Apply the Migration to the Database

* After creating the migration, you can apply it to your database to create the necessary tables and schema:
	```
	dotnet ef database update
	```
* This command will automatically apply the pending migrations to your database.
