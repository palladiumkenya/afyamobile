update   LiveHAPI.dbo.SubscriberTranslations
set SubCode=m.id
FROM            LiveHAPI.dbo.SubscriberTranslations AS h INNER JOIN
                         htchapicodes AS m ON h.SubRef = m.Field AND h.SubDisplay = m.Name


