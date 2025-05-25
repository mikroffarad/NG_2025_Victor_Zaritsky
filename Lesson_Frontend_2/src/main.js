import QuizView from './components/QuizView.js';
import ScoreBoard from './components/ScoreBoard.js';
import { store } from './store.js';

const app = document.getElementById('app');

function render() {
    app.innerHTML = '';
    app.appendChild(QuizView());
    app.appendChild(ScoreBoard(store));
}

// Listen to store changes
document.addEventListener('scoreChange', render);

render();
