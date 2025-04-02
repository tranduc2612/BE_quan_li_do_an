#!/bin/bash
# Đợi SQL Server khởi động
echo "Đợi SQL Server khởi động..."
sleep 15s

# Chạy file SQL
echo "Đang chạy file SQL..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P "YourStrong!Passw0rd" -d master -i /init/my_database.sql

echo "Hoàn thành việc khởi tạo database."
