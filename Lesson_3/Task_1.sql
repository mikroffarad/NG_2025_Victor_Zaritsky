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
        -- Check if the field with the same name and description exists
        IF EXISTS (
            SELECT 1 FROM Project 
            WHERE Name = @Name AND Description = @Description
        )
        BEGIN
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Inserting new unique project
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
-- CreateProject 3 (shouldn't be created because project with the same name and description already exists)
DECLARE @ProjectId INT;
EXEC CreateProject
	@Name = 'AI Startup',
	@Description = 'An innovative AI project',
	@CreatorId = 1,
	@CategoryId = 1 -- Technology
GO
-- CreateProject 4
DECLARE @ProjectId INT;
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

        -- Creating comment
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

-- View Project table, there should be 3 comments
select * from Comment
GO