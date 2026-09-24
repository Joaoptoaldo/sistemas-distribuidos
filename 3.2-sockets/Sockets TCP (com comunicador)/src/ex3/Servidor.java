
package ex3;

import java.net.ServerSocket;
import java.net.Socket;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class Servidor {

    ServerSocket servidor;
    // Pool dinâmico que reutiliza threads ociosas
    private final ExecutorService pool = Executors.newCachedThreadPool();

    public Servidor() {
        criaServerSocket();
        aguardaClientes();
    }

    private void criaServerSocket() {
        try {
            servidor = new ServerSocket(1234);
            System.out.println("Server escutando na porta 1234");
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private void aguardaClientes() {
        while (true) {
            try {
                /* Bloqueia esperando por uma conexão através do accept()
                 Ao receber a conexão, ele receberá como retorno uma referência do Socket do cliente */
                Socket cliente = servidor.accept();
                System.out.println("Recebi uma conexão de um cliente: " + cliente.getInetAddress());

                // Delega a execução das tarefas de envio e recepção para o Pool de Threads
                pool.execute(new ThreadRecebedora(cliente));
                pool.execute(new ThreadEnviadora(cliente));

            } catch (Exception e) {
                e.printStackTrace();
            }
        }
    }

    public static void main(String[] args) {
        Servidor s = new Servidor();
    }
}


