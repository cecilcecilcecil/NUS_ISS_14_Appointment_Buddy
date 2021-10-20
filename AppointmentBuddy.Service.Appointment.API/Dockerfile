FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:3.1-sdk AS build
WORKDIR /src
COPY ["AppointmentBuddy.Service.Appointment.API/AppointmentBuddy.Service.Appointment.API.csproj", "AppointmentBuddy.Service.Appointment.API/"]
RUN dotnet restore "GEMS2.Service.Appointment.API/GEMS2.Service.Appointment.API.csproj"
COPY . .
WORKDIR "/src/AppointmentBuddy.Service.Appointment.API"
RUN dotnet build "GEMS2.Service.Appointment.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AppointmentBuddy.Service.Appointment.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AppointmentBuddy.Service.Appointment.API.dll"]
