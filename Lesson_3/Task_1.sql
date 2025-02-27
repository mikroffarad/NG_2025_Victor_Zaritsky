-- Step 1: Create tables
CREATE TABLE [User] (
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
	CONSTRAINT FK_Project_User FOREIGN KEY (CreatorId) REFERENCES [User](Id) ON DELETE CASCADE,
	CONSTRAINT FK_Project_Category FOREIGN KEY (CategoryId) REFERENCES Category(Id) ON DELETE NO ACTION
);

CREATE TABLE Comment (
	Id INT PRIMARY KEY IDENTITY,
	Text NVARCHAR(MAX) NOT NULL,
	Date DATETIME DEFAULT GETDATE() NOT NULL,
	UserId INT NOT NULL,
	ProjectId INT NOT NULL,
	CONSTRAINT FK_Comment_User FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE NO ACTION,
	CONSTRAINT FK_Comment_Project FOREIGN KEY (ProjectId) REFERENCES [Project](Id) ON DELETE CASCADE
);

CREATE TABLE Vote (
	Id INT PRIMARY KEY IDENTITY,
	UserId INT NOT NULL,
	ProjectId INT NOT NULL,
	CONSTRAINT UQ_Vote_User_Project UNIQUE(UserId, ProjectId),
	CONSTRAINT FK_Vote_User FOREIGN KEY (UserId) REFERENCES [User](Id) ON DELETE NO ACTION,
	CONSTRAINT FK_Vote_Project FOREIGN KEY (ProjectId) REFERENCES Project(Id) ON DELETE CASCADE
);
GO

-- Step 2: Add some users and categories
INSERT INTO [User] (Name, SecondName)
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
SELECT * From [User];
SELECT * From Category;
GO

-- Step 3: CreateProject procedure
CREATE PROCEDURE CreateProject
    @Name NVARCHAR(100),
    @Description NVARCHAR(500),
    @CreatorId INT,
    @CategoryId INT,
    @NewProjectId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Check if the field with the same name and description exists
        IF EXISTS (
            SELECT 1 FROM Project 
            WHERE Name = @Name AND Description = @Description
        )
        BEGIN
            SET @NewProjectId = -1; -- Project already exists
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Inserting new unique project
        INSERT INTO Project (Name, Description, CreationDate, CreatorId, CategoryId)
        VALUES (@Name, @Description, GETDATE(), @CreatorId, @CategoryId);

        -- Returning Id of new project
        SET @NewProjectId = SCOPE_IDENTITY();

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

-- Calling procedure to create project
DECLARE @ProjectId INT;
EXEC CreateProject
	@Name = 'AI Startup',
	@Description = 'An innovative AI project',
	@CreatorId = 1,
	@CategoryId = 1,
	@NewProjectId = @ProjectId OUTPUT;

-- View Project table
select * from project
GO