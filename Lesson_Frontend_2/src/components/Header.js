export default function Header() {
    const header = document.createElement('div');
    header.className = 'header';
    header.innerHTML = `
        <h1>🛸 Rick & Morty Quiz</h1>
        <p>Test your knowledge of the multiverse!</p>
    `;
    return header;
}
