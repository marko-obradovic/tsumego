#!/bin/bash

usage() {
  cat <<EOF
This script runs the current C# project.

PARAMETERS:
   -n Project Name

OPTIONS:
   -c clean
   -b build
   -h Help Message
EOF
}

while getopts "hcbn:" OPTION; do
  case $OPTION in
  h)
    usage
    exit 1
    ;;
  n)
    executable=$OPTARG
    ;;
  b)
    is_clean_build=$OPTARG
    ;;
  ?)
    usage
    exit
    ;;
  esac
done

if [ -z "$executable" ]; then
  echo "No project name specified."
  usage
  exit 1
fi

if [ -z "$is_clean_build" ]; then
  dotnet clean
  dotnet build
fi

dotnet run ./bin/Debug/net8.0/$executable
