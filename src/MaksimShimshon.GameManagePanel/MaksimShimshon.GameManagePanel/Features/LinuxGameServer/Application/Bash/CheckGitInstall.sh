#!/usr/bin/env bash
set -euo pipefail

# Load the shared JSON‑safe pipeline
. /usr/lib/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/bash/kernel/jsonsafepipeline.sh

# -------------------------
# Your logic here
# -------------------------

if ! command -v git >/dev/null 2>&1; then
    json_fail "git is not installed or not in PATH"
fi

# If we reach this point, the wrapper will emit success JSON automatically 