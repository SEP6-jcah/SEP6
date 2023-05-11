FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

RUN dotnet tool install --global dotnet-ef

ENV PATH="$PATH:/root/.dotnet/tools"

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build -c Release

WORKDIR /app/MovieService
RUN dotnet ef migrations add InitialiseDB --project ../MovieService.csproj

WORKDIR /app
RUN dotnet publish -c Release -o out

COPY ./Persistence/movies.db /app/out/movies.db

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

CMD ["dotnet", "MovieService.dll"]