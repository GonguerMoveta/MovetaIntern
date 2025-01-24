IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DropIndex]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DropIndex]
GO

CREATE PROCEDURE [dbo].[DropIndex]
    @indexName varchar(255)
AS
    DECLARE @tableName varchar(255)

    SET @tableName= (
        SELECT T.name FROM sysobjects T join sysindexes I on T.id = I.id
		WHERE I.name = @indexName)
    
		IF LEN(@tableName) > 0
    BEGIN
			DECLARE @dropIndex varchar(4000)

    	SET @dropindex= 'DROP INDEX ['+ @indexName + '] ON ' + @tableName
    	EXEC(@dropIndex)
  	END
GO
