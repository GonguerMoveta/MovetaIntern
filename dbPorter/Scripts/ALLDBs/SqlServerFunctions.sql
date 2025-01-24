
/* =============================================
   Function: CTERM(int, fv, pv)

   Corresponding SQLBase function: @CTERM

   Description:
	This function returns the number of compounding periods to an investment of present value pv to grow to
   a future value fv, earning a fixed periodic interest rate int.  
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CTERM]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[CTERM]
GO

CREATE FUNCTION [dbo].[CTERM] 
	(@int float, @fv float, @pv float)
RETURNS float
AS
BEGIN
RETURN LOG(@fv / @PV)/LOG(1 + @int) 
END
GO

/* =============================================
   Function: DATETOCHAR(date, style)
   Corresponding SQLBase function: @DATETOCHAR

   Description:
	This function accepts a DATETIME data type value, applies the editing specified by 
   style and returns the edited value. The data type of the result is character.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DATETOCHAR]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[DATETOCHAR]
GO

CREATE FUNCTION [dbo].[DATETOCHAR]
	(@dDate datetime, @sPicture varchar(40))
RETURNS varchar(40)
AS
BEGIN
	DECLARE @sDate AS Varchar(254)
	DECLARE @sTemp AS Varchar(20)
	DECLARE @nTemp AS tinyint
	DECLARE @nPos AS tinyint
	SET @sDate = ''
	SET @nPos = 1
	IF @dDate IS NULL
	BEGIN
		SET @sDate = NULL
	END
	IF @dDate IS NOT NULL
	BEGIN
		WHILE @nPos <= DATALENGTH(@sPicture) 
		BEGIN
			IF UPPER(SUBSTRING(@sPicture,1,4)) = 'YYYY'
			BEGIN
		    	SET @sTemp = LTRIM(STR(DATEPART(yyyy,@dDate)))
			    SET @sDate = @sDate + @sTemp
		    	SET @sPicture = RIGHT(@sPicture,DATALENGTH(@sPicture)- 4)
			END 
			ELSE IF UPPER(SUBSTRING(@sPicture,1,2)) = 'YY'
			BEGIN
		    	SET @sTemp = LTRIM(STR(DATEPART(yyyy,@dDate)))
			    IF DATALENGTH(@sTemp) = 4 SET @sTemp = SUBSTRING(@sTemp, 3, 2)
			    SET @sDate = @sDate + @sTemp
		    	SET @sPicture = RIGHT(@sPicture,DATALENGTH(@sPicture)- 2)
			END 
			ELSE IF UPPER(SUBSTRING(@sPicture,1,2)) = 'MM'
			BEGIN
		    	SET @sTemp = LTRIM(STR(DATEPART(mm,@dDate)))
			    IF DATALENGTH(@sTemp) = 1 SET @sTemp = '0' + @sTemp
			    SET @sDate = @sDate + @sTemp
		    	SET @sPicture = RIGHT(@sPicture,DATALENGTH(@sPicture)- 2)
			END 
			ELSE IF UPPER(SUBSTRING(@sPicture,1,2)) = 'HH'
			BEGIN
				SET @nTemp = STR(DATEPART(hh,@dDate))
			    SET @sTemp = LTRIM(@nTemp)
				IF DATALENGTH(@sTemp) = 1 SET @sTemp = '0' + @sTemp
				SET @sDate = @sDate + @sTemp
				SET @sPicture = RIGHT(@sPicture,DATALENGTH(@sPicture)- 2)
			END 
			ELSE IF UPPER(SUBSTRING(@sPicture,1,2)) = 'MI'
			BEGIN
		    	SET @sTemp = LTRIM(STR(DATEPART(mi,@dDate)))
			    IF DATALENGTH(@sTemp) = 1 SET @sTemp = '0' + @sTemp
			    SET @sDate = @sDate + @sTemp
		    	SET @sPicture = RIGHT(@sPicture,DATALENGTH(@sPicture)- 2)
			END 
			ELSE IF UPPER(SUBSTRING(@sPicture,1,2)) = 'SS'
			BEGIN
		    	SET @sTemp = LTRIM(STR(DATEPART(ss,@dDate)))
			    IF DATALENGTH(@sTemp) = 1 SET @sTemp = '0' + @sTemp
			    SET @sDate = @sDate + @sTemp
			    SET @sPicture = RIGHT(@sPicture,DATALENGTH(@sPicture)- 2)
			END 
			ELSE IF UPPER(SUBSTRING(@sPicture,1,2)) = 'DD'
			BEGIN
		    	SET @sTemp = LTRIM(STR(DATEPART(dd,@dDate)))
			    IF DATALENGTH(@sTemp) = 1 SET @sTemp = '0' + @sTemp
			    SET @sDate = @sDate + @sTemp
			    SET @sPicture = RIGHT(@sPicture,DATALENGTH(@sPicture)- 2)
			END 
			ELSE 
			BEGIN
		    	SET @sDate = @sDate + LEFT(@sPicture,1)
			    SET @sPicture = RIGHT(@sPicture,DATALENGTH(@sPicture)- 1)
			END 
		END
	END
	RETURN @sDate
END 
GO


/* =============================================
   Function: DATEVALUE(date)
   Corresponding SQLBase function: @DATEVALUE 

   Description:
	This function converts the argument (string) to a date
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DATEVALUE]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[DATEVALUE]
GO

CREATE FUNCTION [dbo].[DATEVALUE]
	(@date varchar(40))
RETURNS datetime
AS
BEGIN
 RETURN CAST(CONVERT(datetime2, @date)AS datetime)
END
GO


/* =============================================
   Function: EXACT(string1, string2)
   Corresponding SQLBase function: @EXACT

   Description:
	This function compares two strings or numbers.
   If the strings are identical, the function returns 1; otherwise the function returns 0.
   This function is case sensitive.
   ============================================= */
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EXACT]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[EXACT]
GO

CREATE FUNCTION [dbo].[EXACT]
	(@string1 varchar(2000), @string2 varchar(2000))
RETURNS bit
AS
BEGIN
   DECLARE @len AS smallint
   DECLARE @i AS smallint
   IF @string1 IS NULL
		RETURN NULL
   IF DATALENGTH(@string1) = DATALENGTH(@string2)
	BEGIN
		SET @len = DATALENGTH(@string1)
		SET @i = 1
		WHILE (ASCII(SUBSTRING(@string1, @i, 1)) = 
			ASCII(SUBSTRING(@string2, @i, 1)) AND @i <= @len)
			SET @i = @i + 1
		IF @i = @len + 1
			RETURN 1
		ELSE
			RETURN 0
	END
   RETURN 0
END
GO

/* =============================================
   Function:FACTORIAL(x)

   Corresponding SQLBase function:@FACTORIAL

   Description:
	This function computes the factorial of the argument. The argument must be an INTEGER 
   (no decimal portion) and non-negative (>= 0). The upper limit is 69.  
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FACTORIAL]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FACTORIAL]
GO

CREATE FUNCTION [dbo].[FACTORIAL]
	(@x smallint)
RETURNS bigint
AS
BEGIN
  DECLARE @index smallint
  DECLARE @fact bigint

  IF @x = 0
    RETURN 1  

  SET @index = 1
  SET @fact = 1

  WHILE @index <= @x
     BEGIN
	   SET @fact = @fact * @index
	   SET @index = @index + 1 
     END

RETURN @fact
END
GO

/* =============================================
   Function:FV(pmt, int, n)

   Corresponding SQLBase function:@FV

   Description:
	This function returns the future value of a series of equal payments (pmt) 
   earning periodic interest rate (int) over the number of periods (n).

   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FV]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FV]
GO

CREATE FUNCTION [dbo].[FV]
	(@pmt float, @int float, @n float)
RETURNS float
AS
BEGIN
RETURN @pmt * ((POWER(1 + @int, @n) - 1) / @int)
END
GO

/* =============================================
   Function: GETHOUR(date)
   Corresponding SQLBase function: @HOUR

   Description:
	This function returns a number between 0 and 23 that represents the hour of the day.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETHOUR]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GETHOUR]
GO

CREATE FUNCTION [dbo].[GETHOUR]
	(@date datetime)
RETURNS tinyint
AS
BEGIN
  RETURN DATEPART(hour, @date) 
END
GO

/* =============================================
   Function: GETMINUTE(date)
   Corresponding SQLBase function: @MINUTE

   Description:
	This function returns a number between 0 and 59 that represents the minute of the hour.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETMINUTE]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GETMINUTE]
GO

CREATE FUNCTION [dbo].[GETMINUTE]
	(@date datetime)
RETURNS int
AS
BEGIN
  RETURN DATEPART(minute, @date) 
END
GO

/* =============================================
   Function: GETSECOND(date)
   Corresponding SQLBase function: @SECOND

   Description:
	This function returns a number between 0 and 59 that represents the second of a minute.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETSECOND]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GETSECOND]
GO

CREATE FUNCTION [dbo].[GETSECOND]
	(@date datetime)
RETURNS int
AS
BEGIN
  RETURN DATEPART(second, @date) 
END
GO

/* =============================================
   Function: TIME(hour, minute, second)
   Corresponding SQLBase function: @TIME

   Description:
	This function returns a time value given the hour, minute, and second. 
   An hour is a number from 0 to 23; a minute is a number from 0 to 59; a second is a number from 0 to 59.

   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETTIME]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GETTIME]
GO

CREATE FUNCTION [dbo].[GETTIME]
	(@hour tinyint, @minute tinyint, @second tinyint)
RETURNS datetime
AS
BEGIN
  RETURN  CONVERT(datetime, CAST(@hour AS varchar(2)) + ':' + CAST(@minute AS varchar(2)) + ':' + CAST(@second AS varchar(2)), 14)
END
GO

/* =============================================
   Function: GETWEEKDAY(date)
   Corresponding SQLBase function: @WEEKDAY

   Description:
	This function returns a number between 0 and 6 
   (Saturday = 0 and Friday = 6) that represents the day of the week.
   Make sure that SET DATEFIRST 6
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETWEEKDAY]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GETWEEKDAY]
GO

CREATE FUNCTION [dbo].[GETWEEKDAY]
	(@date datetime)
RETURNS tinyint
AS
BEGIN
  	DECLARE @weekDay tinyint
	
	SET @weekDay = (DATEPART(dw, @date) + @@DATEFIRST)%7
	
	IF @weekDay = 7
		SET @weekDay = 0
		
	RETURN @weekDay
END
GO

/* =============================================
   Function:HEX(number)

   Corresponding SQLBase function:@HEX

   Description:
	This function returns the hexadecimal equivalent for the given decimal number.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HEX]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[HEX]
GO

CREATE FUNCTION [dbo].[HEX]
	(@number int)
RETURNS varchar(10)
AS
BEGIN
  DECLARE @remainder int
  DECLARE @decimal varchar(10)
  DECLARE @char varchar(1)

  SET @decimal = ''

  WHILE @number > 0
	BEGIN
	   SET @remainder = @number % 16
           SET @number = @number / 16
	   IF @remainder <= 9
		SET @char = @remainder
	   ELSE 
		SET @char =
		   CASE @remainder
			WHEN 10 THEN 'A'
			WHEN 11 THEN 'B'	
			WHEN 12 THEN 'C'
			WHEN 13 THEN 'D'
			WHEN 14 THEN 'E'
			WHEN 15 THEN 'F'
		   END
	   SET @decimal = @decimal + @char
	END
	   
RETURN REVERSE(@decimal)
END
GO


/* =============================================
   Function: LICS(string)
   Corresponding SQLBase function: @LICS 

   Description:
	This function uses an international character set for sorting its argument, instead of the ASCII character set. 
   This is useful for sorting characters not in the English language.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LICS]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[LICS]
GO

CREATE FUNCTION [dbo].[LICS]
	(@string varchar(8000))
RETURNS varchar(8000)
AS
BEGIN
  DECLARE @i AS smallint
  DECLARE @result AS varchar(8000)
  DECLARE @char AS varchar(1)
  DECLARE @ascii AS smallint
  
  SET @result = ''
  SET @i = 1
  
  WHILE @i <= DATALENGTH(@string)
	BEGIN
		SET @char = SUBSTRING(@string, @i, 1)
		SET @ascii = ASCII(@char)
		SET @result = @result + CHAR(CASE 
					  	WHEN @ascii <= 68 THEN @ascii
						WHEN @ascii > 68 AND @ascii <= 78 THEN @ascii + 1
						WHEN @ascii > 78 AND @ascii <= 83 THEN @ascii + 2
						WHEN @ascii = 84 THEN 87
						WHEN @ascii = 85 THEN 88
						WHEN @ascii = 86 THEN 89
						WHEN @ascii = 87 THEN 90 
						WHEN @ascii = 88 THEN 91
						WHEN @ascii = 89 THEN 92 
						WHEN @ascii = 90 THEN 93
						WHEN @ascii = 91 THEN 99
						WHEN @ascii = 84 THEN 87
						WHEN @ascii = 85 THEN 88
						WHEN @ascii = 86 THEN 89
						WHEN @ascii = 87 THEN 90 
						WHEN @ascii = 88 THEN 91
						WHEN @ascii = 89 THEN 92 
						WHEN @ascii = 90 THEN 93
						WHEN @ascii = 91 THEN 99 
						WHEN @ascii = 92 THEN 100
						WHEN @ascii = 93 THEN 101
						WHEN @ascii = 94 THEN 102
						WHEN @ascii = 95 THEN 103 
						WHEN @ascii = 96 THEN 104
						WHEN @ascii = 97 THEN 65 
						WHEN @ascii = 98 THEN 66
						WHEN @ascii = 99 THEN 67 
						WHEN @ascii = 100 THEN 68
						WHEN @ascii = 101 THEN 70
						WHEN @ascii = 102 THEN 71
						WHEN @ascii = 103 THEN 72 
						WHEN @ascii = 104 THEN 73
						WHEN @ascii = 105 THEN 74 
						WHEN @ascii = 106 THEN 75
						WHEN @ascii = 107 THEN 76
						WHEN @ascii = 108 THEN 77
						WHEN @ascii = 109 THEN 78
						WHEN @ascii = 110 THEN 79
						WHEN @ascii = 111 THEN 81 
						WHEN @ascii = 112 THEN 82
						WHEN @ascii = 113 THEN 83 
						WHEN @ascii = 114 THEN 84
						WHEN @ascii = 115 THEN 85
						WHEN @ascii = 116 THEN 87
						WHEN @ascii = 117 THEN 88
						WHEN @ascii = 118 THEN 89
						WHEN @ascii = 119 THEN 90 
						WHEN @ascii = 120 THEN 91
						WHEN @ascii = 121 THEN 92 
						WHEN @ascii = 122 THEN 93
						WHEN @ascii >= 123 AND @ascii <= 191 THEN @ascii - 18
						WHEN @ascii >= 192 AND @ascii <= 197 THEN 65
						WHEN @ascii = 198 THEN 94
						WHEN @ascii = 199 THEN 67
						WHEN @ascii = 200 THEN 70 
						WHEN @ascii = 201 THEN 70
						WHEN @ascii = 202 THEN 70 
						WHEN @ascii = 203 THEN 70
						WHEN @ascii = 204 THEN 74
						WHEN @ascii = 205 THEN 74
						WHEN @ascii = 206 THEN 74
						WHEN @ascii = 207 THEN 74
						WHEN @ascii = 208 THEN 69 
						WHEN @ascii = 209 THEN 80
						WHEN @ascii = 210 THEN 81 
						WHEN @ascii = 211 THEN 81
						WHEN @ascii = 212 THEN 81
						WHEN @ascii = 213 THEN 81
						WHEN @ascii = 213 THEN 81
						WHEN @ascii = 214 THEN 81
						WHEN @ascii = 215 THEN 80 
						WHEN @ascii = 216 THEN 96
						WHEN @ascii = 217 THEN 88 
						WHEN @ascii = 218 THEN 88
						WHEN @ascii = 219 THEN 88
						WHEN @ascii = 220 THEN 88
						WHEN @ascii = 221 THEN 92
						WHEN @ascii = 222 THEN 98
						WHEN @ascii = 223 THEN 86 
						WHEN @ascii = 224 THEN 65
						WHEN @ascii = 225 THEN 65 
						WHEN @ascii = 226 THEN 65
						WHEN @ascii = 227 THEN 65
						WHEN @ascii = 228 THEN 65
						WHEN @ascii = 229 THEN 65
						WHEN @ascii = 230 THEN 95
						WHEN @ascii = 231 THEN 67 
						WHEN @ascii = 232 THEN 70
						WHEN @ascii = 233 THEN 70 
						WHEN @ascii = 234 THEN 70
						WHEN @ascii = 235 THEN 70
						WHEN @ascii = 236 THEN 74 
						WHEN @ascii = 237 THEN 74
						WHEN @ascii = 238 THEN 74 
						WHEN @ascii = 239 THEN 74
						WHEN @ascii = 240 THEN 69
						WHEN @ascii = 241 THEN 80
						WHEN @ascii = 242 THEN 81
						WHEN @ascii = 243 THEN 81
						WHEN @ascii = 244 THEN 81 
						WHEN @ascii = 245 THEN 81
						WHEN @ascii = 246 THEN 81 
						WHEN @ascii = 247 THEN 80
						WHEN @ascii = 248 THEN 81
						WHEN @ascii = 249 THEN 88
						WHEN @ascii = 250 THEN 88
						WHEN @ascii = 251 THEN 88
						WHEN @ascii = 252 THEN 88 
						WHEN @ascii = 253 THEN 92
						WHEN @ascii = 254 THEN 174 
					END)
  		SET @i = @i + 1
 	END 
  RETURN @result

END
GO


/* =============================================
   Function: MICROSECOND(date)
   Corresponding SQLBase function: @MICROSECOND

   Description:
	This function returns the microsecond value in a DATETIME value. 
   If a microsecond quantity was not specified on input, zero is returned.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MICROSECOND]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[MICROSECOND]
GO

CREATE FUNCTION [dbo].[MICROSECOND]
	(@date datetime)
RETURNS int 
AS
BEGIN
  RETURN DATEPART(millisecond, @date) * 1000 
END
GO

/* =============================================
   Function: MOD(x, y)
   Corresponding SQLBase function: @MOD

   Description:
	This function returns the modulo (remainder) of x/y.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MOD]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[MOD]
GO

CREATE FUNCTION [dbo].[MOD]
	(@x int, @y int)
RETURNS int
AS
BEGIN
  RETURN (@x % @y) 
END
GO


/* =============================================
   Function: MONTHBEG(date)

   Corresponding SQLBase function: @MONTHBEG

   Description:
	Returns the first day of the month represented by the date.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MONTHBEG]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[MONTHBEG]
GO

CREATE FUNCTION [dbo].[MONTHBEG]
	(@date datetime)
RETURNS datetime
AS
BEGIN
  RETURN @date-DATEPART(dd,@date)+1
END
GO


/* =============================================
   Function: PMT(prin, int, n)

   Corresponding SQLBase function: @PMT

   Description:
	This function returns the amount of each periodic payment needed to pay off a 
   loan principal (prin) at a periodic interest rate (int) over a number of periods (n).
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMT]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[PMT]
GO


CREATE FUNCTION [dbo].[PMT]
	(@prin float, @int float, @n float)
RETURNS float
AS
BEGIN
  RETURN (@prin * @int)/(1 - POWER(1 + @int,-@n))   
END
GO


/* =============================================
   Function: PROPER(string)
   Corresponding SQLBase function: @PROPER

   Description:
	This function converts the first character of each word in a string to 
   uppercase and other characters to lower case.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROPER]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[PROPER]
GO

CREATE FUNCTION [dbo].[PROPER]
	(@string varchar(8000))
RETURNS varchar(8000)
AS
BEGIN
  DECLARE @index smallint
  DECLARE @char char(1)
  DECLARE @temp char(1)

  SET @index = 1
  SET @string = LOWER(@string)
  
  SET @char = SUBSTRING(@string, 1, 1)
  IF @char != ' '
  	SET @string = STUFF(@string, 1, 1, UPPER(@char))
  
  WHILE @index < DATALENGTH(@string)
	BEGIN   
	   SET @temp = SUBSTRING(@string, @index, 1)
	   SET @index = @index + 1
	   SET @char = SUBSTRING(@string, @index, 1)
	   IF @char >= 'a' AND @char <='z' AND @temp = ' '
		SET @string = STUFF(@string, @index, 1, UPPER(@char))
	 END     
  
  RETURN @string
END
GO

/* =============================================
   Function: PV(pmt, int, n)
   Corresponding SQLBase function: @PV

   Description:
	This function returns the present value of a series of equal payments (pmt) 
   discounted at periodic interest rate (int) over the number of periods (n).
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PV]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[PV]
GO

CREATE FUNCTION [dbo].[PV]
	(@pmt float, @int float, @n float)
RETURNS float
AS
BEGIN
  RETURN @pmt * (1 - POWER(1 + @int, -@n)) / @int 
END
GO

/* =============================================
   Function: GETQUARTER(date)
   Corresponding SQLBase function: @QUARTER 

   Description:
	This function returns a number between 1 and 4 that represents the quarter. 
   For Example, the first quarter of the year is January through March.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETQUARTER]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GETQUARTER]
GO

CREATE FUNCTION [dbo].[GETQUARTER]
	(@date datetime)
RETURNS int
AS
BEGIN
  RETURN DATEPART(quarter, @date) 
END
GO

/* =============================================
   Function: QUARTERBEG(date)
   Corresponding SQLBase function: @QUARTERBEG 

   Description:
	This function returns the first day of the quarter represented by the date.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[QUARTERBEG]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[QUARTERBEG]
GO

CREATE FUNCTION [dbo].[QUARTERBEG]
	(@date datetime)
RETURNS varchar(15) 
AS
BEGIN
  DECLARE @string varchar(10)
  SET @string = 
  	CASE DATEPART(qq,@date)
		WHEN 1 THEN '01/01/'
		WHEN 2 THEN '04/01/'
		WHEN 3 THEN '07/01/'
		WHEN 4 THEN '10/01/'
	END
  SET @string = @string + STR(DATEPART(yy, @date))
  RETURN @string
END
GO

/* =============================================
   Function: RATE(fv, pv, n)
   Corresponding SQLBase function: @RATE

   Description:
	This function returns the interest rate for an investment of present value (pv) 
   to grow to a future value (fv) over the number of compounding periods (n).

   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RATE]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[RATE]
GO

CREATE FUNCTION [dbo].[RATE]
	(@fv float, @pv float, @n float)
RETURNS float
AS
BEGIN
  RETURN ((@fv / @pv)* (1 / @n)) - 1	
END
GO

/* =============================================
   Function: SLN(cost, salvage, life)
   Corresponding SQLBase function: @SLN

   Description:
	This function returns the straight-line depreciation allowance of an asset for each period, 
   given the base cost, predicted salvage value, and expected life of the asset.

   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SLN]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SLN]
GO

CREATE FUNCTION [dbo].[SLN]
	(@cost float, @salvage float, @life float)
RETURNS float
AS
BEGIN
  RETURN (@cost - @salvage) / @life
END
GO

/* =============================================
   Function: STRING
   Corresponding SQLBase function: @STRING

   Description:
	This function converts a number into a string with the number of decimal places 
	specified by scale. Numbers are rounded where appropriate.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STRING]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[STRING]
GO

CREATE FUNCTION [dbo].[STRING]
	(@number float, @scale int)
RETURNS varchar(8000)
AS
BEGIN
  RETURN LTRIM(STR(@number, 100, @scale)) 
END
GO

/* =============================================
   Function: SYD(cost, sal, life, per)
   Corresponding SQLBase function: @SYD

   Description:
	This function returns the Sum-of-the-Years'-Digits depreciation allowance 
   of an asset for a given period, given the base cost, predicted salvage value, 
   expected life of the asset and specific period.

   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYD]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[SYD]
GO

CREATE FUNCTION [dbo].[SYD]
	(@cost float, @sal float, @life float, @per float)
RETURNS float
AS
BEGIN
  RETURN (@cost - @sal) * (@life - @per + 1) / (@life * (@life + 1)/2)	
END
GO

/* =============================================
   Function: TERM(pmt, int, fv)

   Corresponding SQLBase function: @TERM
 
   Description:
	This function returns the number of payment periods for an investment, 
   given the amount of each payment pmt, the periodic interest rate int, 
   and the future value fv of the investment.

   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TERM]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[TERM]
GO

CREATE FUNCTION [dbo].[TERM]
	(@pmt float, @int float, @fv float)
RETURNS float
AS
BEGIN
  RETURN LOG(1 + (@fv * @int/@pmt)) / LOG(1 + @int)		
END
GO

/* =============================================
   Function: TIMEVALUE(time)
   Corresponding SQLBase function: @TIMEVALUE

   Description:
	The function returns a time value, given a string in the form HH:MM:SS [AM or PM]. 
If the AM or PM parameter is omitted, military time is used. 
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TIMEVALUE]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[TIMEVALUE]
GO

CREATE FUNCTION [dbo].[TIMEVALUE]
	(@time datetime)
RETURNS datetime
AS
BEGIN
RETURN '30.12.1899 ' + 
		CONVERT(nvarchar, datepart( hh, @time)) + ':' +
		CONVERT(nvarchar, datepart( mi, @time )) + ':' +
		CONVERT(nvarchar, datepart( ss, @time ))
END
GO

/* =============================================
   Function: TODATE(year, month, day)
   Corresponding SQLBase function: @DATE

   Description:
	This function converts the arguments to a date.
   The data type of the result is datetime.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TODATE]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[TODATE]
GO

CREATE FUNCTION [dbo].[TODATE]
	(@year smallint, @month smallint, @day smallint)
RETURNS datetime
AS
BEGIN
	IF @year IS NULL OR @month IS NULL OR @day IS NULL
		RETURN NULL
  RETURN CAST(@year AS varchar(4)) +  '-' + CAST(@month AS varchar(2)) 
	  + '-' + CAST(@day AS varchar(2))
END
GO

/* =============================================
   Function: TODECIMAL(string)

   Corresponding SQLBase function:@DECIMAL

   Description:
	This function returns the decimal equivalent for the given hexadecimal number.  
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TODECIMAL]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[TODECIMAL]
GO

CREATE FUNCTION [dbo].[TODECIMAL]
	(@string nvarchar(5))
RETURNS decimal
AS
BEGIN

DECLARE @index smallint
DECLARE @character varchar(1)
DECLARE @result int
DECLARE @number int

SET @index = DATALENGTH(@string)
SET @result = 0
SET @string = UPPER(@string)

WHILE (@index > 0)
 BEGIN
    SET @character = SUBSTRING(@string, @index,1)
	SET @number = 
	  CASE @character
		WHEN '0' THEN 0
		WHEN '1' THEN 1
		WHEN '2' THEN 2
		WHEN '3' THEN 3
		WHEN '4' THEN 4
		WHEN '5' THEN 5
		WHEN '6' THEN 6
		WHEN '7' THEN 7
		WHEN '8' THEN 8
		WHEN '9' THEN 9
		WHEN 'A' THEN 10
		WHEN 'B' THEN 11
		WHEN 'C' THEN 12
		WHEN 'D' THEN 13
		WHEN 'E' THEN 14
		WHEN 'F' THEN 15
	  END
     SET @result = @result + @number * POWER(16, DATALENGTH(@string) - @index)
     SET @index = @index - 1
 END
	
RETURN @result
END
GO

/* =============================================
   Function: TOINT(x)
   Corresponding SQLBase function: @INT

   Description:
	This function returns the integer portion of x. 
   If x is negative, the decimal portion is truncated.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TOINT]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[TOINT]
GO

CREATE FUNCTION [dbo].[TOINT]
	(@x decimal(20,10))
RETURNS int
AS
BEGIN
  RETURN ROUND(@x,0,1)
END
GO

/* =============================================
   Function: TRIM(string)
   Corresponding SQLBase function: @TRIM

   Description:
	This function strips leading and trailing blanks from a string and 
   compresses multiple spaces within the string into single spaces.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TRIM]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[TRIM]
GO

CREATE FUNCTION [dbo].[TRIM]
	(@string varchar(8000))
RETURNS varchar(8000)
AS
BEGIN
  RETURN LTRIM(RTRIM(@string)) 
END
GO

/* =============================================
   Function: VALUE(string)
   Corresponding SQLBase function: @VALUE 

   Description:
	This function converts a character string that has the digits (0-9) and an optional decimal point 
   or negative sign into the number represented by that string.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VALUE]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[VALUE]
GO

CREATE FUNCTION [dbo].[VALUE]
	(@string varchar(50))
RETURNS float
AS
BEGIN
  RETURN CAST(@string AS float) 
END
GO


/* =============================================
   Function: WEEKBEG(date)
   Corresponding SQLBase function: @WEEKBEG

   Description:
	This function returns the date of the Monday of the week containing the date. 
   This is the previous Monday if the date is not a Monday, and the date value itself if it is a Monday.
   Before using this functions make sure that SET DATEFIRST 1
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WEEKBEG]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[WEEKBEG]
GO

CREATE FUNCTION [dbo].[WEEKBEG]
	(@date datetime)
RETURNS datetime
AS
BEGIN
   RETURN DATEADD(dd, 1 -DATEPART(dw, @date),@date)
END
GO


/* =============================================
   Function: YEARBEG(date)
   Corresponding SQLBase function: @YEARBEG

   Description:
	This function returns the first day of the year represented by the date.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YEARBEG]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[YEARBEG]
GO


CREATE FUNCTION [dbo].[YEARBEG]
	(@date datetime)
RETURNS datetime
AS
BEGIN
  RETURN '01/01/' + CAST(YEAR(@date) AS varchar(4))
END
GO


/* =============================================
   Function: YEARNO(date)
   Corresponding SQLBase function: @YEAR

   Description:
	This function returns a number between -1900 and +200 that represents the year relative to 1900. 
   The year 1900 is 0, 1986 is 86, and 2000 is 100. Years before 1900 are negative numbers and 1899 is -1.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[YEARNO]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[YEARNO]
GO


CREATE FUNCTION [dbo].[YEARNO]
	(@date datetime)
RETURNS smallint
AS
BEGIN
  RETURN YEAR(@date) - 1900
END
GO


/* =============================================
   Function: LENGTH(string)
   Corresponding SQLBase function: @LENGTH

   Description:
	This function returns a number representing the length of the string parameter.
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LENGTH]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[LENGTH]
GO

CREATE FUNCTION [dbo].[LENGTH]
(
	@string nvarchar(max)
)
RETURNS int
AS
BEGIN
	IF @string IS NULL
		RETURN 0
	RETURN DATALENGTH(@string)
END
GO

/* =============================================
   Function: CURRENTDATE
   Corresponding SQLBase function: CURRENT DATE

   Description:
	This function returns the date part of the current datetime
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CURRENTDATE]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[CURRENTDATE]
GO


CREATE FUNCTION [dbo].[CURRENTDATE]()
RETURNS datetime
AS
BEGIN
	RETURN Dateadd(hh, -datepart(hh, current_timestamp), 
					Dateadd(mi, -datepart(mi, current_timestamp), 
					Dateadd(ss, -datepart(ss, current_timestamp), 
					Dateadd(ms, -datepart(ms, current_timestamp),current_timestamp))))
END
GO

/* =============================================
   Function: CURRENTTIME
   Corresponding SQLBase function: CURRENT TIME

   Description:
	This function returns the time part of the current datetime
   ============================================= */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CURRENTTIME]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[CURRENTTIME]
GO

CREATE FUNCTION [dbo].[CURRENTTIME]()
RETURNS datetime
AS
BEGIN
	RETURN Dateadd(ss, datepart(ss, current_timestamp), 
					Dateadd(mi, datepart(mi, current_timestamp),
					Dateadd(hh, datepart(hh, current_timestamp), '1899-12-30')))
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETNORESULTS]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT', N'P'))
DROP PROCEDURE [dbo].[GETNORESULTS]
GO

CREATE PROCEDURE [dbo].[GETNORESULTS] 
AS
BEGIN
RETURN 0
END
GO

DECLARE @USERNAME VARCHAR(500)
DECLARE @STRSQL NVARCHAR(MAX)
SET @USERNAME=' public '
SET @STRSQL=''

select @STRSQL+=CHAR(13)+ 'GRANT EXECUTE ON ['+obj.name+'] TO public' +';'
from sys.objects obj where obj.type in ('FN') and obj.is_ms_shipped = 0

EXEC SP_EXECUTESQL @STRSQL
GO