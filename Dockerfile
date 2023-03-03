FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

COPY ./TrafikverketMQTT/* ./TrafikverketMQTT/
COPY ./Trafikverket/* ./Trafikverket/
COPY ./TrafikverketMQTT.sln ./TrafikverketMQTT.sln
RUN dotnet restore

RUN dotnet build -c release

RUN dotnet publish TrafikverketMQTT -o out -c Release

FROM mcr.microsoft.com/dotnet/runtime:7.0

WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "TrafikverketMQTT.dll", "/etc/TrafikverketMQTT/TrafikverketMQTT.json"]