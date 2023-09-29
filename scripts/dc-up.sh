#!/usr/bin/env bash

_eval() {
	local command=("$@")
	echo ">>> ${command[*]}"

	"${command[@]}"
}

_docker_compose() {
	local dc_filenames
	read -ra dc_filenames < <(_get_docker_compose_file_names)
	_eval docker-compose "${dc_filenames[@]}" "$@"
}

_get_docker_compose_file_names() {
	readonly SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

	local filenames=(
		-f "${SCRIPT_DIR}/../src/docker-compose.yml"
		-f "${SCRIPT_DIR}/../src/docker-compose.override.yml"
	)

	echo "${filenames[@]}"
}

if [[ "${BASH_SOURCE[0]}" == "${0}" ]]; then
	set -eu -o pipefail

	_docker_compose "$@"
fi
