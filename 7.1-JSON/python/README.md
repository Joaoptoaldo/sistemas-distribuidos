# Serialização em Python

## Sobre o projeto

Este projeto é um exercício didático para trabalhar com objetos Python, dicionários, JSON e CSV. A ideia principal é representar alunos em uma classe e converter esses dados entre diferentes formatos, mantendo a estrutura simples e legível.

O código atual demonstra:

- criação de objetos `Aluno`;
- conversão para dicionários;
- serialização em JSON;
- leitura e escrita em CSV;
- validação básica dos dados do modelo.

## Tecnologias utilizadas

- Python 3
- Biblioteca padrão do Python:
  - `json`
  - `csv`
  - `pathlib`
  - `dataclasses`
  - `typing`


## Organização do código

### `classe_dicionario/aluno.py`

Contém a representação do modelo `Aluno` usando `@dataclass`. A classe define os campos `nome`, `idade` e `matricula` e valida regras básicas, como:

- nome não vazio;
- idade inteira e maior ou igual a zero;
- matrícula preenchida e sem espaços.

Também possui conversões entre objeto e dicionário.

### `classe_dicionario/dicionario.py`

Centraliza as operações de conversão e persistência. Neste módulo estão as funções que:

- transformam `Aluno` em dicionário;
- transformam dicionário em `Aluno`;
- salvam dados em JSON;
- leem JSON;
- exportam para CSV;
- importam CSV;
- convertem entre JSON e CSV.

Esse arquivo é o ponto principal de serialização do projeto.

### `classe_dicionario/principal.py`

Executa o fluxo principal da aplicação. Ele verifica se os arquivos JSON e CSV existem e carrega os alunos. Se o JSON não existir, cria uma lista de exemplo e salva os dados.

### `json.py`

Arquivo de demonstração que gera alunos, serializa em JSON e recupera os dados para impressão no terminal.

### `principal.py`

Ponto de entrada do projeto, responsável por chamar a função principal do pacote `classe_dicionario`.

### `classe_dicionario/prompt_exemplo.json`

Exemplo de estrutura JSON organizada para representar um contexto de instrução e pergunta. Ele serve como referência para engenharia de prompt, sem implementar integração real com IA.

### `tests/`

Contém testes automatizados com `unittest` para validar:

- criação do objeto `Aluno`;
- conversão para dicionário;
- serialização JSON;
- importação/exportação de CSV;
- validação de entradas inválidas;
- tratamento de arquivos inexistentes e JSON inválido.

## Como executar

A partir da raiz:

```bash
cd python
python principal.py
```

Para executar a demonstração de serialização:

```bash
cd python
python json.py
```

Para rodar os testes:

```bash
cd python
python -m unittest discover -s tests -v
```

## Relação com Engenharia de Prompt JSON e RAG

O projeto organiza os dados em estruturas JSON padronizadas. Isso é importante porque facilita a leitura, a validação e a troca de informações entre partes diferentes do sistema.

Em um cenário de Engenharia de Prompt, um JSON pode separar:

- contexto do sistema;
- instruções da tarefa;
- schema do dado;
- entrada do usuário;
- resposta esperada.

Essa padronização ajuda a preparar dados para integrações futuras com LLMs e também serve como base para arquiteturas de RAG, em que o sistema recupera informações relevantes antes de gerar uma resposta.

O arquivo `classe_dicionario/prompt_exemplo.json` mostra esse padrão de organização. Exemplo:

```json
{
  "contexto_sistema": {
    "role": "Assistente de consultas acadêmicas",
    "tarefa": "Analisar informações dos alunos"
  },
  "esquema_banco_dados": {
    "tabela": "alunos",
    "colunas": [
      {"nome": "nome", "tipo": "VARCHAR(100)"},
      {"nome": "idade", "tipo": "INTEGER"},
      {"nome": "matricula", "tipo": "VARCHAR(20)"}
    ]
  },
  "entrada_usuario": {
    "pergunta": "Quais alunos possuem mais de 21 anos?"
  }
}
```

## Observações finais

A implementação atual segue um fluxo simples:

1. cria-se um objeto `Aluno`;
2. converte-se para dicionário;
3. salva em JSON ou exporta para CSV;
4. lê novamente os dados e recupera objetos válidos.

Esse padrão é suficiente para entender os fundamentos de serialização e reaproveitamento de dados em Python.
