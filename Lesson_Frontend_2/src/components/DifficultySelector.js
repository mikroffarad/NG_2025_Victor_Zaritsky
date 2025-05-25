export default function DifficultySelector(store) {
    const container = document.createElement('div');
    container.className = 'difficulty-selector';

    container.innerHTML = `
        <label for="difficulty">Choose Difficulty:</label>
        <select id="difficulty">
            <option value="easy" ${store.difficulty === 'easy' ? 'selected' : ''}>Easy (Names)</option>
            <option value="medium" ${store.difficulty === 'medium' ? 'selected' : ''}>Medium (Status)</option>
            <option value="hard" ${store.difficulty === 'hard' ? 'selected' : ''}>Hard (Species)</option>
        </select>
    `;

    return container;
}
