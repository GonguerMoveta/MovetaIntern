IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DropColumn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DropColumn]
GO

CREATE PROCEDURE [dbo].[DropColumn]
    @tableName varchar(255),
    @columnName varchar(255)
AS
	DECLARE @statement varchar(255)
	DECLARE @constraintName varchar(255)

BEGIN   			
	SET @constraintName = '[DF_' + @tableName + '_' + @columnName + ']'
	
	IF EXISTS(SELECT COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = @tableName AND column_name = @columnName AND COLUMN_DEFAULT IS NOT NULL)		
		BEGIN		
			SET @statement = 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT ' + @constraintName		
			EXEC(@statement)			
		END
	
	SET @statement = 'ALTER TABLE ' + @tableName + ' DROP COLUMN ' + @columnName
	EXEC(@statement)
END
GO