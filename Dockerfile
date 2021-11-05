FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

####################################################################################
### Install OpenSSH and set the password for root to "Docker!"
### This will make it possible to debug the container in the App Service SCM tooling
####################################################################################
RUN apt-get update \
     && apt-get install -y openssh-server htop dos2unix \
     && echo "root:Docker!" | chpasswd 
# Copy the sshd_config file to the /etc/ssh/ directory
COPY ssh/sshd_config /etc/ssh/
RUN dos2unix /etc/ssh/sshd_config

# Copy and configure the ssh_setup file
COPY ssh/ssh_setup.sh /tmp/ssh_setup.sh
RUN dos2unix /tmp/ssh_setup.sh \
    && chmod +x /tmp/ssh_setup.sh \
    && (sleep 1;/tmp/ssh_setup.sh 2>&1 > /dev/null)
EXPOSE 2222
####################################################################################

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/AksStartupDotnetApp/AksStartupDotnetApp.csproj", "AksStartupDotnetApp/"]
RUN dotnet restore "AksStartupDotnetApp/AksStartupDotnetApp.csproj"
COPY src/ .
WORKDIR "/src/AksStartupDotnetApp"
RUN dotnet build "AksStartupDotnetApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AksStartupDotnetApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# ENTRYPOINT ["dotnet", "AksStartupDotnetApp.dll"]

COPY docker-entrypoint.sh /usr/local/bin/docker-entrypoint.sh
RUN dos2unix /usr/local/bin/docker-entrypoint.sh \
    && chmod +x /usr/local/bin/docker-entrypoint.sh
ENTRYPOINT ["/usr/local/bin/docker-entrypoint.sh"]
