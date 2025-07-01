let user = null;
let space = null;

document.addEventListener('DOMContentLoaded', () => {
    const userDataJSON = sessionStorage.getItem('currentUserData');
    const spaceDataJSON = localStorage.getItem('selectedSpace');

    if (!userDataJSON || !spaceDataJSON) return;

    user = JSON.parse(userDataJSON);
    space = JSON.parse(spaceDataJSON);

    const IMAGE_BASE_URL = `${location.protocol}//${location.host}/Assets/Imagens/Spaces/`;

    // Espaço
    document.getElementById('space-image').src = space.imageUrls && space.imageUrls.length > 0
        ? `${IMAGE_BASE_URL}${space.id}/${space.imageUrls[0]}`
        : 'Assets/Imagens/default.jpg';

    document.getElementById('space-name').textContent = space.name || '';
    document.getElementById('space-description').textContent = space.description || '';
    document.getElementById('space-capacity').textContent = space.capacity || '';
    document.getElementById('space-price').textContent = `R$ ${parseFloat(space.price).toFixed(2)}`;

    const [hh, mm] = space.reserveTime.split(":");
    document.getElementById('space-duration').textContent = `${parseInt(hh)} hora(s) e ${parseInt(mm)} minuto(s)`;

    // Usuário
    document.getElementById('user-name').textContent = user.username || '';
    document.getElementById('user-email').textContent = user.email || '';
    document.getElementById('user-phone').textContent = user.phoneNumber || '';
});

async function confirmarReserva() {
    const date = document.getElementById('reserve-date').value;
    const time = document.getElementById('reserve-time').value;
    const success = document.getElementById('success-message');
    const error = document.getElementById('error-message');

    success.style.display = 'none';
    error.style.display = 'none';

    if (!date || !time || !user || !space) {
        error.textContent = "Preencha todos os dados corretamente.";
        error.style.display = 'block';
        return;
    }

    const urlApi = "https://localhost:7181/api/v1/Reserve/AddReserve";

    const requestBody = {
        SpaceId: space.id,
        UserId: user.id,
        StartDate: date,
        Hours: time
    };

    console.log(requestBody);

    try {
        const response = await fetch(urlApi, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(requestBody)
        });

        if (response.ok) {
            success.textContent = "Reserva confirmada com sucesso!";
            success.style.display = 'block';
            setTimeout(function () {
                window.location.href = './home.html';
            }, 2000)
        } else {
            const errorText = await response.text();
            throw new Error(errorText);
        }
    } catch (err) {
        error.textContent = "Erro ao salvar a reserva. Tente novamente.";
        error.style.display = 'block';
        console.error(err);
    }
}