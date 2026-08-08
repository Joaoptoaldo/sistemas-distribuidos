package view;

import java.util.List;

public class ResultadosView {

    /**
     * mostra o resultado do processamento
     * @param nomesOriginais lista de nomes originais
     * @param partes lista de partes
     * @param resultado lista de resultados
     */
    public void mostrarResultado(List<String> nomesOriginais, List<List<String>> partes, List<String> resultado) {

        System.out.println("----- EXERCÍCIO 2: FILTRO DE DADOS -----");
        
        System.out.println("Total de nomes originais: " + nomesOriginais.size());
        System.out.println("Quantidade de tarefas: " + partes.size());

        for (int i = 0; i < partes.size(); i++) {
            System.out.println("Parte " + (i + 1) + ": " + partes.get(i).size() + " nomes");
        }

        System.out.println("Total processado: " + resultado.size());
        System.out.println("--------------------------------------------------");

        System.out.println("Exemplos após limpeza:\n");

        resultado.stream().limit(10).forEach(nome -> System.out.println(" - " + nome));

        System.out.println("--------------------------------------------------");
    }
}
