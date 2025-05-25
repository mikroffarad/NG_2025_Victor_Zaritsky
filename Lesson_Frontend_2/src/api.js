const API_BASE = 'https://rickandmortyapi.com/api';

export async function fetchRandomCharacters(count = 4) {
    const resp = await fetch(`${API_BASE}/character`);
    const { info: { count: total } } = await resp.json();

    const ids = new Set();
    while (ids.size < count) {
        ids.add(Math.floor(Math.random() * total) + 1);
    }

    const res = await fetch(`${API_BASE}/character/${[...ids]}`);
    const data = await res.json();
    return Array.isArray(data) ? data : [data];
}

export async function getQuizCard(difficulty = 'easy') {
    const characters = await fetchRandomCharacters(4);
    const correct = characters[Math.floor(Math.random() * characters.length)];

    let question = '';
    let options = [];

    switch (difficulty) {
        case 'easy':
            question = "What's the name of this character?";
            options = characters.map(char => ({
                text: char.name,
                isCorrect: char.id === correct.id
            }));
            break;

        case 'medium':
            question = `What's the status of this ${correct.gender.toLowerCase()} character?`;
            const allChars = await fetchRandomCharacters(20);
            const statuses = [...new Set(allChars.map(c => c.status))];
            options = statuses.slice(0, 4).map(status => ({
                text: status,
                isCorrect: status === correct.status
            }));
            // Fill with common statuses if needed
            if (options.length < 4) {
                const commonStatuses = ['Alive', 'Dead', 'unknown'];
                while (options.length < 4) {
                    const status = commonStatuses[Math.floor(Math.random() * commonStatuses.length)];
                    if (!options.find(o => o.text === status)) {
                        options.push({ text: status, isCorrect: status === correct.status });
                    }
                }
            }
            break;

        case 'hard':
            question = "What species is this character?";
            const speciesChars = await fetchRandomCharacters(20);
            const allSpecies = [...new Set(speciesChars.map(c => c.species))];
            options = allSpecies.slice(0, 4).map(species => ({
                text: species,
                isCorrect: species === correct.species
            }));
            if (options.length < 4) {
                const commonSpecies = ['Human', 'Alien', 'Robot', 'Animal'];
                while (options.length < 4) {
                    const species = commonSpecies[Math.floor(Math.random() * commonSpecies.length)];
                    if (!options.find(o => o.text === species)) {
                        options.push({ text: species, isCorrect: species === correct.species });
                    }
                }
            }
            break;
    }

    // Shuffle options
    options.sort(() => Math.random() - 0.5);

    return { correct, question, options };
}
