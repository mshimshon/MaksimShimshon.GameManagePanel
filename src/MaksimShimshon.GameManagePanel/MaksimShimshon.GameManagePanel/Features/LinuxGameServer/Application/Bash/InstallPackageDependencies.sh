#!/bin/bash
set -euo pipefail

if [ $# -eq 0 ]; then
	echo '{"completed":false,"failure_message":"No packages provided"}'
	exit 1
fi

PACKAGES=("$@")
ROLLBACK_LIST=""
FAILURE_MESSAGE=""

rollback() {
	if [ -n "$ROLLBACK_LIST" ]; then
		apt-get remove -y $ROLLBACK_LIST >/dev/null 2>&1 || true
	fi
	echo "{\"completed\":false,\"failure_message\":\"$FAILURE_MESSAGE\"}"
	exit 1
}

trap 'FAILURE_MESSAGE="Package installation failed"; rollback' ERR

apt-get update -y >/dev/null 2>&1

for pkg in "${PACKAGES[@]}"; do
	if ! dpkg -s "$pkg" >/dev/null 2>&1; then
		apt-get install -y "$pkg" >/dev/null 2>&1
		ROLLBACK_LIST="$ROLLBACK_LIST $pkg"
	fi
done

echo '{"completed":true,"failure_message":""}'
