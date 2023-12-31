USE [master]
GO
/****** Object:  Database [Timesheet]    Script Date: 2021-09-29 8:36:24 PM ******/
CREATE DATABASE [Timesheet]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RepliconCloudData', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RepliconCloudData.mdf' , SIZE = 1392640KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RepliconCloudData_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RepliconCloudData_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Timesheet] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Timesheet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Timesheet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Timesheet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Timesheet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Timesheet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Timesheet] SET ARITHABORT OFF 
GO
ALTER DATABASE [Timesheet] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Timesheet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Timesheet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Timesheet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Timesheet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Timesheet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Timesheet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Timesheet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Timesheet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Timesheet] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Timesheet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Timesheet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Timesheet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Timesheet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Timesheet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Timesheet] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Timesheet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Timesheet] SET RECOVERY FULL 
GO
ALTER DATABASE [Timesheet] SET  MULTI_USER 
GO
ALTER DATABASE [Timesheet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Timesheet] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Timesheet] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Timesheet] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Timesheet] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Timesheet] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Timesheet] SET QUERY_STORE = OFF
GO
USE [Timesheet]
GO
/****** Object:  User [WTSRate]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE USER [WTSRate] FOR LOGIN [WTSRate] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [timeuser]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE USER [timeuser] FOR LOGIN [timeuser] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [sergiobarbosa]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE USER [sergiobarbosa] FOR LOGIN [HDINC-VAN\SergioBarbosa] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [replicondatasync]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE USER [replicondatasync] FOR LOGIN [replicondatasync] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [WTSRate]
GO
ALTER ROLE [db_owner] ADD MEMBER [timeuser]
GO
ALTER ROLE [db_owner] ADD MEMBER [sergiobarbosa]
GO
ALTER ROLE [db_owner] ADD MEMBER [replicondatasync]
GO
/****** Object:  UserDefinedTableType [dbo].[TimeSheetDetailsType]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE TYPE [dbo].[TimeSheetDetailsType] AS TABLE(
	[timesheetperiod] [datetime] NOT NULL,
	[startdate] [datetime] NOT NULL,
	[enddate] [datetime] NOT NULL,
	[entrydate] [datetime] NOT NULL,
	[userid] [int] NOT NULL,
	[DepartmentId] [int] NULL,
	[supervisorid] [int] NULL,
	[projectid] [int] NULL,
	[projectcode] [nvarchar](255) NULL,
	[projectname] [nvarchar](255) NULL,
	[projectdescription] [nvarchar](255) NULL,
	[billingmethod] [nvarchar](255) NULL,
	[clientid] [int] NULL,
	[billingpercentage_ratio] [numeric](19, 4) NULL,
	[projectleaderid] [int] NULL,
	[clientuserid] [int] NULL,
	[tasktimeoffid] [int] NULL,
	[tasktimeoffname] [nvarchar](255) NULL,
	[htasktimeoffname] [nvarchar](255) NULL,
	[tasktimeoffcode] [nvarchar](50) NULL,
	[ActivityId] [int] NULL,
	[ActivityName] [nvarchar](255) NULL,
	[ActivityCode] [nvarchar](50) NULL,
	[billable] [int] NULL,
	[billablename] [nvarchar](255) NULL,
	[non_billable_hours] [decimal](18, 6) NULL,
	[billable_hours] [decimal](18, 6) NULL,
	[time_off_hours] [decimal](18, 12) NULL,
	[project_hours] [decimal](18, 6) NULL,
	[total_hours] [decimal](18, 6) NULL,
	[comments] [nvarchar](max) NULL,
	[rowposition] [int] NULL,
	[approvalstatus] [int] NULL,
	[taskinfo1] [nvarchar](255) NULL,
	[taskinfo2] [nvarchar](255) NULL,
	[taskinfo3] [nvarchar](255) NULL,
	[taskinfo4] [nvarchar](255) NULL,
	[taskinfo5] [nvarchar](255) NULL,
	[taskinfo6] [nvarchar](255) NULL,
	[taskinfo7] [nvarchar](255) NULL,
	[taskinfo8] [nvarchar](255) NULL,
	[taskinfo9] [nvarchar](255) NULL,
	[taskinfo10] [nvarchar](255) NULL,
	[rpinfo1] [nvarchar](255) NULL,
	[rpinfo2] [nvarchar](255) NULL,
	[rpinfo3] [nvarchar](255) NULL,
	[rpinfo4] [nvarchar](255) NULL,
	[rpinfo5] [nvarchar](255) NULL,
	[ttinfo1] [nvarchar](255) NULL,
	[ttinfo2] [nvarchar](255) NULL,
	[ttinfo3] [nvarchar](255) NULL,
	[ttinfo4] [nvarchar](255) NULL,
	[ttinfo5] [nvarchar](255) NULL,
	[teinfo1] [nvarchar](255) NULL,
	[teinfo2] [nvarchar](255) NULL,
	[teinfo3] [nvarchar](255) NULL,
	[teinfo4] [nvarchar](255) NULL,
	[teinfo5] [nvarchar](255) NULL,
	[reportperiodid] [int] NULL,
	[tasktimesheetid] [int] NULL,
	[timesheetentryid] [int] NULL,
	[taskname1] [nvarchar](255) NULL,
	[fulltaskname] [nvarchar](255) NULL,
	[cellcomments] [nvarchar](max) NULL,
	[billingpercentage] [numeric](19, 4) NULL,
	[username] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[employeeid] [nvarchar](255) NULL,
	[userinfo1] [nvarchar](255) NULL,
	[userinfo2] [nvarchar](255) NULL,
	[userinfo3] [nvarchar](255) NULL,
	[userinfo4] [nvarchar](255) NULL,
	[userinfo5] [nvarchar](255) NULL,
	[loginname] [nvarchar](255) NULL,
	[employeetypeid] [nvarchar](255) NULL,
	[employeetypename] [nvarchar](255) NULL,
	[pl_name] [nvarchar](50) NULL,
	[pl_email] [nvarchar](255) NULL,
	[cu_name] [nvarchar](50) NULL,
	[cu_email] [nvarchar](255) NULL,
	[supervisorname] [nvarchar](255) NULL,
	[DepId] [int] NULL,
	[departmentname] [nvarchar](255) NULL,
	[departmentcode] [nvarchar](50) NULL,
	[clientname] [nvarchar](255) NULL,
	[clientcode] [nvarchar](50) NULL,
	[UserGroupSetId] [int] NULL,
	[UserGroupSetName] [nvarchar](50) NULL,
	[UserGroupId] [int] NULL,
	[UserGroupName] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TimeSheetReportID]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE TYPE [dbo].[TimeSheetReportID] AS TABLE(
	[ReportPeriodID] [int] NULL
)
GO
/****** Object:  Table [dbo].[Client]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Code] [nvarchar](50) NULL,
	[Comments] [nvarchar](max) NULL,
	[Address1] [nvarchar](255) NULL,
	[Address2] [nvarchar](255) NULL,
	[City] [nvarchar](100) NULL,
	[StateProvince] [nvarchar](100) NULL,
	[ZipPostalCode] [nvarchar](20) NULL,
	[Country] [nvarchar](100) NULL,
	[Telephone] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[Website] [nvarchar](100) NULL,
	[Disabled] [bit] NULL,
	[DefaultBillingRateAmount] [decimal](12, 4) NULL,
	[DefaultBillingRateDescription] [nvarchar](max) NULL,
	[BillingGLAccount] [nvarchar](50) NULL,
	[BillingCurrencyCode] [nvarchar](3) NULL,
	[GSTRatePercent] [decimal](5, 2) NULL,
	[APVendorID] [nvarchar](50) NULL,
	[APCurrencyCode] [nvarchar](3) NULL,
	[OriginalCode] [nvarchar](50) NULL,
	[Group] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
 CONSTRAINT [PK_Client_RowID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vMostRecentClients]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vMostRecentClients]
AS
SELECT Id, Name, Code, Comments, Address1, Address2, City, StateProvince, ZipPostalCode, Country, Telephone, Fax, Website, [Disabled], DefaultBillingRateAmount, 
       DefaultBillingRateDescription, BillingGLAccount, BillingCurrencyCode, GSTRatePercent, APVendorID, APCurrencyCode, OriginalCode, [Group]
FROM dbo.Client 
WHERE [INTERNAL_BATCH_NUM] = (SELECT MAX([INTERNAL_BATCH_NUM]) FROM dbo.Client) 

-- Note that [BatchNum] is a non-nullable column defined in the dbo.Client table, with a default value as -1 (INT). 
-- In case this column was not filled properly by the SSIS package, it at least contains -1, which is not NULL. So don't worry about the "...WHERE BatchNum IS NULL" scenario.

GO
/****** Object:  Table [dbo].[Department]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Code] [nvarchar](50) NULL,
	[Comments] [nvarchar](max) NULL,
	[DisabledSetting] [bit] NULL,
	[CostCenterGroup] [nvarchar](100) NULL,
	[ParentId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vMostRecentDepts]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vMostRecentDepts]
AS
SELECT [Id],[Name],[Code],[Comments],[DisabledSetting],[CostCenterGroup],[ParentId]
FROM dbo.Department
WHERE [INTERNAL_BATCH_NUM] = (SELECT MAX([INTERNAL_BATCH_NUM]) FROM dbo.Department) 

-- Note that [BatchNum] is a non-nullable column defined in the table, with a default value as -1 (INT). 
-- In case this column was not filled properly by the SSIS package, it at least contains -1, which is not NULL. So don't worry about the "...WHERE BatchNum IS NULL" scenario.


GO
/****** Object:  Table [dbo].[PrimaryDepartment]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrimaryDepartment](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](255) NULL,
	[DepartmentCode] [nvarchar](50) NULL,
	[CostCenterGroup] [nvarchar](255) NULL,
	[UserId] [int] NULL,
	[IsPrimaryDepartment] [bit] NULL,
	[UserFirstName] [nvarchar](255) NULL,
	[UserLastName] [nvarchar](255) NULL,
	[IsUserDisabled] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_PrimaryDepartment] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vMostRecentPrimaryDepartments]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vMostRecentPrimaryDepartments]
AS
SELECT [DepartmentName],[DepartmentCode],[CostCenterGroup],[UserId],[IsPrimaryDepartment],[UserFirstName],[UserLastName],[IsUserDisabled]
FROM dbo.PrimaryDepartment 
WHERE [INTERNAL_BATCH_NUM] = (SELECT MAX([INTERNAL_BATCH_NUM]) FROM dbo.PrimaryDepartment) 

-- Note that [BatchNum] is a non-nullable column defined in the dbo.Client table, with a default value as -1 (INT). 
-- In case this column was not filled properly by the SSIS package, it at least contains -1, which is not NULL. So don't worry about the "...WHERE BatchNum IS NULL" scenario.


GO
/****** Object:  Table [dbo].[Project]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[ProjectCode] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[TimeEntryAllowed] [bit] NULL,
	[EntryStartDate] [datetime] NULL,
	[EntryEndDate] [datetime] NULL,
	[ClosedStatus] [bit] NULL,
	[UdfGroups] [nvarchar](255) NULL,
	[UdfPlpCostCenter] [nvarchar](255) NULL,
	[UdfProjectType] [nvarchar](255) NULL,
	[UdfAudience] [nvarchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vMostRecentProjects]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[vMostRecentProjects]
AS
SELECT [Id],[Name],[ProjectCode],[Description],[TimeEntryAllowed],[EntryStartDate],[EntryEndDate],[ClosedStatus],[UdfGroups],[UdfPlpCostCenter],[UdfProjectType],[UdfAudience]
FROM dbo.Project
WHERE [INTERNAL_BATCH_NUM] = (SELECT MAX([INTERNAL_BATCH_NUM]) FROM dbo.Project) 

-- Note that [BatchNum] is a non-nullable column defined in the dbo.Client table, with a default value as -1 (INT). 
-- In case this column was not filled properly by the SSIS package, it at least contains -1, which is not NULL. So don't worry about the "...WHERE BatchNum IS NULL" scenario.


GO
/****** Object:  Table [dbo].[UserActivityCode]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActivityCode](
	[UserActivityCodeId] [int] IDENTITY(1,1) NOT NULL,
	[ActivityId] [int] NULL,
	[IsActivityEnabled] [bit] NULL,
	[ActivityName] [nvarchar](50) NULL,
	[ActivityCode] [nvarchar](50) NULL,
	[UserId] [int] NULL,
	[IsSampleUser] [bit] NULL,
	[IsUserDisabled] [bit] NULL,
	[UserTitle] [nvarchar](255) NULL,
	[PositionTitle] [nvarchar](255) NULL,
	[FirstName] [nvarchar](128) NULL,
	[LastName] [nvarchar](128) NULL,
	[DepartmentName] [nvarchar](255) NULL,
	[IsPrimaryDepartment] [bit] NULL,
	[CostCenterGroup] [nvarchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_UserActivityCode_1] PRIMARY KEY CLUSTERED 
(
	[UserActivityCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TranslationMappings_ActivityGrouping]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TranslationMappings_ActivityGrouping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InitialValue] [varchar](128) NOT NULL,
	[TransformedValue] [varchar](128) NOT NULL,
 CONSTRAINT [PK_TranslationMappings_ActivityGrouping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vMostRecentUserActivityCodes]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE VIEW [dbo].[vMostRecentUserActivityCodes]
AS
WITH uac AS (
	SELECT [ActivityId],[IsActivityEnabled],[ActivityName],[ActivityCode],[UserId],[IsSampleUser],
			[IsUserDisabled],[UserTitle],[PositionTitle],
			COALESCE(FirstName, N'') + (CASE WHEN FirstName IS NULL THEN N'' ELSE N' ' END) + COALESCE(LastName, N'') AS FullName,
			[DepartmentName],[IsPrimaryDepartment],[CostCenterGroup],
			RTRIM(LEFT(ActivityName, CHARINDEX(' ', ActivityName))) AS ActivityGroupCode
	FROM dbo.UserActivityCode 
	WHERE [INTERNAL_BATCH_NUM] = (SELECT MAX([INTERNAL_BATCH_NUM]) FROM dbo.UserActivityCode)
		AND IsSampleUser = 0
		AND IsUserDisabled = 0
		AND IsPrimaryDepartment = 1	
		AND IsActivityEnabled = 1
		AND LEN(FirstName) > 1	
	)
SELECT uac.*, tmag.TransformedValue AS ActivityGroupName 
FROM uac LEFT JOIN dbo.TranslationMappings_ActivityGrouping tmag ON uac.ActivityGroupCode = tmag.InitialValue

-- Note that [BatchNum] is a non-nullable column defined in the dbo.Approval table, with a default value as -1 (INT). 
-- In case this column was not filled properly by the SSIS package, it at least contains -1, which is not NULL. So don't worry about the "...WHERE BatchNum IS NULL" scenario.









GO
/****** Object:  Table [dbo].[UserId_Dept]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserId_Dept](
	[UserDeptId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[EmployeeId] [nvarchar](255) NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[IsUserEnabled] [bit] NULL,
	[EmployeeTypeId] [int] NULL,
	[DepartmentName] [nvarchar](255) NULL,
	[DepartmentCode] [nvarchar](50) NULL,
	[CostCenterGroup] [nvarchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_UserId_Dept_1] PRIMARY KEY CLUSTERED 
(
	[UserDeptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vMostRecentUserIdDepts]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[vMostRecentUserIdDepts]
AS
SELECT 
	   [UserId]
      ,[EmployeeId]
      ,[FirstName]
      ,[LastName]
      ,[IsUserEnabled]
      ,[EmployeeTypeId]
      ,[DepartmentName]
      ,[DepartmentCode]
      ,[CostCenterGroup]
	  ,INTERNAL_ROW_ID
FROM dbo.UserId_Dept
WHERE [INTERNAL_BATCH_NUM] = (SELECT MAX([INTERNAL_BATCH_NUM]) FROM dbo.UserId_Dept) 

-- Note that [BatchNum] is a non-nullable column defined in the table, with a default value as -1 (INT). 
-- In case this column was not filled properly by the SSIS package, it at least contains -1, which is not NULL. So don't worry about the "...WHERE BatchNum IS NULL" scenario.



GO
/****** Object:  Table [dbo].[User]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExternalId] [nvarchar](50) NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[LoginName] [nvarchar](200) NULL,
	[Disabled] [bit] NULL,
	[IsSampleUser] [bit] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[AccountExpiry] [datetime] NULL,
	[Domain] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[ExternalEmail] [nvarchar](100) NULL,
	[OfflineEmail] [nvarchar](100) NULL,
	[CurrentHoursPerDay] [time](7) NULL,
	[DefaultBillingRate] [float] NULL,
	[DisablePasswordChange] [bit] NULL,
	[ForcePasswordChange] [bit] NULL,
	[NumberOfTimesheetsPendingApproval] [int] NULL,
	[NumberOfTimesheetsWithPreviousApprovalAction] [int] NULL,
	[BillingRate] [float] NULL,
	[JobTitle] [nvarchar](100) NULL,
	[ReportsTo] [nvarchar](100) NULL,
	[ConvertDaysToHours] [float] NULL,
	[PLPMarkUp] [float] NULL,
	[PositionTitle] [nvarchar](100) NULL,
	[RoleId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vMostRecentUsers]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[vMostRecentUsers]
AS
SELECT [Id],[ExternalId],[FirstName],[LastName],[LoginName],[Disabled],[IsSampleUser],
	CAST([StartDate] AS DATE) AS StartDate,
	CAST([EndDate] AS DATE) AS EndDate,
	CAST([AccountExpiry] AS DATE) AS AccountExpiry,
	[Domain],[Email],[ExternalEmail],[OfflineEmail],
	CAST([CurrentHoursPerDay] AS TIME) AS [CurrentHoursPerDay],
	[DefaultBillingRate],[DisablePasswordChange],[ForcePasswordChange],[NumberOfTimesheetsPendingApproval],
	[NumberOfTimesheetsWithPreviousApprovalAction],[BillingRate],[JobTitle],[ReportsTo],[ConvertDaysToHours],[PLPMarkUp],[PositionTitle]
FROM dbo.[User] 
WHERE [INTERNAL_BATCH_NUM] = (SELECT MAX([INTERNAL_BATCH_NUM]) FROM dbo.[User]) 

-- Note that [BatchNum] is a non-nullable column defined in the dbo.[User] table, with a default value as -1 (INT). 
-- In case this column was not filled properly by the SSIS package, it at least contains -1, which is not NULL. So don't worry about the "...WHERE BatchNum IS NULL" scenario.





GO
/****** Object:  View [dbo].[vMostRecentApprovals]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* Note that [BatchNum] is a non-nullable column defined in the dbo.Approval table, with a default value as -1 (INT). 
 In case this column was not filled properly by the SSIS package, it at least contains -1, which is not NULL. So don't worry about the "...WHERE BatchNum IS NULL" scenario.*/
CREATE VIEW [dbo].[vMostRecentApprovals]
AS
SELECT        TimesheetId, ApproverId, EffectiveDate, ApprovalAction, ApprovalComments, ApproverTypeId, ApproverTypeName, ApproverFirstName, ApproverLastName, ApproverEmail
FROM            dbo.Approval
GO
/****** Object:  View [dbo].[vMostRecentPastDue]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE VIEW [dbo].[vMostRecentPastDue]
AS
SELECT [TimesheetId],[UserId],[FirstName],[LastName],[DueDate],[EffectiveDate],[EffectiveDateUtc],[ApprovalAction]
FROM dbo.PastDue 
WHERE [INTERNAL_BATCH_NUM] = (SELECT MAX([INTERNAL_BATCH_NUM]) FROM dbo.PastDue)

-- Note that [BatchNum] is a non-nullable column defined in the dbo.PastDue table, with a default value as -1 (INT). 
-- In case this column was not filled properly by the SSIS package, it at least contains -1, which is not NULL. So don't worry about the "...WHERE BatchNum IS NULL" scenario.







GO
/****** Object:  Table [dbo].[Activity]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[ActivityId] [int] IDENTITY(1,1) NOT NULL,
	[ActivityName] [nvarchar](255) NOT NULL,
	[ActivityCode] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_Activity_1] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApprovalStatus]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApprovalStatus](
	[approvalstatusid] [int] NOT NULL,
	[appstatusname] [nvarchar](255) NULL,
 CONSTRAINT [PK_ApprovalStatus] PRIMARY KEY CLUSTERED 
(
	[approvalstatusid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostCenter]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostCenter](
	[costcenterid] [int] IDENTITY(1,1) NOT NULL,
	[costcentername] [nvarchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_CostCenter] PRIMARY KEY CLUSTERED 
(
	[costcenterid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeType]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeType](
	[emptypeid] [int] IDENTITY(1,1) NOT NULL,
	[employeetypeid] [nvarchar](255) NULL,
	[employeetypename] [nvarchar](255) NULL,
 CONSTRAINT [PK_EmployeeType] PRIMARY KEY CLUSTERED 
(
	[emptypeid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[roleid] [int] IDENTITY(1,1) NOT NULL,
	[rolename] [nvarchar](255) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[roleid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RP_TimeSheetData]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RP_TimeSheetData](
	[timesheetId] [bigint] IDENTITY(1,1) NOT NULL,
	[timesheetperiod] [datetime] NOT NULL,
	[startdate] [datetime] NOT NULL,
	[enddate] [datetime] NOT NULL,
	[entrydate] [datetime] NOT NULL,
	[userid] [int] NOT NULL,
	[DepartmentId] [int] NULL,
	[supervisorid] [int] NULL,
	[projectid] [int] NULL,
	[projectcode] [nvarchar](255) NULL,
	[projectname] [nvarchar](255) NULL,
	[projectdescription] [nvarchar](255) NULL,
	[billingmethod] [nvarchar](255) NULL,
	[clientid] [int] NULL,
	[billingpercentage_ratio] [numeric](19, 4) NULL,
	[projectleaderid] [int] NULL,
	[clientuserid] [int] NULL,
	[tasktimeoffid] [int] NULL,
	[tasktimeoffname] [nvarchar](255) NULL,
	[htasktimeoffname] [nvarchar](255) NULL,
	[tasktimeoffcode] [nvarchar](50) NULL,
	[ActivityId] [int] NULL,
	[ActivityName] [nvarchar](255) NULL,
	[ActivityCode] [nvarchar](50) NULL,
	[billable] [int] NULL,
	[billablename] [nvarchar](255) NULL,
	[non_billable_hours] [decimal](18, 6) NULL,
	[billable_hours] [decimal](18, 6) NULL,
	[time_off_hours] [decimal](18, 12) NULL,
	[project_hours] [decimal](18, 6) NULL,
	[total_hours] [decimal](18, 6) NULL,
	[comments] [nvarchar](max) NULL,
	[rowposition] [int] NULL,
	[approvalstatus] [int] NULL,
	[taskinfo1] [nvarchar](255) NULL,
	[taskinfo2] [nvarchar](255) NULL,
	[taskinfo3] [nvarchar](255) NULL,
	[taskinfo4] [nvarchar](255) NULL,
	[taskinfo5] [nvarchar](255) NULL,
	[taskinfo6] [nvarchar](255) NULL,
	[taskinfo7] [nvarchar](255) NULL,
	[taskinfo8] [nvarchar](255) NULL,
	[taskinfo9] [nvarchar](255) NULL,
	[taskinfo10] [nvarchar](255) NULL,
	[rpinfo1] [nvarchar](255) NULL,
	[rpinfo2] [nvarchar](255) NULL,
	[rpinfo3] [nvarchar](255) NULL,
	[rpinfo4] [nvarchar](255) NULL,
	[rpinfo5] [nvarchar](255) NULL,
	[ttinfo1] [nvarchar](255) NULL,
	[ttinfo2] [nvarchar](255) NULL,
	[ttinfo3] [nvarchar](255) NULL,
	[ttinfo4] [nvarchar](255) NULL,
	[ttinfo5] [nvarchar](255) NULL,
	[teinfo1] [nvarchar](255) NULL,
	[teinfo2] [nvarchar](255) NULL,
	[teinfo3] [nvarchar](255) NULL,
	[teinfo4] [nvarchar](255) NULL,
	[teinfo5] [nvarchar](255) NULL,
	[reportperiodid] [int] NULL,
	[tasktimesheetid] [int] NULL,
	[timesheetentryid] [int] NULL,
	[taskname1] [nvarchar](255) NULL,
	[fulltaskname] [nvarchar](255) NULL,
	[cellcomments] [nvarchar](max) NULL,
	[billingpercentage] [numeric](19, 4) NULL,
	[username] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[employeeid] [nvarchar](255) NULL,
	[userinfo1] [nvarchar](255) NULL,
	[userinfo2] [nvarchar](255) NULL,
	[userinfo3] [nvarchar](255) NULL,
	[userinfo4] [nvarchar](255) NULL,
	[userinfo5] [nvarchar](255) NULL,
	[loginname] [nvarchar](255) NULL,
	[employeetypeid] [nvarchar](255) NULL,
	[employeetypename] [nvarchar](255) NULL,
	[pl_name] [nvarchar](50) NULL,
	[pl_email] [nvarchar](255) NULL,
	[cu_name] [nvarchar](50) NULL,
	[cu_email] [nvarchar](255) NULL,
	[supervisorname] [nvarchar](255) NULL,
	[DepId] [int] NULL,
	[departmentname] [nvarchar](255) NULL,
	[departmentcode] [nvarchar](50) NULL,
	[clientname] [nvarchar](255) NULL,
	[clientcode] [nvarchar](50) NULL,
	[UserGroupSetId] [int] NULL,
	[UserGroupSetName] [nvarchar](50) NULL,
	[UserGroupId] [int] NULL,
	[UserGroupName] [nvarchar](50) NULL,
 CONSTRAINT [PK_RP_TimeSheetData3] PRIMARY KEY CLUSTERED 
(
	[timesheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[tasktimeoffid] [int] IDENTITY(1,1) NOT NULL,
	[tasktimeoffname] [nvarchar](255) NOT NULL,
	[tasktimeoffcode] [nvarchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreateIP] [varchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [int] NULL,
	[UpdateIP] [varchar](80) NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[tasktimeoffid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CIX_TranslationMappings_ActivityGrouping]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE NONCLUSTERED INDEX [CIX_TranslationMappings_ActivityGrouping] ON [dbo].[TranslationMappings_ActivityGrouping]
(
	[InitialValue] ASC
)
INCLUDE([TransformedValue]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NonClusteredIndex-20141203-103641]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20141203-103641] ON [dbo].[User]
(
	[ExternalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [NonClusteredIndex-20141203-104025]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20141203-104025] ON [dbo].[User]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NonClusteredIndex-20141203-105505]    Script Date: 2021-09-29 8:36:25 PM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20141203-105505] ON [dbo].[User]
(
	[ExternalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[REP_SP_TimeSheetDetails]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

    Create PROCEDURE [dbo].[REP_SP_TimeSheetDetails]
	@Type VARCHAR(15),
	@Details TimeSheetDetailsType READONLY
	AS
	BEGIN
	      
		  INSERT INTO RP_TimeSheetData(
										timesheetperiod,startdate ,enddate ,entrydate ,userid,DepartmentId,supervisorid,
										projectid,projectcode,projectname,projectdescription,billingmethod,clientid,
										billingpercentage_ratio,projectleaderid,clientuserid,tasktimeoffid,tasktimeoffname,
										htasktimeoffname,tasktimeoffcode,ActivityId,ActivityName,ActivityCode,billable,billablename,
										non_billable_hours,billable_hours,time_off_hours,project_hours,total_hours,comments,
										rowposition,approvalstatus,taskinfo1,taskinfo2,taskinfo3,taskinfo4,taskinfo5,taskinfo6,taskinfo7,
										taskinfo8,taskinfo9,taskinfo10,rpinfo1,rpinfo2,rpinfo3,rpinfo4,rpinfo5,ttinfo1,ttinfo2,ttinfo3,ttinfo4,ttinfo5,
										teinfo1,teinfo2,teinfo3,teinfo4,teinfo5,reportperiodid,tasktimesheetid,timesheetentryid,
										taskname1,fulltaskname,cellcomments,billingpercentage,username,email,employeeid,
										userinfo1,userinfo2,userinfo3,userinfo4,userinfo5,loginname,employeetypeid,employeetypename,
										pl_name,pl_email,cu_name,cu_email,supervisorname,DepId,departmentname,departmentcode,clientname,
										clientcode,UserGroupSetId,UserGroupSetName,UserGroupId,UserGroupName
									)   
							SELECT      timesheetperiod,startdate ,enddate ,entrydate ,userid,DepartmentId,supervisorid,
										projectid,projectcode,projectname,projectdescription,billingmethod,clientid,
										billingpercentage_ratio,projectleaderid,clientuserid,tasktimeoffid,tasktimeoffname,
										htasktimeoffname,tasktimeoffcode,ActivityId,ActivityName,ActivityCode,billable,billablename,
										non_billable_hours,billable_hours,time_off_hours,project_hours,total_hours,comments,
										rowposition,approvalstatus,taskinfo1,taskinfo2,taskinfo3,taskinfo4,taskinfo5,taskinfo6,taskinfo7,
										taskinfo8,taskinfo9,taskinfo10,rpinfo1,rpinfo2,rpinfo3,rpinfo4,rpinfo5,ttinfo1,ttinfo2,ttinfo3,ttinfo4,ttinfo5,
										teinfo1,teinfo2,teinfo3,teinfo4,teinfo5,reportperiodid,tasktimesheetid,timesheetentryid,
										taskname1,fulltaskname,cellcomments,billingpercentage,username,email,employeeid,
										userinfo1,userinfo2,userinfo3,userinfo4,userinfo5,loginname,employeetypeid,employeetypename,
										pl_name,pl_email,cu_name,cu_email,supervisorname,DepId,departmentname,departmentcode,clientname,
										clientcode,UserGroupSetId,UserGroupSetName,UserGroupId,UserGroupName

							FROM @Details;
	END
	
GO
/****** Object:  StoredProcedure [dbo].[REP_SP_TimeSheetDetailsDelete]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[REP_SP_TimeSheetDetailsDelete]
@Type VARCHAR(15),
@Details TimeSheetReportID READONLY
As
Begin
      DELETE FROM RP_TimeSheetData WHERE reportperiodid IN(SELECT ReportPeriodID FROM @Details)
End

GO
/****** Object:  StoredProcedure [dbo].[REP_SP_ToolLastRandate]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[REP_SP_ToolLastRandate]	
as
insert into dbo.RP_SyncToolRanDate values(GETUTCDATE())

GO
/****** Object:  StoredProcedure [dbo].[spCleanUpExpiredData]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ===========================================================================
-- Author:		Kai Lin
-- Create date: 2013-Dec-9
-- Description:	Delete the data older than 1 week, however, keep the last batch 
--				no matter what.
--		Note that it only uses the date part for comparison
-- ===========================================================================
CREATE PROCEDURE [dbo].[spCleanUpExpiredData]
(
	@DaysToKeep int = 7
) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @CurrentDT2 AS datetime2, @MaxBatch AS int
	SET @CurrentDT2 = CAST(GETDATE() AS datetime2)
	
	DELETE
		FROM dbo.Department 
		WHERE (INTERNAL_BATCH_NUM < (SELECT MAX(INTERNAL_BATCH_NUM) FROM dbo.Department)) 
			AND (DateDiff(DAY, INTERNAL_TIME_STAMP, @CurrentDT2) > @DaysToKeep);

	DELETE
		FROM dbo.PrimaryDepartment 
		WHERE (INTERNAL_BATCH_NUM < (SELECT MAX(INTERNAL_BATCH_NUM) FROM dbo.PrimaryDepartment)) 
			AND (DateDiff(DAY, INTERNAL_TIME_STAMP, @CurrentDT2) > @DaysToKeep);

	DELETE
		FROM dbo.Client 
		WHERE (INTERNAL_BATCH_NUM < (SELECT MAX(INTERNAL_BATCH_NUM) FROM dbo.Client)) 
			AND (DateDiff(DAY, INTERNAL_TIME_STAMP, @CurrentDT2) > @DaysToKeep);

	DELETE
		FROM dbo.UserId_Dept 
		WHERE (INTERNAL_BATCH_NUM < (SELECT MAX(INTERNAL_BATCH_NUM) FROM dbo.UserId_Dept)) 
			AND (DateDiff(DAY, INTERNAL_TIME_STAMP, @CurrentDT2) > @DaysToKeep);

	DELETE
		FROM dbo.Project 
		WHERE (INTERNAL_BATCH_NUM < (SELECT MAX(INTERNAL_BATCH_NUM) FROM dbo.Project)) 
			AND (DateDiff(DAY, INTERNAL_TIME_STAMP, @CurrentDT2) > @DaysToKeep);

	DELETE
		FROM dbo.Approval 
		WHERE (INTERNAL_BATCH_NUM < (SELECT MAX(INTERNAL_BATCH_NUM) FROM dbo.Approval)) 
			AND (DateDiff(DAY, INTERNAL_TIME_STAMP, @CurrentDT2) > @DaysToKeep);

END

GO
/****** Object:  StoredProcedure [dbo].[spGetMaxBatchNum]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ===========================================================================
-- Author:		Kai Lin
-- Create date: 2013-Nov-20
-- Description:	Get the next [BatchNum] from the dbo.Client table
-- ===========================================================================
CREATE PROCEDURE [dbo].[spGetMaxBatchNum]
(
	@TableName sysname
) 
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @NextBatchNum AS INT, @sql AS NVARCHAR(200), @param AS NVARCHAR(100)

	SELECT @sql = N'SELECT @NextBatchNumOUT = MAX([INTERNAL_BATCH_NUM]) FROM dbo.[' + @TableName + N']',
			@param = N'@NextBatchNumOUT INT OUTPUT'
	BEGIN TRY
		EXECUTE sp_executesql @sql, @param, @NextBatchNumOUT = @NextBatchNum OUTPUT
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
	SET @NextBatchNum = CASE 
							WHEN @NextBatchNum IS NULL THEN 1 
							WHEN @NextBatchNum = -1 THEN 1
							ELSE @NextBatchNum + 1 
						END;
	RETURN @NextBatchNum   
END





GO
/****** Object:  StoredProcedure [dbo].[usp_Update_WTS_LastRanDate]    Script Date: 2021-09-29 8:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_Update_WTS_LastRanDate] 

AS 
BEGIN 

DECLARE @DeleteDate as datetime
DECLARE @OldDate as datetime

Set @DeleteDate = dateadd(d,-4,getdate())

Select @OldDate = dateadd(HOUR,-7,max(LastRanDate)) FROM [RepliconCloudData].[dbo].[RP_SyncToolRanDate] 

--print @DeleteDate

Delete FROM [RepliconCloudData].[dbo].[RP_SyncToolRanDate] where LastRanDate >= @DeleteDate

Select dateadd(HOUR,-7,max(LastRanDate)) as LastDate,@OldDate as OldDate FROM [RepliconCloudData].[dbo].[RP_SyncToolRanDate] 

	
END





GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Project ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Project', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Approval"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 234
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vMostRecentApprovals'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vMostRecentApprovals'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Client"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 251
               Right = 364
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vMostRecentClients'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vMostRecentClients'
GO
USE [master]
GO
ALTER DATABASE [Timesheet] SET  READ_WRITE 
GO
