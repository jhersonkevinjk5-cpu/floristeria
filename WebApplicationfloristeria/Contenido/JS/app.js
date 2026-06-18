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

// ===============================
// JavaScript abrir/cerrar Carrito
// ===============================
const btnCarrito = document.getElementById("btnCarrito");
const panelCarrito = document.getElementById("panelCarrito");
const cerrarCarrito = document.getElementById("cerrarCarrito");

if (btnCarrito != null && panelCarrito != null) {
    btnCarrito.addEventListener("click", function () {
        panelCarrito.classList.add("activo");
    });
}

if (cerrarCarrito != null && panelCarrito != null) {
    cerrarCarrito.addEventListener("click", function () {
        panelCarrito.classList.remove("activo");
    });
}

// =============================================
// JavaScript Para agregar productos al carrito
// =============================================
const botonesAgregar = document.querySelectorAll(".agregar-carrito");
const contenidoCarrito = document.getElementById("contenidoCarrito");
const contador = document.getElementById("contadorCarrito");

let cantidad = 0;
let total = 0;

botonesAgregar.forEach(boton => {

    boton.addEventListener("click", () => {

        let id = parseInt(boton.dataset.id);
        let imagen = boton.dataset.imagen;
        let nombre = boton.dataset.nombre;
        let precio = parseFloat(boton.dataset.precio);

        // =========================
        // 1. Mostrar en carrito UI
        // =========================
        let div = document.createElement("div");

        div.classList.add("item-carrito");

        div.dataset.id = id;

        div.innerHTML = `
            <img src="${imagen}" class="img-carrito">

            <div class="info-item">
                <p>${nombre}</p>
                <span>S/. ${precio.toFixed(2)}</span>
            </div>

            <button class="eliminar-item">✖</button>
        `;

        contenidoCarrito.appendChild(div);

        // =========================
        // 2. Guardar en Session MVC
        // =========================
        fetch('/Home/AgregarCarrito', {
            method: 'POST',

            headers: {
                'Content-Type': 'application/json'
            },

            body: JSON.stringify({
                id: id,
                nombre: nombre,
                precio: precio,
                imagen: imagen
            })
        })

            .then(response => response.json())

            .then(data => {

                // Lo que viene del Controller
                cantidad = data.cantidad;
                total = data.total;

                contador.textContent = cantidad;
                document.getElementById("totalCarrito").textContent = total.toFixed(2);

            })

            .catch(error => {
                console.log("Error:", error);
            });

        // =========================
        // 3. Botón eliminar SOLO UI
        // =========================
        const btnEliminar = div.querySelector(".eliminar-item");

        btnEliminar.addEventListener("click", function () {

            fetch('/Home/EliminarCarrito', {

                method: 'POST',

                headers: {
                    'Content-Type': 'application/json'
                },

                body: JSON.stringify({
                    id: id
                })

            })

                .then(response => response.json())

                .then(data => {

                    div.remove();

                    cantidad = data.cantidad;
                    total = data.total;

                    contador.textContent = cantidad;

                    document.getElementById("totalCarrito")
                        .textContent = total.toFixed(2);

                })

                .catch(error => {
                    console.log(error);
                });

        });

    });   // ← cierra boton.addEventListener

});      


// =============================
// Botón comprar
// =============================
const btnComprar = document.getElementById("btnComprar");

if (btnComprar) {
    btnComprar.addEventListener("click", function () {

        fetch('/Home/FinalizarCompra', {

            method: 'POST',

            headers: {
                'Content-Type': 'application/json'
            }

        })

            .then(response => response.json())

            .then(data => {

                if (data.ok) {

                    alert("Compra registrada correctamente");

                    contenidoCarrito.innerHTML = "";

                    contador.textContent = "0";

                    document.getElementById("totalCarrito").textContent = "0.00";
                }
                else {

                    alert(data.mensaje);
                }

            })

            .catch(error => {
                console.log(error);
            });

    });
}


//alert("JS cargado");