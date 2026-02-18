#!/usr/bin/env bash
set -euo pipefail
. /usr/lib/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/bash/kernel/json_safe_pipeline.sh
. /usr/lib/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/bash/kernel/plugin_configuration.sh

LOCKFILE="/tmp/user_startup_params.lock"

exec 9>"$LOCKFILE" || json_fail "failed to open startup params lockfile"
flock -n 9 || json_fail "someone else is already writing changes to startup params"

if [ $# -lt 1 ]; then
    json_fail "missing argument 1: startup params file path"
fi

PARAMS_FILE="$1"

if [ ! -e "$PARAMS_FILE" ]; then
    json_fail "startup params file does not exist: $PARAMS_FILE"
fi

USER_CONFIG_FILE="$(lp_user_config_for "maksimshimshon_gamemanagepanel" "lifecycle" "user_startup_params.json")"

mv -f "$PARAMS_FILE" "$USER_CONFIG_FILE"
