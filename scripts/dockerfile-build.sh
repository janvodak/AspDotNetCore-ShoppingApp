#!/usr/bin/env bash

_stderr() {
	echo "$@" >&2
}

_eval() {
	_stderr "> $*"

	if ! "$@"; then
		_eval "$@"
	fi
}

_get_image_tag() {
	local docker_project_dir="$1"
	local docker_image_tag_file_path="${docker_project_dir}/Dockerfile.image.tag"

	if [[ ! -f "${docker_image_tag_file_path}" ]]; then
		_stderr "Could not find tag file '${docker_image_tag_file_path}'. Please, create it."
		exit 1
	fi

	head -n1 "${docker_image_tag_file_path}"
}

# Function to check Docker Hub login
_check_docker_login() {
	# Check if the user is logged in to Docker Hub
	if docker info | grep -q "Username:"; then
		_stderr "Already logged in to Docker Hub."
	else
		_stderr "Not logged in to Docker Hub. Logging in..."
		_docker_login
	fi
}

# Function to perform Docker Hub login
_docker_login() {
	# Prompt for Docker Hub credentials
	read -p "Enter Docker Hub username: " username
	read -sp "Enter Docker Hub password: " password

	# Log in to Docker Hub
	echo "$password" | docker login --username "$username" --password-stdin
	unset password

	# Check if login was successful
	if [ $? -eq 0 ]; then
		_stderr "Login successful."
	else
		_stderr "Login failed. Please check your credentials and try again."
		exit 1
	fi
}

_build_docker_image() {
	local context="$1"
	local dockerfile="$2"
	local tag="$3"
	local use_build_cache="$4"
	local build_cache_flag=()

	_check_docker_login

	if [[ "${use_build_cache}" == "${TEXT_NO}" ]]; then
		build_cache_flag=(--no-cache)
	fi

	_eval \
		docker build "${context}" \
			"${build_cache_flag[@]}" \
			--file "${dockerfile}" \
			--tag "${tag}"
}

if [[ "${BASH_SOURCE[0]}" == "${0}" ]]; then
	set -eu -o pipefail

	readonly DOCKER_PROJECT="$1"
	
	readonly SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
	readonly PROJECT_DIR="${SCRIPT_DIR}/.."
	readonly DOCKER_CONTEXT="${PROJECT_DIR}"
	readonly DOCKER_IMAGE_FILE="${PROJECT_DIR}/${DOCKER_PROJECT}/Dockerfile"
	readonly TEXT_NO="NO"

	declare FULL_IMAGE_TAG
	
	FULL_IMAGE_TAG="$(_get_image_tag "${PROJECT_DIR}/${DOCKER_PROJECT}")"

	if [[ ! -f "${DOCKER_IMAGE_FILE}" ]]; then
		_stderr "Could not find Dockerfile '${DOCKER_IMAGE_FILE}'. Please, create it."
		exit 1
	fi

	_stderr
	_stderr "Building Docker image ..."
	_stderr "  from file ${DOCKER_IMAGE_FILE}"
	_stderr "  with directory context of ${DOCKER_CONTEXT}"
	_stderr "  and tagging it as ${FULL_IMAGE_TAG}"

	_build_docker_image \
		"${DOCKER_CONTEXT}" \
		"${DOCKER_IMAGE_FILE}" \
		"${FULL_IMAGE_TAG}" \
		"${TEXT_NO}"

	_eval docker push "${FULL_IMAGE_TAG}"
fi
