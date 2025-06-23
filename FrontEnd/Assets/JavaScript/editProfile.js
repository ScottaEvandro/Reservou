const loadingMessage = document.getElementById('loadingMessage');
const errorMessageDiv = document.getElementById('errorMessage');
const editProfileForm = document.getElementById('editProfileForm');
const messageDiv = document.getElementById('message');

const userIdInput = document.getElementById('userId');
const usernameInput = document.getElementById('username');
const taxIdInput = document.getElementById('taxId');
const emailInput = document.getElementById('email');
const phoneNumberInput = document.getElementById('phoneNumber');
const userTypeInput = document.getElementById('userType');

/**
 * Exibe uma mensagem na interface do usuário.
 * @param {string} text - O texto da mensagem.
 * @param {'success'|'error'|'info'} type - O tipo da mensagem para estilização.
 */
function displayMessage(text, type) {
    messageDiv.textContent = text;
    messageDiv.className = `message ${type}`; 
    messageDiv.style.display = 'block';
    setTimeout(() => {
        messageDiv.style.display = 'none';
    }, 5000);
}

/**
 * Carrega os dados do usuário do sessionStorage e preenche o formulário.
 */
function loadUserProfile() {
    loadingMessage.style.display = 'block';
    editProfileForm.style.display = 'none';
    errorMessageDiv.style.display = 'none';
    messageDiv.style.display = 'none'; 

    const storedUserDataString = sessionStorage.getItem('currentUserData');

    if (storedUserDataString) {
        try {
            const userData = JSON.parse(storedUserDataString);

            
            userIdInput.value = userData.id || ''; 
            usernameInput.value = userData.username || '';
            taxIdInput.value = userData.taxId || '';
            emailInput.value = userData.email || '';
            phoneNumberInput.value = userData.phoneNumber || '';
            userTypeInput.value = userData.userType || ''; 

            loadingMessage.style.display = 'none';
            editProfileForm.style.display = 'block';
            displayMessage('Dados carregados da sessão. Você pode editá-los.', 'info');

        } catch (e) {
            console.error('Erro ao fazer parse dos dados do usuário do sessionStorage:', e);
            errorMessageDiv.textContent = 'Erro ao carregar dados do perfil. Dados da sessão corrompidos.';
            errorMessageDiv.style.display = 'block';
            loadingMessage.style.display = 'none';
            
            alert('Dados da sessão inválidos. Por favor, faça login novamente.');
            sessionStorage.removeItem('currentUserData'); 
            window.location.href = './login.html'; 
        }
    } else {
        
        errorMessageDiv.textContent = 'Você não está logado ou sua sessão expirou. Por favor, faça login.';
        errorMessageDiv.style.display = 'block';
        loadingMessage.style.display = 'none';
        
        setTimeout(() => {
            window.location.href = './login.html'; 
        }, 3000);
    }
}

/**
 * Lida com o envio do formulário para salvar as alterações.
 */
async function saveProfileChanges(event) {
    event.preventDefault(); 

    displayMessage('Salvando alterações...', 'info');

    const updatedUserData = {
        id: parseInt(userIdInput.value),
        username: usernameInput.value,
        taxId: taxIdInput.value,
        email: emailInput.value,
        phoneNumber: phoneNumberInput.value,
        userType: parseInt(userTypeInput.value) 
    };

    try {
        
        
        

        const url = `${API_UPDATE_URL}/${updatedUserData.id}`;
        const response = await fetch(url, {
            method: 'PUT', 
            headers: {
                'Content-Type': 'application/json',
                
            },
            body: JSON.stringify(updatedUserData)
        });

        if (response.ok) {
            const responseData = await response.json(); 
            displayMessage('Perfil atualizado com sucesso!', 'success');
            
            sessionStorage.setItem('currentUserData', JSON.stringify(responseData));

            
            document.querySelector('button[type="submit"]').disabled = true;
            setTimeout(() => {
                document.querySelector('button[type="submit"]').disabled = false;
            }, 2000); 
            
        } else {
            const errorData = await response.json();
            displayMessage(`Erro ao salvar perfil: ${errorData.message || response.statusText}`, 'error');
            console.error('Erro ao salvar:', response.status, errorData);
        }
    } catch (error) {
        console.error('Erro de rede ou inesperado ao salvar:', error);
        displayMessage('Não foi possível conectar ao servidor ou ocorreu um erro inesperado.', 'error');
    }
}


document.addEventListener('DOMContentLoaded', loadUserProfile);


editProfileForm.addEventListener('submit', saveProfileChanges);