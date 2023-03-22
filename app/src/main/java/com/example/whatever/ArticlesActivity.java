package com.example.whatever;

import androidx.appcompat.app.AppCompatActivity;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.example.whatever.models.Articles;
import com.example.whatever.models.Favorites;
import com.example.whatever.models.Language;
import com.google.android.material.floatingactionbutton.FloatingActionButton;
import com.squareup.picasso.Picasso;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ArticlesActivity extends AppCompatActivity {

    TextView header, text, language, user;

    FloatingActionButton floatingActionButton;
    ApiInterface apiInterface;
    Integer id;

    SharedPreferences preferences;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_articles);

        preferences = getSharedPreferences("1", MODE_PRIVATE);

        header = findViewById(R.id.articles_header_textView);
        text = findViewById(R.id.articles_text_textView);
        language = findViewById(R.id.articles_languageProgrammingId_textView);
        user = findViewById(R.id.articles_userId_textView);
        floatingActionButton = findViewById(R.id.fab);

        apiInterface = RequestBuilder.buildRequest();
        id = getIntent().getIntExtra("item",0);

        Toast.makeText(getApplicationContext(),preferences.getInt("uid",0),Toast.LENGTH_LONG).show();

        Call<Articles> getArticles = apiInterface.getArticleById(id);

        getArticles.enqueue(new Callback<Articles>() {
            @Override
            public void onResponse(Call<Articles> call, Response<Articles> response) {
                if(response.isSuccessful()) {
                    Articles articles = response.body();

                    header.setText(articles.getHeader());
                    text.setText(articles.getText());
                    language.setText(articles.getLanguage().getLanguageProgramming1());
                    user.setText(articles.getUser().getLoginUser());

                }else
                {
                    Toast.makeText(getApplicationContext(), "Error", Toast.LENGTH_LONG).show();
                }
            }
            @Override
            public void onFailure(Call<Articles> call, Throwable t) {

            }
        });
        floatingActionButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
            }
        });
    }
}