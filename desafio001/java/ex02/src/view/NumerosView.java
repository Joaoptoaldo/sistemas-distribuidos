package view;

import model.Dados;

public class NumerosView {
     public void exibirNumeros(Dados dados) {

        System.out.println();
        System.out.println("--- NÚMEROS ---");

        for (Integer numero : dados.getNumeros()) {
            System.out.println(numero);
        }

        System.out.println();
        System.out.println("Quantidade total: " + dados.getQuantidade());
    }
}
