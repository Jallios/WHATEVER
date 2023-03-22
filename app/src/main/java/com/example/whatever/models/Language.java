package com.example.whatever.models;

public class Language {
    private int idLanguageProgramming;
    private String languageProgramming1;

    public Language(int idLanguageProgramming, String languageProgramming1) {
        this.idLanguageProgramming = idLanguageProgramming;
        this.languageProgramming1 = languageProgramming1;
    }

    public int getIdLanguageProgramming() {
        return idLanguageProgramming;
    }

    public void setIdLanguageProgramming(int idLanguageProgramming) {
        this.idLanguageProgramming = idLanguageProgramming;
    }

    public String getLanguageProgramming1() {
        return languageProgramming1;
    }

    public void setLanguageProgramming1(String languageProgramming1) {
        this.languageProgramming1 = languageProgramming1;
    }
}
