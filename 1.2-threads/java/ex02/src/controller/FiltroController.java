package controller;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.Future;

import thread.FiltroTask;

/**
 * Classe responsavel por controlar o processamento das tarefas
 */
public class FiltroController {
   private static final int NUM_THREADS = 2;

    /**
     * método que processa as tarefas e retorna o resultado final
     * @param partes lista de partes
     * @return lista de resultados
     */
    public List<String> processar(List<List<String>> partes) {

        if (partes == null || partes.size() != NUM_THREADS) {
            throw new IllegalArgumentException("É necessário fornecer exatamente 2 partes!");
        }

        ExecutorService executor = Executors.newFixedThreadPool(NUM_THREADS);

        List<Future<List<String>>> futuros = new ArrayList<>();

        try {

            for (List<String> parte : partes) {

                FiltroTask tarefa = new FiltroTask(parte);

                futuros.add(executor.submit(tarefa));
            }

            List<String> resultadoFinal = new ArrayList<>();

            for (Future<List<String>> futuro : futuros) {
                resultadoFinal.addAll(futuro.get());
            }

            return resultadoFinal;

        } catch (InterruptedException e) {

            Thread.currentThread().interrupt();

            throw new RuntimeException("O processamento foi interrompido", e);

        } catch (ExecutionException e) {
            throw new RuntimeException("Erro durante o processamento dos dados", e.getCause());

        } finally {
            executor.shutdown();
        }
    }
}
