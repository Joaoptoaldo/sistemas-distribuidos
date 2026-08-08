# Threads

Threads são "mini processos" que compartilham o mesmo espaço de memória dentro de um processo. Elas permitem a execução concorrente de tarefas dentro de um mesmo programa, melhorando a eficiência e a responsividade.

## Tipos

- Com seção critica = memória compartilhada
  - > Sincronismo de processamento é responsabilidade do programador
- Sem seção crítica = memória não compartilhada
  - > Sincronismo de processamento é responsabilidade do sistema operacional

- Sincronized 
    - Usa mecanismos de sincronização para garantir que apenas uma thread acesse a seção crítica por vez, evitando condições de corrida e garantindo a integridade dos dados.

- Lock
    - Permite que uma thread adquira um bloqueio antes de acessar a seção crítica, garantindo exclusão mútua e evitando conflitos entre threads.

## Processo 

- Therad (classe) -> sem memória compartilhada
- Runnable (interface) -> com memória compartilhada
