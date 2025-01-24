IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AlterColumn]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AlterColumn]
GO

CREATE PROCEDURE [dbo].[AlterColumn]
    @tableName varchar(255),
    @columnName varchar(255),
    @value varchar(255)	-- NULL or NOT NULL
AS
    DECLARE @dataType varchar(255)
	DECLARE @alterColumn varchar(4000)

	IF @value != 'NOT NULL WITH DEFAULT'

		BEGIN		

		SET @dataType= (
			SELECT data_type +  
					CASE WHEN character_maximum_length IS NOT NULL THEN 
						'(' + str(character_maximum_length, 4) + ')' ELSE 
						'' 
					END  +
					CASE WHEN numeric_precision IS NOT NULL THEN 
						'(' + str(numeric_precision, 4) + 
								CASE WHEN numeric_scale IS NOT NULL THEN 
									',' + str(numeric_scale, 4) + ')' ELSE 
									')' 
								END ELSE 
						'' 
					END
					FROM INFORMATION_SCHEMA.COLUMNS
					WHERE table_name = @tableName
					AND column_name = @columnName)

		SET @alterColumn = 'ALTER TABLE ' + @tableName + ' ALTER COLUMN ' + @columnName + ' ' + @dataType + ' ' + @value

		END

	ELSE
		
		BEGIN		
		
		DECLARE @constraintName varchar(255)
		
		SET @constraintName = '[' + @tableName + '_' + @columnName + '_DEFAULTVALUE]'
		
		IF EXISTS(SELECT COLUMN_DEFAULT FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = @tableName AND column_name = @columnName AND COLUMN_DEFAULT IS NOT NULL)
		
			BEGIN
		
			SET @alterColumn = 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT ' + @constraintName
			
			EXEC(@alterColumn)
			
			END
		
		
		SET @dataType= (
			SELECT UPPER(data_type)
					FROM INFORMATION_SCHEMA.COLUMNS
					WHERE table_name = @tableName
					AND column_name = @columnName)
		
		SET @alterColumn = 'ALTER TABLE ' + @tableName + ' ADD CONSTRAINT ' + @constraintName 
		
		IF @dataType = 'INT' OR @dataType = 'DECIMAL' OR @dataType = 'FLOAT' OR @dataType = 'SMALLINT'
				
			SET @alterColumn = @alterColumn + ' DEFAULT ((0)) FOR ' + @columnName

		ELSE IF @dataType = 'CHAR' OR @dataType = 'NCHAR' OR @dataType = 'VARCHAR' OR @dataType = 'NVARCHAR'
			
			SET @alterColumn = @alterColumn + ' DEFAULT '''' FOR ' + @columnName

		ELSE IF @dataType = 'DATETIME' OR @dataType = 'TIMESTAMP'
		
			SET @alterColumn = @alterColumn + ' DEFAULT CURRENT_TIMESTAMP FOR ' + @columnName	
		
		END

    EXEC(@alterColumn)
GO