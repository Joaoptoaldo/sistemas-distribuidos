from __future__ import annotations

from dataclasses import dataclass
from typing import Any


@dataclass(frozen=True)
class Aluno:
    nome: str
    idade: int
    matricula: str

    def __post_init__(self) -> None:
        nome_limpo = self.nome.strip() if isinstance(self.nome, str) else ""
        matricula_limpa = self.matricula.strip() if isinstance(self.matricula, str) else ""

        if not nome_limpo:
            raise ValueError("O nome do aluno não pode estar vazio.")

        if not isinstance(self.idade, int):
            raise ValueError("A idade deve ser um número inteiro.")

        if self.idade < 0:
            raise ValueError("A idade não pode ser negativa.")

        if not matricula_limpa:
            raise ValueError("A matrícula não pode estar vazia.")

        if any(caracter.isspace() for caracter in matricula_limpa):
            raise ValueError("A matrícula não pode conter espaços.")

    def to_dict(self) -> dict[str, Any]:
        return {
            "nome": self.nome,
            "idade": self.idade,
            "matricula": self.matricula,
        }

    @classmethod
    def from_dict(cls, dados: dict[str, Any]) -> "Aluno":
        if not isinstance(dados, dict):
            raise TypeError("Os dados de entrada devem ser um dicionário.")

        try:
            return cls(
                nome=dados["nome"],
                idade=int(dados["idade"]),
                matricula=dados["matricula"],
            )
        except KeyError as exc:
            raise ValueError(f"Dados inválidos para Aluno: faltando {exc.args[0]!r}.") from exc
        except (TypeError, ValueError) as exc:
            raise ValueError("Dados incompatíveis para criação do objeto Aluno.") from exc

