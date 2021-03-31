#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/FolhaDePagamento.Infra.BackgroundTasks/FolhaDePagamento.Infra.BackgroundTasks.csproj", "src/FolhaDePagamento.Infra.BackgroundTasks/"]
RUN dotnet restore "src/FolhaDePagamento.Infra.BackgroundTasks/FolhaDePagamento.Infra.BackgroundTasks.csproj"
COPY . .
WORKDIR "/src/src/FolhaDePagamento.Infra.BackgroundTasks"
RUN dotnet build "FolhaDePagamento.Infra.BackgroundTasks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FolhaDePagamento.Infra.BackgroundTasks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FolhaDePagamento.Infra.BackgroundTasks.dll"]