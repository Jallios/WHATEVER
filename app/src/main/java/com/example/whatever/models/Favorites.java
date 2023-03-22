package com.example.whatever.models;

public class Favorites {

    private int idFavorites;
    private int userId;
    private int articleId;

    private Users user;
    private Articles articles;

    public Favorites(int idFavorites, int userId, int articleId, Users user, Articles articles) {
        this.idFavorites = idFavorites;
        this.userId = userId;
        this.articleId = articleId;
        this.user = user;
        this.articles = articles;
    }

    public Favorites(int userId, int articleId) {
        this.userId = userId;
        this.articleId = articleId;
    }

    public int getIdFavorites() {
        return idFavorites;
    }

    public void setIdFavorites(int idFavorites) {
        this.idFavorites = idFavorites;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public int getArticleId() {
        return articleId;
    }

    public void setArticleId(int articleId) {
        this.articleId = articleId;
    }

    public Users getUser() {
        return user;
    }

    public void setUser(Users user) {
        this.user = user;
    }

    public Articles getArticles() {
        return articles;
    }

    public void setArticles(Articles articles) {
        this.articles = articles;
    }
}
