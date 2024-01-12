#Build API image

FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /source
COPY . .
RUN dotnet restore "./SupportDeskAPI/SupportDeskAPI.csproj" --disable-parallel
RUN dotnet publish "./SupportDeskAPI/SupportDeskAPI.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5001
ENTRYPOINT [ "dotnet", "SupportDeskAPI.dll" ]