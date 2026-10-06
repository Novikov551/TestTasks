CREATE OR ALTER PROCEDURE [dbo].[sp_Books_Insert]
    @Id UNIQUEIDENTIFIER,
    @Title NVARCHAR(500),
    @Author NVARCHAR(300),
    @Year INT,
    @Publisher NVARCHAR(300) = NULL,
    @Isbn NVARCHAR(20) = NULL,
    @Description NVARCHAR(2000) = NULL,
    @TableOfContentsXml NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [Books] ([id], [title], [author], [year], [publisher], [isbn], [description], [table_of_contents_xml], [created_date], [modified_date])
    VALUES (@Id, @Title, @Author, @Year, @Publisher, @Isbn, @Description, @TableOfContentsXml, GETUTCDATE(), GETUTCDATE());
END
