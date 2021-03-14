#!/bin/bash
cd /app/ContactApi
	con="$ConnectionString"
	dotnet exec \
	--depsfile ContactApi.deps.json \
	--runtimeconfig ContactApi.runtimeconfig.json \
	./lib/ef.dll \
	database update -c context \
	--assembly ContactApi.dll \
	--startup-assembly ContactApi.dll \
	--root-namespace ContactApi \

dotnet ContactApi.dll

