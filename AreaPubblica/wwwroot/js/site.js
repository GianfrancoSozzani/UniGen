
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

$(document).ready(function () {
    $('#loginForm').on('submit', function (e) {
        e.preventDefault();

        $.ajax({
            url: '/Login/Login',
            type: 'POST',
            data: $(this).serialize(),
            success: function (response) {
                if (response.success) {
                    window.location.href = response.redirectUrl;
                } else {
                    $('#loginError').text(response.message).removeClass('d-none');
                }
            },
            error: function () {
                $('#loginError').text('Riprova più tardi').removeClass('d-none');
            }
        });
    });
});
[HttpPost]
public IActionResult VerificaImmagine(IFormFile file)
{
    if (file == null || file.Length == 0)
        return Json(new { success = false, message = "Nessun file selezionato." });

    var allowedExtensions = new [] { ".jpg", ".jpeg", ".png" };
    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

    if (!allowedExtensions.Contains(extension))
        return Json(new { success = false, message = "Formato non valido. Usa JPG, JPEG o PNG." });

    if (file.Length > 10 * 1024 * 1024)
        return Json(new { success = false, message = "Il file supera i 10 MB." });

    return Json(new { success = true, message = "Immagine valida!" });
}
