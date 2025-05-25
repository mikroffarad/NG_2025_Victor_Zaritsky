export default function QuizCard(cardData, onAnswer, onSkip, onNext) {
    const container = document.createElement('div');
    container.className = 'quiz-card';

    if (!cardData) {
        container.innerHTML = '<div class="loading">Loading new character...</div>';
        return container;
    }

    const { correct, question, options } = cardData;

    container.innerHTML = `
        <img src="${correct.image}" alt="Character" class="character-image">
        <div class="question">${question}</div>
        <div class="options">
            ${options.map((option, index) => `
                <button class="option-button" data-correct="${option.isCorrect}" data-index="${index}">
                    ${option.text}
                </button>
            `).join('')}
        </div>
        <div id="feedback" class="feedback"></div>
        <div class="controls">
            <button class="btn btn-secondary" id="skip-btn">Skip Question</button>
            <button class="btn btn-primary" id="next-btn">Next Question</button>
        </div>
    `;

    // Attach event listeners
    container.querySelectorAll('.option-button').forEach(button => {
        button.addEventListener('click', (e) => {
            const isCorrect = e.target.dataset.correct === 'true';
            onAnswer(isCorrect);
        });
    });

    const skipBtn = container.querySelector('#skip-btn');
    if (skipBtn) {
        skipBtn.addEventListener('click', onSkip);
    }

    const nextBtn = container.querySelector('#next-btn');
    if (nextBtn) {
        nextBtn.addEventListener('click', onNext);
    }

    return container;
}
