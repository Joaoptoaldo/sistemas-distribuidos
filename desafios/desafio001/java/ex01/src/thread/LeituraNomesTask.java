package thread;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;

public class LeituraNomesTask implements Runnable {
    private final String caminho;
    private final List<String> nomes;

    public LeituraNomesTask(String caminho, List<String> nomes) {
        this.caminho = caminho;
        this.nomes = nomes;
    }

    @Override
    public void run() {
        try {
            List<String> linhas = Files.readAllLines(Path.of(caminho));

            for (String linha : linhas) {
                nomes.add(linha.trim());
            }

            System.out.println(
                "[Thread " + Thread.currentThread().getId()
                + "] Nomes carregados."
            );

        } catch (IOException e) {
            System.out.println("Erro ao ler nomes: " + e.getMessage());
        }
    }
}
