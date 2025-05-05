
    document.getElementById('recoverBtn').addEventListener('click', async function () {
    const emailInput = document.getElementById('username');
    const email = emailInput.value.trim();
    const errorDiv = document.getElementById('recoveryError');
    const messageDiv = document.getElementById('recoveryMessage');

    // Reset messaggi
    errorDiv.style.display = 'none';
    messageDiv.style.display = 'none';

    // Validazione email
    const emailRegex = /^[^@\s]+@[^@\s]+\.[^@\s]+$/;
    if (!emailRegex.test(email)) {
        errorDiv.textContent = "Inserisci un'email valida.";
    errorDiv.style.display = 'block';
    return;
    }

    try {
        const response = await fetch('/Login/RecoverPassword', {
        method: 'POST',
    headers: {
        'Content-Type': 'application/x-www-form-urlencoded',
    'Accept': 'text/plain'
            },
    body: new URLSearchParams({email: email })
        });

    const text = await response.text();

    if (response.ok) {
        messageDiv.textContent = text;
    messageDiv.style.display = 'block';
        } else {
        errorDiv.textContent = text || "Errore durante il recupero.";
    errorDiv.style.display = 'block';
        }
    } catch (err) {
        errorDiv.textContent = "Errore nella richiesta. Riprova più tardi.";
    errorDiv.style.display = 'block';
    }
});

