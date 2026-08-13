class MinhaPrimeiraThread extends Thread {

    private int quantidade;

    public MinhaPrimeiraThread(int quantidade) {
        this.quantidade = quantidade;
    }


    @Override
    public void run() {
        for (int i = 0; i < this.quantidade; i++) {
            System.out.println("Executando a thread 1: " + i);
        }
    }
}

class MinhaSegundaThread extends Thread {

    private int quantidade;

    public MinhaSegundaThread(int quantidade) {
        this.quantidade = quantidade;
    }

    @Override
    public void run() {
        for (int i = 0; i < this.quantidade; i++) {
            System.out.println("Executando a thread 2: " + i);
        }
    }
}


public class Threads {
    public static void main(String[] args) {
        MinhaPrimeiraThread t1 = new MinhaPrimeiraThread(100);
        MinhaSegundaThread t2 = new MinhaSegundaThread(500);

        t1.start();
        t2.start();
    }
}