#!/usr/bin/env bash
set -euo pipefail

# Load the shared JSON‑safe pipeline
. /usr/lib/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/bash/kernel/jsonsafepipeline.sh

LGSM_USER="lgsm"
LGSM_HOME="/home/$LGSM_USER"
SUDOERS_FILE="/etc/sudoers.d/lgsm"

# -------------------------
# Argument validation
# -------------------------
if [ $# -ne 1 ]; then
    json_fail "Missing game_server argument"
fi

GAME_SERVER="$1"

# -------------------------
# SteamCMD cleanup
# -------------------------
if [ -d "/home/lgsm/.steam/steamcmd" ]; then
    rm -rf "/home/lgsm/.steam/steamcmd/"* || true
fi

# -------------------------
# Sudoers setup
# -------------------------
echo "$LGSM_USER ALL=(ALL) NOPASSWD:ALL" > "$SUDOERS_FILE"
chmod 440 "$SUDOERS_FILE"

# -------------------------
# Combined EXIT trap (your cleanup + wrapper cleanup)
# -------------------------
cleanup() {
    rm -f "$SUDOERS_FILE" || true
    json_cleanup
}
trap cleanup EXIT

# -------------------------
# Create LGSM user if missing
# -------------------------
if ! id "$LGSM_USER" >/dev/null 2>&1; then
    useradd -m -d "$LGSM_HOME" -s /bin/bash "$LGSM_USER"
    passwd -d "$LGSM_USER"
fi

# -------------------------
# Install dependencies
# -------------------------
DEPENDENCY_SCRIPT="/usr/lib/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/bash/kernel/installpackagedependencies.sh"
bash "$DEPENDENCY_SCRIPT" curl locales >&2

# -------------------------
# Install LGSM as lgsm user
# -------------------------
runuser -l "$LGSM_USER" -c "
    set -euo pipefail
    export DEBIAN_FRONTEND=noninteractive
    cd \"$LGSM_HOME\"
    curl -Lo linuxgsm.sh https://linuxgsm.sh
    chmod +x linuxgsm.sh
    bash ./linuxgsm.sh $GAME_SERVER
    printf \"y\n\" | bash ./$GAME_SERVER install
" >&2

# -------------------------
# Write installation state
# -------------------------
STATE_DIR="/etc/lunaticpanel/plugins/maksimshimshon_gamemanagepanel/linuxgameserver"
STATE_FILE="$STATE_DIR/installationstate.json"

mkdir -p "$STATE_DIR"

cat > "$STATE_FILE" <<EOF
{
  "Id": "$GAME_SERVER",
  "InstallDate": "$(date -u +"%Y-%m-%dT%H:%M:%SZ")"
}
EOF