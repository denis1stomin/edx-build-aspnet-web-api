############################
# To make client code work:

cd lab1/client
npm install request

# My current nodejs version is
#   nodejs --version
#   v4.7.2

############################
# To make client request:

# Run server
cd lab1/server
dotnet build && dotnet run

# Make request
cd lab1/client
nodejs service.js
