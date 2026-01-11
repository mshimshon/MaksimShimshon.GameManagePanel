#!/bin/bash
set -euo pipefail

COMPLETED=true
FAILURE_MESSAGE=""

cleanup() {
    if [ "$COMPLETED" = true ]; then
        echo '{"completed":true,"failure_message":""}'
    else
        echo "{\"completed\":false,\"failure_message\":\"$FAILURE_MESSAGE\"}"
    fi
}

fail() {
    COMPLETED=false
    FAILURE_MESSAGE="$1"
    exit 1
}

trap cleanup EXIT
trap 'fail "Locale configuration failed"' ERR

sed -i 's/^# *en_US.UTF-8 UTF-8/en_US.UTF-8 UTF-8/' /etc/locale.gen
locale-gen en_US.UTF-8
echo 'LANG=en_US.UTF-8' > /etc/default/locale
export LANG=en_US.UTF-8
export LC_ALL=en_US.UTF-8
