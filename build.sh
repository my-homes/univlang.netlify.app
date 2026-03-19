#! /usr/bin/env bash
set -uvx
set -e
cd "$(dirname "$0")"
cwd=$(pwd)
ts=$(date "+%Y.%m%d.%H%M.%S")

#rm -rf *.exe *.pdb
wingen.exe *.main.cs
./random-1080p.do @merge
./random-m4a.do @merge
./html-filter.do @merge
./gen-list-1080p.do @merge
./gen-list-m4a.do @merge
./inject-1080p.do @merge
./inject-m4a.do @merge
