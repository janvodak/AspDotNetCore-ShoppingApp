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
