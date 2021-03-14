FROM microsoft/dotnet:2.2-sdk AS build

ADD ContactApi /app/ContactApi

WORKDIR /app/ContactApi
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

RUN  apt-get update \
  && apt-get install -y wget \
  && rm -rf /var/lib/apt/lists/*

WORKDIR /app/ContactApi
COPY /ContactApi/lib/ef.dll /app/ContactApi/lib/ef.dll
COPY ./__docker_content_start.sh /app/ContactApi/start.sh
COPY --from=build /app/ContactApi/out ./

RUN chmod +x /app/ContactApi/start.sh

ENV ASPNETCORE_URLS http://*:5002
EXPOSE 5002/tcp

CMD /app/ContactApi/start.sh