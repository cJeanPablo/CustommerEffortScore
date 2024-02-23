FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

ARG PROJECT_NAME_PATH

WORKDIR /build
COPY . .


FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
ARG PROJECT_NAME_PATH
RUN apk add icu-libs
ENV PROJECT="${PROJECT_NAME_PATH}"
ENV DOTNET_gcServer=1
ENV DOTNET_GCHeapCount=c

RUN apk add tzdata
RUN cp /usr/share/zoneinfo/America/Sao_Paulo /etc/localtime
RUN echo "America/Sao_Paulo" > /etc/timezone

WORKDIR /app
COPY --from=build /app .
ENV PROJECT="${PROJECT_NAME_PATH}"

ENTRYPOINT [ "/bin/sh", "-c", "dotnet ${PROJECT}.dll" ]
