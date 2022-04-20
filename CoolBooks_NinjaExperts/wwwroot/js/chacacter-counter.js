const descArea = document.getElementById('desc-area');
const charsRemaining = document.getElementById('chars-remaining');
const MAX_CHARS = 500;
descArea.addEventListener('input', () => {
    const remaining = MAX_CHARS - descArea.value.length;
    const color = remaining < MAX_CHARS * 0.1 ? 'red' : null;
    charsRemaining.textContent = remaining + ' characters remaining';
    charsRemaining.style.color = color;
})
