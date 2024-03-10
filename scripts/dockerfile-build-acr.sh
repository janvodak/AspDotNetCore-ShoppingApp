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
	local docker_image_tag_file_path="${docker_project_dir}/Dockerfile.image.acr.tag"

	if [[ ! -f "${docker_image_tag_file_path}" ]]; then
		_stderr "Could not find tag file '${docker_image_tag_file_path}'. Please, create it."
		exit 1
	fi

	head -n1 "${docker_image_tag_file_path}"
}

_check_az_login() {
	if az account show &>/dev/null; then
		_stderr "Already logged in to Azure."
	else
		_stderr "Not logged in to Azure. Logging in..."
		_az_login
	fi
}

_check_acr_login() {
	if az acr show --name "${ACR_NAME}" &>/dev/null; then
		_stderr "Already logged in to Azure Container Registry."
	else
		_stderr "Not logged in to Azure Container Registry. Logging in..."
		_acr_login
	fi
}

_az_login() {
	az login
}

_acr_login() {
	az acr login \
		--name "${ACR_NAME}"
}

_build_docker_image() {
	local context="$1"
	local dockerfile="$2"
	local tag="$3"
	local use_build_cache="$4"
	local build_cache_flag=()

	_check_az_login
	_check_acr_login

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

	readonly ACR_NAME="acrshopappallplandev01"

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
