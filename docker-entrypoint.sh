#!/bin/sh

# Start sshd server in the background
nohup /usr/sbin/sshd -D & disown

dotnet AksStartupDotnetApp.dll
