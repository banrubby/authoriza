FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# Copy the csproj and restore all of the nugets
COPY Authorizeniki.sln ./
COPY Authorizeniki/*.csproj ./Authorizeniki/
COPY Authorizeniki.Datalayer/*.csproj ./Authorizeniki.Datalayer/

RUN dotnet restore Authorizeniki/Authorizeniki.csproj
RUN dotnet restore Authorizeniki.Datalayer/Authorizeniki.Datalayer.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Authorizeniki.dll"]