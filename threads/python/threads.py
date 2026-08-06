import threading
import random


def popular_lista(lista, quantidade):
    for i in range(quantidade):
        lista.append(random.randint(1, 100))
    print(f"Lista populada com {quantidade} elementos na lista: {lista}")


def bolha(lista):
    n = len(lista)
    for i in range(n):
        for j in range(0, n-i-1):
            if lista[j] > lista[j+1]:
                lista[j], lista[j+1] = lista[j+1], lista[j]
    print("\nLista ordenada com o método da bolha")


def pente(lista):
    n = len(lista)
    distancia = n
    fator = 1.3
    houve_troca = True
    
    while distancia > 1 or houve_troca:
        distancia = int(distancia / fator)
        if distancia < 1:
            distancia = 1
        houve_troca = False
    
        for i in range(n - distancia):
            if lista[i] > lista[i + distancia]:
                lista[i], lista[i + distancia] = lista[i + distancia], lista[i]
                houve_troca = True
    
    print("Pente finalizado...")

# def quick_sort(lista):
#     if len(lista) <= 1:
#         return lista
#     else:
#         pivot = lista[len(lista) // 2]
#         left = [x for x in lista if x < pivot]
#         middle = [x for x in lista if x == pivot]
#         right = [x for x in lista if x > pivot]

#         return quick_sort(left) + middle + quick_sort(right)

# if __name__ == "__main__":
#     t1 = threading.Thread(target=tarefa1, args=(100,))
#     t2 = threading.Thread(target=tarefa2, args=(500,))

#     t1.start()
#     t2.start()


lista1 = []
lista2 = []

t1 = threading.Thread(target=popular_lista, args=(lista1, 10000))
t2 = threading.Thread(target=popular_lista, args=(lista2, 5000))
t1.start()
t2.start()

t1.join()
t2.join()



t3 = threading.Thread(target=pente, args=(lista1,))
t4 = threading.Thread(target=bolha, args=(lista2,))
t4.start()
t3.start()