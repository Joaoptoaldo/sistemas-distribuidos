package thread;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

import model.Dados;

public class LeituraNumerosTask implements Runnable {
    private final String caminhoArquivo;
    private final Dados dados;

    public LeituraNumerosTask(String caminhoArquivo, Dados dados) {
        this.caminhoArquivo = caminhoArquivo;
        this.dados = dados;
    }

    @Override
    public void run() {

        try {
            for (String linha : Files.readAllLines(Path.of(caminhoArquivo))) {

                if (!linha.isBlank()) {
                    int numero = Integer.parseInt(linha.trim());

                    dados.adicionarNumero(numero);
                }
            }

            System.out.println(
                    "[Thread " + Thread.currentThread().getId()
                            + "] Arquivo " + caminhoArquivo
                            + " carregado."
            );

        } catch (IOException | NumberFormatException e) {

            System.out.println(
                    "[Thread " + Thread.currentThread().getId()
                            + "] Erro ao ler " + caminhoArquivo
                            + ": " + e.getMessage()
            );
        }
    }
}
