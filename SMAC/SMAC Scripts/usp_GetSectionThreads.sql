USE [db993ac12f90b045a99682a45d00246cf2]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetSectionThreads]    Script Date: 04/02/2015 01:17:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetSectionThreads] 
	-- Add the parameters for the stored procedure here
	@sectionId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @thread table
	(
		id int,
		title varchar(250),
		author varchar(101),
		lastPostAt datetime,
		lastPostBy varchar(101),
		replies int
	)

	insert into @thread (id, title, author, lastPostAt, lastPostBy, replies)
	(
		select t.ThreadId, t.ThreadTitle, u.FirstName + ' ' + u.LastName, null, null, null from Threads t
		inner join Users u on t.UserId = u.UserId
		where t.SectionId = @sectionId
		and t.RepliedTo IS NULL
	)

	declare @minID int
	declare @count int
	declare @poster varchar(20)
	declare @posted datetime

	select @minID = min(id) from @thread

	while @minID is NOT NULL
	BEGIN
		set @poster = NULL
		set @posted = NULL
		select @count = count(*) from Threads where RepliedTo = @minID

		select TOP(1) @poster = u.FirstName + ' ' + u.LastName, @posted = t.DateTimePosted 
		from Threads t 
		inner join Users u on u.UserId = t.UserId
		where RepliedTo = @minID
		order by t.DateTimePosted desc

		IF @poster IS NULL
		BEGIN
			select TOP(1) @poster = u.FirstName + ' ' + u.LastName, @posted = t.DateTimePosted
			from Threads t 
			inner join Users u on u.UserId = t.UserId
			where ThreadId = @minID
		END

		update @thread
		set replies = @count, lastPostAt = @posted, lastPostBy = @poster
		where id = @minID

		select @minID = min(id) from @thread where @minID < id
	END

	select * from @thread
	order by lastPostAt desc
END

GO

