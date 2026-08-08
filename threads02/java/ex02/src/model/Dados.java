package model;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.List;

public class Dados {
    private static final int NUM_PARTES = 2;

    /**
     * método que carrega os nomes do arquivo
     * @param caminho caminho do arquivo
     * @return lista de nomes
     * @throws IOException caso o arquivo não seja encontrado
     */
    public List<String> carregarNomes(String caminho) throws IOException {
        return Files.readAllLines(Path.of(caminho));
    }

    /**
     * método que divide a lista de nomes em partes
     * @param nomes lista de nomes
     * @return lista de listas de nomes
     */
    public List<List<String>> dividirEmPartes(List<String> nomes) {
        if (nomes == null || nomes.isEmpty()) {
            throw new IllegalArgumentException("A lista de nomes não pode ser nula ou vazia");
        }

        int tamanhoBase = nomes.size() / NUM_PARTES;
        int resto = nomes.size() % NUM_PARTES;

        List<List<String>> partes = new ArrayList<>();

        int inicio = 0;

        for (int i = 0; i < NUM_PARTES; i++) {

            int tamanhoParte = tamanhoBase + (i < resto ? 1 : 0);
            int fim = inicio + tamanhoParte;

            partes.add(
                new ArrayList<>(nomes.subList(inicio, fim))
            );

            inicio = fim;
        }

        return partes;
    }
}
