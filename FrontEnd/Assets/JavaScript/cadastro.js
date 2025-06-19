import { hashPassword } from './utils.js';

document.getElementById('cadastro-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const phone = document.getElementById('phone').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('error-message');

    errorMessage.textContent = '';
    errorMessage.style.display = 'none';

    try {
        const passHased = await hashPassword(password)

        const requestBody = {
            Username: username,
            Phone: phone,
            Email: email,
            Password: passHased
        }

        const urlApi = "https://localhost:7181/api/v1/Cadastro";

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