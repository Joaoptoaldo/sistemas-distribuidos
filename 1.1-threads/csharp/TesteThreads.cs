using System;
using System.Threading;

class Program {
    static void MinhaTarefa(object nome) {
        Thread t = Thread.CurrentThread;
        Console.WriteLine($"Thread {t.ManagedThreadId} | Nome: {t.Name} | Param: {nome}");
    }

    static void Main() {
        Thread t1 = new Thread(MinhaTarefa);
        t1.Name = "Tarefa-1";
        Thread t2 = new Thread(MinhaTarefa);
        t2.Name = "Tarefa-2";

        t1.Start("Parametro A");
        t2.Start("Parametro B");
    }
}