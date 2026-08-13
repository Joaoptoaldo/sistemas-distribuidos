# Metodologia 5S aplicada à migração Java -> C#

Descreve como o programa 5S foi usado para organizar o aprendizado e a migração dos exercícios de threads de Java para C#.

---

## Mapeamento dos Sensos

- S1 - Seiri (Utilização): extrair requisitos
- S2 - Seiton (Organização): mapear conceitos e estrutura
- S3 - Seiso (Limpeza): C# idiomático sem vícios do Java
- S4 - Seiketsu (Padronização): critérios de validação
- S5 - Shitsuke (Disciplina): ciclo de refinamento

---

### S1 - Seiri

Identificar apenas o comportamento funcional do Java, sem tradução sintática linha a linha.

- Ex 1: "Gerar 10.000 inteiros, dividir em 4 blocos, somar concorrentemente, validar com soma total."
- Ex 2: "Carregar 5.000 strings, dividir em 2 blocos, sanitizar em paralelo, agrupar."

---

### S2 - Seiton

Organizar a estrutura de pastas e mapear abstrações Java -> C#.

```
threads02/
├── java/       # código de referência
├── csharp/     # implementações C#
└── docs/       # documentação 5S e evidências
```

A pasta `thread/` do Java foi mapeada para `Tasks/`, refletindo o padrão TAP.

---

### S3 - Seiso

Escrever C# idiomático, sem transportar vícios sintáticos do Java.

- `ExecutorService` + `Callable<T>` -> `Task.Run` + `Task<T>`
- `subList(inicio, fim)` -> `GetRange(inicio, quantidade)`
- `future.get()` -> `await Task.WhenAll()`
- File-scoped namespaces, target-typed new, interpolação de strings

---

### S4 - Seiketsu

A implementação C# é válida apenas quando:

- Resultado computacional idêntico ao baseline Java.
- Threads distintas comprovadas via `Environment.CurrentManagedThreadId`.
- Timestamps de início e fim registrados por thread (`DateTime.Now`).
- Duração medida com `Stopwatch`.
- Sobreposição de intervalos analisada — não apenas IDs diferentes.

---

### S5 - Shitsuke

Manter o ciclo de experimentação

```
S1: Extrair requisitos
      |
S2: Organizar conceitos e estrutura MVC
      |
S3: Implementar em C# idiomático
      |
S4: Validar com evidências empíricas objetivas
      |
S5: Registrar erros e correções -> próximo exercício
```
