package controller;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;
import thread.SomaTask;

/**
 * Classe responsavel por coordenar a execucao das tarefas de soma
 * e agregar os resultados parciais.
 */
public class SomaController {

    /**
     * recebe as sublistas de numeros, cria e executa uma SomaTask para cada sublista,
     * aguarda todas terminarem e retorna a soma total
     * @param partes lista contendo sublistas de numeros
     * @return a soma total de todos os numeros
     */
    public int calcularSomaTotal(List<List<Integer>> partes) {

        if (partes == null || partes.isEmpty()) {
            return 0;
        }

        int somaTotal = 0;
    
        ExecutorService executor = Executors.newFixedThreadPool(partes.size());

        try {
            List<Future<Integer>> futuros = new ArrayList<>();

            // dispara uma SomaTask para cada sublista
            for (List<Integer> parte : partes) {
                futuros.add(executor.submit(new SomaTask(parte)));
            }

            // coleta cada soma parcial
            for (Future<Integer> futuro : futuros) {
                try {
                    somaTotal += futuro.get();

                } catch (InterruptedException e) {
                    Thread.currentThread().interrupt();
                    System.err.println("Thread interrompida durante a execução da tarefa: " + e.getMessage());

                } catch (ExecutionException e) {
                    System.err.println("Erro na execução da tarefa de soma: " + e.getCause());
                }
            }
        } finally {
            executor.shutdown();
        }

        return somaTotal;
    }
}

