#! /usr/bin/env bash
# -*- mode: sh -*-
chcp.com 65001 > /dev/null 2>&1
script_dir="$(dirname "$0")"
script_dir="$(realpath $script_dir)"
str="$1"
if [[ "${str:0:1}" == "@" ]]; then
  if [[ "$str" == "@run" ]]; then
    shift
  elif [[ "$str" == "@exe" ]]; then
    if [ ! -f "$script_dir/test01.exe" ]; then
      bash "$script_dir/.r.test01.sh" "@merge" -f 1>&2
    fi
    shift
    $script_dir/test01.exe "$@"
  elif [[ "$str" == "@bin" ]]; then
    if [ ! -f "$script_dir/test01.exe" ]; then
      bash "$script_dir/.r.test01.sh" "@pack" -f 1>&2
    fi
    shift
    $script_dir/test01.exe "$@"
  else
    bash.exe "$script_dir/.r.test01.sh" "$@"
  fi
else
  cscs -nuget:restore "$script_dir/test01.main.cs" 1>&2
  cscs -l:0 "$script_dir/test01.main.cs" "$@"
fi
