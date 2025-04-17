
--    CREATE TABLE ToDoItems(
--    ToDoItemId BIGINT PRIMARY KEY IDENTITY(1,1), 
--    Title NVARCHAR(255) NOT NULL,
--    Description NVARCHAR(MAX) NULL,
--    IsCompleted BIT NOT NULL DEFAULT 0,
--    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(), 
--    DueDate DATETIME NOT NULL                     
--);
--CREATE PROCEDURE sp_AddToDoItem
    
--    @Title NVARCHAR(255),
--    @Description NVARCHAR(MAX),  
--    @DueDate DATETIME
--AS
--BEGIN
--    SET NOCOUNT ON;
--	INSERT INTO ToDoItems(Title,Description,DueDate)
--	Values(@Title,@Description,@DueDate)
    
--    END;

    
--    CREATE PROCEDURE sp_DeleteToDoItem
--@ToDoItemId BIGINT
--As
--BEGIN
--DELETE FROM ToDoItems WHERE ToDoItemId=@ToDoItemId
--END



--CREATE PROCEDURE sp_UpdateToDoItem
--    @ToDoItemId BIGINT,
--    @Title NVARCHAR(255),
--    @Description NVARCHAR(MAX),
--    @IsCompleted BIT,
--    @DueDate DATETIME
--AS
--BEGIN
--    SET NOCOUNT ON;

--    UPDATE ToDoItems
--    SET
--        Title = @Title,
--        Description = @Description,
--        IsCompleted = @IsCompleted,
--        DueDate = @DueDate
--    WHERE ToDoItemId = @ToDoItemId;
--    END;

--    CREATE PROCEDURE sp_GetAllToDoItems
--As
--BEGIN
--SELECT*FROM ToDoItems
--END

--CREATE PROCEDURE sp_GetToDoItemById
--@ToDoItemId BIGINT
--As
--BEGIN
--SELECT*FROM ToDoItems WHERE ToDoItemId=@ToDoItemId
--END

--CREATE PROCEDURE sp_GetByDueDate
--@DueDate DateTime
--As
--BEGIN
--SELECT*FROM ToDoItems WHERE DueDate=@DueDate
--END


--CREATE PROCEDURE sp_GetCompleted
--@GetCompleted BIT
--As
--BEGIN
--SELECT*FROM ToDoItems WHERE IsCompleted=@GetCompleted
--END

--CREATE PROCEDURE sp_GetInCompleted
--@GetInCompleted BIT
--As
--BEGIN
--SELECT*FROM ToDoItems WHERE IsCompleted=@GetInCompleted
--END
