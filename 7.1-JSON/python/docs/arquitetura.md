# Arquitetura do projeto

## Fluxo

1. Criação do objeto `Aluno`

   O modelo é definido em `classe_dicionario/aluno.py`. Cada aluno possui nome, idade e matrícula, além de validações simples para evitar dados inconsistentes.

2. Conversão para dicionário

   O módulo `classe_dicionario/dicionario.py` converte o objeto `Aluno` para um dicionário com as chaves `nome`, `idade` e `matricula`.

3. Serialização em JSON ou CSV

   Depois da conversão, os dados podem ser salvos em JSON ou exportados para CSV usando as funções do módulo `Dicionario`.

4. Leitura e recuperação

   Os dados podem ser lidos novamente a partir do arquivo e convertidos de volta para objetos `Aluno`, preservando a estrutura do modelo.

## Estrutura funcional

- `Aluno`: representa o dado principal do sistema.
- `Dicionario`: centraliza as operações de conversão e persistência.
- `principal.py`: executa o fluxo principal do projeto.
- `tests/test_serializacao.py`: valida o comportamento esperado dos módulos.

