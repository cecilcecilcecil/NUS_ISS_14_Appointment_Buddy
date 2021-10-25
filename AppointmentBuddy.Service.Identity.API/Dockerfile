FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AppointmentBuddy.Service.Identity.API/AppointmentBuddy.Service.Identity.API.csproj", "AppointmentBuddy.Service.Identity.API/"]
RUN dotnet restore "AppointmentBuddy.Service.Identity.API/AppointmentBuddy.Service.Identity.API.csproj"
COPY . .
WORKDIR "/src/AppointmentBuddy.Service.Identity.API"
RUN dotnet build "AppointmentBuddy.Service.Identity.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AppointmentBuddy.Service.Identity.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AppointmentBuddy.Service.Identity.API.dll"]
