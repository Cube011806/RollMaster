function toggleDrawer(id) {
    document.getElementById(id).classList.toggle("open");
}


document.addEventListener("DOMContentLoaded", () => {

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


        addLog(
            `${characterName} rzuca ${skillName}${enhancedText}: ` +
            `d12: ${d12Text}, ` +
            `d6: ${d6Text}, ` +
            `wynik: ${total}`
        );

    });

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