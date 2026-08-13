using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Servidor {
    public static void Main() {
        TcpListener listener = new TcpListener(IPAddress.Loopback, 12345);
        listener.Start();
        Console.WriteLine("Servidor rodando na porta 12345");

        while (true) {
            TcpClient client = listener.AcceptTcpClient();
            Thread thread = new Thread(() => TrataCliente(client));
            thread.Start();
        }
    }

    static void TrataCliente(TcpClient client) {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        try {
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0) {
                string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Recebido: {data}");
                byte[] response = Encoding.UTF8.GetBytes("Eco: " + data);
                stream.Write(response, 0, response.Length);
            }
        } catch (Exception e) {
            Console.WriteLine($"Erro: {e.Message}");
        } finally {
            client.Close();
        }
    }
}