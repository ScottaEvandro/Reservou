document.getElementById('add-space-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    const spaceType = parseInt(document.getElementById('space-type').value, 10);
    const spaceName = document.getElementById('space-name').value;
    const spaceDescription = document.getElementById('space-description').value;
    const spaceCapacity = parseInt(document.getElementById('space-capacity').value, 10);
    const spacePrice = parseFloat(document.getElementById('space-price').value);
    const reserveTime = document.getElementById('reserve-time').value;
    const maintenanceTime = document.getElementById('maintenance-time').value;
    const spaceImagesInput = document.getElementById('space-images');
    const selectedFiles = spaceImagesInput.files;
    const isActive = (document.getElementById('space-status').value === "true");

    const errorMessage = document.getElementById('error-message');
    const successMessage = document.getElementById('success-message');

    errorMessage.textContent = '';
    errorMessage.style.display = 'none';

    successMessage.textContent = '';
    successMessage.style.display = 'none';

    try {
        const formData = new FormData();

        formData.append('SpaceName', spaceName);
        formData.append('Description', spaceDescription);
        formData.append('Capacity', spaceCapacity.toString());
        formData.append('SpaceTypeId', spaceType.toString());
        formData.append('Price', spacePrice.toString());
        formData.append('ReserveDuration', reserveTime);
        formData.append('MaintenanceTime', maintenanceTime);
        formData.append('IsActive', isActive.toString());

        for (let i = 0; i < selectedFiles.length; i++) {
            formData.append('SpaceImages', selectedFiles[i]);
        }

        const urlApi = "https://localhost:7181/api/v1/Spaces/AddSpace";

        const response = await fetch(urlApi, {
            method: 'POST',
            body: formData
        });

        if (response.ok) {
            successMessage.textContent = 'Cadastro do espaço realizado com sucesso!';
            successMessage.style.display = 'block';
            
            setTimeout(() => {
                successMessage.style.display = 'none';
            }, 10000);

            document.getElementById('add-space-form').reset();
        } else {
            const errorText = await response.text().catch(() => 'Nenhuma mensagem de erro.');
            console.error("Erro do servidor:", response.status, errorText);
            const displayErrorMessage = `Erro ao salvar o espaço: ${response.status} - ${errorText.substring(0, 100)}`;
            throw new Error(displayErrorMessage);
        }

    } catch (error) {
        console.error('Erro na requisição Fetch:', error);
        errorMessage.textContent = error.message || 'Ocorreu um erro ao conectar com o servidor. Verifique a API e tente novamente.';
        errorMessage.style.display = 'block';
    }
});