## Setup application
1. Clone the repository: `git clone `.
   
   **Windows users:** If you use Docker on Windows with WSL2 enabled, make sure you clone it directly into the linux partition (e.g.,`\\wsl$\Ubuntu...`).
1. Once Docker for Windows is installed, go to the **Settings > Advanced option**, from the Docker icon in the system tray, to configure the minimum amount of memory and CPU like so:
	* **Memory: 4 GB**
	* **CPU: 2**
1. Go into the project directory
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
* **Discount Grpc -> http://host.docker.internal:8003**
* **Portainer -> http://host.docker.internal:9000**   -- admin/admin1234
* **Mongo Client -> http://host.docker.internal:3000**
* **pgAdmin PostgreSQL -> http://host.docker.internal:5050**   -- admin@aspnetrun.com/admin1234

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