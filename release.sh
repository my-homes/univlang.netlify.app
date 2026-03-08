#! /usr/bin/env bash
#set -uvx
set -e
cd "$(dirname "$0")"
cwd=`pwd`
ts=`date "+%Y.%m%d.%H%M.%S"`
version="${ts}"

killall.exe bluegriffon.exe || true

current=$(cd $(dirname $0);pwd)
echo $current
name=`echo "$current" | sed -e 's/.*\/\([^\/]*\)$/\1/'`
echo $name

sed -i -e "s/<title>.*<\/title>/<title>${name}.netlify.app<\/title>/g" index.template.html
sed -i -e "s/<h1>.*<\/h1>/<h1>${name}.netlify.app ${version}<\/h1>/g" index.template.html
cd $cwd/
echo ${version}>version.txt

cp index.template.html index.html
echo "#! /usr/bin/env open-markdown">@@index.mk
./html-filter.exe @@playlist.md|shuffle>>@@index.md
cat @@index.md|shuffle>>index.html

tag="v$version"
cd $cwd
git-put -a "v$version"
start https://app.netlify.com/projects/${name}/overview
