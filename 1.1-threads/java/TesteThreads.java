class MinhaTarefa implements Runnable { //permite o uso de memória compartilhada
    int quantidade;

    public MinhaTarefa(int quantidade) {
        this.quantidade = quantidade;
    }


    @Override
    public void run() {
        //aqui está o código a ser concomitado
        Thread t = Thread.currentThread();
        for (int i = 0; i < this.quantidade; i++) {
            System.out.println("Executando a thread: " + t.getName() + " | ID: " + t.getId() + " | Contador: " + i);
        }
    }
}


public class TesteThreads {
    public static void main(String[] args) {
        Thread t1 = new Thread(new MinhaTarefa(100), "Tarefa-1");
        Thread t2 = new Thread(new MinhaTarefa(50), "Tarefa-2");

        t1.start();
        t2.start();
    }
}