function toggleDrawer(id) {
    document.getElementById(id).classList.toggle("open");
}

let diceConnection = null;

function renderDiceLog(entry) {
    addLog(entry.description);
}

async function initializeDiceSignalR() {
    const logDrawer = document.getElementById("logDrawer");

    if (!logDrawer || typeof signalR === "undefined") {
        console.error("Brak panelu logu lub biblioteki SignalR.");
        return;
    }

    const gameId = Number(logDrawer.dataset.gameId);

    if (!Number.isInteger(gameId) || gameId <= 0) {
        console.error("Nieprawidłowy identyfikator gry.");
        return;
    }

    diceConnection = new signalR.HubConnectionBuilder()
        .withUrl("/characterHub")
        .withAutomaticReconnect()
        .build();

    diceConnection.on("ReceiveDiceLog", renderDiceLog);

    try {
        await diceConnection.start();

        await diceConnection.invoke("JoinGame", gameId);

        const history =
            await diceConnection.invoke("GetRecentDiceLogs", gameId);

        // Serwer zwraca wpisy od najnowszego.
        // addLog dodaje wpis na początek, więc odwracamy kolejność.
        history.reverse().forEach(renderDiceLog);

        console.log("Połączono z logiem gry:", gameId);
    } catch (error) {
        console.error("Błąd inicjalizacji logu:", error);
    }
}

async function sendDiceLog(message) {
    if (!diceConnection ||
        diceConnection.state !== signalR.HubConnectionState.Connected) {
        console.error("Połączenie z logiem gry nie jest aktywne.");
        return;
    }

    const activeForm =
        document.querySelector(".tab-pane.active .characterForm") ||
        document.querySelector(".characterForm");

    if (!activeForm) {
        console.error("Nie znaleziono formularza aktywnej postaci.");
        return;
    }

    const characterId = Number(activeForm.dataset.characterId);

    if (!Number.isInteger(characterId) || characterId <= 0) {
        console.error("Nieprawidłowy identyfikator postaci.");
        return;
    }

    try {
        await diceConnection.invoke(
            "SendDiceLog",
            characterId,
            message
        );
    } catch (error) {
        console.error("Nie udało się zapisać rzutu:", error);
    }
}

document.addEventListener("DOMContentLoaded", () => {
    initializeDiceSignalR();
    const button = document.getElementById("rollButton");
    const skillSelect = document.getElementById("skillSelect");

    const diceResult = document.getElementById("diceResult");

    if (!button || !skillSelect)
        return;


    button.addEventListener("click", () => {

        // Wybrana umiejętność
        const selectedOption =
            skillSelect.options[skillSelect.selectedIndex];

        if (!selectedOption)
            return;


        // Dane umiejętności
        const skillName =
            selectedOption.textContent.trim();

        const skillValue =
            parseInt(selectedOption.dataset.value);

        const isEnhanced =
            selectedOption.dataset.enhanced.toLowerCase() === "true";


        // -----------------------------------------
        // RZUT D12
        // -----------------------------------------

        const d12Rolls = [];

        if (isEnhanced) {

            // Wzmocniony:
            // 2d12 i wybieramy wyższy

            d12Rolls.push(rollDice(12));
            d12Rolls.push(rollDice(12));

        } else {

            // Normalny:
            // 1d12

            d12Rolls.push(rollDice(12));
        }


        const d12Result =
            Math.max(...d12Rolls);


        // -----------------------------------------
        // RZUTY D6
        // -----------------------------------------

        const d6Rolls = [];

        for (let i = 0; i < skillValue; i++) {

            d6Rolls.push(rollDice(6));

        }


        // Suma d6
        const d6Total =
            d6Rolls.reduce((sum, value) => sum + value, 0);


        // Całkowity wynik
        const total =
            d12Result + d6Total;


        // -----------------------------------------
        // WYŚWIETLENIE WYNIKU
        // -----------------------------------------

        displayDiceResult(
            skillName,
            skillValue,
            isEnhanced,
            d12Rolls,
            d12Result,
            d6Rolls,
            d6Total,
            total
        );


        // -----------------------------------------
        // LOG
        // -----------------------------------------

        let characterName = "Nieznana postać";

        const characterNameElement =
            document.querySelector(".header .name");

        if (characterNameElement)
            characterName =
                characterNameElement.textContent.trim();


        let d12Text;

        if (isEnhanced) {

            d12Text =
                `${d12Rolls[0]} / ${d12Rolls[1]} → ${d12Result}`;

        } else {

            d12Text =
                `${d12Result}`;

        }


        const d6Text =
            d6Rolls.length > 0
                ? d6Rolls.join(" + ")
                : "brak";


        const enhancedText =
            isEnhanced
                ? " [WZMOCNIONY]"
                : "";


        sendDiceLog(
            `${characterName} rzuca ${skillName}${enhancedText}: ` +
            `D12: ${d12Text}, ` +
            `D6: ${d6Text}, ` +
            `wynik: ${total}`
        );

    });

    // -----------------------------------------
    // DOWOLNY RZUT KOŚĆMI
    // -----------------------------------------

    const customRollButton =
        document.getElementById("customRollButton");

    const customD12Count =
        document.getElementById("customD12Count");

    const customD6Count =
        document.getElementById("customD6Count");

    const customDiceResult =
        document.getElementById("customDiceResult");

    if (customRollButton &&
        customD12Count &&
        customD6Count &&
        customDiceResult) {

        customRollButton.addEventListener("click", () => {
            let characterName = "Nieznana postać";

            const logDrawer = document.getElementById("logDrawer");
            const isGM = logDrawer?.dataset.isGm === "true";

            if (isGM) {
                characterName = "GM";
            } else {
                const characterNameElement =
                    document.querySelector(".header .name");

                if (characterNameElement) {
                    characterName =
                        characterNameElement.textContent.trim();
                }
            }
            const d12Count = Number(customD12Count.value);
            const d6Count = Number(customD6Count.value);

            // Walidacja liczby kości
            if (
                !Number.isInteger(d12Count) ||
                !Number.isInteger(d6Count) ||
                d12Count < 0 ||
                d6Count < 0 ||
                d12Count > 100 ||
                d6Count > 100 ||
                d12Count + d6Count === 0
            ) {
                alert(
                    "Podaj liczbę kości od 0 do 100. " +
                    "Musisz wybrać przynajmniej jedną kość."
                );
                return;
            }

            // Rzuty D12
            const d12Rolls = [];

            for (let i = 0; i < d12Count; i++) {
                d12Rolls.push(rollDice(12));
            }

            // Rzuty D6
            const d6Rolls = [];

            for (let i = 0; i < d6Count; i++) {
                d6Rolls.push(rollDice(6));
            }

            // Sumy poszczególnych rodzajów kości
            const d12Total = d12Rolls.reduce(
                (sum, value) => sum + value, 0
            );

            const d6Total = d6Rolls.reduce(
                (sum, value) => sum + value, 0
            );

            // Wynik końcowy
            const total = d12Total + d6Total;

            const d12Text = d12Rolls.length > 0
                ? d12Rolls.join(" + ")
                : "brak";

            const d6Text = d6Rolls.length > 0
                ? d6Rolls.join(" + ")
                : "brak";

            // Wyświetlenie wyniku

            customDiceResult.innerHTML = `
            <div class="dice-result-entry">
                <div class="dice-result-skill">
                    Dowolny rzut
                </div>

                <div class="dice-result-info">
                    Liczba kości: ${d12Count}D12 + ${d6Count}D6
                </div>

                <div class="dice-result-roll">
                    <strong>D12:</strong>
                    ${d12Text}
                </div>

                <div class="dice-result-roll">
                    <strong>D6:</strong>
                    ${d6Text}
                </div>

                <div class="dice-result-total">
                    Wynik:
                    <strong>${total}</strong>
                </div>
            </div>
        `;


            // Dodanie do istniejącego logu
            sendDiceLog(
                `${characterName} rzuca ` +
                `${d12Count}D12 (${d12Text} = ${d12Total}), ` +
                `${d6Count}D6 (${d6Text} = ${d6Total}), ` +
                `wynik: ${total}`
            );
        });
    }

});


/*
 * Rzut pojedynczą kością
 */
function rollDice(sides) {

    return Math.floor(Math.random() * sides) + 1;

}


/*
 * Wyświetlenie wyniku rzutu
 */
function displayDiceResult(
    skillName,
    skillValue,
    isEnhanced,
    d12Rolls,
    d12Result,
    d6Rolls,
    d6Total,
    total
) {
    const diceResult = document.getElementById("diceResult");

    if (!diceResult) {
        return;
    }

    const d12Text = isEnhanced
        ? `${d12Rolls[0]} / ${d12Rolls[1]} → ${d12Result}`
        : `${d12Result}`;

    const d6Text = d6Rolls.length > 0
        ? d6Rolls.join(" + ")
        : "brak";

    diceResult.innerHTML = `
        <div class="dice-result-entry">
            <div class="dice-result-skill">
                ${skillName}
            </div>

            <div class="dice-result-info">
                Poziom umiejętności: ${skillValue}
            </div>

            <div class="dice-result-roll">
                <strong>d12:</strong>
                ${d12Text}
            </div>

            <div class="dice-result-roll">
                <strong>d6:</strong>
                ${d6Text}
            </div>

            <div class="dice-result-total">
                Wynik:
                <strong>${total}</strong>
            </div>
        </div>
    `;
}


/*
 * Log rzutów
 */
function addLog(message) {

    const log =
        document.getElementById("logMessages");

    if (!log)
        return;


    const entry =
        document.createElement("div");

    entry.className =
        "log-entry";

    entry.textContent =
        message;


    log.prepend(entry);

}