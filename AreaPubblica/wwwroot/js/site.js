
document.getElementById("ImmagineFile").addEventListener("change", function (e) {
    const file = e.target.files[0];
    const uploadMsg = document.getElementById("upload-msg");
    const submitBtn = document.querySelector("button[type='submit']");

    if (file) {
        const validExtensions = ["image/jpeg", "image/jpg", "image/png"];
        const maxSize = 10 * 1024 * 1024; // 10 MB

        // Controllo estensione
        if (!validExtensions.includes(file.type)) {
            uploadMsg.innerHTML = `<div class="alert alert-danger">Formato non valido. Usa JPG, JPEG o PNG.</div>`;
            submitBtn.disabled = true;
            return;
        }

        // Controllo dimensione
        if (file.size > maxSize) {
            uploadMsg.innerHTML = `<div class="alert alert-danger">L'immagine supera i 10 MB consentiti.</div>`;
            submitBtn.disabled = true;
            return;
        }

        // Anteprima immagine
        const reader = new FileReader();
        reader.onload = function (event) {
            const imgContainer = document.getElementById("img-container");
            imgContainer.innerHTML = `<img src="${event.target.result}" class="w-100 h-100 object-fit-cover" />`;
        };
        reader.readAsDataURL(file);

        uploadMsg.innerHTML = `<div class="alert alert-success">Immagine valida!</div>`;
        submitBtn.disabled = false;
    }
});
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
document.getElementById("registerForm").addEventListener("submit", function (e) {
    var fileInput = document.getElementById("ImmagineFile");
    var file = fileInput.files[0];

    if (file) {
        var allowedExtensions = ["jpg", "jpeg", "png"];
        var fileExtension = file.name.split('.').pop().toLowerCase();

        if (!allowedExtensions.includes(fileExtension)) {
            e.preventDefault();  // Ferma l'invio del form
            alert("Il file caricato deve essere un'immagine con estensione .jpg, .jpeg o .png.");
        }
    }
});
document.getElementById("register-form").addEventListener("submit", function (e) {
    const fileInput = document.getElementById("ImmagineFile");
    const file = fileInput.files[0];

    // Solo se è stato caricato un file
    if (file) {
        const validExtensions = [".jpg", ".jpeg", ".png"];
        const extension = file.name.substring(file.name.lastIndexOf('.')).toLowerCase();
        const maxSize = 10 * 1024 * 1024; // 10 MB

        if (!validExtensions.includes(extension)) {
            e.preventDefault();
            document.querySelector("span[asp-validation-for='ImmagineFile']").innerText = "Formato immagine non valido. Solo JPG, JPEG, PNG.";
            return;
        }

        if (file.size > maxSize) {
            e.preventDefault();
            document.querySelector("span[asp-validation-for='ImmagineFile']").innerText = "L'immagine supera il limite di 10 MB.";
            return;
        }

        // Pulisce eventuali messaggi precedenti se il file è valido
        document.querySelector("span[asp-validation-for='ImmagineFile']").innerText = "";
    }
});