document.getElementById('login-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('error-message');

    errorMessage.textContent = '';
    errorMessage.style.display = 'none';

    try {
        const passHased = await hashPassword(password);

        const requestBody = {
            Username: username,
            Password: passHased
        }

        const urlApi = "https://localhost:7181/api/v1/Login";

        fetch(urlApi, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        })
            .then(response => {
                if (!response.ok) {
                    errorMessageDiv.textContent = 'Usuário ou senha incorretos. Tente novamente.';
                    errorMessageDiv.style.display = 'block';
                } 
                return response.json()
            })
            .then(data => {
                document.getElementById('resultado').innerText = 'Dados enviados com sucesso! Resposta da API: ' + JSON.stringify(data);
                console.log('Resposta da API:', data);
            })
            .catch(error => {
                console.error('Erro na requisição AJAX:', error);
            });
    }
    catch (error) {
        console.error('Erro no processo de cadastro:', error);
    }
})

async function hashPassword(password) {
    const textEncoder = new TextEncoder();
    const data = textEncoder.encode(password); // Converte a string da senha em bytes

    // Calcula o hash SHA-256
    const hashBuffer = await crypto.subtle.digest('SHA-256', data);

    // Converte o ArrayBuffer do hash para uma string hexadecimal
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    const hexHash = hashArray.map(b => b.toString(16).padStart(2, '0')).join('');

    return hexHash;
}