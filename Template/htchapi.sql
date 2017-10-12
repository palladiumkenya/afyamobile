create view htchapi
as
select distinct * from vw_DetailsOfAllFields where (form like '%htc%' or form like '%link%' )and bindtable='Mst_ModDecode'