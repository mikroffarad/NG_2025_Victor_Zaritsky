-- Step 1: Create tables
CREATE TABLE Users (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL,
	SecondName NVARCHAR(100) NOT NULL
);

CREATE TABLE Category (
	Id INT PRIMARY KEY IDENTITY,
	Description NVARCHAR(500) NOT NULL
);

CREATE TABLE Project (
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100) NOT NULL,
	Description NVARCHAR(500) NOT NULL,
	CreationDate DATETIME DEFAULT GETDATE() NOT NULL,
	CreatorId INT NOT NULL,
	CategoryId INT NOT NULL,
	CONSTRAINT FK_Project_User FOREIGN KEY (CreatorId) REFERENCES Users(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Project_Category FOREIGN KEY (CategoryId) REFERENCES Category(Id) ON DELETE NO ACTION
);

CREATE TABLE Comment (
	Id INT PRIMARY KEY IDENTITY,
	Text NVARCHAR(MAX) NOT NULL,
	Date DATETIME DEFAULT GETDATE() NOT NULL,
	UserId INT NOT NULL,
	ProjectId INT NOT NULL,
	CONSTRAINT FK_Comment_User FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE NO ACTION,
	CONSTRAINT FK_Comment_Project FOREIGN KEY (ProjectId) REFERENCES [Project](Id) ON DELETE CASCADE
);

CREATE TABLE Vote (
	Id INT PRIMARY KEY IDENTITY,
	UserId INT NOT NULL,
	ProjectId INT NOT NULL,
	CONSTRAINT UQ_Vote_User_Project UNIQUE(UserId, ProjectId),
	CONSTRAINT FK_Vote_User FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE NO ACTION,
	CONSTRAINT FK_Vote_Project FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE
);
GO

-- Step 2: Add some users and categories
INSERT INTO Users (Name, SecondName)
VALUES
	('John', 'Doe'),
	('Alice', 'Smith'),
	('Bob', 'Johnson');
INSERT INTO Category (Description) 
VALUES 
    ('Technology'),
    ('Education'),
    ('Health');

-- View User and Category tables
SELECT * From Users;
SELECT * From Category;
GO

-- Step 3: CreateProject procedure
CREATE PROCEDURE CreateProject
    @Name NVARCHAR(100),
    @Description NVARCHAR(500),
    @CreatorId INT,
    @CategoryId INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
		-- Check if user not exists
        IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @CreatorId)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN;
        END

		-- Check if category not exists
        IF NOT EXISTS (SELECT 1 FROM Category WHERE Id = @CategoryId)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Creating new project
        INSERT INTO Project (Name, Description, CreationDate, CreatorId, CategoryId)
        VALUES (@Name, @Description, GETDATE(), @CreatorId, @CategoryId);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- Call procedure to create project
-- You should run this procedure several times to test GetPaginatedProjects procedure (view step 7)
-- CreateProject 1
EXEC CreateProject
	@Name = 'AI Startup',
	@Description = 'An innovative AI project',
	@CreatorId = 1,
	@CategoryId = 1 -- Technology
GO
-- CreateProject 2
EXEC CreateProject 
    @Name = 'Online Learning Platform',
    @Description = 'An interactive platform for online education',
    @CreatorId = 2,
    @CategoryId = 2 -- Education
GO
-- CreateProject 3 (shouldn't be create because of non-exist user)
EXEC CreateProject 
    @Name = 'Online Learning Platform',
    @Description = 'An interactive platform for online education',
    @CreatorId = 999, -- User not exists
    @CategoryId = 2 -- Education
GO
-- CreateProject 4 (shouldn't be create because of non-exist category)
EXEC CreateProject 
    @Name = 'Online Learning Platform',
    @Description = 'An interactive platform for online education',
    @CreatorId = 2, 
    @CategoryId = 999 -- Category not exists
GO
-- CreateProject 5
EXEC CreateProject 
    @Name = 'AI-Powered Diagnosis Tool',
    @Description = 'An AI-based tool to assist doctors in diagnostics',
    @CreatorId = 3,
    @CategoryId = 3 -- Health
GO

-- View Project table, there should be 3 projects
select * from project
GO

-- Step 4: CreateСomment procedure
CREATE PROCEDURE CreateComment
    @Text NVARCHAR(MAX),
    @UserId INT,
    @ProjectId INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Check if user exists
        IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Check if project exists
        IF NOT EXISTS (SELECT 1 FROM Project WHERE Id = @ProjectId)
        BEGIN
	        ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Creating new comment
        INSERT INTO Comment (Text, Date, UserId, ProjectId)
        VALUES (@Text, GETDATE(), @UserId, @ProjectId);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

-- CreateComment 1 (Valid comment)
EXEC CreateComment 
    @Text = 'This is a great project!', 
    @UserId = 3, 
    @ProjectId = 1;
GO
-- CreateComment 2 (Shouldn't be created because of non-exist user)
EXEC CreateComment 
    @Text = 'Invalid user test', 
    @UserId = 999, 
    @ProjectId = 1;
GO

-- CreateComment 3 (Shouldn't be created because of non-exist project)
EXEC CreateComment 
    @Text = 'Invalid project test', 
    @UserId = 1, 
    @ProjectId = 999;
GO

-- CreateComment 4
EXEC CreateComment 
    @Text = 'Keep it up!', 
    @UserId = 1, 
    @ProjectId = 2;
GO

-- CreateComment 5
EXEC CreateComment 
    @Text = 'That is the best thing I have ever seen', 
    @UserId = 2, 
    @ProjectId = 3;
GO

-- View Comment table, there should be 3 comments
select * from Comment
GO

-- Step 5: GetProjectInfo procedure
CREATE PROCEDURE GetProjectInfo
    @ProjectId INT
AS
BEGIN
    -- Check if project not exists
    IF NOT EXISTS (SELECT 1 FROM Project WHERE Id = @ProjectId)
    BEGIN
        RETURN;
    END

    -- Project info
    SELECT 
        P.Id, 
        P.Name, 
        P.Description, 
        P.CreationDate, 
        U.Name + ' ' + U.SecondName AS Creator, 
        C.Description AS Category, 
        (SELECT COUNT(*) FROM Vote V WHERE V.ProjectId = P.Id) AS VotesCount
    FROM Project P
    JOIN Users U ON P.CreatorId = U.Id
    JOIN Category C ON P.CategoryId = C.Id
    WHERE P.Id = @ProjectId;

    -- Comments info
    SELECT 
        C.Id, 
        C.Text, 
        C.Date, 
        U.Name + ' ' + U.SecondName AS UserName
    FROM Comment C
    JOIN Users U ON C.UserId = U.Id
    WHERE C.ProjectId = @ProjectId
    ORDER BY C.Date ASC;
END
GO

-- Check projects info by their IDs (1-3)
EXEC GetProjectInfo @ProjectId = 2;
GO

-- Check non-exist project
EXEC GetProjectInfo @ProjectId = 99;
GO

-- Step 6: GetPaginatedProjects procedure
CREATE PROCEDURE GetPaginatedProjects
    @PageNumber INT,
    @PageSize INT,
    @ProjectName NVARCHAR(100) = NULL,
    @CategoryId INT = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;

	-- Define offset
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

	-- Get filtered projects with pagination
    SELECT 
        P.Id, 
        P.Name, 
        P.Description, 
        P.CreationDate, 
        U.Name + ' ' + U.SecondName AS Creator, 
        C.Description AS Category,
        (SELECT COUNT(*) FROM Vote V WHERE V.ProjectId = P.Id) AS VotesCount
    FROM Project P
    JOIN Users U ON P.CreatorId = U.Id
    JOIN Category C ON P.CategoryId = C.Id
    WHERE 
        (@ProjectName IS NULL OR P.Name LIKE '%' + @ProjectName + '%')
        AND (@CategoryId IS NULL OR P.CategoryId = @CategoryId)
        AND (@StartDate IS NULL OR P.CreationDate >= @StartDate)
        AND (@EndDate IS NULL OR P.CreationDate <= @EndDate)
    ORDER BY P.CreationDate DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
END;
GO

-- Make sure you added enough projects to test this procedure (view Step 3)
-- Get first page which contains 5 projects
EXEC GetPaginatedProjects @PageNumber = 1, @PageSize = 5;
GO
-- Get third page which contains 3 projects about Technology (1)
EXEC GetPaginatedProjects @PageNumber = 3, @PageSize = 3, @CategoryId = 1;
GO
-- Get second page which contains 10 projects made after February 28, 2025
EXEC GetPaginatedProjects @PageNumber = 2, @PageSize = 10, @StartDate = '2025-02-28';
GO

-- Step 7: AddVote procedure
CREATE PROCEDURE AddVote
    @UserId INT,
    @ProjectId INT
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Check if user exists
        IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Check if project exists
        IF NOT EXISTS (SELECT 1 FROM Project WHERE Id = @ProjectId)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Check if user already voted
        IF EXISTS (SELECT 1 FROM Vote WHERE UserId = @UserId AND ProjectId = @ProjectId)
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Add vote
        INSERT INTO Vote (UserId, ProjectId)
        VALUES (@UserId, @ProjectId);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

-- User 1 adds vote to Project 1 first time
EXEC AddVote @UserId = 1, @ProjectId = 1;
GO
-- User 1 adds vote to Project 1 first time (it fails)
EXEC AddVote @UserId = 1, @ProjectId = 1;
GO
-- Non-exist user adds vote to Project 1 (it fails)
EXEC AddVote @UserId = 999, @ProjectId = 1;
GO
-- User 1 adds vote to non-exist project (it fails)
EXEC AddVote @UserId = 1, @ProjectId = 999;
GO

-- View Vote table
SELECT * from Vote;

-- Make sure vote is added
EXEC GetProjectInfo @ProjectId = 1;