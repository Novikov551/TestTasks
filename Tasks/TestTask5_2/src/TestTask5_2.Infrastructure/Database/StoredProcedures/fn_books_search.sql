CREATE OR REPLACE FUNCTION fn_books_search(
    p_query TEXT,
    p_field TEXT
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
    WHERE
        CASE p_field
            WHEN 'author' THEN b.author ILIKE '%' || p_query || '%'
            WHEN 'tableofcontents' THEN b.table_of_contents_xml ILIKE '%' || p_query || '%'
            ELSE b.title ILIKE '%' || p_query || '%'
        END
    ORDER BY b.title;
END;
$$;
