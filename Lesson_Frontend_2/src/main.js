import QuizApp from './components/QuizApp.js';
import { store } from './store.js';

const app = document.getElementById('app');

// Initialize store
store.load();

// Create and start the quiz app
const quizApp = new QuizApp(app, store);
quizApp.init();
