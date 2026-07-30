Conteúdo Programático

    - Unidade 1: Fundamentos em Sistemas Distribuídos
    - Unidade 2: Comunicação em Sistemas Distribuídos
    - Unidade 3: Comunicação em grupo
    - Unidade 4: Sistemas de Arquivos Distribuídos e Memória Compartilhada Distribuída

---

Aula 01 (29/07/2026)

Conceito central: 
- Um sistema distribuído é um conjunto de computadores independentes conectados via rede que trabalham em equipe para parecerem um único sistema unificado aos olhos do usuário final

  - Comunicação:
    - broadcast, multicast, unicast
    - é bloqueante (nativamente): escrever (writer ou sender) e ler (reader ou receiver)
    - respeita ou segue o modelo TCP/IP (aplicação, transporte, interface, rede)
        - endereço IP: servidor, cliente, grupo
        - mácara ou classe de rede e domínio 
        - soquete de rede (socket) é um ponto final de um fluxo de comunicação entre processos através de uma rede de computadores

  - Processamento no Nó: 

    - thread (ou linha de execução) é a menor sequência de instruções de um programa que o sistema operacional consegue gerenciar e enviar para o processador (CPU)
        - finalidade de threads garantir processamento concomitante/paralelo
        - estados de uma thread: execução, finalizado/pronto, espera/aguardando, parado, dormindo, cancelado.
        - ha comandos que garantem SINCRONISMO de processamento.
        - Thread com compartilhamento de memoria/recurso (o processamento bloqueante). Fica de responsabilidade do PROGRAMADOR garantir SINCRONISMO


    - Aplicações em Java (JVM):
        - Thread sem compartilhamento de memória/recurso
        - Thread em Java > processamento concomitante (JVM)
        - Com compartilhamento de memoria Interface Runnable
        - Sem compartilhamento de memória Classe Thread.

        
        Processamento Concomitante vs Paralelo 

            Concomitante: 
            - um sistema gerencia várias tarefas que progridem de maneira intercalada

                - grid computacional
                - fracamente acoplado
                - CPU
            
            Paralelo:
            - um sistema executa várias tarefas ou partes de uma tarefa exatamente no mesmo instante de tempo

                - cluster computacional
                - fortemente acoplado
                - sistemas homogêneos

        

    - Arquitetura:
        - cliente-servidor
        - ponto-a-ponto