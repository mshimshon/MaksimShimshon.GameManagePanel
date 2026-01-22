#!/usr/bin/env bash
set -euo pipefail
set -E

LGSM_USER="lgsm"
LGSM_HOME="/home/$LGSM_USER"
SUDOERS_FILE="/etc/sudoers.d/lgsm"

if [ $# -ne 2 ]; then
    echo "Missing arguments: <game_server> <display_name>" >&2
    exit 1
fi

GAME_SERVER="$1"
DISPLAY_NAME="$2"

STATE_DIR="/etc/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/config/linuxgameserver"
PROGRESS_FILE="$STATE_DIR/installation_progress_state.json"
FINAL_FILE="$STATE_DIR/installation_state.json"

AVAILABLE_GAMES_DIR="/etc/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/repos/linuxgameserver/available_games/games"
GAME_REPO_PATH="$AVAILABLE_GAMES_DIR/$GAME_SERVER"
TARGET_CONTROL_DIR="/home/lgsm/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/bash/server_control"

if [ ! -d "$GAME_REPO_PATH" ]; then
    echo "Cannot install unsupported server by the panel, unless you forgot to fetch/clone the panel repos of supported game scripts." >&2
    exit 1
fi

mkdir -p "$TARGET_CONTROL_DIR"

mkdir -p "$STATE_DIR"
chmod -R 755 "$STATE_DIR"

if [ -f "$FINAL_FILE" ]; then
    echo "Server for $DISPLAY_NAME is already installed" >&2
    exit 1
fi

write_progress() {
cat > "$PROGRESS_FILE" <<EOF
{
  "FailureReason": null,
  "IsInstalling": true,
  "CurrentStep": "Installing Server for $DISPLAY_NAME...",
  "Id": "$GAME_SERVER",
  "DisplayName": "$DISPLAY_NAME"
}
EOF
chmod -R 755 "$STATE_DIR"

}

write_failure() {
local reason="$1"
cat > "$PROGRESS_FILE" <<EOF
{
  "FailureReason": "$reason",
  "IsInstalling": false,
  "CurrentStep": "Installing Server for $DISPLAY_NAME...",
  "Id": "$GAME_SERVER",
  "DisplayName": "$DISPLAY_NAME"
}
EOF
chmod -R 755 "$STATE_DIR"



}

write_progress
ls -l "$PROGRESS_FILE" || echo "NO PROGRESS FILE FROM INSIDE SCRIPT" >&2


SUCCESS=0

cleanup() {
    # ALWAYS remove sudo perms
    rm -f "$SUDOERS_FILE" || true
    # Clear the locked file
    rm -f "$STATE_DIR/.install_lock" || true
    # On success, delete progress file
    if [ "$SUCCESS" -eq 1 ]; then
        rm -f "$PROGRESS_FILE" || true
    fi

}
trap cleanup EXIT

# ERR trap: write failure, keep progress file, still remove sudo perms
trap '
    trap - EXIT
    write_failure "Installation failed"
    rm -f "$SUDOERS_FILE" || true
    exit 1
' ERR

# Clean Steam Cache from Previous Install Attempts

if [ -d "/home/lgsm/.steam/steamcmd" ]; then
    rm -rf "/home/lgsm/.steam/steamcmd/"* || true
fi

echo "$LGSM_USER ALL=(ALL) NOPASSWD:ALL" > "$SUDOERS_FILE"
chmod 440 "$SUDOERS_FILE"

DEPENDENCY_SCRIPT="/usr/lib/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/bash/kernel/install_package_dependencies.sh"
bash "$DEPENDENCY_SCRIPT" curl locales >&2

runuser -l "$LGSM_USER" -c "
    set -euo pipefail
    set -E
    export DEBIAN_FRONTEND=noninteractive
    cd \"$LGSM_HOME\"
    curl -Lo linuxgsm.sh https://linuxgsm.sh
    chmod +x linuxgsm.sh
    bash ./linuxgsm.sh \"$GAME_SERVER\"
    bash \"./$GAME_SERVER\" auto-install
" >&2


cat > "$FINAL_FILE" <<EOF
{
  "Id": "$GAME_SERVER",
  "InstallDate": "$(date -u +"%Y-%m-%dT%H:%M:%SZ")"
}
EOF

chmod -R 755 "$STATE_DIR"


cp -a "$GAME_REPO_PATH"/. "$TARGET_CONTROL_DIR"/

bash -e /usr/lib/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/bash/linuxgameserver/fix_home_permission.sh
SUCCESS=1


