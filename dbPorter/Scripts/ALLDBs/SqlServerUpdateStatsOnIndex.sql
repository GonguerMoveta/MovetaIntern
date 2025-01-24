IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateStatsOnIndex]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateStatsOnIndex]
GO

CREATE PROCEDURE [dbo].[UpdateStatsOnIndex]
    @indexName varchar(255)
AS
    DECLARE @tableName varchar(255)

    SET @tableName= (
        SELECT T.name FROM sysobjects T join sysindexes I on T.id = I.id
		WHERE I.name = @indexName)
    
		DECLARE @updateStats varchar(4000)

    SET @updateStats= 'UPDATE STATISTICS ' + @tableName + ' ' + @indexName
    EXEC(@updateStats)
GO