package com.example.whatever.models;

public class Users {

    private int idUser;
    private String loginUser;
    private String passwordUser;
    private String email;
    private  int roleId;
    private  int statusUserId;

    public Users(String loginUser, String passwordUser, String email, int roleId, int statusUserId) {
        this.loginUser = loginUser;
        this.passwordUser = passwordUser;
        this.email = email;
        this.roleId = roleId;
        this.statusUserId = statusUserId;
    }

    public int getIdUser() {
        return idUser;
    }

    public void setIdUser(int idUser) {
        this.idUser = idUser;
    }

    public String getLoginUser() {
        return loginUser;
    }

    public void setLoginUser(String loginUser) {
        this.loginUser = loginUser;
    }

    public String getPasswordUser() {
        return passwordUser;
    }

    public void setPasswordUser(String passwordUser) {
        this.passwordUser = passwordUser;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public int getRoleId() {
        return roleId;
    }

    public void setRoleId(int roleId) {
        this.roleId = roleId;
    }

    public int getStatusUserId() {
        return statusUserId;
    }

    public void setStatusUserId(int statusUserId) {
        this.statusUserId = statusUserId;
    }
}
