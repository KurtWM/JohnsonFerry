USE [ArenaDB]
GO

/****** Object:  StoredProcedure [dbo].[core_sp_JFBC_get_staff_contact_list]    Script Date: 02/14/2013 14:22:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[core_sp_JFBC_get_staff_contact_list]

AS

SELECT DISTINCT 
      CP.person_id, 
      CP.nick_name + ' ' + CP.last_name AS name, 
      CP.nick_name, 
      CP.first_name, 
      CP.last_name,
      CPA3.varchar_value AS business_phone,
      NULL AS business_ext,
      CPA4.varchar_value AS email,
      CP.guid,
      CL1.lookup_value AS ministry,
      CL2.lookup_value AS title

      FROM dbo.core_person AS CP

      LEFT JOIN dbo.core_person_attribute AS CPA1 ON CPA1.person_id = CP.person_id AND CPA1.attribute_id = 99 --This ties core_person to core_person_attribute for rows that match both the person ID and Attribute ID for Ministry
      LEFT JOIN dbo.core_lookup AS CL1 ON CL1.lookup_id = CPA1.int_value --This join buids on the one above and ties the core_person_attribute to the core_lookup table to get the actual text value
      LEFT JOIN dbo.core_person_attribute AS CPA2 ON CPA2.person_id = CP.person_id AND CPA2.attribute_id = 100 --This join and the one below does the exact same thing for attribute 100. We can then just refrence CL1 and CL2 in the SELECT statement without needed a subquery
      LEFT JOIN dbo.core_lookup AS CL2 ON CL2.lookup_id = CPA2.int_value
      LEFT JOIN dbo.core_person_attribute AS CPA3 ON CPA3.person_id = CP.person_id AND CPA3.attribute_id = 225 --This ties core_person to core_person_attribute for rows that match both the person ID and Attribute ID for Contact Phone Number
      LEFT JOIN dbo.core_person_attribute AS CPA4 ON CPA4.person_id = CP.person_id AND CPA4.attribute_id = 226 --This ties core_person to core_person_attribute for rows that match both the person ID and Attribute ID for Contact Email
      LEFT JOIN dbo.core_person_attribute AS CPA5 ON CPA5.person_id = CP.person_id AND CPA5.attribute_id = 227 --This ties core_person to core_person_attribute for rows that match both the person ID and Attribute ID for Include in Website Directory

      WHERE CP.staff_member = 1 AND CPA5.int_value = 1

	  ORDER BY CP.last_name, CP.first_name
GO


