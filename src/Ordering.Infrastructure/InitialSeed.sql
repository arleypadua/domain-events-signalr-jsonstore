-- JsonStore doesn't support migrations or schema creations, therefore you need to provide them

IF NOT EXISTS (select * from sysobjects where name='Order' and xtype='U')
BEGIN
	CREATE TABLE [dbo].[Order] -- The table name is inferred by the content class name or customized via collection
	(
		[_Id] VARCHAR(50) PRIMARY KEY, -- default field name for the id required by JsonStore
		[_Document] VARCHAR(MAX) NOT NULL, -- column where the document will be stored
		[CustomerName] VARCHAR(250) NOT NULL -- customized by the Order Collection when overriding the method: GetIndexedValuesInternal
	)
END
GO