package com.example.whatever;

import com.example.whatever.models.Articles;
import com.example.whatever.models.Favorites;
import com.example.whatever.models.Language;
import com.example.whatever.models.Users;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface ApiInterface {

    @GET("Users")
    Call<ArrayList<Users>> getUsersList();

    @POST("Users")
    Call<Users> postUsers(@Body Users users);

    @GET("Articles")
    Call<ArrayList<Articles>> getArticlesList();

    @GET("Articles/{id}")
    Call<Articles> getArticleById(@Path("id") Integer id);

    @POST("Favorites")
    Call<Favorites> postFavorites(@Body Favorites favorites);
}
