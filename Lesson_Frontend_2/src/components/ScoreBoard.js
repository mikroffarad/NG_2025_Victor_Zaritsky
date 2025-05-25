export default function ScoreBoard(store) {
  const container = document.createElement('div');
  container.className = 'score-board';

  container.innerHTML = `
        <h3>📊 Your Stats</h3>
        <div class="stats">
            <div class="stat stat-correct">
                <div class="stat-value">${store.correct}</div>
                <div class="stat-label">Correct</div>
            </div>
            <div class="stat stat-wrong">
                <div class="stat-value">${store.wrong}</div>
                <div class="stat-label">Wrong</div>
            </div>
            <div class="stat stat-skipped">
                <div class="stat-value">${store.unanswered}</div>
                <div class="stat-label">Skipped</div>
            </div>
        </div>
    `;

  return container;
}
