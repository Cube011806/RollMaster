function toggleDrawer(id) {
    document.getElementById(id).classList.toggle("open");
}

document.addEventListener("DOMContentLoaded", () => {

    const button = document.getElementById("rollD12Button");

    if (!button)
        return;

    button.addEventListener("click", () => {

        const result = Math.floor(Math.random() * 12) + 1;

        let characterName = "Nieznana postać";

        const characterNameElement = document.querySelector(".header .name");

        if (characterNameElement)
            characterName = characterNameElement.textContent.trim();

        addLog(`${characterName} wyrzucił ${result}`);
    });

});

function addLog(message) {

    const log = document.getElementById("logMessages");

    if (!log)
        return;

    const entry = document.createElement("div");
    entry.className = "log-entry";
    entry.textContent = message;

    log.prepend(entry);
}