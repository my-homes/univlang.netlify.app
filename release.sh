#! /usr/bin/env bash
#set -uvx
set -e
cd "$(dirname "$0")"
cwd=`pwd`
ts=`date "+%Y.%m%d.%H%M.%S"`
version="${ts}"

current=$(cd $(dirname $0);pwd)
echo $current
name=`echo "$current" | sed -e 's/.*\/\([^\/]*\)$/\1/'`
echo $name

#cd $cwd/$name
sed -i -e "s/<h1>.*<\/h1>/<h1>${name} ${version}<\/h1>/g" index.html
cd $cwd/
echo ${version}>version.txt

tag="v$version"
cd $cwd
git add .
git commit -m"$tag"
git tag -a "$tag" -m"$tag"
git push origin "$tag"
git push origin HEAD:main
git remote -v
start https://app.netlify.com/projects/univlang/overview
