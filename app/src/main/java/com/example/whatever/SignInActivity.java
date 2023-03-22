package com.example.whatever;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.whatever.models.Users;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SignInActivity extends AppCompatActivity {

    Button SignUp_BT_signIn,SignIn_BT_signIn;
    EditText Login_ET_signIn,Password_ET_signIn;
    ApiInterface apiInterface;

    SharedPreferences preferences;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_in);

        preferences = getSharedPreferences("1", MODE_PRIVATE);

        SignUp_BT_signIn = findViewById(R.id.SignUp_BT_signIn);
        SignIn_BT_signIn = findViewById(R.id.SignIn_BT_signIn);
        Login_ET_signIn = findViewById(R.id.Login_ET_signIn);
        Password_ET_signIn = findViewById(R.id.Password_ET_signIn);

        apiInterface = RequestBuilder.buildRequest();

        Call<ArrayList<Users>> getUsersList = apiInterface.getUsersList();

        SignIn_BT_signIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                getUsersList.enqueue(new Callback<ArrayList<Users>>() {
                    @Override
                    public void onResponse(Call<ArrayList<Users>> call, Response<ArrayList<Users>> response) {

                        if(response.isSuccessful()){

                            ArrayList<Users> users = response.body();

                            for(int i = 0; i < users.size(); i++)
                            {
                                if (Login_ET_signIn.getText().toString().trim().equals(users.get(i).getLoginUser())) {
                                    if (Password_ET_signIn.getText().toString().trim().equals(users.get(i).getPasswordUser())) {
                                        Intent main = new Intent(getApplicationContext(), MainActivity.class);
                                        preferences.edit().putInt("uid",users.get(i).getIdUser());
                                        startActivity(main);
                                    }
                                }
                            }
                        }
                        else{
                            Toast.makeText(getApplicationContext(),"Error",Toast.LENGTH_LONG).show();

                        }
                    }

                    @Override
                    public void onFailure(Call<ArrayList<Users>> call, Throwable t) {

                        Toast.makeText(getApplicationContext(),t.getMessage(),Toast.LENGTH_LONG).show();
                    }
                });

            }
        });

        SignUp_BT_signIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent a = new Intent(getApplicationContext(), SignUpActivity.class);
                startActivity(a);
            }
        });

    }
}