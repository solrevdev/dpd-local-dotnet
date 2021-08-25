#!/usr/bin/env bash

cd $(dirname $0)

dotnet test ../dpd-local-dotnet.sln
