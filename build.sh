#! /usr/bin/env bash
set -uvx
set -e
cd "$(dirname "$0")"
cwd=$(pwd)
ts=$(date "+%Y.%m%d.%H%M.%S")

#rm -rf random-*.exe randome-*.pdb
rm -rf *.exe *.pdb
wingen.exe
./.r.random-1080p @merge
./.r.random-m4a @merge
./.r.html-filter @merge
