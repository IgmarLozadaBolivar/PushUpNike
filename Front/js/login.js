Swal.fire({
    title: '¡Bienvenido!',
    text: 'Te invito a que valides tu usuario!',
    icon: 'info',
    confirmButtonText: 'Aceptar'
});

const urlAuth = "http://localhost:5057/api/User/token";
const headers = new Headers({ 'Content-Type': 'application/json' });
const boton = document.getElementById('botonLogin');

boton.addEventListener("click", function (e) {
    e.preventDefault();
    autorizarUsuario();
});

async function autorizarUsuario() {
    let inputUsuario = document.getElementById('username').value;
    let inputPassword = document.getElementById('password').value;

    let data = {
        "nombre": inputUsuario,
        "password": inputPassword
    };

    const config = {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(data)
    };

    try {
        const response = await fetch(urlAuth, config);
    
        if (response.ok) {
            window.location.href = "../html/inicio.html";
        } else {
            const errorData = await response.json();
            console.log(errorData);
            alert("Autenticación fallida, verifique sus credenciales o regístrese.");
        }
    } catch (error) {
        console.error("Error al realizar la autenticación:", error);
    }
}