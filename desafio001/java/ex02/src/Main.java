import controller.NumerosController;
import view.NumerosView;

public class Main {

    public static void main(String[] args) {

        System.out.println("EXERCÍCIO 2");

        String caminhoNumeros1 = "data/numeros1.txt";
        String caminhoNumeros2 = "data/numeros2.txt";

        NumerosController controller = new NumerosController();


        controller.carregarDados(caminhoNumeros1, caminhoNumeros2);

        NumerosView view = new NumerosView();

        view.exibirNumeros(controller.getDados());

        System.out.println();
        System.out.println("--------------------------------------");
        System.out.println("Processamento finalizado");
    }
}