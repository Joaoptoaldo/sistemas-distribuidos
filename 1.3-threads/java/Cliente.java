import java.io.*;
import java.net.*;

public class Cliente {
    private static final String HOST = "10.104.12.13";
    private static final int PORTA = 12345;

    public static void main(String[] args) {
        try {
            System.out.println("Tentando se conectar ao servidor...");
        
            // Conecta ao servidor no host e porta definidos
            Socket socket = new Socket(HOST, PORTA);
            
            // Fluxos para comunicação com o servidor
            PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
            BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            
            // Fluxo para ler o que o usuário digita no terminal
            BufferedReader leitorTeclado = new BufferedReader(new InputStreamReader(System.getenv("STDOUT") != null ? System.in : System.in));
            System.out.println("Conectado com sucesso! Digite suas mensagens (ou 'sair' para encerrar):");
            String textoUsuario;

            // Lê as mensagens do terminal
            while ((textoUsuario = leitorTeclado.readLine()) != null) {
                if ("sair".equalsIgnoreCase(textoUsuario.trim())) {
                    break;
                }

                // Envia para o servidor
                out.println(textoUsuario);
                
                // Recebe e mostra a resposta do servidor
                String resposta = in.readLine();
                System.out.println("Servidor respondeu: " + resposta);
            }

        } catch (UnknownHostException e) {
            System.err.println("Não foi possível encontrar o host: " + HOST);
        } catch (IOException e) {
            System.err.println("Erro de I/O na conexão com o servidor: " + e.getMessage());
        }
        
        System.out.println("Conexão encerrada.");
    }
}
