package thread;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;

public class LeituraNumerosTask implements Runnable {
    private final String caminho;
    private final List<Integer> numeros;

    public LeituraNumerosTask(String caminho, List<Integer> numeros) {
        this.caminho = caminho;
        this.numeros = numeros;
    }

    @Override
    public void run() {
        try {
            List<String> linhas = Files.readAllLines(Path.of(caminho));

            for (String linha : linhas) {
                numeros.add(Integer.parseInt(linha.trim()));
            }

            System.out.println(
                "[Thread " + Thread.currentThread().getId()
                + "] Números carregados."
            );

        } catch (IOException | NumberFormatException e) {
            System.out.println("Erro ao ler números: " + e.getMessage());
        }
    }
}
