package model;

import java.util.ArrayList;
import java.util.List;

public class Dados {
    private final List<Integer> numeros;
    private final List<String> nomes;

    public Dados() {
        this.numeros = new ArrayList<>();
        this.nomes = new ArrayList<>();
    }

    public List<Integer> getNumeros() {
        return numeros;
    }

    public List<String> getNomes() {
        return nomes;
    }
}
