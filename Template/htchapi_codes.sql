SELECT        htchapi.FeatureId, htchapi.Form, htchapi.SectionId, htchapi.Field, htchapi.[Table], htchapi.SectionName, htchapi.FieldLabel, htchapi.BindID, mst_ModDeCode.ID, mst_ModDeCode.Name
FROM            htchapi INNER JOIN
                         mst_ModDeCode ON htchapi.BindID = mst_ModDeCode.CodeID




