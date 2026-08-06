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
