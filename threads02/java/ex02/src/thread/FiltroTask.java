package thread;

import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.concurrent.Callable;

public class FiltroTask implements Callable<List<String>> {
    private final List<String> nomes;

    /**
     * construtor da classe
     * @param nomes lista de nomes a serem processados
     */
    public FiltroTask(List<String> nomes) {
        if (nomes == null) {
            throw new IllegalArgumentException("A lista não pode ser nula!");
        }

        this.nomes = nomes;
    }

    /**
     * processa a lista de nomes, removendo espaços e convertendo para maiúsculas.
     * @return lista de nomes processados
     */
    @Override
    public List<String> call() {
        System.out.println("[" + Thread.currentThread().getName() + "] Processando parte de " + nomes.size() + " elementos.");

        List<String> nomesLimpos = new ArrayList<>(nomes.size());

        for (String nome : nomes) {

            if (nome == null) {
                nomesLimpos.add(null);
                continue;
            }

            String nomeLimpo = nome.trim().toUpperCase(Locale.ROOT);

            nomesLimpos.add(nomeLimpo);
        }

        return nomesLimpos;
    }
}
