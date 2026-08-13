package model;

import java.util.ArrayList;
import java.util.List;

public class Dados {
    private final List<Integer> numeros;

    public Dados() {
        numeros = new ArrayList<>();
    }

    public synchronized void adicionarNumero(int numero) {
        numeros.add(numero);
    }

    public List<Integer> getNumeros() {
        return new ArrayList<>(numeros);
    }

    public int getQuantidade() {
        return numeros.size();
    }
}
