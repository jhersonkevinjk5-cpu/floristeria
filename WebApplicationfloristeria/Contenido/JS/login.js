function flipCard() {
    const card = document.getElementById("card");
    const tipo = document.getElementById("tipoLogin");

    if (!card || !tipo) return;

    card.classList.toggle("flipped");
    document.body.classList.toggle("cliente-mode");

    tipo.value = (tipo.value === "usuario") ? "cliente" : "usuario";

    spawnPetals();
}

function spawnPetals() {
    for (let i = 0; i < 6; i++) {
        const petal = document.createElement("div");
        petal.className = "petal";
        petal.innerHTML = "🌸";

        petal.style.left = Math.random() * 100 + "vw";
        petal.style.fontSize = (14 + Math.random() * 18) + "px";
        petal.style.animationDuration = (3 + Math.random() * 2) + "s";

        document.body.appendChild(petal);

        setTimeout(() => petal.remove(), 4000);
    }
}

function loginError() {
    const card = document.getElementById("card");

    if (!card) return;

    card.classList.add("shake");

    setTimeout(() => card.classList.remove("shake"), 500);
}

