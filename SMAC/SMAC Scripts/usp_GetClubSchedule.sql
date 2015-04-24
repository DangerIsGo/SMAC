USE [db993ac12f90b045a99682a45d00246cf2]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetClubSchedule]    Script Date: 04/01/2015 10:38:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetClubSchedule]
	-- Add the parameters for the stored procedure here
	@clubId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @tss table
	(
		day varchar(15),
		timespan varchar(50)
	)

	insert into @tss (day, timespan)
	( select cs.Day, CONVERT(varchar(30), ts.StartTime, 108) + ' to ' + CONVERT(varchar(30), ts.EndTime, 108) from Clubs c
			inner join ClubEnrollments ce on ce.ClubId = c.ClubId
			inner join ClubSchedules cs on cs.ClubId = c.ClubId
			inner join TimeSlots ts on ts.TimeSlotId = cs.TimeSlotId
			where c.ClubId = @clubId
			)

	select day as 'Day', stuff( (select distinct ',' + timespan
	from @tss 
	where day = tss.day
	for xml path('')), 1, 1, '') as TimeSpans
	from @tss as tss
	group by day
END

GO

