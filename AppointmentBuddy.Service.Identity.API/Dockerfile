FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:3.1-sdk AS build
WORKDIR /src
COPY ["GEMS2.Service.TenderSpec.API/GEMS2.Service.TenderSpec.API.csproj", "GEMS2.Service.TenderSpec.API/"]
RUN dotnet restore "GEMS2.Service.TenderSpec.API/GEMS2.Service.TenderSpec.API.csproj"
COPY . .
WORKDIR "/src/GEMS2.Service.TenderSpec.API"
RUN dotnet build "GEMS2.Service.TenderSpec.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "GEMS2.Service.TenderSpec.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GEMS2.Service.TenderSpec.API.dll"]
