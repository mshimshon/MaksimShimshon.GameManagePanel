#!/usr/bin/env bash
set -euo pipefail

ERROR_MESSAGE=""

emit_json() {
    local msg="$1"
    msg="${msg//\\/\\\\}"
    msg="${msg//\"/\\\"}"
    msg="${msg//$'\n'/\\n}"
    msg="${msg//$'\r'/\\r}"
    printf '{ "Completed": false, "failure_message": "%s" }\n' "$msg"
}

emit_success() {
    printf '{ "Completed": true }\n'
}

fail() {
    ERROR_MESSAGE="$1"
    exit 1
}

trap 'status=$?; if [[ $status -ne 0 ]]; then emit_json "${ERROR_MESSAGE:-Unexpected failure}"; fi' EXIT

GIT_URL="${1:-}"
TARGET_DIR="${2:-}"

[[ -z "$GIT_URL" ]] && fail "Missing required argument: git URL"

TMP_ERR="$(mktemp)"
cleanup() { rm -f "$TMP_ERR"; }
trap cleanup EXIT

if [[ -n "$TARGET_DIR" ]]; then
    CANON="$(realpath -m "$TARGET_DIR")" || fail "Failed to resolve target directory path"
    [[ "$CANON" == "/" ]] && fail "Refusing to clear unsafe target directory: $TARGET_DIR"

    if [[ -d "$CANON" ]]; then
        rm -rf "$CANON" 2>"$TMP_ERR" || fail "Failed to clear target directory: $(<"$TMP_ERR")"
    fi
fi

if [[ -z "$TARGET_DIR" ]]; then
    git clone "$GIT_URL" >/dev/null 2>"$TMP_ERR" || fail "$(cat "$TMP_ERR")"
else
    git clone "$GIT_URL" "$TARGET_DIR" >/dev/null 2>"$TMP_ERR" || fail "$(cat "$TMP_ERR")"
fi

trap - EXIT
cleanup
emit_success
