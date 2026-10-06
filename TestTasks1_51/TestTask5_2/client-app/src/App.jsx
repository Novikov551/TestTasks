import { Routes, Route, Link } from 'react-router-dom';
import BookList from './pages/BookList';
import BookDetails from './pages/BookDetails';
import BookForm from './pages/BookForm';

export default function App() {
    return (
        <>
            <nav className="navbar">
                <div className="container">
                    <Link to="/" className="navbar-brand">📚 Домашняя библиотека</Link>
                    <div className="navbar-links">
                        <Link to="/">Список книг</Link>
                        <Link to="/create">Добавить книгу</Link>
                    </div>
                </div>
            </nav>
            <div className="container">
                <Routes>
                    <Route path="/" element={<BookList />} />
                    <Route path="/create" element={<BookForm />} />
                    <Route path="/edit/:id" element={<BookForm />} />
                    <Route path="/book/:id" element={<BookDetails />} />
                </Routes>
            </div>
        </>
    );
}
