#!/usr/bin/env bash

# jsonwrap.sh
# Provides JSON-safe stdout isolation and helper functions.

# Redirect stdout → stderr, preserve original stdout in FD3
exec 3>&1 1>&2

# Neutralize inherited functions
unset -f exit || true
unset -f source || true
unset -f . || true
IFS=$' \t\n'

# State
__JSON_COMPLETED=true
__JSON_FAILURE_MESSAGE=""

json_fail() {
    __JSON_COMPLETED=false
    __JSON_FAILURE_MESSAGE="$1"
    exit 1
}

json_emit_failure() {
    local msg="$1"
    msg="${msg//\\/\\\\}"
    msg="${msg//\"/\\\"}"
    msg="${msg//$'\n'/\\n}"
    msg="${msg//$'\r'/\\r}"
    printf '{"completed":false,"failure_message":"%s"}\n' "$msg" >&3
}

json_emit_success() {
    printf '{"completed":true,"failure_message":""}\n' >&3
}

json_cleanup() {
    if [ "$__JSON_COMPLETED" = true ]; then
        json_emit_success
    else
        json_emit_failure "$__JSON_FAILURE_MESSAGE"
    fi
}

# Install traps
trap json_cleanup EXIT
trap 'json_fail "Script failed"' ERR