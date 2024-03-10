# Kubernetes Documentation

## Table of Contents

- [Introduction](#introduction)
- [What is Kubernetes?](#what-is-kubernetes)
- [Purpose and Use Cases](#purpose-and-use-cases)
  - [Purpose](#purpose)
  - [Use Cases](#use-cases)
- [General Principles](#general-principles)
  - [Declarative Configuration](#declarative-configuration)
  - [Containerization](#containerization)
- [Kubernetes Objects](#kubernetes-objects)
  - [Pods](#pods)
  - [Deployments](#deployments)
  - [Services](#services)
- [How to Use Kubernetes](#how-to-use-kubernetes)
  - [Installation](#installation)
  - [Kubectl - Kubernetes Command-Line Tool](#kubectl---kubernetes-command-line-tool)
    - [Cluster Information](#cluster-information)
    - [Basic Information](#basic-information)
    - [Pod Operations](#pod-operations)
    - [Debugging](#debugging)
    - [Imperative Commands](#imperative-commands)
    - [Declarative Commands](#declarative-commands)
  - [YAML Definitions](#yaml-definitions)
- [Security](#security)
- [Kubernetes Dashboard Documentation](#kubernetes-dashboard-documentation)
  - [Installation](#installation-1)
  - [Accessing the Dashboard](#accessing-the-dashboard)
    - [Obtain Token](#obtain-token)
    - [Proxy to the Dashboard](#proxy-to-the-dashboard)
  - [Cleanup](#cleanup)
- [Conclusion](#conclusion)

## Introduction

Welcome to the documentation on Kubernetes, a powerful open-source container orchestration platform.
As a seasoned software engineer with extensive experience in both software development and DevOps principles,
This will guide you through the fundamentals of Kubernetes and its usage.

## What is Kubernetes?

Kubernetes, often abbreviated as K8s, is a container orchestration platform designed to automate the deployment,
scaling, and management of containerized applications. It provides a robust and flexible framework for running distributed systems seamlessly.

## Purpose and Use Cases

### Purpose

Kubernetes simplifies the deployment and management of containerized applications,
allowing developers to focus on writing code without worrying about the underlying infrastructure.
It offers a consistent environment across development, testing, and production, ensuring smooth transitions and reducing potential issues.

### Use Cases

Kubernetes is particularly beneficial in the following scenarios:

1. **Container Orchestration**: Efficiently manage and scale containers.
1. **Microservices Architecture**: Deploy and manage microservices-based applications.
1. **Multi-Cloud Deploymentv: Run applications seamlessly across various cloud providers.
1. **Automatic Scaling**: Dynamically scale applications based on demand.
1. **Service Discovery and Load Balancing**: Easily discover and balance traffic between services.

## General Principles

### Declarative Configuration

Kubernetes relies on declarative configuration, where you specify the desired state of your system in YAML files. The platform then takes care of bringing the system to that state, making it easy to manage and reproduce configurations.

### Containerization

Kubernetes operates with containerized applications, usually using Docker containers. This ensures consistency and portability, making it easier to manage dependencies and isolate applications.

## Kubernetes Objects

Kubernetes defines various objects to model your application's components and its state. Let's explore some key objects:

### Pods

Pods are the smallest deployable units in Kubernetes, representing a single instance of a running process. They are suitable for deploying tightly coupled applications or a single container.

```
apiVersion: v1
kind: Pod
metadata:
  name: example-pod
spec:
  containers:
	- name: app-container
	  image: nginx:latest
```

### Deployments

Deployments provide declarative updates to applications, managing the deployment and scaling of pods.

```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: example-deployment
spec:
  replicas: 3
  selector:
	matchLabels:
	  app: example
  template:
	metadata:
	  labels:
		app: example
	spec:
	  containers:
		- name: app-container
		  image: nginx:latest
```

### Services

Services enable networking and communication between different pods, allowing them to work together as a cohesive application.

```
apiVersion: v1
kind: Service
metadata:
  name: example-service
spec:
  selector:
	app: example
  ports:
	- protocol: TCP
	  port: 80
	  targetPort: 8080
```

## How to Use Kubernetes

### Installation
To start using Kubernetes, you first need to install it. Follow the instructions for your specific environment: Kubernetes Installation Guide.

### Kubectl - Kubernetes Command-Line Tool

`kubectl` is the command-line tool for interacting with Kubernetes clusters. Here are some essential commands:

#### Cluster Information

- `kubectl cluster-info`: Display information about the cluster.
- `kubectl version`: Check the Kubernetes version.
- `kubectl get nodes`: List all nodes in the cluster.

#### Basic Information

- `kubectl get pod`: List all pods in the current namespace.
- `kubectl get service`: List all services in the current namespace.
- `kubectl get replicaset`: List all ReplicaSets in the current namespace.
- `kubectl get deployment`: List all Deployments in the current namespace.
- `kubectl get deployment [name] -o yaml`: Get detailed information about a specific Deployment.
- `kubectl get all`: List all resources in the current namespace.

#### Pod Operations

- `kubectl exec [name] -it sh`: Execute an interactive shell in a pod.
- `kubectl delete deployment [name]`: Delete a Deployment.
- `kubectl delete service [name]`: Delete a Service.
- `kubectl delete -f ./k8s/`: Delete resources defined in a directory.
- `kubectl delete -f .`: Delete resources defined in the current directory.

#### Debugging

- `kubectl get pod -o wide`: List pods with additional details.
- `kubectl describe pod [name]`: Describe the details of a pod.
- `kubectl get logs [name]`: Display the logs of a pod.

#### Imperative Commands

- `kubectl run [name] --image=[image:tag] --replicas 2`: Create a Deployment imperatively.
- `kubectl port-forward [name] 8080:80`: Forward a local port to a pod's port.
- `kubectl create deployment [name] --image=[image:tag]`: Create a Deployment declaratively.
- `kubectl edit deployment [name]`: Edit the configuration of a Deployment.
- `kubectl scale deployment <deployment-name> --replicas=<num>`: Scale a deployment to the specified number of replicas.

#### Declarative Commands

- `kubectl create -f [yaml file]`: Create resources from a YAML file.
- `kubectl apply -f [yaml file]`: Apply changes to resources defined in a YAML file.
- `kubectl apply -f [dir name] -R`: Recursively apply changes to resources in a directory.
- `kubectl delete -f [yaml file]`: Delete resources defined in a YAML file.

### YAML Definitions
All YAML definitions for your Kubernetes deployments can be found in the `./deploy/k8s/` directory. You can modify these files to suit your application's specific requirements.

## Security

To encode secrets, use [Base64Encode](https://www.base64encode.org/) for sensitive information.

## Kubernetes Dashboard Documentation

The Kubernetes Dashboard provides a web-based user interface for managing and monitoring your Kubernetes cluster.
Follow the steps below to deploy and access the Kubernetes Dashboard.

### Installation

To install the Kubernetes Dashboard, execute the following command:

```bash
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.7.0/aio/deploy/recommended.yaml
```

### Accessing the Dashboard

#### Obtain Token

To obtain the admin user token, run the following command:

```bash
kubectl -n kubernetes-dashboard create token admin-user
```
Or

```bash
kubectl -n kube-system describe secret $(kubectl -n kube-system get secret | grep admin-user | awk '{print $1}')
```

Copy the token generated, as it will be used to log in to the Kubernetes Dashboard.

#### Proxy to the Dashboard

Start a proxy to the Kubernetes API server:

```bash
kubectl proxy
```

Access the Kubernetes Dashboard using the following URL:

[http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/](http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/)

### Cleanup

To remove the admin user and associated resources, use the following commands:

```bash
kubectl -n kubernetes-dashboard delete serviceaccount admin-user
kubectl -n kubernetes-dashboard delete clusterrolebinding admin-user
```

Please note that the Kubernetes Dashboard exposes sensitive information, and access should be restricted to authorized users.
Ensure that proper RBAC configurations are in place to control access to the Dashboard in a production environment.

## Conclusion

This documentation provides a comprehensive overview of Kubernetes, covering its purpose, principles, key objects,
and how to use it effectively. Whether you're deploying microservices or managing containerized applications,
Kubernetes offers a robust solution for orchestrating your infrastructure.

Feel free to copy and paste these commands into your terminal for efficient Kubernetes management.
If you have any questions or need further assistance, refer to the official [Kubernetes Documentation](https://kubernetes.io/docs/)
for detailed information. Happy orchestrating!