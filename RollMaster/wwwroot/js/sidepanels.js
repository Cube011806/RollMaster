
function toggleDrawer(id) {
    const drawer = document.getElementById(id);
    if (!drawer) return;

    const willOpen = !drawer.classList.contains("open");

    // Przy otwieraniu panelu zamknij pozostałe panele po prawej.
    if (willOpen && drawer.classList.contains("drawer-right")) {
        document.querySelectorAll(".drawer-right.open").forEach(otherDrawer => {
            if (otherDrawer !== drawer) {
                otherDrawer.classList.remove("open");
            }
        });
    }

    drawer.classList.toggle("open", willOpen);

    // Ukryj przycisk „Postacie”, gdy otwarty jest panel kości.
    const diceDrawer = document.getElementById("diceDrawer");
    const charactersDrawer = document.getElementById("charactersDrawer");

    if (diceDrawer && charactersDrawer) {
        const charactersTab = charactersDrawer.querySelector(".drawer-tab");
        const diceIsOpen = diceDrawer.classList.contains("open");

        if (charactersTab) {
            charactersTab.style.display = diceIsOpen ? "none" : "";
        }
    }

    // Po otwarciu logu usuń powiadomienie o nowych wpisach.
    if (id === "logDrawer" && willOpen) {
        drawer.classList.remove("has-new-log");

        const tab = drawer.querySelector(".drawer-tab");
        if (tab) {
            tab.removeAttribute("data-unread");
            tab.removeAttribute("aria-label");
        }
    }
}


let diceConnection = null;

function renderDiceLog(entry, notify = true) {
    addLog(entry.description, notify);
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
        history.reverse().forEach(entry => {
            renderDiceLog(entry, false);
        });

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
function updateSkillSelectFromForm(form) {
    if (!form) return;

    const skillSelect = document.getElementById("skillSelect");
    if (!skillSelect) return;

    // Aktualizujemy listę tylko dla aktywnej postaci.
    const activeForm = document.querySelector(
        ".tab-pane.active .characterForm"
    );

    if (activeForm && activeForm !== form) return;

    const skillInputs = form.querySelectorAll(
        'input[type="radio"][name^="Skills["][name$="].Value"]'
    );

    skillInputs.forEach(input => {
        if (!input.checked) return;

        const match = input.name.match(/^Skills\[(\d+)\]\.Value$/);
        if (!match) return;

        const index = Number(match[1]);
        const option = skillSelect.options[index];

        if (!option) return;

        const value = input.value;

        option.dataset.value = value;
        option.textContent =
            `${option.dataset.skillName} (${value})`;
    });
}

document.addEventListener("change", function (event) {
    const input = event.target;

    if (
        !input.matches(
            'input[type="radio"][name^="Skills["][name$="].Value"]'
        )
    ) {
        return;
    }

    const form = input.closest(".characterForm");

    updateSkillSelectFromForm(form);
});

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
function addLog(message, notify = true) {
    const log = document.getElementById("logMessages");
    if (!log) return;

    const entry = document.createElement("div");
    entry.className = "log-entry";
    entry.textContent = message;
    log.prepend(entry);

    // Powiadomienie tylko dla rzeczywiście nowych wpisów
    if (!notify) return;

    const drawer = document.getElementById("logDrawer");
    if (!drawer || drawer.classList.contains("open")) return;

    drawer.classList.add("has-new-log");

    const tab = drawer.querySelector(".drawer-tab");
    if (tab) {
        tab.dataset.unread = "true";
        tab.setAttribute("aria-label", "Log — nowe wpisy");
    }
}

(function () {
    async function initializeOtherCharactersPanel() {
        const drawer = document.getElementById("charactersDrawer");
        const list = document.getElementById("otherCharactersList");
        const preview = document.getElementById("characterPreview");

        if (!drawer || !list || !preview) return;

        const currentCharacterId = drawer.dataset.currentCharacterId;

        if (!currentCharacterId) {
            list.textContent = "Nie można ustalić aktualnej postaci.";
            return;
        }

        try {
            const url = `/Character/OtherCharacters?currentCharacterId=${encodeURIComponent(currentCharacterId)}`;
            console.log("Adres listy postaci:", url);

            const response = await fetch(url);
            if (!response.ok) {
                throw new Error(`HTTP ${ response.status }`);
            }

            const characters = await response.json();
            list.replaceChildren();

            if (characters.length === 0) {
                list.textContent = "Brak innych postaci w tej grze.";
                return;
            }

            characters.forEach(character => {
                const button = document.createElement("button");
                button.type = "button";
                button.className = "other-character-button";
                button.textContent = character.imie ?? character.Imie ?? "Postać";

                button.addEventListener("click", async () => {
                    list.querySelectorAll("button").forEach(item => {
                        item.classList.toggle("selected", item === button);
                    });

                    preview.textContent = "Wczytywanie karty...";

                    try {
                        const url =
                            `/Character/CharacterPreview?id=${encodeURIComponent(character.id ?? character.Id)}` +
                            `&currentCharacterId=${encodeURIComponent(currentCharacterId)}`;

                        console.log("Adres karty postaci:", url);

                        const previewResponse = await fetch(url);

                        if (!previewResponse.ok) {
                            throw new Error(`HTTP ${ previewResponse.status }`);
                        }

                        preview.innerHTML = await previewResponse.text();
                    } catch (error) {
                        console.error("Nie udało się pobrać karty postaci:", error);
                        preview.textContent = "Nie udało się wczytać karty postaci.";
                    }
                });

                list.appendChild(button);
            });
        } catch (error) {
            console.error("Nie udało się pobrać listy postaci:", error);
            list.textContent = "Nie udało się pobrać listy postaci.";
        }
    }

    if (document.readyState === "loading") {
        document.addEventListener(
            "DOMContentLoaded",
            initializeOtherCharactersPanel
        );
    } else {
        initializeOtherCharactersPanel();
    }
})();