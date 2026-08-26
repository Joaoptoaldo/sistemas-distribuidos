package thread;

import java.util.List;

public class ExibicaoNumerosTask implements Runnable {
    private final List<Integer> numeros;

    public ExibicaoNumerosTask(List<Integer> numeros) {
        this.numeros = numeros;
    }

    @Override
    public void run() {
        long threadId = Thread.currentThread().getId();
        System.out.println("\n--- Thread " + threadId + " | NÚMEROS ---");

        for (Integer numero : numeros) {
            System.out.println("[Thread " + threadId + "] Número: " + numero);
        }
    }
}
