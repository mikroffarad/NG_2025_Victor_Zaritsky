import { getQuizCard } from '../api.js';
import { store } from '../store.js';
import Header from './Header.js';
import DifficultySelector from './DifficultySelector.js';
import QuizCard from './QuizCard.js';
import ScoreBoard from './ScoreBoard.js';

export default class QuizApp {
    constructor(appElement, storeInstance) {
        this.app = appElement;
        this.store = storeInstance;
        this.currentCard = null;
        this.answered = false;
    }

    init() {
        this.render();
        this.loadNewCard();
        this.attachEventListeners();
    }

    render() {
        this.app.innerHTML = `
            <div class="app">
                <div id="header"></div>
                <div class="quiz-container">
                    <div id="difficulty-selector"></div>
                    <div id="quiz-content"></div>
                </div>
                <div id="score-board"></div>
            </div>
        `;

        // Render components
        document.getElementById('header').appendChild(Header());
        document.getElementById('difficulty-selector').appendChild(DifficultySelector(this.store));
        document.getElementById('score-board').appendChild(ScoreBoard(this.store));
    }

    async loadNewCard() {
        this.answered = false;
        const quizContent = document.getElementById('quiz-content');

        if (quizContent) {
            quizContent.innerHTML = '<div class="loading">Loading new character...</div>';
        }

        try {
            this.currentCard = await getQuizCard(this.store.difficulty);
            if (quizContent) {
                quizContent.innerHTML = '';
                quizContent.appendChild(QuizCard(this.currentCard, this.handleAnswer.bind(this), this.handleSkip.bind(this), this.loadNewCard.bind(this)));
            }
        } catch (error) {
            console.error('Error loading quiz card:', error);
            if (quizContent) {
                quizContent.innerHTML = '<div class="loading">Error loading character. Please try again.</div>';
            }
        }
    }

    handleAnswer(isCorrect) {
        if (this.answered) return;

        this.answered = true;

        if (isCorrect) {
            this.store.addCorrect();
            this.showFeedback('Correct! Well done! 🎉', 'correct');
        } else {
            this.store.addWrong();
            this.showFeedback('Wrong answer! 😞', 'wrong');
        }

        // Disable all option buttons
        document.querySelectorAll('.option-button').forEach(btn => {
            btn.style.pointerEvents = 'none';
            btn.style.opacity = '0.6';
        });
    }

    handleSkip() {
        if (this.answered) return;
        this.store.addSkipped();
        this.loadNewCard();
    }

    showFeedback(message, type) {
        const feedback = document.getElementById('feedback');
        if (feedback) {
            feedback.textContent = message;
            feedback.className = `feedback ${type} show`;

            setTimeout(() => {
                feedback.classList.remove('show');
            }, 3000);
        }
    }

    attachEventListeners() {
        // Difficulty change
        document.addEventListener('change', (e) => {
            if (e.target.id === 'difficulty') {
                this.store.changeDifficulty(e.target.value);
                this.loadNewCard();
            }
        });

        // Score changes
        document.addEventListener('scoreChange', () => {
            const scoreBoardElement = document.getElementById('score-board');
            if (scoreBoardElement) {
                scoreBoardElement.innerHTML = '';
                scoreBoardElement.appendChild(ScoreBoard(this.store));
            }
        });
    }
}
