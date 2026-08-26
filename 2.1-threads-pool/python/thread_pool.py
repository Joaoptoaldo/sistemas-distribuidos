from concurrent.futures import ThreadPoolExecutor
import random


def processar_lista(id_tarefa, tamanho_lista):
    # Cada tarefa possui sua própria lista isolada

    # 1. Popular a lista de forma aleatória
    lista = [random.randint(1, 100) for _ in range(tamanho_lista)]

    # 2. Exibir lista original
    print(f"Tarefa {id_tarefa} (Original): {lista}")

    # 3. Ordenar
    lista.sort()

    # 4. Exibir lista ordenada
    print(f"Tarefa {id_tarefa} (Ordenada): {lista}")


# Quantidade máxima de trabalhadores
N = 5

# Tamanho de cada lista
tamanho_lista = 20

# Cria o ThreadPool com no máximo 5 threads
with ThreadPoolExecutor(max_workers=N) as pool:

    # Cria e envia as tarefas para o pool
    for i in range(1, N + 1):
        pool.submit(processar_lista, i, tamanho_lista)
