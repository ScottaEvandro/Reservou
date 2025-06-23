import { hashPassword } from "./utils.js";

document.getElementById('login-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const username = document.getElementById('username').value;
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
            Password: passHased
        }

        const urlApi = "https://localhost:7181/api/v1/User/Login";

        const response = await fetch(urlApi, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        });

        switch (response.status) {
            case 404:
                errorMessage.textContent = 'Usuário ou senha incorretos. Tente novamente.';
                errorMessage.style.display = 'block';
                break;
            case 200:
                successMessage.textContent = 'Login efetuado com sucesso!';
                successMessage.style.display = 'block';
                const data = await response.json();

                sessionStorage.setItem('currentUserData', JSON.stringify(data));
                // UserType 1 = Administrador                    
                if (data.userType == 1) {
                    setTimeout(function () {
                    window.location.href = './dashboard.html';
                }, 500)
                } else {
                    setTimeout(function () {
                    window.location.href = './home.html';
                }, 500)
                }
                break;
            default:
                const errorText = await response.text().catch(() => '');
                const errorMessage1 = errorText || `Erro inesperado do servidor: ${response.status}`;
                throw new Error(errorMessage1);
        }
    }
    catch (error) {
        errorMessage.textContent = 'Ocorreu um erro ao conectar com o servidor. Tente novamente em instantes.';
        errorMessage.style.display = 'block';
    }
})