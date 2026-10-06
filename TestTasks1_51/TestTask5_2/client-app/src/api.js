const BASE = '/api/books';

function toSnakeCase(str) {
    return str.replace(/[A-Z]/g, letter => `_${letter.toLowerCase()}`);
}

function keysToSnakeCase(obj) {
    const result = {};
    for (const [key, value] of Object.entries(obj)) {
        result[toSnakeCase(key)] = value;
    }
    return result;
}

export async function fetchBooks(query = '', field = 'title') {
    const params = new URLSearchParams();
    if (query) {
        params.set('query', query);
        params.set('field', field);
    }
    const res = await fetch(`${BASE}?${params}`);
    if (!res.ok) throw new Error('Ошибка загрузки');
    return res.json();
}

export async function fetchBook(id) {
    const res = await fetch(`${BASE}/${id}`);
    if (!res.ok) throw new Error('Книга не найдена');
    return res.json();
}

export async function createBook(data) {
    const res = await fetch(BASE, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(keysToSnakeCase(data))
    });
    if (!res.ok) throw new Error('Ошибка создания');
    return res.json();
}

export async function updateBook(id, data) {
    const res = await fetch(`${BASE}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(keysToSnakeCase(data))
    });
    if (!res.ok) throw new Error('Ошибка обновления');
    return res.json();
}

export async function deleteBook(id) {
    const res = await fetch(`${BASE}/${id}`, { method: 'DELETE' });
    if (!res.ok) throw new Error('Ошибка удаления');
}
