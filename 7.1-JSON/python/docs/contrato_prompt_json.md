# Contrato de Prompt JSON

## 1. Finalidade

Este documento explica como os dados estruturados do projeto podem se relacionar com o conceito de Prompt JSON apresentado no material de Engenharia de Prompt.

O arquivo `classe_dicionario/prompt_exemplo.json` é apresentado no relatório da refatoração como um exemplo de contrato de entrada para uma futura integração com IA.

**Este contrato é didático:** ele não representa uma chamada real a um LLM e não está conectado a um banco de dados.

## 2. Por que estruturar o prompt?

Ao organizar a solicitação em campos JSON, a aplicação consegue separar diferentes tipos de informação:

| Parte | Finalidade |
|---|---|
| `contexto_sistema` | Define o papel e a tarefa que o modelo deverá realizar. |
| `esquema_banco_dados` | Descreve as tabelas e colunas que podem ser consideradas na tarefa. |
| `entrada_usuario` | Contém a pergunta recebida da pessoa usuária. |
| `formato_saida_esperado` | Descreve o formato que a aplicação espera receber. |

Essa organização facilita a manutenção do contrato e a montagem programática do conteúdo. Ela não garante, isoladamente, que o modelo respeitará as instruções.

## 3. Exemplo de entrada

O exemplo abaixo representa um cenário hipotético de consulta de alunos. O esquema é ilustrativo e não comprova a existência de um banco de dados conectado ao projeto.

```json
{
  "contexto_sistema": {
    "role": "Assistente de consultas acadêmicas",
    "tarefa": "Analisar a pergunta e relacioná-la ao esquema de dados fornecido."
  },
  "esquema_banco_dados": {
    "tabela": "alunos",
    "colunas": [
      {
        "nome": "id",
        "tipo": "INTEGER",
        "chave_primaria": true
      },
      {
        "nome": "nome",
        "tipo": "VARCHAR(100)"
      },
      {
        "nome": "idade",
        "tipo": "INTEGER"
      },
      {
        "nome": "matricula",
        "tipo": "VARCHAR(20)"
      }
    ]
  },
  "entrada_usuario": {
    "pergunta": "Quais são as matrículas dos alunos com mais de 21 anos?"
  },
  "formato_saida_esperado": {
    "status": "sucesso ou erro",
    "justificativa_resumida": "Explicação breve e verificável.",
    "sql_gerado": "Consulta SQL ou null quando não for possível gerar."
  }
}
```

## 4. Exemplo de saída esperada

```json
{
  "status": "sucesso",
  "justificativa_resumida": "A pergunta solicita as matrículas dos alunos cuja idade é maior que 21.",
  "sql_gerado": "SELECT matricula FROM alunos WHERE idade > 21;"
}
```

A aplicação deve validar se a resposta é JSON válido, se contém os campos esperados e se os valores respeitam os tipos e regras definidos. Uma resposta sintaticamente válida ainda pode conter uma consulta incorreta.

## 5. JSON Mode e Structured Outputs

Quando houver integração com uma API de LLM, é preferível utilizar o recurso oficial de saída estruturada oferecido pelo provedor, quando disponível. JSON Mode pode ajudar a obter JSON sintaticamente válido, enquanto mecanismos de Structured Outputs podem restringir a resposta a um esquema definido, conforme as capacidades e limitações da API.

Mesmo com esses mecanismos, a aplicação continua responsável por validar o conteúdo e aplicar as regras de segurança e negócio.

## 6. Delimitação entre instruções e dados

Dados recuperados de arquivos, documentos, banco de dados ou fontes externas devem ser tratados como **dados**, não como instruções que substituem as regras do sistema.

Uma montagem robusta deve manter separados:

1. instruções estáveis da aplicação;
2. esquema autorizado;
3. contexto recuperado;
4. pergunta do usuário;
5. formato de saída definido.

Delimitadores e campos estruturados ajudam a explicitar essas fronteiras, mas não eliminam todos os riscos de interpretação indevida do conteúdo. A aplicação deve tratar o contexto recuperado como entrada não confiável.

## 7. Relação com RAG

RAG combina recuperação de informações com geração por LLM. Em termos simplificados:

1. a aplicação recebe uma pergunta;
2. busca informações relevantes em uma ou mais fontes;
3. seleciona o contexto que pode ajudar a responder;
4. organiza instruções, contexto e pergunta;
5. envia a solicitação ao modelo;
6. valida e apresenta a resposta.

O projeto atual contribui principalmente para a organização e serialização dos dados. Ainda não implementa os passos de recuperação, geração ou validação de consultas SQL.

## 8. Cuidados com Text-to-SQL

Uma consulta SQL gerada por IA deve ser considerada entrada não confiável. Antes de executá-la, uma aplicação real precisaria, no mínimo:

- validar a consulta e sua finalidade;
- permitir apenas operações e objetos autorizados;
- utilizar uma conexão com privilégios mínimos;
- preferir acesso somente de leitura para consultas analíticas;
- limitar duração e volume dos resultados;
- registrar a operação e tratar falhas.

Não execute automaticamente SQL produzido por um modelo apenas porque a resposta veio em JSON ou passou por uma validação sintática.
