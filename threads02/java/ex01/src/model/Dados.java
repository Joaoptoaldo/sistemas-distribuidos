package model;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

/**
 * Classe responsavel por gerar e particionar os dados numericos do exercicio
 */
public class Dados {
    private static final int QTD_TOTAL = 10000; // qtd de numeros para gerar
    private static final int NUM_PARTES = 4;   // qtd de partes para dividir

    /**
     * metodo que gera uma lista com numeros inteiros aleatorios
     * @return uma lista de inteiros com QTD_TOTAL elementos
     */
    public List<Integer> gerarNumeros() {
        List<Integer> numeros = new ArrayList<>();
        Random random = new Random();

        for (int i = 0; i < QTD_TOTAL; i++) {
            numeros.add(random.nextInt(100));
        }

        return numeros;
    }

    /**
     * metodo que divide a lista de numeros em partes iguais (ou quase iguais em caso de resto),
     * garantindo que cada parte seja uma lista independente e que nenhum elemento seja perdido
     * @param lista a lista completa de numeros
     * @return uma lista contendo sublistas independentes de numeros
     */
    public List<List<Integer>> dividirEmPartes(List<Integer> lista) {
        List<List<Integer>> partes = new ArrayList<>();

        if (lista == null || lista.isEmpty()) {
            return partes;
        }

        int total = lista.size();
        int tamanhoBase = total / NUM_PARTES;
        int resto = total % NUM_PARTES;

        int inicio = 0;
        for (int i = 0; i < NUM_PARTES; i++) {
            int tamanhoAtual = tamanhoBase + (i < resto ? 1 : 0);
            int fim = inicio + tamanhoAtual;

            // cria uma nova ArrayList independente a partir da fatia
            List<Integer> parteIndependente = new ArrayList<>(lista.subList(inicio, fim));
            partes.add(parteIndependente);

            inicio = fim;
        }

        return partes;
    }
}

