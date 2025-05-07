document.getElementById("form-insert").addEventListener("submit", function (event) {
	const testoInput = document.getElementById("testoComunicazioneAdd");
	if (testoInput.value.trim() === "") {
		alert("Il testo della comunicazione non può contenere solo spazi.");
		event.preventDefault(); // Prevent form submission
	}
});



document.addEventListener('DOMContentLoaded', function () {
    const forms = document.querySelectorAll('form[asp-action="AddRisposta"]');
    console.log("Trovate form con AddRisposta:", forms);

    forms.forEach(form => {
        console.log("Trovata una form AddRisposta:", form);
        form.addEventListener('submit', function (event) {
            console.log("Form AddRisposta sottomessa:", this);
            const codiceComunicazioneInput = this.querySelector('input[name="Codice_Comunicazione"]');
            console.log("Input Codice_Comunicazione:", codiceComunicazioneInput);
            const codiceComunicazioneValue = codiceComunicazioneInput ? codiceComunicazioneInput.value : null;
            console.log("Valore Codice_Comunicazione:", codiceComunicazioneValue);
            const textAreaId = `textArea_${codiceComunicazioneValue}`;
            console.log("textAreaId costruito:", textAreaId);
            const testoInput = document.getElementById(textAreaId);
            console.log("Elemento textarea trovato:", testoInput);

            if (testoInput && testoInput.value.trim() === "") {
                alert("Il messaggio non può contenere solo spazi.");
                event.preventDefault();
            }
        });
    });
});