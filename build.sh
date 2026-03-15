#! /usr/bin/env bash
set -uvx
set -e
cd "$(dirname "$0")"
cwd=$(pwd)
ts=$(date "+%Y.%m%d.%H%M.%S")

#rm -rf random-*.exe randome-*.pdb
rm -rf *.exe *.pdb
wingen.exe
./random-1080p.main.sh @merge
./random-m4a.main.sh @merge
./html-filter.main.sh @merge
./gen-list-1080p.main.sh @merge
./gen-list-m4a.main.sh @merge
./inject-1080p.main.sh @merge
./inject-m4a.main.sh @merge
