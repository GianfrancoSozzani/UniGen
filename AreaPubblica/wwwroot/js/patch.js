function modifica() { 

    var aggiornamento = new Object();
    aggiornamento.cognome = "malini";
$.ajax({
    url: "http://localhost:5207/api/studenti/461c9997-fabc-4627-8143-04739d512e3f",
    type: "PATCH",
    contentType: "application/json",
    data: JSON.stringify(aggiornamento),
    success: function (risposta) {
        alert("Aggiornamento completato!");
    },
    error: function (errore) {
        alert("Errore durante la PATCH: " + errore.responseText);
    }
});
}

