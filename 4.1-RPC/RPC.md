# RPC - Remote Procedure Call

RPC é um protocolo de comunicação entre processos que permite que um programa execute uma função ou procedimento em outro programa, possivelmente em outro computador na rede, como se fosse uma chamada local. Ele abstrai a complexidade da comunicação entre sistemas distribuídos, permitindo que os desenvolvedores chamem funções remotas de maneira semelhante às chamadas de funções locais.

## Características
- Funciona como uma chamada de função local, mas executa o código em um servidor remoto.
- Permite a comunicação entre diferentes linguagens de programação.
- Pode ser usado para criar aplicações distribuídas e microserviços.
- Oferece uma interface simples para o desenvolvimento de aplicações distribuídas.

## Tipos de RPC
- **Síncrono**: O cliente envia uma solicitação e espera pela resposta do servidor antes de continuar a execução.
- **Assíncrono**: O cliente envia uma solicitação e continua a execução sem esperar pela resposta do servidor. A resposta é recebida posteriormente, geralmente através de um callback ou evento.
- **Baseado em Mensagens**: O cliente envia uma mensagem para o servidor, que processa a solicitação e envia uma resposta de volta. A comunicação é feita através de mensagens, e não de chamadas de função diretas.
- **Baseado em Streaming**: Permite a transmissão contínua de dados entre o cliente e o servidor, permitindo que ambos enviem e recebam dados em tempo real.
- **Baseado em Eventos**: O cliente se inscreve em eventos no servidor e recebe notificações quando esses eventos ocorrem, permitindo uma comunicação reativa entre os sistemas.