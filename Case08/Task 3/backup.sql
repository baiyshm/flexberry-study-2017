USE [master]
GO
/****** Object:  Database [DB_CALENDAR]    Script Date: 01.03.2017 18:46:28 ******/
CREATE DATABASE [DB_CALENDAR]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_CALENDAR', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DB_CALENDAR.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DB_CALENDAR_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DB_CALENDAR_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DB_CALENDAR] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_CALENDAR].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_CALENDAR] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DB_CALENDAR] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_CALENDAR] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_CALENDAR] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DB_CALENDAR] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_CALENDAR] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_CALENDAR] SET  MULTI_USER 
GO
ALTER DATABASE [DB_CALENDAR] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_CALENDAR] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_CALENDAR] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_CALENDAR] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DB_CALENDAR] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DB_CALENDAR]
GO
/****** Object:  UserDefinedFunction [dbo].[CheckDay]    Script Date: 01.03.2017 18:46:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--
--C помощью данной функции можно получать общее количество часов в месяце, указывая при этом порядковый номер месяца (1..12) и его год
--Например SELECT getMonthWorkHours(месяц, год)
--
--
--3. Написать хранимую функцию, по переданному первичному ключу повторяющегося дня-исключения и дате определяющую, соответствует ли данный день-исключение переданной дате (с учетом повторения).
--
CREATE FUNCTION [dbo].[CheckDay]
(
	@idExcDay int,
	@checkDayDate datetime
)
RETURNS bit
AS
BEGIN
	declare @startDayDate DATETIME
	declare @endDate DATETIME
	declare @iterCount INT
	declare @iterTipe INT
	declare @inc INT
	declare @check bit

	set @check = 0

	select @startDayDate = tED.startDate,@endDate = tED.endDate,@iterCount = tED.iterationCount,@iterTipe = tED.iterationTipe,@inc = tED.inc from tExceptionDay as tED
	where tED.idExcDay = @idExcDay

	declare @tempDate datetime
	set @tempDate = @startDayDate
	if (@endDate is null) begin
		declare @i INT
		set @i = 1
		while (@i <= @iterCount) 
		begin
			if (@tempDate = @checkDayDate) begin
				set @check = 1
				break
			end
			set @i = @i + 1
			set @tempDate = 
			case @iterTipe
				when 1 then DATEADD(d,@inc,@tempDate)
				when 2 then DATEADD(d,@inc*7,@tempDate)
				when 3 then DATEADD(m,@inc,@tempDate)
				when 4 then DATEADD(YY,@inc,@tempDate)
			end
		end
	end
	else if (@iterCount is not null) begin
		while (@tempDate <= @endDate) 
		begin
			if (@tempDate = @checkDayDate) begin
				set @check = 1
				break
			end
			set @tempDate = 
			case @iterTipe
				when 1 then DATEADD(d,@inc,@tempDate)
				when 2 then DATEADD(d,@inc*7,@tempDate)
				when 3 then DATEADD(m,@inc,@tempDate)
				when 4 then DATEADD(YY,@inc,@tempDate)
			end
		end
	end

	RETURN @check
END

GO
/****** Object:  UserDefinedFunction [dbo].[CheckMonth]    Script Date: 01.03.2017 18:46:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--
--4. На основе функции из пункта 3 написать запрос, вычисляющий количество дней-исключений в указанный месяц.
--
--Функция проверяет встречается ли день с указанным ID в указанный месяц
CREATE FUNCTION [dbo].[CheckMonth]
(
	@idExcDay INT,
	@month INT
)
RETURNS int
AS
BEGIN
	declare @startDayDate DATETIME
	declare @endDate DATETIME
	declare @iterCount INT
	declare @iterTipe INT
	declare @inc INT
	declare @check int

	set @check = 0

	select @startDayDate = tED.startDate,@endDate = tED.endDate,@iterCount = tED.iterationCount,@iterTipe = tED.iterationTipe,@inc = tED.inc from tExceptionDay as tED
	where tED.idExcDay = @idExcDay

	declare @tempDate DATETIME
	set @tempDate = @startDayDate
	if (@endDate is null) begin
		declare @i INT
		set @i = 1
		while (@i <= @iterCount) 
		begin
			if (DATEPART(MM,@tempDate) = @month) begin
				set @check = 1
				break
			end
			set @i = @i + 1
			set @tempDate = 
			case @iterTipe
				when 1 then DATEADD(d,@inc,@tempDate)
				when 2 then DATEADD(d,@inc*7,@tempDate)
				when 3 then DATEADD(m,@inc,@tempDate)
				when 4 then DATEADD(YY,@inc,@tempDate)
			end
		end
	end
	else if (@iterCount is not null) begin
		while (@tempDate <= @endDate) 
		begin
			if (DATEPART(MM,@tempDate) = @month) begin
				set @check = 1
				break
			end
			set @tempDate = 
			case @iterTipe
				when 1 then DATEADD(d,@inc,@tempDate)
				when 2 then DATEADD(d,@inc*7,@tempDate)
				when 3 then DATEADD(m,@inc,@tempDate)
				when 4 then DATEADD(YY,@inc,@tempDate)
			end
		end
	end

	RETURN @check
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetDaysCount]    Script Date: 01.03.2017 18:46:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--
--Функция вычисляет количество дней-исключений в указанный месяц
--
CREATE FUNCTION [dbo].[GetDaysCount]
(
	@month INT
)
RETURNS int
AS
BEGIN
	declare @dayCount INT
	select @dayCount = SUM(dbo.CheckMonth(tED.idExcDay,@month)) from tExceptionDay as tED
	RETURN @dayCount
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetMonthWorkHours]    Script Date: 01.03.2017 18:46:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetMonthWorkHours]
(
	--Порядковый номер месяца
	@monthNumber int,
	--Год
	@yearNumber int
)
RETURNS int
AS
BEGIN
	declare @startDate DATETIME;
	set @startDate = CAST(CAST(1 AS nvarchar) + '-' +  CAST(@monthNumber AS nvarchar) + '-' + CAST(@yearNumber AS nvarchar) AS DATETIME);
	declare @lastMonthDate DATETIME;
	set @lastMonthDate = EOMONTH(@startDate);

	declare @idExcDay INT
	declare @sumHours INT
	declare @startDayDate DATETIME
	declare @endDate DATETIME
	declare @iterCount INT
	declare @iterTipe INT
	declare @inc INT
	declare @sumHoursMonth INT

	set @sumHoursMonth = 0

	declare @cursor cursor

	set @cursor = cursor scroll
	for
	select tWTS.idExcDay,SUM(DATEDIFF(HH,tWtS.upTime,tWTS.endTime)) as sumHour,tED.startDate,tED.endDate,tED.iterationCount,tED.iterationTipe,tED.inc from tWorkTimeSpans as tWTS 
	inner join tExceptionDay as tED on tWTS.idExcDay = tED.idExcDay
	group by tWTS.idExcDay,tED.startDate,tED.endDate,tED.iterationCount,tED.iterationTipe,tED.inc
	
	open @cursor

	fetch next from @cursor into @idExcDay,@sumHours,@startDayDate,@endDate,@iterCount,@iterTipe,@inc
	while @@FETCH_STATUS = 0
	begin
		declare @tempDate datetime;
		set @tempDate = @startDayDate;
		if (@endDate is null) begin
			declare @i INT
			set @i = 1
			while (@i <= @iterCount) 
			begin
				if (@tempDate >= @startDate) and (@tempDate <= @lastMonthDate) begin
					set @sumHoursMonth = @sumHoursMonth + @sumHours;
				end
				set @i = @i + 1;
				set @tempDate = 
				case @iterTipe
					when 1 then DATEADD(d,@inc,@tempDate)
					when 2 then DATEADD(d,@inc*7,@tempDate)
					when 3 then DATEADD(m,@inc,@tempDate)
					when 4 then DATEADD(YY,@inc,@tempDate)
				end
			end
		end
		else begin
			while (@tempDate <= @lastMonthDate) 
			begin
				if (@tempDate >= @startDate)
					set @sumHoursMonth = @sumHoursMonth + @sumHours;
				set @tempDate = 
				case @iterTipe
					when 1 then DATEADD(d,@inc,@tempDate)
					when 2 then DATEADD(d,@inc*7,@tempDate)
					when 3 then DATEADD(m,@inc,@tempDate)
					when 4 then DATEADD(YY,@inc,@tempDate)
				end
			end
		end
		fetch next from @cursor into @idExcDay,@sumHours,@startDayDate,@endDate,@iterCount,@iterTipe,@inc
	end
	close @cursor
	RETURN @sumHoursMonth;
END

GO
/****** Object:  Table [dbo].[tExceptionDay]    Script Date: 01.03.2017 18:46:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tExceptionDay](
	[idExcDay] [int] NOT NULL,
	[startDate] [datetime] NOT NULL,
	[endDate] [datetime] NULL,
	[iterationTipe] [int] NOT NULL,
	[inc] [int] NOT NULL,
	[iterationCount] [int] NULL,
	[descDay] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[idExcDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tWorkTimeSpans]    Script Date: 01.03.2017 18:46:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tWorkTimeSpans](
	[idWTS] [int] NOT NULL,
	[upTime] [time](0) NOT NULL,
	[endTime] [time](0) NOT NULL,
	[idExcDay] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idWTS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [ind_1]    Script Date: 01.03.2017 18:46:28 ******/
CREATE NONCLUSTERED INDEX [ind_1] ON [dbo].[tExceptionDay]
(
	[idExcDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [ind_2]    Script Date: 01.03.2017 18:46:28 ******/
CREATE NONCLUSTERED INDEX [ind_2] ON [dbo].[tWorkTimeSpans]
(
	[idWTS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tWorkTimeSpans]  WITH CHECK ADD FOREIGN KEY([idExcDay])
REFERENCES [dbo].[tExceptionDay] ([idExcDay])
GO
USE [master]
GO
ALTER DATABASE [DB_CALENDAR] SET  READ_WRITE 
GO
