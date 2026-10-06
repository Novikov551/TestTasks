import { useState, useEffect } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { fetchBook, deleteBook } from "../api";

export default function BookDetails() {
    const { id } = useParams();
    const navigate = useNavigate();
    const [book, setBook] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchBook(id).then(setBook).catch(() => navigate("/")).finally(() => setLoading(false));
    }, [id]);

    const handleDelete = async () => {
        if (!confirm("Удалить книгу?")) return;
        await deleteBook(id);
        navigate("/");
    };

    const parseToc = (xml) => {
        try {
            const doc = new DOMParser().parseFromString(xml, "text/xml");
            return Array.from(doc.querySelectorAll("chapter")).map((ch) => ({
                number: ch.getAttribute("number") || "",
                title: ch.getAttribute("title") || ""
            }));
        } catch { return []; }
    };

    if (loading) return <p>Загрузка...</p>;
    if (!book) return <div className="alert alert-info">Книга не найдена.</div>;

    const toc = parseToc(book.table_of_contents_xml);

    return (
        <>
            <div className="page-header">
                <h2>{book.title}</h2>
                <div style={{ display: "flex", gap: "0.5rem" }}>
                    <Link to={`/edit/${book.id}`} className="btn btn-warning">Редактировать</Link>
                    <button onClick={handleDelete} className="btn btn-danger">Удалить</button>
                    <Link to="/" className="btn btn-outline">Назад</Link>
                </div>
            </div>
            <h5 style={{ color: "#666", marginBottom: "1rem" }}>{book.author}</h5>
            <table className="detail-table">
                <tbody>
                    <tr><th>Год издания</th><td>{book.year}</td></tr>
                    {book.publisher && <tr><th>Издательство</th><td>{book.publisher}</td></tr>}
                    {book.isbn && <tr><th>ISBN</th><td>{book.isbn}</td></tr>}
                    {book.description && <tr><th>Описание</th><td>{book.description}</td></tr>}
                    <tr><th>Добавлено</th><td>{new Date(book.created_date).toLocaleString("ru-RU")}</td></tr>
                    <tr><th>Обновлено</th><td>{new Date(book.modified_date).toLocaleString("ru-RU")}</td></tr>
                </tbody>
            </table>
            <h3 style={{ marginTop: "1.5rem", marginBottom: "0.75rem" }}>Оглавление</h3>
            <div className="toc-content">
                {toc.length === 0 ? <p style={{ color: "#888" }}>Оглавление пустое</p> :
                    toc.map((ch, i) => <div key={i} className="toc-item"><strong>{ch.number}.</strong> {ch.title}</div>)
                }
            </div>
        </>
    );
}