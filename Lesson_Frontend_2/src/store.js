export const store = {
    correct: 0,
    wrong: 0,
    unanswered: 0,
    difficulty: 'easy',

    save() {
        const data = {
            correct: this.correct,
            wrong: this.wrong,
            unanswered: this.unanswered,
        };
        localStorage.setItem('rm-quiz-score', JSON.stringify(data));
    },

    load() {
        const data = JSON.parse(localStorage.getItem('rm-quiz-score') || '{}');
        this.correct = data.correct || 0;
        this.wrong = data.wrong || 0;
        this.unanswered = data.unanswered || 0;
    },

    addCorrect() {
        this.correct++;
        this.save();
        this.notifyChange();
    },

    addWrong() {
        this.wrong++;
        this.save();
        this.notifyChange();
    },

    addSkipped() {
        this.unanswered++;
        this.save();
        this.notifyChange();
    },

    changeDifficulty(level) {
        this.difficulty = level;
        this.notifyChange();
    },

    notifyChange() {
        document.dispatchEvent(new CustomEvent('scoreChange'));
    }
};
