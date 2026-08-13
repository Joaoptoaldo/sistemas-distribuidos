package thread;

import java.util.List;

public class ExibicaoNomesTask implements Runnable {
    private final List<String> nomes;

    public ExibicaoNomesTask(List<String> nomes) {
        this.nomes = nomes;
    }

    @Override
    public void run() {
        long threadId = Thread.currentThread().getId();
        System.out.println("\n--- Thread " + threadId + " | NOMES ---");

        for (String nome : nomes) {
            System.out.println("[Thread " + threadId + "] Nome: " + nome);
        }
    }
}
