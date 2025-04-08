# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Sao chép toàn bộ file dự án vào thư mục /app trong container
COPY . . 
COPY ./GraduateProject/file ./GraduateProject/file

# Chạy restore và publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Sao chép các file đã publish vào thư mục /app trong container
COPY --from=build /app/out .

# Cấu hình ứng dụng chạy
CMD ["dotnet", "GraduateProject.dll"]
