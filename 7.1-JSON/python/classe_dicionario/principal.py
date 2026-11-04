from __future__ import annotations

from pathlib import Path

try:
    from .aluno import Aluno
    from .dicionario import Dicionario
except ImportError:  # pragma: no cover - suporte para execução direta
    from aluno import Aluno
    from dicionario import Dicionario


def main() -> None:
    pasta_atual = Path(__file__).resolve().parent
    arquivo_json = pasta_atual / "arquivo.json"
    arquivo_csv = pasta_atual / "arquivo.csv"

    if arquivo_json.exists():
        alunos = Dicionario.json_para_alunos(arquivo_json)
    else:
        alunos = [
            Aluno("Maria", 20, "A123"),
            Aluno("João", 22, "B456"),
            Aluno("Ana", 21, "C789"),
        ]
        Dicionario.alunos_para_json(alunos, arquivo_json)

    if arquivo_csv.exists():
        print("Arquivo CSV encontrado e validado.")

    print("Lista de alunos carregados:")
    for aluno in alunos:
        print(f"- {aluno}")


if __name__ == "__main__":
    main()