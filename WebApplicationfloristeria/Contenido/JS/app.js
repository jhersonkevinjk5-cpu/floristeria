const menu = document.querySelector('.hamburguesa');
const navegacion = document.querySelector('.navegacion');
const imagenes = document.querySelectorAll('img');

document.addEventListener('DOMContentLoaded', () => {
    eventos();
    filtrarProductos();
});


// ==========================
// EVENTOS MENU
// ==========================
const eventos = () => {
    if (menu) {
        menu.addEventListener('click', abrirMenu);
    }
}

const abrirMenu = () => {
    navegacion.classList.remove('ocultar');
    botonCerrar();
}

const botonCerrar = () => {
    const btnCerrar = document.createElement('p');
    const overlay = document.createElement('div');

    overlay.classList.add('pantalla-completa');

    const body = document.querySelector('body');

    if (document.querySelectorAll('.pantalla-completa').length > 0) return;

    body.appendChild(overlay);

    btnCerrar.textContent = 'x';
    btnCerrar.classList.add('btn-cerrar');

    navegacion.appendChild(btnCerrar);

    cerrarMenu(btnCerrar, overlay);
}

const cerrarMenu = (boton, overlay) => {

    boton.addEventListener('click', () => {
        navegacion.classList.add('ocultar');
        overlay.remove();
        boton.remove();
    });

    overlay.onclick = function () {
        overlay.remove();
        navegacion.classList.add('ocultar');
        boton.remove();
    }
}


// ==========================
// LAZY LOAD IMAGENES
// ==========================
const observer = new IntersectionObserver((entries, observer) => {

    entries.forEach(entry => {

        if (entry.isIntersecting) {

            const imagen = entry.target;

            imagen.src = imagen.dataset.src;

            observer.unobserve(imagen);
        }

    });

});

imagenes.forEach(imagen => {
    observer.observe(imagen);
});


// ==========================
// FILTRAR PRODUCTOS
// ==========================
const filtrarProductos = () => {

    const botones = document.querySelectorAll('.filtro');
    const productos = document.querySelectorAll('.producto');

    botones.forEach(boton => {

        boton.addEventListener('click', () => {

            const categoria = boton.dataset.categoria;

            productos.forEach(producto => {

                if (categoria === "todos") {
                    producto.style.display = "block";
                }
                else if (producto.dataset.categoria === categoria) {
                    producto.style.display = "block";
                }
                else {
                    producto.style.display = "none";
                }

            });

        });

    });

}