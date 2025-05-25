export default function ScoreBoard(store) {
    const container = document.createElement('div');
    container.className = 'score-board';
    container.innerHTML = `
    <p>Correct: ${store.correct}</p>
    <p>Wrong: ${store.wrong}</p>
    <p>Skipped: ${store.unanswered}</p>
  `;
    return container;
}
