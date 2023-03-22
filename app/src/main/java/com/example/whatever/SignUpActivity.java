package com.example.whatever;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.example.whatever.models.Users;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SignUpActivity extends AppCompatActivity {

    Button SignUp_BT_signUp;
    EditText Login_ET_signUp,Password_ET_signUp,Email_ET_signUp;
    ApiInterface apiInterface;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_up);

        SignUp_BT_signUp = findViewById(R.id.SignUp_BT_signUp);
        Login_ET_signUp = findViewById(R.id.Login_ET_signUp);
        Password_ET_signUp = findViewById(R.id.Password_ET_signUp);
        Email_ET_signUp = findViewById(R.id.Email_ET_signUp);

        apiInterface = RequestBuilder.buildRequest();

        SignUp_BT_signUp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Users user = new Users(Login_ET_signUp.getText().toString().trim(),Password_ET_signUp.getText().toString().trim(),Email_ET_signUp.getText().toString().trim(),1,1);
                Call<Users> postUsers = apiInterface.postUsers(user);

                postUsers.enqueue(new Callback<Users>() {
                    @Override
                    public void onResponse(Call<Users> call, Response<Users> response) {
                        if (response.isSuccessful())
                        {
                            finish();
                            Intent intent = new Intent(getApplicationContext(),SignInActivity.class);
                            startActivity(intent);
                        }
                    }

                    @Override
                    public void onFailure(Call<Users> call, Throwable t) {

                    }
                });
            }
        });
    }
}