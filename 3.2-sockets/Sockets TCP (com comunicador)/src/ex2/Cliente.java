package ex2;

import java.io.IOException;
import java.net.Socket;

public class Cliente {

    private final String host;
    private final int porta;

    public Cliente(String host, int porta) {
        this.host = host;
        this.porta = porta;
    }

    public void executar() {
        // try-with-resources garante o fechamento do socket ao finalizar
        try (Socket socket = new Socket(this.host, this.porta)) {
            
            Pessoa p = new Pessoa(22, "João");
            ComunicadorObjetos.enviaObjeto(socket, p);
            
            Pessoa p2 = ComunicadorObjetos.recebeObjeto(socket);
            if (p2 != null) {
                System.out.println("Recebi: " + p2.getNome() + ", " + p2.getIdade());
            }

        } catch (IOException e) {
            System.err.println("Erro ao conectar ou se comunicar com o servidor: " + e.getMessage());
            e.printStackTrace();
        }
    }

    public static void main(String[] args) {
        // Conecta ao servidor local na porta 1234
        Cliente cliente = new Cliente("localhost", 1234);
        cliente.executar();
    }
}