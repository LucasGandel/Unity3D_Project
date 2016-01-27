#!/bin/bash

DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
rm $DIR/Temp/UnityLockfile

cd /opt/Unity/Editor/
export LC_ALL=C
./Unity