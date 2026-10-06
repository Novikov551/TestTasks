CREATE OR ALTER PROCEDURE [dbo].[sp_Books_Search]
    @Query NVARCHAR(500),
    @Field NVARCHAR(50)
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
    WHERE
        CASE @Field
            WHEN ''author'' THEN [author]
            WHEN ''tableofcontents'' THEN [table_of_contents_xml]
            ELSE [title]
        END LIKE ''%'' + @Query + ''%''
    ORDER BY [title];
END
