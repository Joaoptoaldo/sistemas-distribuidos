import json
import tempfile
import unittest
from pathlib import Path

from classe_dicionario.aluno import Aluno
from classe_dicionario.dicionario import Dicionario


class TestAluno(unittest.TestCase):
    def test_criacao_de_aluno(self):
        aluno = Aluno("Maria", 20, "A123")
        self.assertEqual(aluno.nome, "Maria")
        self.assertEqual(aluno.idade, 20)
        self.assertEqual(aluno.matricula, "A123")

    def test_validacoes_basicas(self):
        with self.assertRaises(ValueError):
            Aluno("", 20, "A123")
        with self.assertRaises(ValueError):
            Aluno("Maria", -1, "A123")
        with self.assertRaises(ValueError):
            Aluno("Maria", 20, "")

    def test_conversao_para_dict(self):
        aluno = Aluno("João", 22, "B456")
        self.assertEqual(
            Dicionario.aluno_para_dict(aluno),
            {"nome": "João", "idade": 22, "matricula": "B456"},
        )

    def test_dict_para_aluno(self):
        aluno = Dicionario.dict_para_aluno({"nome": "João", "idade": 22, "matricula": "B456"})
        self.assertIsInstance(aluno, Aluno)
        self.assertEqual(aluno.nome, "João")
        self.assertEqual(aluno.idade, 22)
        self.assertEqual(aluno.matricula, "B456")

    def test_serializacao_json(self):
        aluno = Aluno("José da Silva", 23, "X-123")
        with tempfile.TemporaryDirectory() as tmpdir:
            caminho = Path(tmpdir) / "alunos.json"
            Dicionario.alunos_para_json([aluno], caminho)
            dados = Dicionario.json_para_dict(caminho)
            self.assertEqual(dados[0]["nome"], "José da Silva")
            self.assertEqual(dados[0]["matricula"], "X-123")

    def test_exportacao_e_importacao_csv(self):
        alunos = [
            Aluno("Maria", 20, "A123"),
            Aluno("João", 22, "B456"),
            Aluno("Ana", 21, "C789"),
        ]
        with tempfile.TemporaryDirectory() as tmpdir:
            caminho_csv = Path(tmpdir) / "alunos.csv"
            Dicionario.alunos_para_csv(alunos, caminho_csv)
            recuperados = Dicionario.csv_para_alunos(caminho_csv)
            self.assertEqual(len(recuperados), 3)
            self.assertEqual(recuperados[1].nome, "João")

    def test_preservacao_de_acentos(self):
        aluno = Aluno("Francisco da Costa Júnior", 30, "Ç123")
        with tempfile.TemporaryDirectory() as tmpdir:
            caminho = Path(tmpdir) / "acentos.json"
            Dicionario.alunos_para_json([aluno], caminho)
            dados = Dicionario.json_para_dict(caminho)
            self.assertEqual(dados[0]["nome"], "Francisco da Costa Júnior")

    def test_arquivo_inexistente(self):
        with tempfile.TemporaryDirectory() as tmpdir:
            caminho = Path(tmpdir) / "faltante.json"
            with self.assertRaises(FileNotFoundError):
                Dicionario.json_para_dict(caminho)

    def test_json_invalido(self):
        with tempfile.TemporaryDirectory() as tmpdir:
            caminho = Path(tmpdir) / "invalido.json"
            caminho.write_text("{not valid json}", encoding="utf-8")
            with self.assertRaises(json.JSONDecodeError):
                Dicionario.json_para_dict(caminho)

    def test_dados_incompativeis(self):
        with self.assertRaises(ValueError):
            Dicionario.dict_para_aluno({"nome": "Maria", "idade": "vinte", "matricula": "A123"})


if __name__ == "__main__":
    unittest.main()
