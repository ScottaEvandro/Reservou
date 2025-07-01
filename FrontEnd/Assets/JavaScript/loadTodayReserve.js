document.addEventListener('DOMContentLoaded', async function () {
    const reserveListContainer = document.getElementById('reserve-list-container');

    const urlApi = "https://localhost:7181/api/v1/Reserve/GetTodayReserves";

    try {
        const response = await fetch(urlApi);

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Erro ao buscar espaços: ${response.status} - ${errorText}`);
        }

        if (response.status == 204) {
            reserveListContainer.innerHTML = 'Nenhuma reserva hoje.';
            return;
        }
        const reserves = await response.json();

        if (reserves.length === 0) {
            reserveListContainer.innerHTML = 'Nenhuma reserva hoje.';
            return;
        }

        const table = document.createElement('table');
        table.classList.add('reserves-table');

        // Cabeçalho da tabela
        const thead = document.createElement('thead');
        thead.innerHTML = `
            <tr>
                <th>Espaço</th>
                <th>Usuário</th>
                <th>Telefone</th>
                <th>Email</th>
                <th>Início</th>
                <th>Término</th>
            </tr>
        `;
        table.appendChild(thead);


        // Corpo da tabela
        const tbody = document.createElement('tbody');


        reserves.forEach(reserve => {

            console.log("Aqui", reserve.userMail);

            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${reserve.spaceName || '-'}</td>
                <td>${reserve.userName || '-'}</td>
                <td>${reserve.userPhone || '-'}</td>
                <td>${reserve.userMail || '-'}</td>
                <td>${formatDateTime(reserve.startTime)}</td>
                <td>${formatDateTime(reserve.endTime)}</td>
            `;
            tbody.appendChild(tr);
        });

        table.appendChild(tbody);
        reserveListContainer.innerHTML = '';
        reserveListContainer.appendChild(table);

    } catch (error) {
        console.error('Erro ao carregar espaços:', error);
        reserveListContainer.innerHTML = `Erro ao carregar as reservas. Tente novamente mais tarde.</p > `;
    }
});

function formatDateTime(dateString) {
    const date = new Date(dateString);
    return date.toLocaleString('pt-BR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit'
    });
}