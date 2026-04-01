#! /usr/bin/env bash
set -uvx
set -e
cd "$(dirname "$0")"
cwd=$(pwd)
ts=$(date "+%Y.%m%d.%H%M.%S")

#rm -rf *.exe *.pdb
wingen.exe *.main.cs
./random-1080p.task @merge -f
./random-m4a.task @merge -f
./html-filter.task @merge -f
./gen-list-1080p.task @merge -f
./gen-list-m4a.task @merge -f
./inject-1080p.task @merge -f
./inject-m4a.task @merge -f
