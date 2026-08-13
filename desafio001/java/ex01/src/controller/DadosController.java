package controller;

import model.Dados;
import thread.ExibicaoNomesTask;
import thread.ExibicaoNumerosTask;
import thread.LeituraNomesTask;
import thread.LeituraNumerosTask;

public class DadosController {
    public void carregarDados(Dados dados) {

        Thread threadNumeros = new Thread(
            new LeituraNumerosTask(
                "data/numeros.txt",
                dados.getNumeros()
            )
        );

        Thread threadNomes = new Thread(
            new LeituraNomesTask(
                "data/nomes.txt",
                dados.getNomes()
            )
        );

        threadNumeros.start();
        threadNomes.start();

        try {
            threadNumeros.join();
            threadNomes.join();
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            System.out.println("Thread principal interrompida.");
        }
    }

    public void exibirDados(Dados dados) {

        Thread threadNumeros = new Thread(
            new ExibicaoNumerosTask(dados.getNumeros())
        );

        Thread threadNomes = new Thread(
            new ExibicaoNomesTask(dados.getNomes())
        );

        threadNumeros.start();
        threadNomes.start();

        try {
            threadNumeros.join();
            threadNomes.join();
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            System.out.println("Thread principal interrompida.");
        }
    }
}
