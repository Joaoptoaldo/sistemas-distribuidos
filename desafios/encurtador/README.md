# Encurtador de URLs

Projeto desenvolvido em **C# com .NET 10**, com o objetivo de criar uma aplicação capaz de encurtar URLs, armazenar os links gerados e realizar o redirecionamento para os endereços originais.

A aplicação possui uma interface gráfica desenvolvida com Windows Forms e um servidor HTTP local utilizando ASP.NET Core (Kestrel).

## Funcionalidades

- Validação e normalização de URLs.
- Geração de códigos curtos e aleatórios.
- Armazenamento persistente dos links utilizando SQLite.
- Consulta de links pelo código gerado.
- Listagem e exclusão de links cadastrados.
- Redirecionamento HTTP para a URL original.
- Abertura da URL original diretamente pela interface.

## Tecnologias utilizadas

- C#
- .NET 10
- Windows Forms
- ASP.NET Core / Kestrel
- SQLite

## Execução

Para executar o projeto, utilize:

```bash
dotnet run
```

O servidor HTTP local é iniciado em:

```text
http://localhost:5000
```
