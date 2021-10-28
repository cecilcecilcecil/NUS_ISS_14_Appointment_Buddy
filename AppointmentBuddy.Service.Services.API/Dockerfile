FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
EXPOSE 8006

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AppointmentBuddy.Service.Services.API/AppointmentBuddy.Service.Services.API.csproj", "AppointmentBuddy.Service.Services.API/"]
RUN dotnet restore "AppointmentBuddy.Service.Services.API/AppointmentBuddy.Service.Services.API.csproj"
COPY . .
WORKDIR "/src/AppointmentBuddy.Service.Services.API"
RUN dotnet build "AppointmentBuddy.Service.Services.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AppointmentBuddy.Service.Services.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AppointmentBuddy.Service.Services.API.dll"]
