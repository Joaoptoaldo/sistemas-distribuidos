from __future__ import annotations

import csv
import json
from pathlib import Path
from typing import Any, Iterable, Sequence

try:
    from .aluno import Aluno
except ImportError:  # pragma: no cover - suporte para execução direta do módulo
    from aluno import Aluno


class Dicionario:
    @staticmethod
    def aluno_para_dict(aluno: Aluno) -> dict[str, Any]:
        """Converte um objeto Aluno para um dicionário."""
        if not isinstance(aluno, Aluno):
            raise TypeError("O valor informado não é um objeto Aluno.")
        return aluno.to_dict()

    @staticmethod
    def dict_para_aluno(dicionario: dict[str, Any]) -> Aluno:
        """Converte um dicionário em um objeto Aluno."""
        return Aluno.from_dict(dicionario)

    @staticmethod
    def dict_para_json(dicionario: object, nome_arquivo: str | Path) -> Path:
        """Salva um dicionário em um arquivo JSON."""
        caminho = Path(nome_arquivo)
        with caminho.open("w", encoding="utf-8", newline="") as arquivo:
            json.dump(dicionario, arquivo, indent=4, ensure_ascii=False)
        return caminho

    @staticmethod
    def json_para_dict(nome_arquivo: str | Path) -> list[dict[str, Any]] | dict[str, Any]:
        """Lê um arquivo JSON e retorna o conteúdo em estrutura Python."""
        caminho = Path(nome_arquivo)
        if not caminho.exists():
            raise FileNotFoundError(f"Arquivo não encontrado: {caminho}")

        with caminho.open("r", encoding="utf-8") as arquivo:
            return json.load(arquivo)

    @staticmethod
    def alunos_para_json(alunos: Sequence[Aluno], nome_arquivo: str | Path) -> Path:
        """Serializa uma lista de Aluno em JSON."""
        payload = [Dicionario.aluno_para_dict(aluno) for aluno in alunos]
        return Dicionario.dict_para_json(payload, nome_arquivo)

    @staticmethod
    def json_para_alunos(nome_arquivo: str | Path) -> list[Aluno]:
        """Desserializa um JSON em uma lista de objetos Aluno."""
        dados = Dicionario.json_para_dict(nome_arquivo)
        if not isinstance(dados, list):
            raise ValueError("O conteúdo JSON deve representar uma lista de alunos.")

        return [Dicionario.dict_para_aluno(item) for item in dados]

    @staticmethod
    def alunos_para_csv(alunos: Iterable[Aluno], nome_arquivo: str | Path) -> Path:
        """Exporta uma sequência de Aluno para CSV."""
        caminho = Path(nome_arquivo)
        with caminho.open("w", encoding="utf-8", newline="") as arquivo:
            escritor = csv.DictWriter(arquivo, fieldnames=["nome", "idade", "matricula"])
            escritor.writeheader()
            for aluno in alunos:
                escritor.writerow(Dicionario.aluno_para_dict(aluno))
        return caminho

    @staticmethod
    def csv_para_alunos(nome_arquivo: str | Path) -> list[Aluno]:
        """Lê um CSV e retorna uma lista de objetos Aluno."""
        caminho = Path(nome_arquivo)
        if not caminho.exists():
            raise FileNotFoundError(f"Arquivo CSV não encontrado: {caminho}")

        with caminho.open("r", encoding="utf-8", newline="") as arquivo:
            leitor = csv.DictReader(arquivo)
            if leitor.fieldnames is None:
                raise ValueError("O CSV está vazio ou sem cabeçalho válido.")

            campos_esperados = {"nome", "idade", "matricula"}
            if set(leitor.fieldnames) != campos_esperados:
                raise ValueError("O CSV não possui as colunas esperadas: nome, idade, matricula.")

            return [Dicionario.dict_para_aluno(linha) for linha in leitor]

    @staticmethod
    def json_csv(nome_arquivo_json: str | Path, nome_arquivo_csv: str | Path) -> Path:
        """Converte um JSON em CSV."""
        alunos = Dicionario.json_para_alunos(nome_arquivo_json)
        return Dicionario.alunos_para_csv(alunos, nome_arquivo_csv)

    @staticmethod
    def csv_json(nome_arquivo_csv: str | Path, nome_arquivo_json: str | Path) -> Path:
        """Converte um CSV em JSON."""
        alunos = Dicionario.csv_para_alunos(nome_arquivo_csv)
        return Dicionario.alunos_para_json(alunos, nome_arquivo_json)
