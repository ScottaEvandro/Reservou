import { hashPassword } from "./utils.js";

document.getElementById('cadastro-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
    const cpf = document.getElementById('cpf').value;
    const phone = document.getElementById('phone').value;
    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('error-message');
    const successMessage = document.getElementById('success-message');

    errorMessage.textContent = '';
    errorMessage.style.display = 'none';

    successMessage.textContent = '';
    successMessage.style.display = 'none';

    try {
        const passHased = await hashPassword(password);

        const requestBody = {
            Username: username,
            TaxId: cpf,
            PhoneNumber: phone,
            Email: email,
            Password: passHased
        }

        const urlApi = "https://localhost:7181/api/v1/User/Cadastro";

        const response = await fetch(urlApi, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        })

        switch (response.status) {
            case 201:
                sessionStorage.setItem('currentUserData', JSON.stringify(requestBody));

                successMessage.textContent = 'Cadastro realizado com sucesso! Redirecionando...';
                successMessage.style.display = 'block';
                setTimeout(function () {
                    window.location.href = './home.html';
                }, 1000)
                break;
            case 400:
                errorMessage.textContent = 'Dados inválidos! Verifique as informações e tente novamente.';
                errorMessage.style.display = 'block';
                break;
            case 409:
                errorMessage.textContent = 'Usuário informado já está cadastrado!';
                errorMessage.style.display = 'block';
                break;
            default:
                const errorText = await response.text().catch(() => '');
                const errorMessage1 = `Erro inesperado do servidor: ${response.status}`;
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