#! /usr/bin/env bash
set -uvx
set -e
cd "$(dirname "$0")"
cwd=$(pwd)
ts=$(date "+%Y.%m%d.%H%M.%S")
"C:\Program Files\BlueGriffon\bluegriffon.exe" list.html &
