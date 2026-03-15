#! /usr/bin/env bash
chcp.com 65001 > /dev/null 2>&1
script_dir="$(dirname "$0")"
script_dir="$(realpath $script_dir)"
str="$1"
if [[ "${str:0:1}" == "@" ]]; then
  if [[ "$str" == "@run" ]]; then
    shift
  fi
  "$script_dir/.r.test01.sh" "$@"
else
  cscs "$script_dir/test01.main.cs" "$@"
fi
