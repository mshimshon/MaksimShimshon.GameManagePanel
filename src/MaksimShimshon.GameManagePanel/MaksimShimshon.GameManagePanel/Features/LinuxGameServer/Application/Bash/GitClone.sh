#!/usr/bin/env bash

set -euo pipefail

GIT_URL="${1:-}"
TARGET_DIR="${2:-}"

json_success() {
    printf '{ "Completed": true }\n'
}

json_failure() {
    local msg="$1"
    msg="${msg//\"/\\\"}"
    printf '{ "Completed": false, "failure_message": "%s" }\n' "$msg"
}

# --- Validation ---
if [[ -z "$GIT_URL" ]]; then
    json_failure "Missing required argument: git URL"
    exit 1
fi

# --- Clear target directory if provided ---
if [[ -n "$TARGET_DIR" ]]; then
    # Prevent catastrophic mistakes like rm -rf /
    if [[ "$TARGET_DIR" == "/" || "$TARGET_DIR" == "." ]]; then
        json_failure "Refusing to clear unsafe target directory: $TARGET_DIR"
        exit 1
    fi

    if [[ -d "$TARGET_DIR" ]]; then
        if ! rm -rf "$TARGET_DIR" 2>clear_error.log; then
            json_failure "Failed to clear target directory: $(cat clear_error.log)"
            exit 1
        fi
    fi
fi

# --- Clone operation ---
if [[ -z "$TARGET_DIR" ]]; then
    if ! git clone "$GIT_URL" 2>clone_error.log; then
        json_failure "$(cat clone_error.log)"
        exit 1
    fi
else
    if ! git clone "$GIT_URL" "$TARGET_DIR" 2>clone_error.log; then
        json_failure "$(cat clone_error.log)"
        exit 1
    fi
fi

json_success