FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AppointmentBuddy.Service.Specialist.API/AppointmentBuddy.Service.Specialist.API.csproj", "AppointmentBuddy.Service.Specialist.API/"]
RUN dotnet restore "AppointmentBuddy.Service.Specialist.API/AppointmentBuddy.Service.Specialist.API.csproj"
COPY . .
WORKDIR "/src/AppointmentBuddy.Service.Specialist.API"
RUN dotnet build "AppointmentBuddy.Service.Specialist.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AppointmentBuddy.Service.Specialist.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AppointmentBuddy.Service.Specialist.API.dll"]
