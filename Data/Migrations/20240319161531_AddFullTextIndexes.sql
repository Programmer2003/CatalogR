IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[Collections]'))
ALTER FULLTEXT INDEX ON [dbo].[Collections] DISABLE

GO
IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[Collections]'))
BEGIN
	DROP FULLTEXT INDEX ON [dbo].[Collections]
End

Go
IF EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE [name]='FTCCollection')
BEGIN
	DROP FULLTEXT CATALOG FTCCollection
END

CREATE FULLTEXT CATALOG FTCCollection AS DEFAULT;
CREATE FULLTEXT INDEX ON dbo.Collections(Name) KEY INDEX PK_Collections ON FTCCollection WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;




IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[Items]'))
ALTER FULLTEXT INDEX ON [dbo].[Items] DISABLE

GO
IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[Items]'))
BEGIN
	DROP FULLTEXT INDEX ON [dbo].[Items]
End

Go
IF EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE [name]='FTCItem')
BEGIN
	DROP FULLTEXT CATALOG FTCItem 
END

CREATE FULLTEXT CATALOG FTCItem AS DEFAULT;
CREATE FULLTEXT INDEX ON dbo.Items(Name, CustomString1, CustomString2, CustomString3) KEY INDEX PK_Items ON FTCItem WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;




IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[Comments]'))
ALTER FULLTEXT INDEX ON [dbo].[Comments] DISABLE

GO
IF  EXISTS (SELECT * FROM sys.fulltext_indexes fti WHERE fti.object_id = OBJECT_ID(N'[dbo].[Comments]'))
BEGIN
	DROP FULLTEXT INDEX ON [dbo].[Comments]
End

Go
IF EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE [name]='FTCComment')
BEGIN
	DROP FULLTEXT CATALOG FTCComment 
END

CREATE FULLTEXT CATALOG FTCComment AS DEFAULT;
CREATE FULLTEXT INDEX ON dbo.Comments(Text) KEY INDEX PK_Comments ON FTCComment WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;