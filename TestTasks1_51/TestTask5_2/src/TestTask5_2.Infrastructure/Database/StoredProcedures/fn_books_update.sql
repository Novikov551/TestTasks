CREATE OR REPLACE FUNCTION fn_books_update(
    p_id UUID,
    p_title VARCHAR,
    p_author VARCHAR,
    p_year INT,
    p_publisher VARCHAR,
    p_isbn VARCHAR,
    p_description VARCHAR,
    p_table_of_contents_xml TEXT
)
RETURNS BOOLEAN
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE books
    SET
        title = p_title,
        author = p_author,
        year = p_year,
        publisher = p_publisher,
        isbn = p_isbn,
        description = p_description,
        table_of_contents_xml = p_table_of_contents_xml,
        modified_date = NOW() AT TIME ZONE 'utc'
    WHERE id = p_id;

    IF NOT FOUND THEN
        RETURN FALSE;
    END IF;

    RETURN TRUE;
END;
$$;
