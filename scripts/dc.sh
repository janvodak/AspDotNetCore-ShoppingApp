#!/usr/bin/env bash

WITH_PORTAINER=${WITH_PORTAINER:-0}  # 0 = no, 1 = yes
WITH_ELASTIC_STACK=${WITH_ELASTIC_STACK:-0}  # 0 = no, 1 = yes
WITH_MONITORING=${WITH_MONITORING:-0}  # 0 = no, 1 = yes

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
		-f "${SCRIPT_DIR}/../docker-compose.yml"
		-f "${SCRIPT_DIR}/../docker-compose.override.yml"
	)

	# Check if an additional file with portainer should be added to run portainer
	if [[ "${WITH_PORTAINER}" == "1" ]]; then
		filenames+=(-f "${SCRIPT_DIR}/../docker-compose.portainer.yml")
	fi

	# Check if an additional file with elasticsearch should be added to run portainer
	if [[ "${WITH_ELASTIC_STACK}" == "1" ]]; then
		filenames+=(-f "${SCRIPT_DIR}/../docker-compose.elastic-stack.yml")
	fi

	# Check if an additional file with elasticsearch should be added to run portainer
	if [[ "${WITH_MONITORING}" == "1" ]]; then
		filenames+=(-f "${SCRIPT_DIR}/../docker-compose.monitoring.yml")
	fi

	echo "${filenames[@]}"
}

if [[ "${BASH_SOURCE[0]}" == "${0}" ]]; then
	set -eu -o pipefail

	_docker_compose "$@"
fi
