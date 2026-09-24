package exemplo2_gerarEmail;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class ServidorTCPBasico {

    public static void main(String[] args) {
        try {
            int portaServidor = 50000;
            ServerSocket servidor = new ServerSocket(portaServidor);

            System.out.println("Servidor ouvindo a porta: " + portaServidor);

            ArrayList<Pessoa> lista = new ArrayList<>();

            // Pool com 4 threads
            ExecutorService pool = Executors.newFixedThreadPool(4);

            while (true) {

                // o método accept() bloqueia a execução até que
                // o servidor receba um pedido de conexão
                Socket cliente = servidor.accept();

                // Envia o cliente para uma thread do pool
                pool.execute(() -> atenderCliente(cliente, lista));
            }

        } catch (IOException e) {
            System.out.println("Erro: " + e.getMessage());
        }
    }

    private static void atenderCliente(Socket cliente, ArrayList<Pessoa> lista) {

        try {
            String enderecoIP = cliente.getInetAddress().getHostAddress();
            int portaCliente = cliente.getPort();

            System.out.println("Cliente conectado no IP: " + enderecoIP);
            System.out.println("Cliente conectado via porta: " + portaCliente);

            ObjectInputStream entrada =
                    new ObjectInputStream(cliente.getInputStream());

            // receber o nome completo do lado do cliente
            String nomePessoa = (String) entrada.readObject();

            // gerar o email a partir do nome completo
            // primeiro.sobrenome@ufn.edu.br
            String vetorNome[] = nomePessoa.split(" ");

            // monta email
            String email = vetorNome[0]
                    + "." + vetorNome[vetorNome.length - 1]
                    + "@ufn.edu.br";

            nomePessoa = nomePessoa.toUpperCase();
            email = email.toLowerCase();

            // criar um objeto Pessoa(nome, email)
            Pessoa p = new Pessoa(nomePessoa, email);

            Boolean encontrado;

            // acessar a lista de forma segura
            synchronized (lista) {
                if (lista.contains(p)) {
                    encontrado = true;
                } else {
                    encontrado = false;
                    lista.add(p);
                }
            }

            // devolver o objeto criado
            ObjectOutputStream saida =
                    new ObjectOutputStream(cliente.getOutputStream());

            saida.flush();

            if (!encontrado) {
                saida.writeObject(p);
            } else {
                saida.writeObject(null);
            }

            saida.close();
            entrada.close();
            cliente.close();

            System.out.println("Clientes na base....");

            synchronized (lista) {
                for (Pessoa pessoa : lista) {
                    System.out.println(pessoa);
                }
            }

        } catch (IOException | ClassNotFoundException e) {
            System.out.println("Erro: " + e.getMessage());
        }
    }
}