from __future__ import annotations

from classe_dicionario.aluno import Aluno
from classe_dicionario.dicionario import Dicionario


def demonstrar_serializacao() -> None:
    alunos = [
        Aluno("Maria", 20, "A123"),
        Aluno("João", 22, "B456"),
        Aluno("Ana", 21, "C789"),
    ]

    dados = Dicionario.alunos_para_json(alunos, "arquivo_exemplo.json")
    recuperados = Dicionario.json_para_alunos(dados)

    print("Lista de alunos em JSON:")
    print(dados.read_text(encoding="utf-8"))
    print("\nLista recuperada:")
    for aluno in recuperados:
        print(aluno)


if __name__ == "__main__":
    demonstrar_serializacao()
