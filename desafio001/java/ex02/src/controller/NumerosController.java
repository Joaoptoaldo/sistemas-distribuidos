package controller;

import model.Dados;
import thread.LeituraNumerosTask;

public class NumerosController {
    private final Dados dados;

    public NumerosController() {
        dados = new Dados();
    }

    public void carregarDados(String caminhoNumeros1, String caminhoNumeros2) {

        Thread threadNumeros1 =
                new Thread(
                        new LeituraNumerosTask(caminhoNumeros1, dados)
                );

        Thread threadNumeros2 =
                new Thread(
                        new LeituraNumerosTask(caminhoNumeros2, dados)
                );

        threadNumeros1.start();
        threadNumeros2.start();

        try {
            threadNumeros1.join();
            threadNumeros2.join();

        } catch (InterruptedException e) {

            Thread.currentThread().interrupt();

            System.out.println(
                    "Thread principal interrompida durante o carregamento"
            );
        }
    }

    public Dados getDados() {
        return dados;
    }
}
