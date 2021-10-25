# FROM surnet/alpine-wkhtmltopdf:3.9-0.12.5-full as wkhtmltopdf
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base

# Install networking tools
#RUN apk add --no-cache bash \
#  busybox-extras \		
#  curl

# COPY ["Certs/*.crt", "Certs/"]
# ADD Certs/*.crt /usr/local/share/ca-certificates/
# RUN chmod 644 /usr/local/share/ca-certificates/*.crt && update-ca-certificates

# Install libwkhtmltox dependencies
# ENV LD_LIBRARY_PATH /lib64
#RUN apk add libgdiplus \
#  --no-cache --repository http://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted
RUN apk add --no-cache \
  libstdc++ \
  libx11 \
  libc6-compat \
  libxrender \
  libxext \
  libssl1.1 \
  ca-certificates \
  fontconfig \
  freetype \
  ttf-dejavu \
  ttf-droid \
  ttf-freefont \
  ttf-liberation \
  icu-libs \
  tzdata \
  && apk add --no-cache --virtual .build-deps \
  msttcorefonts-installer \
  \
  # Install microsoft fonts
  && update-ms-fonts \
  && fc-cache -f \
  \
  # Clean up when done
  && rm -rf /tmp/* \
  && apk del .build-deps

RUN apk add icu-libs \
  tzdata
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Set TimeZone
ENV TZ=Asia/Singapore
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["NUS_ISS_14_Appointment_Buddy/NUS_ISS_14_Appointment_Buddy.csproj", "NUS_ISS_14_Appointment_Buddy/"]
RUN dotnet restore "NUS_ISS_14_Appointment_Buddy/NUS_ISS_14_Appointment_Buddy.csproj"
COPY . .
WORKDIR "/src/NUS_ISS_14_Appointment_Buddy"
RUN dotnet build "NUS_ISS_14_Appointment_Buddy.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "NUS_ISS_14_Appointment_Buddy.csproj" -c Release -o /app


FROM surnet/alpine-wkhtmltopdf:3.10-0.12.5-full as wkhtmltopdf

FROM base AS final

WORKDIR /app
COPY --from=publish /app .
COPY --from=wkhtmltopdf /bin/wkhtmltopdf .
COPY --from=wkhtmltopdf /bin/libwkhtmltox.so .
COPY --from=wkhtmltopdf /bin/libwkhtmltox* /app/

ENTRYPOINT ["dotnet", "NUS_ISS_14_Appointment_Buddy.dll"]