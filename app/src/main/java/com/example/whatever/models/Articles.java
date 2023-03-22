package com.example.whatever.models;

public class Articles {

    private int idArticle;
    private String header;
    private String text;
    private String dateTimeArticle;
    private  int languageProgrammingId;
    private  int statusArticleId;
    private  int userId;
    private Language language;
    private Users user;

    public Articles(int idArticle, String header, String text, String dateTimeArticle, int languageProgrammingId, int statusArticleId, int userId, Language language, Users user) {
        this.idArticle = idArticle;
        this.header = header;
        this.text = text;
        this.dateTimeArticle = dateTimeArticle;
        this.languageProgrammingId = languageProgrammingId;
        this.statusArticleId = statusArticleId;
        this.userId = userId;
        this.language = language;
        this.user = user;
    }

    public int getIdArticle() {
        return idArticle;
    }

    public void setIdArticle(int idArticle) {
        this.idArticle = idArticle;
    }

    public String getHeader() {
        return header;
    }

    public void setHeader(String header) {
        this.header = header;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public String getDateTimeArticle() {
        return dateTimeArticle;
    }

    public void setDateTimeArticle(String dateTimeArticle) {
        this.dateTimeArticle = dateTimeArticle;
    }

    public int getLanguageProgrammingId() {
        return languageProgrammingId;
    }

    public void setLanguageProgrammingId(int languageProgrammingId) {
        this.languageProgrammingId = languageProgrammingId;
    }

    public int getStatusArticleId() {
        return statusArticleId;
    }

    public void setStatusArticleId(int statusArticleId) {
        this.statusArticleId = statusArticleId;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public Language getLanguage() {
        return language;
    }

    public void setLanguage(Language language) {
        this.language = language;
    }

    public Users getUser() {
        return user;
    }

    public void setUser(Users user) {
        this.user = user;
    }
}
