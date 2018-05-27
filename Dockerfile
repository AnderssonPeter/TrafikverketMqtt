FROM microsoft/dotnet:2.1-sdk AS build

WORKDIR /code

COPY ./TrafikverketMQTT/* ./TrafikverketMQTT/
COPY ./Trafikverket/* ./Trafikverket/
COPY ./TrafikverketMQTT.sln ./TrafikverketMQTT.sln
RUN dotnet restore

RUN dotnet publish -o output -c release -r linux-arm
RUN rm /output/appsettings.json

FROM microsoft/dotnet:2.1-runtime

COPY --from=build /output /app

WORKDIR /app

ENTRYPOINT [ "dotnet", "TrafikverketMQTT.dll", "/etc/TrafikverketMQTT/TrafikverketMQTT.json" ]