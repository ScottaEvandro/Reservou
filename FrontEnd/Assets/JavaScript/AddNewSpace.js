document.getElementById('add-space-form').addEventListener('submit', async function (event) {
    event.preventDefault();

    console.log("Chegou na função");

    console.log()

    const spaceType = parseInt(document.getElementById('space-type').value, 10);
    const spaceName = document.getElementById('space-name').value;
    const spaceDescription = document.getElementById('space-description').value;
    const spaceCapacity = parseInt(document.getElementById('space-capacity').value, 10);
    const spacePrice = parseFloat(document.getElementById('space-price').value,);
    const reserveTime = document.getElementById('reserve-time').value;
    const maintenanceTime = document.getElementById('maintenance-time').value;
    const spaceImages = document.getElementById('space-images');
    const selectedImages = spaceImages.files;
    const isActive = (document.getElementById('space-status').value === "true");

    const errorMessage = document.getElementById('error-message');
    const successMessage = document.getElementById('success-message');

    errorMessage.textContent = '';
    errorMessage.style.display = 'none';

    successMessage.textContent = '';
    successMessage.style.display = 'none';

    try {
        const requestBody = {
            SpaceName: spaceName,
            Description: spaceDescription,
            Capacity: spaceCapacity,
            SpaceTypeId: spaceType,
            Price: spacePrice,
            ReserveDuration: reserveTime,
            MaintenanceTime: maintenanceTime,
            SpaceImages: spaceImages,
            isActive: isActive
        }

        console.log(JSON.stringify(requestBody));

        const urlApi = "https://localhost:7181/api/v1/Spaces/AddSpace";

        const response = await fetch(urlApi, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        })

        switch (response.status) {
            case 201:
                successMessage.textContent = 'Cadastro do espaço realizado com sucesso!';
                successMessage.style.display = 'block';
                document.getElementById('add-space-form').reset();
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