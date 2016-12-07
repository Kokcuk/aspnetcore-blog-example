FROM microsoft/dotnet
ARG source=.
WORKDIR /app
EXPOSE 5000
COPY $source .
WORKDIR /app/src/BlogExampleStatic.Web
RUN ["dotnet", "restore"]
#ENTRYPOINT ["dotnet", "run"]
