let projetos = [];
let tarefas = [];

const formProjeto = document.getElementById('formProjeto');
const nomeProjetoInput = document.getElementById('nomeProjeto');

const formTarefa = document.getElementById('formTarefa');
const tituloTarefaInput = document.getElementById('tituloTarefa');
const descricaoTarefaInput = document.getElementById('descricaoTarefa');
const selectProjeto = document.getElementById('selectProjeto');

const listaTarefasDiv = document.getElementById('listaTarefas');

function atualizarSelectProjetos() {
    selectProjeto.innerHTML = '<option value="" disabled selected>Selecione um projeto</option>';
    projetos.forEach(p => {
        const option = document.createElement('option');
        option.value = p.id;
        option.textContent = p.nome;
        selectProjeto.appendChild(option);
    });
}

function renderizarTarefas() {
    listaTarefasDiv.innerHTML = '';

    if (tarefas.length === 0) {
        listaTarefasDiv.innerHTML = '<p class="text-muted">Nenhuma tarefa cadastrada.</p>';
        return;
    }

    tarefas.forEach(tarefa => {
        const projeto = projetos.find(p => p.id === tarefa.projetoId);
        const card = document.createElement('div');
        card.classList.add('card', 'mb-3', 'shadow-sm');

        card.innerHTML = `
            <div class="card-body">
                <h5 class="card-title ${tarefa.concluida ? 'completed' : ''}">${tarefa.titulo}</h5>
                <h6 class="card-subtitle mb-2 text-muted">Projeto: ${projeto ? projeto.nome : 'Desconhecido'}</h6>
                <p class="card-text ${tarefa.concluida ? 'completed' : ''}">${tarefa.descricao || ''}</p>
                <button class="btn btn-sm btn-outline-success btn-complete" ${tarefa.concluida ? 'disabled' : ''}>
                    ${tarefa.concluida ? 'Concluída' : 'Marcar como concluída'}
                </button>
            </div>
        `;

        const btnConcluir = card.querySelector('button');
        btnConcluir.addEventListener('click', () => {
            tarefa.concluida = true;
            renderizarTarefas();
        });

        listaTarefasDiv.appendChild(card);
    });
}

function gerarId() {
    return '_' + Math.random().toString(36).substr(2, 9);
}

formProjeto.addEventListener('submit', (e) => {
    e.preventDefault();
    const nome = nomeProjetoInput.value.trim();
    if (!nome) return alert('Informe o nome do projeto');

    projetos.push({ id: gerarId(), nome });
    nomeProjetoInput.value = '';
    atualizarSelectProjetos();
    alert('Projeto criado com sucesso!');
});

formTarefa.addEventListener('submit', (e) => {
    e.preventDefault();
    const titulo = tituloTarefaInput.value.trim();
    const descricao = descricaoTarefaInput.value.trim();
    const projetoId = selectProjeto.value;

    if (!titulo) return alert('Informe o título da tarefa');
    if (!projetoId) return alert('Selecione um projeto');

    tarefas.push({
        id: gerarId(),
        titulo,
        descricao,
        projetoId,
        concluida: false
    });

    tituloTarefaInput.value = '';
    descricaoTarefaInput.value = '';
    selectProjeto.value = '';

    renderizarTarefas();
    alert('Tarefa criada com sucesso!');
});

atualizarSelectProjetos();
renderizarTarefas();
