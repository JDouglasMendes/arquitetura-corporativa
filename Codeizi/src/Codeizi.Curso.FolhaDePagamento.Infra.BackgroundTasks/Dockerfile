#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks/Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks.csproj", "src/Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks/"]
RUN dotnet restore "src/Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks/Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks.csproj"
COPY . .
WORKDIR "/src/src/Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks"
RUN dotnet build "Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Codeizi.Curso.FolhaDePagamento.Infra.BackgroundTasks.dll"]