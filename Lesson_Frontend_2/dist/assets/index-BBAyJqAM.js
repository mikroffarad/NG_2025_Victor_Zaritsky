(function(){const e=document.createElement("link").relList;if(e&&e.supports&&e.supports("modulepreload"))return;for(const t of document.querySelectorAll('link[rel="modulepreload"]'))n(t);new MutationObserver(t=>{for(const i of t)if(i.type==="childList")for(const o of i.addedNodes)o.tagName==="LINK"&&o.rel==="modulepreload"&&n(o)}).observe(document,{childList:!0,subtree:!0});function s(t){const i={};return t.integrity&&(i.integrity=t.integrity),t.referrerPolicy&&(i.referrerPolicy=t.referrerPolicy),t.crossOrigin==="use-credentials"?i.credentials="include":t.crossOrigin==="anonymous"?i.credentials="omit":i.credentials="same-origin",i}function n(t){if(t.ep)return;t.ep=!0;const i=s(t);fetch(t.href,i)}})();const f="https://rickandmortyapi.com/api";async function p(r=4){try{const e=await fetch(`${f}/character`);if(!e.ok)throw new Error(`HTTP error! status: ${e.status}`);const{info:{count:s}}=await e.json(),n=new Set;for(;n.size<r;)n.add(Math.floor(Math.random()*s)+1);const t=await fetch(`${f}/character/${[...n]}`);if(!t.ok)throw new Error(`HTTP error! status: ${t.status}`);const i=await t.json();return Array.isArray(i)?i:[i]}catch(e){throw console.error("Error fetching characters:",e),e}}async function y(r="easy"){const e=await p(4),s=e[Math.floor(Math.random()*e.length)];let n="",t=[];switch(r){case"easy":n="What's the name of this character?",t=e.map(a=>({text:a.name,isCorrect:a.id===s.id}));break;case"medium":n="What's the status of this character?";const i=["Alive","Dead","unknown"],o=s.status;t=[{text:o,isCorrect:!0}],i.forEach(a=>{a!==o&&t.length<4&&t.push({text:a,isCorrect:!1})}),t.length<4&&["Presumed dead","Missing"].forEach(c=>{t.length<4&&t.push({text:c,isCorrect:!1})});break;case"hard":n="What species is this character?";const h=["Human","Alien","Humanoid","Robot","Animal","Disease","Cronenberg","Mythological Creature"],d=s.species;t=[{text:d,isCorrect:!0}];const u=h.filter(a=>a!==d).sort(()=>Math.random()-.5);for(;t.length<4&&u.length>0;)t.push({text:u.pop(),isCorrect:!1});if(t.length<4)try{const a=await p(10),c=[...new Set(a.map(l=>l.species))].filter(l=>l!==d&&!t.find(g=>g.text===l));for(;t.length<4&&c.length>0;)t.push({text:c.pop(),isCorrect:!1})}catch{console.warn("Could not fetch additional species, using fallback")}break}return t.sort(()=>Math.random()-.5),{correct:s,question:n,options:t}}const v={correct:0,wrong:0,unanswered:0,difficulty:"easy",save(){const r={correct:this.correct,wrong:this.wrong,unanswered:this.unanswered};localStorage.setItem("rm-quiz-score",JSON.stringify(r))},load(){const r=JSON.parse(localStorage.getItem("rm-quiz-score")||"{}");this.correct=r.correct||0,this.wrong=r.wrong||0,this.unanswered=r.unanswered||0},addCorrect(){this.correct++,this.save(),this.notifyChange()},addWrong(){this.wrong++,this.save(),this.notifyChange()},addSkipped(){this.unanswered++,this.save(),this.notifyChange()},changeDifficulty(r){this.difficulty=r,this.notifyChange()},notifyChange(){document.dispatchEvent(new CustomEvent("scoreChange"))}};function w(){const r=document.createElement("div");return r.className="header",r.innerHTML=`
        <h1>🛸 Rick & Morty Quiz</h1>
        <p>Test your knowledge of the multiverse!</p>
    `,r}function b(r){const e=document.createElement("div");return e.className="difficulty-selector",e.innerHTML=`
        <label for="difficulty">Choose Difficulty:</label>
        <select id="difficulty">
            <option value="easy" ${r.difficulty==="easy"?"selected":""}>Easy (Names)</option>
            <option value="medium" ${r.difficulty==="medium"?"selected":""}>Medium (Status)</option>
            <option value="hard" ${r.difficulty==="hard"?"selected":""}>Hard (Species)</option>
        </select>
    `,e}function C(r,e,s,n){const t=document.createElement("div");if(t.className="quiz-card",!r)return t.innerHTML='<div class="loading">Loading new character...</div>',t;const{correct:i,question:o,options:h}=r;t.innerHTML=`
        <img src="${i.image}" alt="Character" class="character-image">
        <div class="question">${o}</div>
        <div class="options">
            ${h.map((a,c)=>`
                <button class="option-button" data-correct="${a.isCorrect}" data-index="${c}">
                    ${a.text}
                </button>
            `).join("")}
        </div>
        <div id="feedback" class="feedback"></div>
        <div class="controls">
            <button class="btn btn-secondary" id="skip-btn">Skip Question</button>
            <button class="btn btn-primary" id="next-btn">Next Question</button>
        </div>
    `,t.querySelectorAll(".option-button").forEach(a=>{a.addEventListener("click",c=>{const l=c.target.dataset.correct==="true";e(l)})});const d=t.querySelector("#skip-btn");d&&d.addEventListener("click",s);const u=t.querySelector("#next-btn");return u&&u.addEventListener("click",n),t}function m(r){const e=document.createElement("div");return e.className="score-board",e.innerHTML=`
        <h3>📊 Your Stats</h3>
        <div class="stats">
            <div class="stat stat-correct">
                <div class="stat-value">${r.correct}</div>
                <div class="stat-label">Correct</div>
            </div>
            <div class="stat stat-wrong">
                <div class="stat-value">${r.wrong}</div>
                <div class="stat-label">Wrong</div>
            </div>
            <div class="stat stat-skipped">
                <div class="stat-value">${r.unanswered}</div>
                <div class="stat-label">Skipped</div>
            </div>
        </div>
    `,e}class E{constructor(e,s){this.app=e,this.store=s,this.currentCard=null,this.answered=!1}init(){this.render(),this.loadNewCard(),this.attachEventListeners()}render(){this.app.innerHTML=`
            <div class="app">
                <div id="header"></div>
                <div class="quiz-container">
                    <div id="difficulty-selector"></div>
                    <div id="quiz-content"></div>
                </div>
                <div id="score-board"></div>
            </div>
        `,document.getElementById("header").appendChild(w()),document.getElementById("difficulty-selector").appendChild(b(this.store)),document.getElementById("score-board").appendChild(m(this.store))}async loadNewCard(){this.answered=!1;const e=document.getElementById("quiz-content");e&&(e.innerHTML='<div class="loading">Loading new character...</div>');try{this.currentCard=await y(this.store.difficulty),e&&(e.innerHTML="",e.appendChild(C(this.currentCard,this.handleAnswer.bind(this),this.handleSkip.bind(this),this.loadNewCard.bind(this))))}catch(s){console.error("Error loading quiz card:",s),e&&(e.innerHTML='<div class="loading">Error loading character. Please try again.</div>')}}handleAnswer(e){this.answered||(this.answered=!0,e?(this.store.addCorrect(),this.showFeedback("Correct! Well done! 🎉","correct")):(this.store.addWrong(),this.showFeedback("Wrong answer! 😞","wrong")),document.querySelectorAll(".option-button").forEach(s=>{s.style.pointerEvents="none",s.style.opacity="0.6"}))}handleSkip(){this.answered||(this.store.addSkipped(),this.loadNewCard())}showFeedback(e,s){const n=document.getElementById("feedback");n&&(n.textContent=e,n.className=`feedback ${s} show`,setTimeout(()=>{n.classList.remove("show")},3e3))}attachEventListeners(){document.addEventListener("change",e=>{e.target.id==="difficulty"&&(this.store.changeDifficulty(e.target.value),this.loadNewCard())}),document.addEventListener("scoreChange",()=>{const e=document.getElementById("score-board");e&&(e.innerHTML="",e.appendChild(m(this.store)))})}}const S=document.getElementById("app");v.load();const k=new E(S,v);k.init();
