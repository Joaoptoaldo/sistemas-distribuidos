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


---

1) Para que usar Sistemas Distribuidos?

    - Compartilhamento de recursos: Sistemas distribuídos permitem que múltiplos computadores compartilhem recursos, como arquivos, impressoras e bancos de dados, aumentando a eficiência e reduzindo custos.

---

2) Como Sistemas Distribuídos funcionam?

    - Comunicação entre nós: Sistemas distribuídos funcionam através da comunicação entre diferentes nós (computadores) na rede. Cada nó pode executar tarefas independentes e colaborar com outros nós para alcançar objetivos comuns.

        - Protocolo TCP/IP: A comunicação entre os nós geralmente é realizada usando protocolos de rede, como TCP/IP, que garantem a entrega confiável de dados.
        - Ele entrega:
            - Lexemas
            - Sintaxe
            - Semântica
        
    - Serialização de dados: Para que os dados possam ser transmitidos entre os nós, eles precisam ser serializados (em um formato binário, por exemplo) em um formato que possa ser enviado pela rede e depois desserializados no nó receptor.

---

3) Categorias de Comunicação:

    - Broadcast: envia mensagens para todos os nós da rede, permitindo que todos recebam a mesma informação simultaneamente.

    - Multicast: envia mensagens para um grupo específico de nós, permitindo que apenas os nós interessados recebam a informação.

    - Unicast: envia mensagens de um nó para outro nó específico, permitindo comunicação ponto a ponto.

    - Bloqueante: o nó que envia a mensagem espera por uma resposta antes de continuar a execução, garantindo que a comunicação seja síncrona.
        - Escrever = Reader = Sender
        - Ler = Writer = Receiver

---

4) Arquitetura de Clientes e Servidores:

    - Cliente: é o nó que solicita serviços ou recursos de outro nó (o servidor). Ele envia requisições e aguarda respostas.

    - Servidor: é o nó que fornece serviços ou recursos para os clientes. Ele processa as requisições recebidas e envia respostas de volta aos clientes.

    - Comunicação: a comunicação entre clientes e servidores geralmente segue um modelo de requisição-resposta, onde o cliente envia uma requisição e o servidor responde com os dados solicitados.

---

09/09/2026

Sockets

- Todo sistema computacional tem sockets

- Surgimento em 1980

- Foco na camada de transporte: TCP (síncrona) e UDP (assíncrona)

- Baseado na arquitetura cliente-servidor

- Classes, interfaces, métodos, atributos para comunicação entre máquinas de forma EXPLÍCITA - JAVA

    - O programador deve tratar tudo: conexão (endereço e portas lógicas), objetos para ler e escrever no meio (socket), tratar sincronismo (thread)

- Principais funcionalidades

    - Classe Socket

    - Método bind (endereço de uma máquina IP+porta com um socket)

    - Método listen (socket pode ficar escutando pra 'sempre' - thread)

    - Método accept (bloqueia ou garante que o servidor responda uma requisição)

    - Método connect (inicia a conexão)

    - Método read/INPUT (ler os dados que estão no socket -> recebendo)

    - Método write/OUTPUT (escrever algum dado no socket -> enviando)

    - Método close (fechar a conexão)