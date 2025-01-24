IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSDBSequence]') AND type in (N'U'))
DROP TABLE [dbo].[SYSDBSequence]
GO
CREATE TABLE [dbo].[SYSDBSequence](
	[Value] [int] NOT NULL
) ON [PRIMARY]
GO

INSERT INTO [dbo].[SYSDBSequence]
	Values(0)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CURRVAL]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[CURRVAL]
GO
CREATE FUNCTION dbo.CURRVAL()
RETURNS int
AS
BEGIN
DECLARE @ret_value int

SELECT @ret_value = [value] FROM sysdbsequence

RETURN @ret_value

END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NEXTVAL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[NEXTVAL]
GO
CREATE PROCEDURE dbo.NEXTVAL(@ret int output)
AS
BEGIN
BEGIN TRANSACTION

UPDATE sysdbsequence
SET [value] = [value] + 1

SELECT @ret = [value] FROM sysdbsequence

COMMIT TRANSACTION
END
GO
