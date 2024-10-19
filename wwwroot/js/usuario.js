console.log("Hola mundo")

document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('userForm').addEventListener('submit', function (event) {
        event.preventDefault();

        const usuario = document.getElementById('usuario').value;
        const pass = document.getElementById('password').value;
        const acceso = document.getElementById('acceso').value;
        const estado = document.getElementById('estado').value;

        if (!usuario || !pass ) {
            alert('Por favor, llene todos los campos.');
            return;
        }

        fetch('http://localhost:5083/api/usuarios/GuardarCambios', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                usuario: usuario,
                pass: pass,
                acceso: acceso,
                estado: estado
            }),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Error en la solicitud');
                }
                return response.json();
            })
            .then(data => {
                alert('Los datos se han guardado exitosamente.');
                console.log(data);
                
            })
            .catch(error => {
                alert(error.message);
                console.error('Error:', error);
            });
    });
});
