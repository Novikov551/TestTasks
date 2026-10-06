import { useState, useEffect, useRef } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { fetchBook, createBook, updateBook } from "../api";

const EMPTY_TOC_XML = "<tableOfContents></tableOfContents>";

function xmlToHtml(xml) {
    try {
        const doc = new DOMParser().parseFromString(xml, "text/xml");
        const chapters = doc.querySelectorAll("chapter");
        if (chapters.length === 0) return "";
        let html = "<ol>";
        chapters.forEach((ch) => {
            const num = ch.getAttribute("number") || "";
            const title = ch.getAttribute("title") || "";
            html += `<li><strong>${num}.</strong> ${title}</li>`;
        });
        return html + "</ol>";
    } catch { return ""; }
}

function htmlToXml(html) {
    const doc = new DOMParser().parseFromString(html, "text/html");
    const items = doc.querySelectorAll("li");
    if (items.length === 0) return EMPTY_TOC_XML;
    const lines = ["<tableOfContents>"];
    items.forEach((li) => {
        const text = li.textContent.trim();
        const match = text.match(/^(\d+(?:\.\d+)?)\.?\s*(.+)$/);
        if (match) {
            const num = match[1];
            const title = match[2].replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;");
            lines.push(`    <chapter number="${num}" title="${title}" />`);
        } else if (text) {
            const escaped = text.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;");
            lines.push(`    <chapter title="${escaped}" />`);
        }
    });
    lines.push("</tableOfContents>");
    return lines.join("\n");
}

function QuillEditor({ initialValue, onReady }) {
    const containerRef = useRef(null);
    const editorRef = useRef(null);

    useEffect(() => {
        if (!containerRef.current || editorRef.current) return;

        const quill = new Quill(containerRef.current, {
            theme: "snow",
            modules: {
                toolbar: [
                    [{ header: [1, 2, 3, false] }],
                    ["bold", "italic", "underline"],
                    [{ list: "ordered" }, { list: "bullet" }],
                    ["clean"]
                ]
            },
            placeholder: "Введите оглавление..."
        });

        editorRef.current = quill;

        if (initialValue) {
            quill.root.innerHTML = initialValue;
        }

        if (onReady) {
            onReady(quill);
        }

        return () => { editorRef.current = null; };
    }, []);

    useEffect(() => {
        if (editorRef.current && initialValue) {
            editorRef.current.root.innerHTML = initialValue;
        }
    }, [initialValue]);

    return <div ref={containerRef} style={{ height: "300px" }} />;
}

export default function BookForm() {
    const { id } = useParams();
    const navigate = useNavigate();
    const isEdit = Boolean(id);
    const quillRef = useRef(null);

    const [form, setForm] = useState({
        title: "", author: "", year: new Date().getFullYear(),
        publisher: "", isbn: "", description: ""
    });
    const [tocHtml, setTocHtml] = useState("");
    const [loading, setLoading] = useState(isEdit);
    const [saving, setSaving] = useState(false);

    useEffect(() => {
        if (!isEdit) return;
        fetchBook(id).then((book) => {
            setForm({
                title: book.title, author: book.author, year: book.year,
                publisher: book.publisher || "", isbn: book.isbn || "",
                description: book.description || ""
            });
            setTocHtml(xmlToHtml(book.table_of_contents_xml));
        }).catch(() => navigate("/")).finally(() => setLoading(false));
    }, [id]);

    const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

    const handleSubmit = async (e) => {
        e.preventDefault();
        setSaving(true);
        const html = quillRef.current ? quillRef.current.root.innerHTML : tocHtml;
        const data = {
            ...form,
            table_of_contents_xml: html.trim() && html !== "<p><br></p>" ? htmlToXml(html) : EMPTY_TOC_XML
        };
        try {
            if (isEdit) { await updateBook(id, data); navigate(`/book/${id}`); }
            else { const created = await createBook(data); navigate(`/book/${created.id}`); }
        } catch (err) { alert("Ошибка: " + err.message); }
        finally { setSaving(false); }
    };

    if (loading) return <p>Загрузка...</p>;

    return (
        <>
            <h2 style={{ marginBottom: "1.5rem" }}>{isEdit ? "Редактирование книги" : "Добавить книгу"}</h2>
            <form onSubmit={handleSubmit}>
                <div style={{ display: "grid", gridTemplateColumns: "2fr 1fr", gap: "1rem" }}>
                    <div className="form-group">
                        <label>Название *</label>
                        <input name="title" value={form.title} onChange={handleChange} required />
                    </div>
                    <div className="form-group">
                        <label>Год издания *</label>
                        <input name="year" type="number" min="1000" max="2099" value={form.year} onChange={handleChange} required />
                    </div>
                </div>
                <div style={{ display: "grid", gridTemplateColumns: "1fr 1fr 1fr", gap: "1rem" }}>
                    <div className="form-group">
                        <label>Автор *</label>
                        <input name="author" value={form.author} onChange={handleChange} required />
                    </div>
                    <div className="form-group">
                        <label>Издательство</label>
                        <input name="publisher" value={form.publisher} onChange={handleChange} />
                    </div>
                    <div className="form-group">
                        <label>ISBN</label>
                        <input name="isbn" value={form.isbn} onChange={handleChange} />
                    </div>
                </div>
                <div className="form-group">
                    <label>Описание</label>
                    <textarea name="description" value={form.description} onChange={handleChange} rows={3} />
                </div>

                <div className="form-group" style={{ marginBottom: "1.5rem" }}>
                    <label style={{ marginBottom: "0.5rem", display: "block", fontWeight: 600 }}>Оглавление (HTML-редактор)</label>
                    <QuillEditor
                        initialValue={tocHtml}
                        onReady={(quill) => { quillRef.current = quill; }}
                    />
                </div>

                <div style={{ display: "flex", gap: "0.75rem", marginTop: "1rem" }}>
                    <button type="submit" className="btn btn-primary" disabled={saving}>
                        {saving ? "Сохранение..." : "Сохранить"}
                    </button>
                    <button type="button" onClick={() => navigate(-1)} className="btn btn-outline">Отмена</button>
                </div>
            </form>
        </>
    );
}
