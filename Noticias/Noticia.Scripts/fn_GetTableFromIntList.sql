ALTER FUNCTION [dbo].[fn_GetTableFromIntList]
(@strIntList VARCHAR(MAX),
 @strDelimiter VARCHAR(10)
)
 
RETURNS @tblList TABLE (ID INT NOT NULL)
 
AS
 
BEGIN
 
DECLARE    @iStartPos INT,@iEndPos INT,@strValue VARCHAR(15)
SET @iStartPos = 1
SET @strIntList = @strIntList + @strDelimiter
SET @iEndPos = CHARINDEX(@strDelimiter, @strIntList)
 
WHILE @iEndPos > 0
 
BEGIN
 
    SET @strValue = SUBSTRING(@strIntList, @iStartPos, @iEndPos - @iStartPos)
    INSERT @tblList (ID) VALUES(CONVERT(INT, @strValue))
    SET @iStartPos = @iEndPos + 1
    SET @iEndPos = CHARINDEX(@strDelimiter, @strIntList, @iStartPos)
END
RETURN
END