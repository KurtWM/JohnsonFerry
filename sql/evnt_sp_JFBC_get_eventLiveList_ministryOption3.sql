USE [ArenaDev]
GO

/****** Object:  StoredProcedure [dbo].[evnt_sp_JFBC_get_eventLiveList_ministryOption3]    Script Date: 02/14/2013 14:24:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:        Kurt Meredith
-- Create Date:   2013-01-16
-- Modified Date: 2013-01-28
-- Description:   Gets list of current events 
--                with the option to filter by 
--                a list of Topic Areas.
-- =============================================
CREATE proc [dbo].[evnt_sp_JFBC_get_eventLiveList_ministryOption3]
@TopicAreas varchar(200), 
@OrganizationId int 





AS

SET NOCOUNT ON;
/* Variable Declaration */
DECLARE @SelectStatement AS nvarchar(max)
DECLARE @Params AS nvarchar(1000) 

SET @Params = '
	@TopicAreas varchar(200), 
	@OrganizationId int'



/* Build the Transact-SQL String with the input parameters */ 
Set @SelectStatement = 
	'SELECT  
		P.profile_id,
		LU.lookup_id,
		LU.lookup_value,
		P.profile_name,
		P.profile_desc,
		PR.details,
		PR.start,
		PR.[end],
		PR.contact_name,
		PR.contact_phone,
		PR.contact_email, 
		PR.type_luid
	FROM dbo.core_profile AS P 
	INNER JOIN dbo.evnt_event_profile AS PR ON P.profile_id = PR.profile_id 
	INNER JOIN dbo.mktg_promotion_request AS MPR ON PR.profile_id = MPR.event_id
	LEFT JOIN dbo.core_lookup AS LU ON PR.topic_area_luid = LU.lookup_id '

IF @TopicAreas <> '' 
	SET @SelectStatement = @SelectStatement + 
		'LEFT OUTER JOIN skel_bone B ON B.entity_id = PR.profile_id AND B.entity_type = ' + CAST(dbo.[core_funct_luid_topicEntityTypeEvent](@OrganizationId) AS varchar) + ' '

SET @SelectStatement = @SelectStatement + 
	'WHERE (P.active = 1) 
	AND (MPR.web_approved_date > CONVERT(DATETIME, ''1900-01-01 00:00:00'', 102))
	AND (MPR.web_start_date < GETDATE())
	AND (MPR.web_end_date > GETDATE()) 
	AND (P.organization_id = @OrganizationId) '
	
	
	
	
IF @TopicAreas <> '' 
    SET @SelectStatement = @SelectStatement + 
		'AND (PR.topic_area_luid IN (select convert(int, item) from dbo.fnSplit(@TopicAreas)) OR B.topic_luid in (select convert(int, item) from dbo.fnSplit(@TopicAreas))) '
     
     
     
     
     
SET @SelectStatement = @SelectStatement + 
	'ORDER BY PR.start'


/* Execute the Transact-SQL String with all parameter value's Using sp_executesql Command */
exec sp_executesql @SelectStatement, @Params, 
            @TopicAreas = @TopicAreas,
            @OrganizationId = @OrganizationId
            

If @@ERROR <> 0 GoTo ErrorHandler
Set NoCount OFF
Return(0)
  
ErrorHandler:
    Return(@@ERROR)



GO


