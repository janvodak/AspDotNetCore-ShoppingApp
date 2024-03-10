# Dockerfile Build Script Documentation

## Table of Contents

- [Overview](#overview)
- [Purpose](#purpose)
- [Usage](#usage)
- [Example](#example)
- [Command-Line Arguments](#command-line-arguments)
- [Script Workflow](#script-workflow)
- [Dockerfile.image.tag File](#dockerfileimagetag-file)
- [Notes](#notes)
- [Example Output](#example-output)

## Overview

The **dockerfile-build.sh** script is designed to streamline the process of building and pushing Docker images for a given Docker project.
It automates the image building and Docker Hub login steps, ensuring a smooth and efficient workflow for developers.
In addition to the script for working with Docker Hub, there is also a version for working with Azure Container Registry - **dockerfile-build-acr.sh**.
Basically, these are identical scripts, but they work with different providers.

## Purpose

The primary purpose of this script is to simplify the Docker image creation process for a specific project.
It abstracts away the complexities of Docker commands and login procedures,
allowing developers to focus on their code rather than the intricacies of Docker.

## Usage

To use the script, execute it from the command line with the following syntax:

- Docker Hub
	```
	bash ./scripts/dockerfile-build.sh <DOCKER_PROJECT_PATH>
	```
- Azure Container Registry
	```
	bash ./scripts/dockerfile-build-acr.sh <DOCKER_PROJECT_PATH>
	```

## Example

- Docker Hub
	```
	bash ./scripts/dockerfile-build.sh src/Services/Product/Product.API/Docker/DotNet70
	```
- Azure Container Registry
	```
	bash ./scripts/dockerfile-build-acr.sh src/Services/Product/Product.API/Docker/DotNet70
	```
## Command-Line Arguments

- **DOCKER_PROJECT_PATH**: Path to the Docker project directory containing the Dockerfile and related configuration.

## Script Workflow

1. **Docker Login Check**: The script checks if the user is already logged in to Docker Hub / Azure / Azure Container Registry.
If not, it prompts the user to log in.
1. **Docker Image Tag Retrieval**: The script retrieves the Docker image tag from the specified Dockerfile.image.tag file
within the Docker project directory.
1. **Docker Image Build**: The script initiates the Docker image build process using the provided context, Dockerfile,
and tag. It also supports an option to skip the build cache if desired.
1. **Docker Image Push**: Once the image is built successfully, the script pushes the Docker image to Docker Hub.

## Dockerfile.image.tag File

- The `Dockerfile.image.tag` file in the Docker project directory contains the desired tag for the Docker Hub image.
Ensure this file exists and contains a valid image tag.
- The `Dockerfile.image.acr.tag` file in the Docker project directory contains the desired tag for the Azure Container Registry image.
Ensure this file exists and contains a valid image tag.

## Notes

- Make sure to create the Dockerfile within the specified Docker project directory.
- The script automatically handles Docker Hub login, so you don't have to worry about authentication during the build process.

## Example Output

```
Building Docker image ...
  from file /path/to/project/src/Services/Product/Product.API/Docker/DotNet70/Dockerfile
  with directory context of /path/to/project
  and tagging it as <IMAGE_TAG>

Successfully built and pushed Docker image to Docker Hub.
```