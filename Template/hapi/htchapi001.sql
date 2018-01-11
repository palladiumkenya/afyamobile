IF EXISTS(SELECT * FROM sysobjects WHERE name='tempMainFields' AND type='v')
	DROP VIEW tempMainFields
IF EXISTS(SELECT * FROM sysobjects WHERE name='tempConditionalFields' AND type='v')
	DROP VIEW tempConditionalFields
IF EXISTS(SELECT * FROM sysobjects WHERE name='vw_DetailsOfAllFields' AND type='v')
	DROP VIEW vw_DetailsOfAllFields
go
--==

--Main fields
CREATE VIEW tempMainFields 
as
Select * from 
(select tbl1.FeatureId, tbl2.FeatureName, tbl1.SectionId, tbl3.SectionName, 
 case tbl1.Predefined when 1 then '9999'+convert(varchar,tbl1.FieldId) when 0 then '8888'+convert(varchar,Tbl1.FieldId) end [FieldId],   
 tbl4.BindField[FieldName],replace(tbl1.FieldLabel,'''','')[FieldLabel], tbl1.Predefined,
 UPPER(tbl4.PDFTableName)[PDFTableName], tbl4.ControlId, tbl4.BindTable[BindSource],tbl4.CategoryId [CodeId],   
 tbl1.Seq, tbl3.Seq [SeqSection] ,tbl3.IsGridView, tbl7.TabId, tbl8.TabName, tbl8.seq[tabSeq]  
 from Lnk_forms tbl1 inner join mst_feature tbl2 on tbl1.FeatureId = tbl2.FeatureID  
 inner join mst_section tbl3 on tbl1.SectionId=tbl3.SectionID   
 inner join Mst_PreDefinedFields tbl4  on tbl1.FieldId=tbl4.Id    
 left outer join mst_pmtctcode tbl5 on (tbl4.CategoryId=tbl5.CodeId and tbl4.BindTable = 'Mst_PMTCTDecode')
 left outer join mst_code tbl6 on (tbl4.CategoryId=tbl6.CodeId and Tbl4.BindTable = 'Mst_DeCode')   
 inner join dbo.lnk_FormTabSection tbl7 on tbl1.FeatureId=tbl7.FeatureId and tbl1.SectionId=tbl7.SectionId  
 inner join dbo.Mst_FormBuilderTab tbl8 on tbl7.TabId=tbl8.TabId   
 where tbl1.predefined=1 and  tbl1.FeatureId in (SELECT FeatureID FROM dbo.mst_Feature WHERE FeatureID > 1000 AND DeleteFlag=0) and tbl1.FieldId<>71 and (tbl4.PatientRegistration IS NULL or tbl4.PatientRegistration=0) 
 and (tbl2.Deleteflag=0 or tbl2.Deleteflag is null) 
union
select tbl1.FeatureId, tbl2.FeatureName, tbl1.SectionId, tbl3.SectionName, 
  tbl1.FieldId, 'PlaceHolder'+Convert(varchar,tbl1.Seq)+Convert(varchar,tbl1.SectionId)[FieldName],replace(tbl1.FieldLabel,'''','')[FieldLabel], tbl1.Predefined,     
 UPPER(tbl4.PDFTableName)[PDFTableName], tbl4.ControlId, tbl4.BindTable[BindSource],tbl4.CategoryId [CodeId], 
 tbl1.Seq, tbl3.Seq [SeqSection] ,tbl3.IsGridView, tbl7.TabId, tbl8.TabName, tbl8.seq[tabSeq] 
 from Lnk_forms tbl1 inner join mst_feature tbl2 on tbl1.FeatureId = tbl2.FeatureID
  inner join mst_section tbl3 on tbl1.SectionId=tbl3.SectionID   
 inner join Mst_PreDefinedFields tbl4  on 71 =tbl4.Id
 left outer join mst_pmtctcode tbl5 on (tbl4.CategoryId=tbl5.CodeId and tbl4.BindTable = 'Mst_PMTCTDecode')
 left outer join mst_code tbl6 on (tbl4.CategoryId=tbl6.CodeId and Tbl4.BindTable = 'Mst_DeCode')   
 inner join dbo.lnk_FormTabSection tbl7 on tbl1.FeatureId=tbl7.FeatureId and tbl1.SectionId=tbl7.SectionId  
 inner join dbo.Mst_FormBuilderTab tbl8 on tbl7.TabId=tbl8.TabId    
 where tbl1.predefined=1  and  tbl1.FeatureId in (SELECT FeatureID FROM dbo.mst_Feature WHERE FeatureID > 1000 AND DeleteFlag=0)  and tbl1.FieldId=71 and (tbl4.PatientRegistration IS NULL or tbl4.PatientRegistration=0)--and substring(convert(varchar,tbl1.fieldid),3,5) = '00000' 
 and (tbl2.Deleteflag=0 or tbl2.Deleteflag is null)
union   
select tbl1.FeatureId, tbl2.FeatureName, tbl1.SectionId, tbl3.SectionName,
 case tbl1.Predefined when 1 then '9999'+convert(varchar,tbl1.FieldId) when 0 then '8888'+convert(varchar,Tbl1.FieldId) end [FieldId],    
 tbl4.FieldName[FieldName], replace(tbl1.FieldLabel,'''','')[FieldLabel], tbl1.Predefined,
'PDFTableName'=Upper(Case When ControlId = 11 then NULL When ControlId = 12 then NULL when ControlId = 16 then NULL else 'dtl_CustomField' End), 
tbl4.ControlId, tbl4.BindTable [BindSource], tbl5.CodeID,    
tbl1.Seq, tbl3.Seq [SeqSection],tbl3.IsGridView, tbl7.TabId, tbl8.TabName, tbl8.seq[tabSeq]     
from Lnk_forms tbl1 inner join mst_feature tbl2 on tbl1.FeatureId = tbl2.FeatureID     
 inner join mst_section tbl3 on tbl1.SectionId=tbl3.SectionID  
 inner join mst_CustomformField tbl4 on tbl1.FieldId=tbl4.Id  
 left outer join mst_Modcode tbl5 on (tbl4.CategoryId=tbl5.CodeId and tbl4.BindTable = 'Mst_ModDecode')   
 inner join dbo.lnk_FormTabSection tbl7 on tbl1.FeatureId=tbl7.FeatureId and tbl1.SectionId=tbl7.SectionId  
 inner join dbo.Mst_FormBuilderTab tbl8 on tbl7.TabId=tbl8.TabId 
 where tbl1.Predefined=0 and  tbl1.FeatureId in (SELECT FeatureID FROM dbo.mst_Feature WHERE FeatureID > 1000 AND DeleteFlag=0) and (tbl3.IsGridView <>1 or tbl3.IsGridView IS NULL)    
 and (tbl4.PatientRegistration IS NULL or tbl4.PatientRegistration=0)
 and (tbl2.Deleteflag=0 or tbl2.Deleteflag is null) 
union   
select tbl1.FeatureId, tbl2.FeatureName, tbl1.SectionId, tbl3.SectionName,   
 case tbl1.Predefined when 1 then '9999'+convert(varchar,tbl1.FieldId) when 0 then '8888'+convert(varchar,Tbl1.FieldId) end [FieldId],  
 tbl4.FieldName[FieldName], replace(tbl1.FieldLabel,'''','')[FieldLabel], tbl1.Predefined, 
'PDFTableName'=Upper(Case When ControlId = 11 then NULL When ControlId = 12 then NULL when ControlId = 16 then NULL else 'DTL_CUSTOMFORM' End), 
tbl4.ControlId, tbl4.BindTable [BindSource], tbl5.CodeID,    
tbl1.Seq, tbl3.Seq [SeqSection],tbl3.IsGridView, tbl7.TabId, tbl8.TabName, tbl8.seq[tabSeq]  
from Lnk_forms tbl1 inner join mst_feature tbl2 on tbl1.FeatureId = tbl2.FeatureID     
 inner join mst_section tbl3 on tbl1.SectionId=tbl3.SectionID 
 inner join mst_CustomformField tbl4 on tbl1.FieldId=tbl4.Id  
 left outer join mst_Modcode tbl5 on (tbl4.CategoryId=tbl5.CodeId and tbl4.BindTable = 'Mst_ModDecode')  
 inner join dbo.lnk_FormTabSection tbl7 on tbl1.FeatureId=tbl7.FeatureId and tbl1.SectionId=tbl7.SectionId  
 inner join dbo.Mst_FormBuilderTab tbl8 on tbl7.TabId=tbl8.TabId 
  where tbl1.Predefined=0 and  tbl1.FeatureId in (SELECT FeatureID FROM dbo.mst_Feature WHERE FeatureID > 1000 AND DeleteFlag=0) and tbl3.IsGridView =1  and (tbl4.PatientRegistration IS NULL or tbl4.PatientRegistration=0)
 and (tbl2.Deleteflag=0 or tbl2.Deleteflag is null)  
) Z  
go
--==

--Conditional Fields  
CREATE view tempConditionalFields
AS
Select * from 
(select a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,  
case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId) end [FieldId],    
a.ConditionalFieldBindField [FieldName],  
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],  
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],     
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],  
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,    
case a.FieldPredefined when 1 then '9999'+convert(varchar,a.FieldId) when 0 then '8888'+convert(varchar,a.FieldId) end [ConFieldId],    
a.FieldPredefined [ConFieldPredefined], a.TabId, 
a.TabName
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId  
and a.ConditionalFieldPredefined = 1 and b.FeatureId in (SELECT FeatureID FROM dbo.mst_Feature WHERE FeatureID > 1000 AND DeleteFlag=0) and a.ConditionalFieldId is not null     
and a.ConditionalFieldName is not null and a.ConditionalFieldControlId = 6
union  
select a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,  
case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId) end [FieldId],    
a.ConditionalFieldName [FieldName], 
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],  
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],  
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],  
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,    
case a.FieldPredefined when 1 then '9999'+convert(varchar,a.FieldId) when 0 then '8888'+convert(varchar,a.FieldId) end [ConFieldId],    
a.FieldPredefined [ConFieldPredefined], a.TabId,
a.TabName
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId
and a.ConditionalFieldPredefined = 0 and b.FeatureId in (SELECT FeatureID FROM dbo.mst_Feature WHERE FeatureID > 1000 AND DeleteFlag=0) and a.ConditionalFieldId is not null     
and a.ConditionalFieldName is not null and a.ConditionalFieldControlId = 6
union 
select a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,  
case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId) end [FieldId],    
a.ConditionalFieldBindField [FieldName],  
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],  
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],     
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],  
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,    
case a.FieldPredefined when 1 then '9999'+convert(varchar,a.FieldId) when 0 then '8888'+convert(varchar,a.FieldId) end [ConFieldId],    
a.FieldPredefined [ConFieldPredefined], a.TabId, 
a.TabName
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId  
and a.ConditionalFieldPredefined = 1 and b.FeatureId in (SELECT FeatureID FROM dbo.mst_Feature WHERE FeatureID > 1000 AND DeleteFlag=0) and a.ConditionalFieldId is not null     
and a.ConditionalFieldName is not null     
union    
select a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,  
case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId) end [FieldId],    
a.ConditionalFieldName [FieldName], 
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],  
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],  
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],  
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,    
case a.FieldPredefined when 1 then '9999'+convert(varchar,a.FieldId) when 0 then '8888'+convert(varchar,a.FieldId) end [ConFieldId],    
a.FieldPredefined [ConFieldPredefined], a.TabId,
a.TabName
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId
and a.ConditionalFieldPredefined = 0 and b.FeatureId in (SELECT FeatureID FROM dbo.mst_Feature WHERE FeatureID > 1000 AND DeleteFlag=0) and a.ConditionalFieldId is not null     
and a.ConditionalFieldName is not null     
union     
select a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,  
a.ConditionalFieldId [FieldId],'PlaceHolder' [FieldName],  
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],  
Upper(a.ConditionalFieldSavingTable) [PDFTableName],'13' [ControlId],    
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],  
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,a.FieldId [ConFieldId],    
a.FieldPredefined [ConFieldPredefined], a.TabId, 
a.TabName 
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId  
and a.ConditionalFieldPredefined = 1 and b.FeatureId in (SELECT FeatureID FROM dbo.mst_Feature WHERE FeatureID > 1000 AND DeleteFlag=0) and a.ConditionalFieldId is not null
and a.ConditionalFieldId like '710000%' 
) cond
go
--==

--All fields - combined
CREATE VIEW vw_DetailsOfAllFields
AS
SELECT * FROM
(
SELECT FeatureId , FeatureName AS Form, SectionId ,  FieldId , FieldName AS Field,
        CASE WHEN Predefined=0 AND IsGridView=0 AND b.Name = 'Multi select' THEN 'dtl_fb_'+FieldName
			WHEN Predefined=0 AND IsGridView=0 AND b.Name <> 'Multi select' THEN 'DTL_FBCUSTOMFIELD_'+ REPLACE(FeatureName, ' ', '_')
			WHEN Predefined=0 AND IsGridView=1 THEN 'DTL_CUSTOMFORM_'+SectionName+'_'+ REPLACE(FeatureName, ' ', '_')
			WHEN Predefined = 1 THEN PDFTableName
        END AS [Table], 
        SectionName , FieldLabel , Predefined , PDFTableName , b.Name AS FieldType , BindSource BindTable, CodeId BindID, 
        CAST(Seq AS DECIMAL(5,1)) FieldOrder,
        0 AS ConditionalFieldSeq, SeqSection SectionOrder, IsGridView , TabId , TabName , tabSeq AS TabOrder
FROM dbo.tempMainFields a
INNER JOIN dbo.mst_control b ON a.ControlId = b.ControlID      
UNION
SELECT DISTINCT a.FeatureId , a.FeatureName , a.FieldSectionId , a.FieldId, a.FieldName ,
        CASE WHEN a.Predefined=0 AND b.IsGridView=0 AND c.Name = 'Multi select' THEN 'DTL_FB_'+a.FieldName
			WHEN a.Predefined=0 AND b.IsGridView=0 AND c.Name <> 'Multi select' THEN 'DTL_FBCUSTOMFIELD_'+ REPLACE(a.FeatureName, ' ', '_')
			WHEN a.Predefined=0 AND b.IsGridView=1 THEN 'DTL_CUSTOMFORM_'+SectionName+'_'+ REPLACE(a.FeatureName, ' ', '_')
			WHEN a.Predefined = 1 THEN a.PDFTableName
        END AS TableName,
        a.FieldSectionName , a.FieldLabel , a.Predefined , a.PDFTableName , c.Name AS FieldType , a.BindSource , a.CodeId , 
        cast(CAST(b.Seq AS VARCHAR)+'.'+CAST(a.Seq AS VARCHAR) AS decimal(5,1)) AS seq ,
        a.Seq AS ConditionalFieldSeq, a.SeqSection , b.IsGridView , a.TabId , a.TabName , b.tabSeq
FROM dbo.tempConditionalFields a
INNER JOIN dbo.tempMainFields b ON a.ConFieldId = b.FieldId AND a.FieldSectionId = b.SectionId AND a.FeatureId = b.FeatureId
INNER JOIN dbo.mst_control c ON a.ControlId = c.ControlID      
) allFields
go
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM sysobjects WHERE name='htchapiall' AND type='v')
	DROP VIEW htchapiall
Go

create view htchapiall
as
select distinct * from vw_DetailsOfAllFields where (form like '%htc%' or form like '%link%' )

Go
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

IF EXISTS(SELECT * FROM sysobjects WHERE name='htchapi' AND type='v')
	DROP VIEW htchapi
Go

create view htchapi
as
select distinct * from vw_DetailsOfAllFields where (form like '%htc%' or form like '%link%' )and bindtable='Mst_ModDecode'

Go

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


IF EXISTS(SELECT * FROM sysobjects WHERE name='htchapicodes' AND type='v')
	DROP VIEW htchapicodes
Go

create view htchapicodes
as
SELECT        htchapi.FeatureId, htchapi.Form, htchapi.SectionId, htchapi.Field, htchapi.[Table], htchapi.SectionName, htchapi.FieldLabel, htchapi.BindID, mst_ModDeCode.ID, mst_ModDeCode.Name
FROM            htchapi INNER JOIN
                         mst_ModDeCode ON htchapi.BindID = mst_ModDeCode.CodeID

GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


IF EXISTS(SELECT * FROM sysobjects WHERE name='htchapicodelink' AND type='v')
	DROP VIEW htchapicodelink
Go

create view htchapicodelink
as
SELECT        h.Code, h.Display, h.SubCode, h.SubDisplay, h.SubRef, h.Ref, m.ID, m.Name, m.FeatureId, m.Form, m.SectionId, m.Field, m.FieldLabel
FROM            LiveHAPI.dbo.SubscriberTranslations AS h INNER JOIN
                         htchapicodes AS m ON h.SubRef = m.Field AND h.SubDisplay = m.Name
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

update   LiveHAPI.dbo.SubscriberTranslations
set SubCode=m.id
FROM            LiveHAPI.dbo.SubscriberTranslations AS h INNER JOIN
                         htchapicodes AS m ON h.SubRef = m.Field AND h.SubDisplay = m.Name
Go


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
update  LiveHAPI.dbo.SubscriberMaps
set FormId=m.featureid,
SectionId= m.SectionId
FROM            LiveHAPI.dbo.SubscriberMaps AS h INNER JOIN
                         htchapicodes AS m ON h.SubField = m.Field 
  where h.FormId<>'' and h.SectionId <>''
 Go