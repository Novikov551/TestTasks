CREATE OR REPLACE FUNCTION fn_books_insert(
    p_id UUID,
    p_title VARCHAR,
    p_author VARCHAR,
    p_year INT,
    p_publisher VARCHAR,
    p_isbn VARCHAR,
    p_description VARCHAR,
    p_table_of_contents_xml TEXT
)
RETURNS UUID
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO books (id, title, author, year, publisher, isbn, description, table_of_contents_xml, created_date, modified_date)
    VALUES (p_id, p_title, p_author, p_year, p_publisher, p_isbn, p_description, p_table_of_contents_xml, NOW() AT TIME ZONE 'utc', NOW() AT TIME ZONE 'utc');

    RETURN p_id;
END;
$$;
