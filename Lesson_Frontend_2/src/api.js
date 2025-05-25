const API_BASE = 'https://rickandmortyapi.com/api';

export async function fetchRandomCharacters(count = 4) {
    // get total count
    const resp = await fetch(`${API_BASE}/character`);
    const { info: { count: total } } = await resp.json();
    // pick random IDs
    const ids = new Set();
    while (ids.size < count) ids.add(Math.floor(Math.random() * total) + 1);
    const res = await fetch(`${API_BASE}/character/${[...ids]}`);
    const data = await res.json();
    return Array.isArray(data) ? data : [data];
}

export async function fetchCharacterById(id) {
    const resp = await fetch(`${API_BASE}/character/${id}`);
    return await resp.json();
}

export async function getQuizCard(difficulty = 'easy') {
    /**
     * difficulty: easy -> names only
     * medium -> names and status filter
     * hard -> include species or origin clues
     **/
    const options = await fetchRandomCharacters(4);
    const correct = options[Math.floor(Math.random() * options.length)];
    return { options, correct };
}
