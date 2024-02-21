-- Create a new table called '[ApprovalHistory]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ApprovalHistory]', 'U') IS NOT NULL
DROP TABLE [dbo].[ApprovalHistory]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[ApprovalHistory]
(
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    ActionDate DATETIME,
    ActionType INT,
    IdUser INT,
    UserName VARCHAR(500),
    IdTimesheetControl INT,
	ApprovalId INT,
    TimesheetType INT,
    Comments VARCHAR(MAX)
    FOREIGN KEY (IdUser) REFERENCES [User](Id)
)
GO