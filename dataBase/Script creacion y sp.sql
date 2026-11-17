-- 1. Crear la base de datos dedicada (puedes ejecutar este bloque completo)
USE master
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'UrlPrDb')
BEGIN
    ALTER DATABASE UrlPrDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE
    DROP DATABASE UrlPrDb
END
GO

CREATE DATABASE UrlPrDb
GO

USE UrlPrDb
GO

-- 2. Crear la tabla principal
CREATE TABLE UrlShortcuts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    LongUrl NVARCHAR(2000) NOT NULL,
    ShortCode VARCHAR(10) UNIQUE NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    Clicks INT DEFAULT 0
)
GO

-- 3. SP de Inserción (Nombre exacto: sp_InsertUrl y devuelve los datos con ID y fecha)
CREATE PROCEDURE sp_InsertUrl
    @LongUrl NVARCHAR(2000),
    @ShortCode VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO UrlShortcuts (LongUrl, ShortCode)
    VALUES (@LongUrl, @ShortCode)
    
    -- Retorna el registro recién creado para que el repositorio arme la Entidad
    SELECT SCOPE_IDENTITY() AS Id, LongUrl, ShortCode, CreatedAt, Clicks
    FROM UrlShortcuts
    WHERE Id = SCOPE_IDENTITY()
END
GO

-- 4. SP de Búsqueda y Clic (Nombre exacto: sp_GeturlByCode, corrige el parámetro)
CREATE PROCEDURE sp_GeturlByCode
    @ShortCode VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON

    -- Actualiza el contador de clics usando correctamente el parámetro
    UPDATE UrlShortcuts 
    SET Clicks = Clicks + 1 
    WHERE ShortCode = @ShortCode
    
    -- Retorna la información actualizada
    SELECT Id, LongUrl, ShortCode, CreatedAt, Clicks 
    FROM UrlShortcuts 
    WHERE ShortCode = @ShortCode
END
GO

-- 5. SP de Listado (Nombre exacto: sp_ListarUrls)
CREATE PROCEDURE sp_ListarUrls
AS
BEGIN
    SET NOCOUNT ON

    SELECT Id, LongUrl, ShortCode, CreatedAt, Clicks 
    FROM UrlShortcuts
END
GO