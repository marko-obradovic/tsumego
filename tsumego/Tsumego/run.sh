#!/bin/bash
executable=$1

if [ -z "$executable" ]; then
  echo "No project name specified."
  usage
  exit 1
fi

dotnet clean
dotnet build

dotnet run ./bin/Debug/net8.0/$executable --property WarningLevel=0
