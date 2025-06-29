document.addEventListener('DOMContentLoaded', () => {
    const spaceTypeSelect = document.getElementById('space-type');
    const apiUrl = 'https://localhost:7181/api/v1/Spaces/GetSpaces';
    
    async function loadSpaceTypes() {
        try {
            const response = await fetch(apiUrl);

            if (!response.ok) {
                
                throw new Error(`Erro HTTP! Status: ${response.status} - ${response.statusText}`);
            }
            
            const spaceTypes = await response.json();
            spaceTypeSelect.innerHTML = '<option value="">Selecione o tipo</option>';
        
            spaceTypes.forEach(type => {
                const option = document.createElement('option');
                option.value = type.id;   
                option.textContent = type.name;
                spaceTypeSelect.appendChild(option);
            });

        } catch (error) {
            console.error('Erro ao carregar os tipos de espaço:', error);
            const errorOption = document.createElement('option');
            errorOption.value = '';
            errorOption.textContent = 'Erro ao carregar tipos de espaço';
            errorOption.disabled = true;
            spaceTypeSelect.appendChild(errorOption);
            alert('Ops! Não foi possível carregar os tipos de espaço. Por favor, tente novamente mais tarde ou contate o suporte.');
        }
    }

    loadSpaceTypes();
});