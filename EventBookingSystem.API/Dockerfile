FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["EventBookingSystem.API/EventBookingSystem.API.csproj", "EventBookingSystem.API/"]
COPY ["EventBookingSystem.Application/EventBookingSystem.Application.csproj", "EventBookingSystem.Application/"]
COPY ["EventBookingSystem.Domain/EventBookingSystem.Domain.csproj", "EventBookingSystem.Domain/"]
COPY ["EventBookingSystem.Infrastructure/EventBookingSystem.Infrastructure.csproj", "EventBookingSystem.Infrastructure/"]
RUN dotnet restore "EventBookingSystem.API/EventBookingSystem.API.csproj"

COPY . .
WORKDIR "/src/EventBookingSystem.API"

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN rm -rf ../EventBookingSystem.Infrastructure/Migrations
RUN dotnet ef migrations add InitialCreate --project ../EventBookingSystem.Infrastructure/EventBookingSystem.Infrastructure.csproj --startup-project .


RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 5050
ENTRYPOINT ["dotnet", "EventBookingSystem.API.dll"]