USE [db993ac12f90b045a99682a45d00246cf2]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetUsersInSchool]    Script Date: 04/01/2015 10:39:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUsersInSchool]
	-- Add the parameters for the stored procedure here
	@SchoolId int,
	@UserId varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select u.UserId, u.FirstName, u.MiddleName, u.LastName, u.EmailAddress, u.PhoneNumber, 
	u.Gender, u.StartDate, u.EndDate, u.IsActive, u.LastLoggedIn, u.LastLoggedOut,
	case when adm.UserId IS NULL then CONVERT(bit, 0) else CONVERT(bit, 1) end as 'Admin',
	case when staf.UserId IS NULL then CONVERT(bit, 0) else CONVERT(bit, 1) end as 'Staff',
	case when stu.UserId IS NULL then CONVERT(bit, 0) else CONVERT(bit, 1) end as 'Student',
	case when t.UserId IS NULL then CONVERT(bit, 0) else CONVERT(bit, 1) end as 'Teacher',
	s.SchoolName
	from Users u
	left outer join Admins adm on adm.UserId = u.UserId
	left outer join Students stu on stu.UserId = u.UserId
	left outer join Staff staf on staf.UserId = u.UserId
	left outer join Teachers t on t.UserId = u.UserId
	inner join SchoolRoster sr on sr.UserId = u.UserId
	inner join Schools s on s.SchoolId = sr.SchoolId
	where sr.SchoolId = @SchoolId and u.UserId <> @UserId
END

GO

