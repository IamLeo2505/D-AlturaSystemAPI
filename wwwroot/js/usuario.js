document.getElementById('userForm').addEventListener('submit', function(e) {
    e.preventDefault();


    const usuario = document.getElementById('usuario').value.trim();
    const password = document.getElementById('password').value.trim();
    const acceso = document.getElementById('acceso').value;
    const estado = document.querySelector('input[name="estado"]:checked');

    // Validaciones
    if (!usuario || !password || !estado) {
        alert('Todos los campos deben ser completados');
        return;
    }

    if (password.length < 6) {
        alert('La contraseña debe tener al menos 6 caracteres');
        return;
    }

   
    const userData = {
        usuario,
        password,
        acceso,
        estado,
    };

    fetch('http://localhost:5083/api/Usuarios/GuardarCambios', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    })
    .then(response => {
        if (!response.ok){
            throw new Error('Error en la Solicitud');
        }
        return response.json();
    })
    .then(data => {
        alert(data.messager || 'Usuario registrado correctamente.');
        document.getElementById('userForm').reset();
    })
    .catch(error => {
        console.error('Error: ', error);
        alert('Error registrando usuario.');
    })
});
