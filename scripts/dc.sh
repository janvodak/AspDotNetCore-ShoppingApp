#!/usr/bin/env bash

WITH_PORTAINER=${WITH_PORTAINER:-0}  # 0 = no, 1 = yes

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

	# Check if an additional file should be added to run portainer
    if [[ "${WITH_PORTAINER}" == "1" ]]; then
        filenames+=(-f "${SCRIPT_DIR}/../src/docker-compose.portainer.yml")
    fi

	echo "${filenames[@]}"
}

if [[ "${BASH_SOURCE[0]}" == "${0}" ]]; then
	set -eu -o pipefail

	_docker_compose "$@"
fi
