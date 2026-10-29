# RMI - Remote Method Invocation

- Invocação de Método Remoto: mecanismo que permite a um objeto em uma máquina virtual (JVM) chamar métodos de um objeto em outra máquina virtual
- evolução do modelo RPC (Remote Procedure Call), migrando do paradigma procedural para o orientado a objetos
- características principais:
- transparência: chamadas remotas possuem a mesma sintaxe de chamadas locais
- nativo da plataforma Java (pacote java.rmi)
- utiliza passagem de parâmetros por cópia (serialização) para objetos comuns ou por referência para objetos remotos
- arquitetura interna:
- Stub: pedaço de código no lado do cliente que finge ser o objeto real e intermedia a chamada
- Skeleton (ou camada de transporte correspondente): componente no lado do servidor que recebe a requisição do Stub e executa o método real
- Registro (RMI Registry): serviço de nomes (servidor de diretório) onde os objetos remotos são associados a um nome (bind) para serem localizados pelos clientes (lookup)
- dependências técnicas:
- protocolo TCP/IP
- serialização de objetos (conversão de objetos em fluxo de bytes para envio na rede)
- gerenciamento automático através de um coletor de lixo distribuído (DGC)



### Projeto de exemplo disponível em: [Desafio da Telefonia](https://github.com/Joaoptoaldo/metodos-pesquisa-ordenacao/tree/main/03-hash/ProblemaSessaoTelefonia)