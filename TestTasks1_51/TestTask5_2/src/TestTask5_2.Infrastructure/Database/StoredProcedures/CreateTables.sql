CREATE TABLE IF NOT EXISTS books (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    title VARCHAR(500) NOT NULL,
    author VARCHAR(300) NOT NULL,
    year INT NOT NULL,
    publisher VARCHAR(300),
    isbn VARCHAR(20),
    description VARCHAR(2000),
    table_of_contents_xml TEXT NOT NULL,
    created_date TIMESTAMP NOT NULL DEFAULT (NOW() AT TIME ZONE 'utc'),
    modified_date TIMESTAMP NOT NULL DEFAULT (NOW() AT TIME ZONE 'utc')
);
