document.addEventListener('DOMContentLoaded', async function () {
    const spacesListContainer = document.getElementById('spaces-list-container');

    const urlApi = "https://localhost:7181/api/v1/Spaces/GetAllSpaces";
    const IMAGE_BASE_URL = `${this.location.protocol}//${this.location.host}/Assets/Imagens/Spaces/`;

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
                <img src="${imageUrl}" alt="${space.name}" class="space-image">
                <h3>${space.name}</h3>
                <p>${space.description}</p>
                <div class="space-details">
                    <span><i class="fas fa-users"></i> Capacidade: ${space.capacity}</span><br>
                    <span><i class="fas fa-dollar-sign"></i> Preço: R$ ${space.price.toFixed(2)}</span><br>
                    <span><i class="fas fa-clock"></i> Duração da reserva: ${reserveTimeFormated}</span><br>
                </div>
                <div class="reserve-button">
                    <button class="reserve-space-button" data-space-id="${space.id}"><i class="fas fa-calendar-check"></i> Reservar</button>
                </div> <p></p> `;

            spaceCard.querySelector('.reserve-space-button').spaceData = space;

            spacesListContainer.appendChild(spaceCard);
        });

        spacesListContainer.querySelectorAll('.reserve-space-button').forEach(button => {
            button.addEventListener('click', function () {
                const selectedSpace = this.spaceData;
                localStorage.setItem('selectedSpace', JSON.stringify(selectedSpace));
                window.location.href = './addReserve.html';
            });
        });

    } catch (error) {
        console.error('Erro ao carregar espaços:', error);
        spacesListContainer.innerHTML = `< p class="error-message" > Erro ao carregar os espaços: ${error.message}. Tente novamente mais tarde.</p > `;
    }
});