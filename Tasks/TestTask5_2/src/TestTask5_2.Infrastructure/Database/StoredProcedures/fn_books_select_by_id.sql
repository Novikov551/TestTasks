CREATE OR REPLACE FUNCTION fn_books_select_by_id(
    p_id UUID
)
RETURNS TABLE (
    o_id UUID,
    o_title TEXT,
    o_author TEXT,
    o_year INT,
    o_publisher TEXT,
    o_isbn TEXT,
    o_description TEXT,
    o_table_of_contents_xml TEXT,
    o_created_date TIMESTAMPTZ,
    o_modified_date TIMESTAMPTZ
)
LANGUAGE plpgsql
AS $$
BEGIN

    RETURN QUERY
    SELECT
        b.id,
        b.title::TEXT,
        b.author::TEXT,
        b.year,
        b.publisher::TEXT,
        b.isbn::TEXT,
        b.description::TEXT,
        b.table_of_contents_xml,
        b.created_date,
        b.modified_date
    FROM books b
    WHERE b.id = p_id;
END;
$$;
