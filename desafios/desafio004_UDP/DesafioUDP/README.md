# Desafio 004 - UDP (Cadastro e Token)

Cliente e servidor se comunicam via **UDP** para:

1. **Cadastrar** uma pessoa (`nome` + `e-mail`);
2. **Solicitar um token** global com validade de **60 segundos**, manualmente ou a cada 30 segundos.



## Camadas

| Projeto | Responsabilidade |
|---|---|
| **Comunicador** | Classe `Comunicador` (fachada UDP exigida pelo desafio) + `ComunicadorCliente` / `ComunicadorServidor` |
| **Cliente** | Interface e coordenação das operações (cadastro / token) |
| **Servidor** | Escuta na porta, interpreta o protocolo e mantém cadastros e token |

Cliente e servidor usam a **classe `Comunicador`** (nome do enunciado). Ela **delega** para `ComunicadorCliente` (socket efêmero + timeout) ou `ComunicadorServidor` (bind na porta), ambos sob o contrato `IComunicador`.

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download) (ou superior compatível com `net10.0`)
- Windows (o cliente usa Windows Forms)

## Como rodar

Abra o terminal no diretório `DesafioUDP/`.

### 1. Compilar

```bash
dotnet build DesafioUDP.slnx
```

### 2. Subir o servidor (primeiro)

```bash
dotnet run --project Servidor
```

O servidor fica aguardando datagramas na porta **5000**:

```
Servidor UDP aguardando na porta 5000...
```

### 3. Subir o cliente (em outro terminal)

```bash
dotnet run --project Cliente
```

Uma janela de cadastro abre. Use-a para:

- informar **nome** e **e-mail** e clicar em **Cadastrar**;
- clicar em **Solicitar token** (ou aguardar o timer de 30s após o cadastro).

### Observações

- **Ordem importa:** o servidor precisa estar no ar antes do cliente enviar mensagens.
- O cliente aponta para `127.0.0.1:5000` (definido em `Protocolo`).
- Sem servidor, o cliente mostra erro de timeout após ~5 segundos.
- Cada e-mail só pode ser cadastrado uma vez; token é **global** e renovado após **60s**.
- O pedido de token é aceito **apenas** na forma exata `TOKEN` (sem campos extras).
- Resposta `TOKEN|` com valor vazio é rejeitada pelo cliente.

## Protocolo (resumo)

Mensagens em UTF-8, campos separados por `|`.

| Sentido | Mensagem | Resposta |
|---|---|---|
| Cliente → Servidor | `CADASTRO\|nome\|email` | `SUCESSO: ...` ou `ERRO: ...` |
| Cliente → Servidor | `TOKEN` (exato; `TOKEN\|...` é inválido) | `TOKEN\|valor` (vazio é rejeitado) |

Constantes e helpers ficam em `Comunicador/Protocolo.cs`.

## Teste manual

1. Terminal A: `dotnet run --project Servidor`
2. Terminal B: `dotnet run --project Cliente`
3. Cadastre `João` / `joao@exemplo.com` → esperado: `SUCESSO: Pessoa cadastrada.`
4. Solicite o token → esperado: `TOKEN|...`
5. Tente cadastrar o mesmo e-mail de novo → esperado: `ERRO: Pessoa já cadastrada.`
6. Peça o token de novo em seguida → esperado: o **mesmo** valor (ainda válido)
7. Após ~60s, peça de novo → esperado: **novo** token
