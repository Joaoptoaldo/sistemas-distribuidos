# Relatório de Evidências: Exercício 2

## Saída do Console

![print](/threads02/docs/evidencias/terminalEx02.png)


---

## Prova de Concorrência

Ambas as threads iniciaram no mesmo milissegundo. Tempo total seria ~36 ms em sequência; paralelo consumiu ~18 ms — redução de 50%, consistente com execução simultânea real.

---

## Validação

| Requisito                          | Java                   | C#                          | Status    |
| :---                               | :---:                  | :---:                       | :---:     |
| Total de nomes                     | 5.000                  | 5.000                       | PASS      |
| Blocos                             | 2                      | 2                           | PASS      |
| Elementos por bloco                | 2.500                  | 2.500                       | PASS      |
| Threads distintas                  | Sim                    | Sim (IDs 4 e 6)             | PASS      |
| Sobreposição de execução           | Sim                    | Sim (mesmo timestamp)       | PASS      |
| Sanitização Trim + Uppercase       | trim().toUpperCase()   | Trim().ToUpperInvariant()   | PASS      |
| Agregação sem perda                | 5.000                  | 5.000                       | PASS      |
| Itens null na entrada              | Não tratado            | Ignorados com continue      | CORRIGIDO |
