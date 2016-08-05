# Clay Door Master
Project demonstrating API (Web API with EF using MYSQL) that implements CRUD for Entities like:

1. Doors

2. Users

3. Get information about bindinigs Doors-Books that reflect access users to the doors

# PreRequisites:

* MySQL:
First create database schema named test

* Change connection string to your MySQL server:
File Web.config:

  <connectionStrings>
    <add name="DefaultConnection" connectionString="server=127.0.0.1;User Id=test;password=test;database=test;port=3306" providerName="Mysql.Data.MySqlClient" />
  </connectionStrings>


* Run DB Migration in Visual Studio:
Update-Database -ProjectName Api -StartUpProjectName Api -ConnectionStringName "DefaultConnection"

API description: