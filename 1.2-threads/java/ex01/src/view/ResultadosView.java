package view;

import java.util.List;

public class ResultadosView {

    /**
     * metodo que mostra o resultado do calculo da soma total
     * @param totalNumeros total de numeros gerados
     * @param partes partes em que a lista foi dividida
     * @param somaTotal soma total dos numeros
     * @param somaSequencial soma sequencial dos numeros
     */
    public void mostrarResultado(int totalNumeros, List<List<Integer>> partes, int somaTotal, int somaSequencial) {
        System.out.println("---- EXERCÍCIO 1 - SOMA SUBLISTAS ----");
        System.out.println("Total de números gerados: " + totalNumeros);
        System.out.println("Quantidade de partes: " + partes.size());
        
        for (int i = 0; i < partes.size(); i++) {
            System.out.println("Tarefa " + (i + 1) + ": " + partes.get(i).size() + " elementos");
        }

        System.out.println("-------------------------------------");
        System.out.println("Soma Concorrente (Callable): " + somaTotal);
        System.out.println("Soma Sequencial (Validação): " + somaSequencial);
        
        boolean valida = (somaTotal == somaSequencial);
        System.out.println("Status da validação: " + (valida ? "somas equivalentes" : "valores divergentes"));
    }
}

