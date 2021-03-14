#!/bin/bash
echo Cleaning . . .
service="contactapi"
service=`echo "$service" | awk '{print tolower($0)}'`
docker stop $service
docker rm $service
echo Building $service. . .
if [ -d '/c/Windows' ]
then
	/usr/bin/sed -i 's/\r//g' "__docker_content_start.sh"
	/usr/bin/sed -i 's/\r//g' "Dockerfile"
fi
chmod +x __docker_content_start.sh
docker build --no-cache -t $service .
echo Running . . .
# Keep the below -e args in sync with your Global.cs file
docker run -d --name $service --restart always \
	-e CorsOrigin="*" \
	-p 5000:5002 \
	-e ASPNETCORE_ENVIRONMENT="development" \
	-e ConnectionString="server=172.17.0.1;port=13306;database=Contact;userid=root;pwd=Str0ngPa\$\$123" \
	$service
