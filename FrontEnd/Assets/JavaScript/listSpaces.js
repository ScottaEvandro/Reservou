document.addEventListener('DOMContentLoaded', async function () {
    const spacesListContainer = document.getElementById('spaces-list-container');

    const urlApi = "https://localhost:7181/api/v1/Spaces/GetAllSpaces";
    const IMAGE_BASE_URL = "http://127.0.0.1:5501/FrontEnd/Assets/Imagens/Spaces/"

    try {
        const response = await fetch(urlApi);

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Erro ao buscar espaços: ${response.status} - ${errorText}`);
        }

        if (response.status == 204) {
            spacesListContainer.innerHTML = '<p class="no-spaces-message">Nenhum espaço cadastrado.</p>';
            return;
        }

        const spaces = await response.json();

        if (spaces.length === 0) {
            spacesListContainer.innerHTML = '<p class="no-spaces-message">Nenhum espaço cadastrado.</p>';
            return;
        }
        spacesListContainer.innerHTML = '';

        spaces.forEach(space => {
            const spaceCard = document.createElement('div');
            spaceCard.classList.add('space-card');
            
            const reserveTime = space.reserveTime;
            const [hours, minutes] = reserveTime.split(":");

            const reserveTimeFormated = `${parseInt(hours)} hora(s) e ${parseInt(minutes)} minuto(s)`;

            const imageUrl = space.imageUrls && space.imageUrls.length > 0
                ? `${IMAGE_BASE_URL}${space.id}/${space.imageUrls[0]}`
                : 'Assets/Imagens/{space.id}';

            spaceCard.innerHTML = `
                <img src="${imageUrl}" alt="${space.Name}" class="space-image">
                <h3>${space.name}</h3>
                <p>${space.description}</p>
                <div class="space-details">
                    <span><i class="fas fa-users"></i> Capacidade: ${space.capacity}</span><br>
                    <span><i class="fas fa-dollar-sign"></i> Preço: R$ ${space.price.toFixed(2)}</span><br>
                    <span><i class="fas fa-clock"></i> Duração da reserva: ${reserveTimeFormated}</span><br>
                </div>
                `;
            // <div class="space-actions">
            //     <button class="edit-space-button" data-space-id="${space.id}"><i class="fas fa-edit"></i> Editar</button>
            //     <button class="delete-space-button" data-space-id="${space.id}"><i class="fas fa-trash-alt"></i> Excluir</button>
            // </div>
            spacesListContainer.appendChild(spaceCard);
        });

        spacesListContainer.querySelectorAll('.edit-space-button').forEach(button => {
            button.addEventListener('click', function () {
                const spaceId = this.dataset.spaceId;
                alert(`Editar espaço com ID: ${spaceId}`);
                // Redirecionar para uma página de edição: window.location.href = `editSpace.html?id=${spaceId}`;
            });
        });

    } catch (error) {
        console.error('Erro ao carregar espaços:', error);
        spacesListContainer.innerHTML = `<p class="error-message">Erro ao carregar os espaços: ${error.message}. Tente novamente mais tarde.</p>`;
    }
});