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
./.r.gen-list-1080p @merge
./.r.gen-list-m4a @merge
./.r.inject-1080p @merge
./.r.inject-m4a @merge
