USE [master]
GO
/****** Object:  Database [cosmosgu_timesheet]    Script Date: 4/7/2022 9:05:18 AM ******/
CREATE DATABASE [cosmosgu_timesheet]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'cosmosgu_timesheet_data', FILENAME = N'E:\MSSQL13.MSSQLSERVER\MSSQL\DATA\cosmosgu_timesheet_data.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'cosmosgu_timesheet_log', FILENAME = N'E:\MSSQL13.MSSQLSERVER\MSSQL\DATA\cosmosgu_timesheet_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [cosmosgu_timesheet] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [cosmosgu_timesheet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [cosmosgu_timesheet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET ARITHABORT OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [cosmosgu_timesheet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [cosmosgu_timesheet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET  ENABLE_BROKER 
GO
ALTER DATABASE [cosmosgu_timesheet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [cosmosgu_timesheet] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET RECOVERY FULL 
GO
ALTER DATABASE [cosmosgu_timesheet] SET  MULTI_USER 
GO
ALTER DATABASE [cosmosgu_timesheet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [cosmosgu_timesheet] SET DB_CHAINING OFF 
GO
ALTER DATABASE [cosmosgu_timesheet] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [cosmosgu_timesheet] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [cosmosgu_timesheet] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'cosmosgu_timesheet', N'ON'
GO
ALTER DATABASE [cosmosgu_timesheet] SET QUERY_STORE = OFF
GO
USE [cosmosgu_timesheet]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [cosmosgu_timesheet]
GO
/****** Object:  User [cosmosgu_fin2]    Script Date: 4/7/2022 9:05:20 AM ******/
CREATE USER [cosmosgu_fin2] FOR LOGIN [cosmosgu_fin2] WITH DEFAULT_SCHEMA=[cosmosgu_fin2]
GO
ALTER ROLE [db_owner] ADD MEMBER [cosmosgu_fin2]
GO
/****** Object:  Schema [cosmosgu_fin2]    Script Date: 4/7/2022 9:05:20 AM ******/
CREATE SCHEMA [cosmosgu_fin2]
GO
/****** Object:  UserDefinedTableType [dbo].[TimeSheetDetailsType]    Script Date: 4/7/2022 9:05:20 AM ******/
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
/****** Object:  UserDefinedTableType [dbo].[TimeSheetReportID]    Script Date: 4/7/2022 9:05:20 AM ******/
CREATE TYPE [dbo].[TimeSheetReportID] AS TABLE(
	[ReportPeriodID] [int] NULL
)
GO
/****** Object:  Table [dbo].[Activity]    Script Date: 4/7/2022 9:05:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity](
	[ActivityId] [int] IDENTITY(1,1) NOT NULL,
	[ActivityName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[ActivityCode] [nvarchar](50) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedIP] [nvarchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [nvarchar](80) NULL,
	[Disabled] [bit] NULL,
	[CreatedMacAddress] [nvarchar](50) NULL,
	[UpdatedMacAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_Activity_1] PRIMARY KEY CLUSTERED 
(
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApprovalStatus]    Script Date: 4/7/2022 9:05:20 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 4/7/2022 9:05:20 AM ******/
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
	[CreatedIP] [nvarchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [nvarchar](80) NULL,
	[CreatedMacAddress] [nvarchar](50) NULL,
	[UpdatedMacAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_Client_RowID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_Client_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostCenter]    Script Date: 4/7/2022 9:05:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostCenter](
	[costcenterid] [int] IDENTITY(1,1) NOT NULL,
	[costcentername] [nvarchar](255) NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedIP] [nvarchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [nvarchar](80) NULL,
	[Disabled] [bit] NULL,
	[CreatedMacAddress] [nvarchar](50) NULL,
	[UpdatedMacAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_CostCenter] PRIMARY KEY CLUSTERED 
(
	[costcenterid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 4/7/2022 9:05:20 AM ******/
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
	[CreatedIP] [nvarchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [nvarchar](80) NULL,
	[Disabled] [bit] NULL,
	[CreatedMacAddress] [nvarchar](50) NULL,
	[UpdatedMacAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeType]    Script Date: 4/7/2022 9:05:20 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[General]    Script Date: 4/7/2022 9:05:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[General](
	[GralId] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](100) NULL,
	[DefaultValue] [bit] NULL,
	[GralGroup] [nvarchar](100) NULL,
	[Type] [nvarchar](15) NULL,
 CONSTRAINT [PK_General] PRIMARY KEY CLUSTERED 
(
	[GralId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 4/7/2022 9:05:20 AM ******/
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
	[ProjectLeaderId] [int] NULL,
	[ClientId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedIP] [nvarchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [nvarchar](80) NULL,
	[CreatedMacAddress] [nvarchar](50) NULL,
	[UpdatedMacAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectHasUser]    Script Date: 4/7/2022 9:05:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectHasUser](
	[PUId] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PUId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4/7/2022 9:05:20 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RP_TimeSheetData]    Script Date: 4/7/2022 9:05:20 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeOff]    Script Date: 4/7/2022 9:05:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeOff](
	[TimeOffId] [int] IDENTITY(1,1) NOT NULL,
	[TimeOffName] [nvarchar](255) NOT NULL,
	[TimeOffCode] [nvarchar](255) NULL,
	[Disabled] [bit] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[TimeOffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimesheetControl]    Script Date: 4/7/2022 9:05:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimesheetControl](
	[TimesheetPeriodID] [int] IDENTITY(1,1) NOT NULL,
	[CalendarPeriod] [nvarchar](50) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_TimesheetControl] PRIMARY KEY CLUSTERED 
(
	[TimesheetPeriodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TranslationMappings_ActivityGrouping]    Script Date: 4/7/2022 9:05:20 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/7/2022 9:05:20 AM ******/
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
	[CreatedIP] [nvarchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [nvarchar](80) NULL,
	[CreatedMacAddress] [nvarchar](50) NULL,
	[UpdatedMacAddress] [nvarchar](50) NULL,
	[DepartmentId] [int] NULL,
	[EmpTypeId] [int] NULL,
	[SubstituteUser] [int] NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserActivityCode]    Script Date: 4/7/2022 9:05:20 AM ******/
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
	[CreatedIP] [nvarchar](80) NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [nvarchar](80) NULL,
	[Disabled] [bit] NULL,
	[CreatedMacAddress] [nvarchar](50) NULL,
	[UpdatedMacAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserActivityCode_1] PRIMARY KEY CLUSTERED 
(
	[UserActivityCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Year_Month_Period]    Script Date: 4/7/2022 9:05:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Year_Month_Period](
	[YMPid] [int] IDENTITY(1,1) NOT NULL,
	[Year] [nvarchar](255) NULL,
	[Month] [nvarchar](255) NULL,
	[MonthName] [nvarchar](255) NULL,
	[YearMonth] [nvarchar](255) NULL,
	[Year-Month] [nvarchar](255) NULL,
	[Year_MonthName] [nvarchar](255) NULL,
	[Period] [nvarchar](255) NULL,
	[Year_Month_Period] [nvarchar](255) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [CIX_TranslationMappings_ActivityGrouping]    Script Date: 4/7/2022 9:05:20 AM ******/
CREATE NONCLUSTERED INDEX [CIX_TranslationMappings_ActivityGrouping] ON [dbo].[TranslationMappings_ActivityGrouping]
(
	[InitialValue] ASC
)
INCLUDE([TransformedValue]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NonClusteredIndex-20141203-103641]    Script Date: 4/7/2022 9:05:20 AM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20141203-103641] ON [dbo].[User]
(
	[ExternalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [NonClusteredIndex-20141203-104025]    Script Date: 4/7/2022 9:05:20 AM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20141203-104025] ON [dbo].[User]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NonClusteredIndex-20141203-105505]    Script Date: 4/7/2022 9:05:20 AM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20141203-105505] ON [dbo].[User]
(
	[ExternalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_BillingGLAccount]  DEFAULT (N'7780HDI') FOR [BillingGLAccount]
GO
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_BillingCurrencyCode]  DEFAULT (N'CAD') FOR [BillingCurrencyCode]
GO
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_GSTRatePercent]  DEFAULT ((0.05)) FOR [GSTRatePercent]
GO
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_APVendorID]  DEFAULT (N'HUNDIC') FOR [APVendorID]
GO
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_APCurrencyCode]  DEFAULT (N'CAD') FOR [APCurrencyCode]
GO
ALTER TABLE [dbo].[Project]  WITH NOCHECK ADD FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([Id])
GO
ALTER TABLE [dbo].[ProjectHasUser]  WITH CHECK ADD  CONSTRAINT [FK__ProjectHa__UserI__2CF2ADDF] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
ALTER TABLE [dbo].[ProjectHasUser] CHECK CONSTRAINT [FK__ProjectHa__UserI__2CF2ADDF]
GO
ALTER TABLE [dbo].[ProjectHasUser]  WITH CHECK ADD  CONSTRAINT [FK__ProjectHa__UserI__2DE6D218] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ProjectHasUser] CHECK CONSTRAINT [FK__ProjectHa__UserI__2DE6D218]
GO
ALTER TABLE [dbo].[User]  WITH NOCHECK ADD FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[User]  WITH NOCHECK ADD FOREIGN KEY([EmpTypeId])
REFERENCES [dbo].[EmployeeType] ([emptypeid])
GO
ALTER TABLE [dbo].[User]  WITH NOCHECK ADD FOREIGN KEY([SubstituteUser])
REFERENCES [dbo].[User] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[REP_SP_TimeSheetDetails]    Script Date: 4/7/2022 9:05:20 AM ******/
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
/****** Object:  StoredProcedure [dbo].[REP_SP_TimeSheetDetailsDelete]    Script Date: 4/7/2022 9:05:20 AM ******/
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
/****** Object:  StoredProcedure [dbo].[REP_SP_ToolLastRandate]    Script Date: 4/7/2022 9:05:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Proc [dbo].[REP_SP_ToolLastRandate]	
as
insert into dbo.RP_SyncToolRanDate values(GETUTCDATE())

GO
/****** Object:  StoredProcedure [dbo].[spGetMaxBatchNum]    Script Date: 4/7/2022 9:05:20 AM ******/
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
/****** Object:  StoredProcedure [dbo].[usp_Update_WTS_LastRanDate]    Script Date: 4/7/2022 9:05:20 AM ******/
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
USE [master]
GO
ALTER DATABASE [cosmosgu_timesheet] SET  READ_WRITE 
GO
