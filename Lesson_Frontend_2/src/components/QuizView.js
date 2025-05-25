import { getQuizCard } from '../api.js';
import { store } from '../store.js';

export default function QuizView() {
    const container = document.createElement('div');
    container.className = 'quiz-container';

    async function loadCard() {
        container.innerHTML = '<p>Loading...</p>';
        const { options, correct } = await getQuizCard(store.difficulty);
        container.innerHTML = '';

        const img = document.createElement('img');
        img.src = correct.image;
        container.appendChild(img);

        const list = document.createElement('ul');
        options.sort(() => Math.random() - 0.5).forEach(opt => {
            const li = document.createElement('li');
            const btn = document.createElement('button');
            btn.textContent = opt.name;
            btn.onclick = () => {
                if (opt.id === correct.id) store.correct++;
                else store.wrong++;
                store.save();
                dispatchScoreChange();
                loadCard();
            };
            li.appendChild(btn);
            list.appendChild(li);
        });
        container.appendChild(list);

        const skipBtn = document.createElement('button');
        skipBtn.textContent = 'Skip';
        skipBtn.onclick = () => {
            store.unanswered++;
            store.save();
            dispatchScoreChange();
            loadCard();
        };
        container.appendChild(skipBtn);
    }

    // difficulty selector
    const diff = document.createElement('select');
    ['easy', 'medium', 'hard'].forEach(level => {
        const o = document.createElement('option');
        o.value = level; o.textContent = level;
        if (store.difficulty === level) o.selected = true;
        diff.appendChild(o);
    });
    diff.onchange = () => store.changeDifficulty(diff.value);
    container.appendChild(diff);

    // refresh
    const refresh = document.createElement('button');
    refresh.textContent = 'Refresh';
    refresh.onclick = loadCard;
    container.appendChild(refresh);

    loadCard();
    return container;
}

function dispatchScoreChange() {
    const evt = new Event('scoreChange');
    document.dispatchEvent(evt);
}
