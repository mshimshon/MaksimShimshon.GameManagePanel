#!/bin/bash
set -euo pipefail

LGSM_USER="lgsm"
LGSM_HOME="/home/$LGSM_USER"
SUDOERS_FILE="/etc/sudoers.d/lgsm"

COMPLETED=true
FAILURE_MESSAGE=""

cleanup() {
    rm -f "$SUDOERS_FILE" || true
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
trap 'fail "LGSM installation failed"' ERR

if [ $# -ne 1 ]; then
    fail "Missing game_server argument"
fi

GAME_SERVER="$1"

if [ -d "/home/lgsm/.steam/steamcmd" ]; then
    rm -rf "/home/lgsm/.steam/steamcmd/"*
fi

echo "$LGSM_USER ALL=(ALL) NOPASSWD:ALL" > "$SUDOERS_FILE"
chmod 440 "$SUDOERS_FILE"

if ! id "$LGSM_USER" >/dev/null 2>&1; then
    useradd -m -d "$LGSM_HOME" -s /bin/bash "$LGSM_USER"
    passwd -d "$LGSM_USER"
fi

runuser -l "$LGSM_USER" -c "
    set -euo pipefail
    export DEBIAN_FRONTEND=noninteractive
    cd \"$LGSM_HOME\"
    curl -Lo linuxgsm.sh https://linuxgsm.sh
    chmod +x linuxgsm.sh
    bash ./linuxgsm.sh $GAME_SERVER
    yes y | bash ./$GAME_SERVER install
"
