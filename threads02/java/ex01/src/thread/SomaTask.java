package thread;

import java.util.List;
import java.util.concurrent.Callable; // Callable retorna valor, ao contrario de Runnable

/**
 * tarefa responsavel por calcular a soma dos elementos de uma sublista
 * implementa Callable<Integer> para retornar o resultado parcial para o ExecutorService
 */
public class SomaTask implements Callable<Integer> {

    private final List<Integer> sublista;

    /**
     * construtor da tarefa
     * @param sublista sublista de numeros inteiros a ser somada
     */
    public SomaTask(List<Integer> sublista) {
        this.sublista = sublista;
    }

    /**
     * executa a soma dos elementos da sublista recebida
     * @return soma dos elementos da sublista
     */
    @Override
    public Integer call() {
        if (sublista == null || sublista.isEmpty()) {
            return 0;
        }

        int soma = 0;
        for (Integer numero : sublista) {
            soma += numero;
        }

        return soma;
    }
}
