# Relatório de Evidências: Exercício 1

## Saída do Console

![print](/threads02/docs/evidencias/terminalEx01.png)

---

## Prova de Concorrência

As quatro threads iniciaram no mesmo milissegundo. A ordem de escrita no console (2, 3, 1, 4) difere da ordem de criação, confirmando escalonamento não-determinístico pelo ThreadPool.

---

## Validação

| Requisito                          | Java         | C#                      | Status |
| :---                               | :---:        | :---:                   | :---:  |
| Total de elementos                 | 10.000       | 10.000                  | PASS   |
| Partes                             | 4            | 4                       | PASS   |
| Elementos por parte                | 2.500        | 2.500                   | PASS   |
| Threads distintas                  | 4            | 4 (IDs: 4, 6, 7, 8)    | PASS   |
| Sobreposição de execução           | Sim          | Sim (mesmo timestamp)   | PASS   |
| Soma concorrente == sequencial     | Sim          | 504.204 == 504.204      | PASS   |
