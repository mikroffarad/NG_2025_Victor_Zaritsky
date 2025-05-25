const API_BASE = 'https://rickandmortyapi.com/api';

export async function fetchRandomCharacters(count = 4) {
    try {
        const resp = await fetch(`${API_BASE}/character`);
        if (!resp.ok) throw new Error(`HTTP error! status: ${resp.status}`);

        const { info: { count: total } } = await resp.json();

        const ids = new Set();
        while (ids.size < count) {
            ids.add(Math.floor(Math.random() * total) + 1);
        }

        const res = await fetch(`${API_BASE}/character/${[...ids]}`);
        if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);

        const data = await res.json();
        return Array.isArray(data) ? data : [data];
    } catch (error) {
        console.error('Error fetching characters:', error);
        throw error;
    }
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
            question = `What's the status of this character?`;
            // Use predefined statuses to avoid API issues
            const allStatuses = ['Alive', 'Dead', 'unknown'];
            // Make sure correct answer is included
            const correctStatus = correct.status;
            options = [{ text: correctStatus, isCorrect: true }];

            // Add other statuses as wrong answers
            allStatuses.forEach(status => {
                if (status !== correctStatus && options.length < 4) {
                    options.push({ text: status, isCorrect: false });
                }
            });

            // If we still need more options, add some variations
            if (options.length < 4) {
                const extraStatuses = ['Presumed dead', 'Missing'];
                extraStatuses.forEach(status => {
                    if (options.length < 4) {
                        options.push({ text: status, isCorrect: false });
                    }
                });
            }
            break;

        case 'hard':
            question = "What species is this character?";
            // Use predefined species to avoid API complexity
            const commonSpecies = ['Human', 'Alien', 'Humanoid', 'Robot', 'Animal', 'Disease', 'Cronenberg', 'Mythological Creature'];
            const correctSpecies = correct.species;

            options = [{ text: correctSpecies, isCorrect: true }];

            // Add other species as wrong answers
            const shuffledSpecies = commonSpecies.filter(s => s !== correctSpecies).sort(() => Math.random() - 0.5);

            while (options.length < 4 && shuffledSpecies.length > 0) {
                options.push({ text: shuffledSpecies.pop(), isCorrect: false });
            }

            // If still need more options, try to get from API
            if (options.length < 4) {
                try {
                    const speciesChars = await fetchRandomCharacters(10);
                    const uniqueSpecies = [...new Set(speciesChars.map(c => c.species))]
                        .filter(s => s !== correctSpecies && !options.find(o => o.text === s));

                    while (options.length < 4 && uniqueSpecies.length > 0) {
                        options.push({ text: uniqueSpecies.pop(), isCorrect: false });
                    }
                } catch (error) {
                    console.warn('Could not fetch additional species, using fallback');
                }
            }
            break;
    }

    // Shuffle options
    options.sort(() => Math.random() - 0.5);

    return { correct, question, options };
}
