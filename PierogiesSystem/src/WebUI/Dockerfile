#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used for VS debugging on Docker
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV TZ=Europe/Warsaw
ENV LANG pl_PL.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}
ENV ASPNETCORE_URLS=http://+:5000;
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ENV TZ=Europe/Warsaw
ENV LANG pl_PL.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}
WORKDIR /src
COPY ["src/WebUI/WebUI.csproj", "src/WebUI/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
ENV TZ=Europe/Warsaw
ENV LANG pl_PL.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
ENV TZ=Europe/Warsaw
ENV LANG pl_PL.UTF-8
ENV LANGUAGE ${LANG}
ENV LC_ALL ${LANG}
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.WebUI.dll"]