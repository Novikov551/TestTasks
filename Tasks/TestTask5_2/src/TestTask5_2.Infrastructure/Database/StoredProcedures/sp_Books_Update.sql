CREATE OR ALTER PROCEDURE [dbo].[sp_Books_Update]
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

    UPDATE [Books]
    SET
        [title] = @Title,
        [author] = @Author,
        [year] = @Year,
        [publisher] = @Publisher,
        [isbn] = @Isbn,
        [description] = @Description,
        [table_of_contents_xml] = @TableOfContentsXml,
        [modified_date] = GETUTCDATE()
    WHERE [id] = @Id;
END
