update  LiveHAPI.dbo.SubscriberMaps
set FormId=m.featureid,
SectionId= m.SectionId
FROM            LiveHAPI.dbo.SubscriberMaps AS h INNER JOIN
                         htchapicodes AS m ON h.SubField = m.Field 
  where h.FormId<>'' and h.SectionId <>''
						 



					
