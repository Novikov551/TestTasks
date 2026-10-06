CREATE OR ALTER PROCEDURE [dbo].[sp_Books_SelectById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [id],
        [title],
        [author],
        [year],
        [publisher],
        [isbn],
        [description],
        [table_of_contents_xml],
        [created_date],
        [modified_date]
    FROM [Books]
    WHERE [id] = @Id;
END
