import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { fetchBooks, deleteBook } from "../api";

export default function BookList() {
    const [books, setBooks] = useState([]);
    const [query, setQuery] = useState("");
    const [field, setField] = useState("title");
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const timeout = setTimeout(async () => {
            setLoading(true);
            try { setBooks(await fetchBooks(query, field)); }
            catch (e) { console.error(e); }
            finally { setLoading(false); }
        }, 300);
        return () => clearTimeout(timeout);
    }, [query, field]);

    const handleDelete = async (id) => {
        if (!confirm("Удалить книгу?")) return;
        await deleteBook(id);
        setBooks(books.filter(b => b.id !== id));
    };

    return (
        <>
            <div className="page-header">
                <h2>Список книг</h2>
                <Link to="/create" className="btn btn-success">+ Добавить книгу</Link>
            </div>
            <div className="search-bar">
                <input
                    type="text"
                    placeholder="Поиск..."
                    value={query}
                    onChange={(e) => setQuery(e.target.value)}
                />
                <select value={field} onChange={(e) => setField(e.target.value)}>
                    <option value="title">По названию</option>
                    <option value="author">По автору</option>
                    <option value="tableofcontents">По оглавлению</option>
                </select>
            </div>
            {loading ? <p>Загрузка...</p> : books.length === 0 ? <div className="alert alert-info">Книги не найдены.</div> : (
                <div className="card-grid">
                    {books.map((book) => (
                        <div key={book.id} className="card">
                            <h3>{book.title}</h3>
                            <div className="author">{book.author}</div>
                            <div className="meta">Год: {book.year}{book.publisher && <> · {book.publisher}</>}</div>
                            <div className="card-actions">
                                <Link to={`/book/${book.id}`} className="btn btn-outline btn-sm">Просмотр</Link>
                                <Link to={`/edit/${book.id}`} className="btn btn-warning btn-sm">Редактировать</Link>
                                <button onClick={() => handleDelete(book.id)} className="btn btn-danger btn-sm">Удалить</button>
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </>
    );
}
