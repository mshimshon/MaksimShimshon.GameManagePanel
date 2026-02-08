#!/usr/bin/env bash

# Write content to a temporary file and atomically replace the target.
# Usage: write_file <path> <content>

write_file() {
    local filepath="$1"
    local content="$2"
    local tmpfile="${filepath}.tmp"

    # Ensure directory exists
    mkdir -p "$(dirname "$filepath")"

    # Write to temporary file
    printf "%s" "$content" > "$tmpfile"

    # Atomic replace
    mv -f "$tmpfile" "$filepath"
}
