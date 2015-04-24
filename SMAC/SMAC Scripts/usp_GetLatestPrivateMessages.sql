USE [db993ac12f90b045a99682a45d00246cf2]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetLatestPrivateMessages]    Script Date: 04/01/2015 10:38:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_GetLatestPrivateMessages]

@UserID varchar(20)

AS

declare @pms TABLE
(
	userId varchar(20) not null,
	id int not null
)

insert into @pms (UserId, id)  
(
	select ToUser, Max(PrivateMessageId)
	from PrivateMessages
	where FromUser=@UserID
	group by ToUser
)

insert into @pms (UserId, id)  
(
	select FromUser, Max(PrivateMessageId)
	from PrivateMessages
	where ToUser=@UserID
	group by FromUser
)

select pm.Content, pm.DateRead, pm.DateSent, pm.FromUser, pm.PrivateMessageId, pm.ToUser, 
u1.FirstName as 'SenderFN', u1.MiddleName as 'SenderMN', u1.LastName as 'SenderLN',
u2.FirstName as 'ReceiverFN', u2.MiddleName as 'ReceiverMN', u2.LastName as 'ReceiverLN'

from PrivateMessages pm
inner join Users u1 on u1.UserId = pm.FromUser
inner join Users u2 on u2.UserId = pm.ToUser
where pm.PrivateMessageId in(
select MAX(id) from @pms
group by userId)
order by pm.DateSent desc


GO

