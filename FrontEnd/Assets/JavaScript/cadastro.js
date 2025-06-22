import { hashPassword } from "./utils.js";

document.getElementById('cadastro-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const cpf = document.getElementById('cpf').value;
    const phone = document.getElementById('phone').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('error-message');

    errorMessage.textContent = '';
    errorMessage.style.display = 'none';

    try {
        const passHased = await hashPassword(password);

        const requestBody = {
            Username: username,
            Cpf: cpf,
            Phone: phone,
            Email: email,
            Password: passHased
        }

        const urlApi = "https://localhost:7181/api/v1/Cadastro";

        const response = await fetch(urlApi, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        })

        switch (response.status) {
            case 201:
                window.location.href = './login.html';
                break;
            case 409:
                errorMessage.textContent = 'Usuário informado já está cadastrado!';
                errorMessage.style.display = 'block';
                break;
            default:
                const errorText = await response.text().catch(() => '');
                const errorMessage1 = errorText || `Erro inesperado do servidor: ${response.status}`;
                throw new Error(errorMessage1);
        }
    }
    catch (error) {
        console.error('Erro na requisição Ajax: ', error);

        if (error.message !== 'NotFound') {
            errorMessage.textContent = error.message || 'Ocorreu um erro ao conectar com o servidor. Tente novamente em instantes.';
            errorMessage.style.display = 'block';
        }
    }
})