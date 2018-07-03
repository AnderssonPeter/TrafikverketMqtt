FROM microsoft/dotnet:2.1-sdk AS build

WORKDIR /app

COPY ./TrafikverketMQTT/* ./TrafikverketMQTT/
COPY ./Trafikverket/* ./Trafikverket/
COPY ./TrafikverketMQTT.sln ./TrafikverketMQTT.sln
RUN dotnet restore

RUN dotnet build -c release

RUN dotnet publish -o out -c Release

FROM microsoft/dotnet:2.1-runtime

WORKDIR /app

COPY --from=build /app/TrafikverketMQTT/out ./

ENTRYPOINT ["dotnet", "TrafikverketMQTT.dll", "/etc/TrafikverketMQTT/TrafikverketMQTT.json"]