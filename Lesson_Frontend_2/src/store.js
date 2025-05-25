export const store = {
    correct: 0,
    wrong: 0,
    unanswered: 0,
    difficulty: 'easy',
    save() {
        localStorage.setItem('rm-quiz-score', JSON.stringify({
            correct: this.correct,
            wrong: this.wrong,
            unanswered: this.unanswered,
        }));
    },
    load() {
        const data = JSON.parse(localStorage.getItem('rm-quiz-score') || '{}');
        this.correct = data.correct || 0;
        this.wrong = data.wrong || 0;
        this.unanswered = data.unanswered || 0;
    },
    changeDifficulty(level) {
        this.difficulty = level;
        dispatchScoreChange();
    }
};

function dispatchScoreChange() {
    const evt = new Event('scoreChange');
    document.dispatchEvent(evt);
}

// initialize
store.load();
