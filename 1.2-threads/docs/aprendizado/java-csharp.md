# Aprendizado comparativo: Java x C#

Mapeamento dos conceitos de Java para C#, com os motivos de cada decisão de design.

---

## Matriz comparativa

| Conceito | Java | C# | Por que C# faz assim? |
| :--- | :--- | :--- | :--- |
| Ponto de entrada | `public static void main(String[] args)` | Top-level statements em `Program.cs` | C# 9+ elimina o boilerplate de classe. |
| Namespace | `package model;` | `namespace ex01.Model;` | File-scoped namespaces (C# 10+) reduzem indentação. |
| Lista | `List<T>` / `ArrayList` | `List<T>` / `new()` | `List<int>` não faz boxing, ao contrário de `List<Integer>`. |
| Tamanho | `lista.size()` | `lista.Count` | Propriedades usam PascalCase, sem parênteses. |
| Sublista | `subList(inicio, fim)` | `GetRange(inicio, quantidade)` | Segundo parâmetro é count, não índice final — menos bugs. |
| Aleatório | `new Random().nextInt(100)` | `Random.Shared.Next(1, 101)` | `Random.Shared` é thread-safe e evita múltiplas instâncias. |
| Tarefa | `Callable<T>` | `Task<T>` | TAP — `Task<T>` representa o trabalho e o resultado futuro. |
| Despacho | `ExecutorService` | `Task.Run(() => ...)` | O runtime gerencia o ThreadPool. Pool manual não é necessário. |
| Espera | `future.get()` (bloqueante) | `await Task.WhenAll(tasks)` | `await` libera a thread enquanto aguarda. |
| ID de thread | `Thread.currentThread().getName()` | `Environment.CurrentManagedThreadId` | ID numérico único gerenciado pelo runtime. |
| Sanitização | `trim().toUpperCase(Locale.ROOT)` | `Trim().ToUpperInvariant()` | `ToUpperInvariant()` ignora configurações de idioma do SO. |
| Coleções | `stream().mapToInt(...).sum()` | LINQ — `.Sum()`, `.Select()`, `.SelectMany()` | LINQ é integrado à linguagem via métodos de extensão. |
| Leitura de arquivo | `Files.readAllLines(path)` | `File.ReadAllLines(caminho)` | `System.IO.File` centraliza utilitários de I/O. |

---

## Concorrência: Java vs C#

### Java — ExecutorService + Callable\<T\> + Future\<T\>

1. Criar pool explícito com `ExecutorService`.
2. Implementar `Callable<T>`.
3. Submeter para obter `Future<T>`.
4. Bloquear a thread principal com `future.get()`.

### C# — TAP (Task-based Asynchronous Pattern)

1. `Task.Run` despacha para o ThreadPool do .NET.
2. `Task<T>` encapsula o resultado futuro.
3. `await Task.WhenAll(tarefas)` espera sem bloquear e retorna `T[]`.

---

## Thread vs Task vs Task\<T\>

### Thread — fio bruto do sistema operacional

Representa um fio de execução real gerenciado pelo SO. Cada instância aloca recursos próprios (~1 MB de pilha no Windows).

```csharp
Thread t = new Thread(() =>
{
    Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId}");
});
t.Start();
t.Join(); // bloqueia o chamador até terminar
```

Responsabilidades manuais: ciclo de vida (`Start`, `Join`), tratamento de exceções, não há retorno de valor nativo.

Usar quando: trabalho de longa duração que ocuparia o pool indefinidamente, ou quando é necessário configurar `IsBackground`, `Priority` ou `ApartmentState`.

---

### Task — abstração sobre o ThreadPool

Trabalho despachado para o ThreadPool do .NET — threads pré-alocadas e reutilizadas pelo runtime.

```csharp
Task tarefa = Task.Run(() => Processar());
await tarefa; // espera sem bloquear
```

O runtime decide qual thread executa, reutiliza a thread após o término e propaga exceções no `await`.

Limitação: `Task.Run` é para trabalho CPU-bound. Para I/O-bound (arquivo, rede, banco), usar APIs async nativas (`File.ReadAllLinesAsync`, `HttpClient.GetAsync`) — elas não bloqueiam nenhuma thread durante a espera.

---

### Task\<T\> — Task com retorno de valor

Equivalente ao `Future<T>` do Java, integrado nativamente ao `async/await`.

```csharp
Task<int> tarefa = Task.Run(() => sublista.Sum());
int resultado = await tarefa;
```

| Aspecto | Java: Future\<T\> | C#: Task\<T\> |
| :--- | :--- | :--- |
| Espera | `future.get()` — bloqueia | `await` — libera a thread |
| Composição | loop manual | `Task.WhenAll`, `Task.WhenAny` |
| Exceção | `ExecutionException` | propagada diretamente no `await` |
| Cancelamento | `interrupt` (não nativo) | `CancellationToken` integrado |
| I/O | modelo separado | mesmo modelo de CPU e I/O |

---

### Resumo de uso

| Cenário | Usar |
| :--- | :--- |
| Trabalho CPU-bound sem retorno | `Task.Run(() => ...)` |
| Trabalho CPU-bound com retorno | `Task<T>` via `Task.Run` |
| I/O-bound | `await` com API async nativa |
| Loop de serviço de longa duração | `Thread` dedicada |
| Aguardar todas as tarefas | `await Task.WhenAll(...)` |
| Aguardar a primeira que terminar | `await Task.WhenAny(...)` |
