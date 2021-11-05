cd %~dp0

docker-compose -f docker-compose.yml start
REM wait for 1-2 seconds for the container to start
pause
docker exec -it mssql-1 /bin/bash
