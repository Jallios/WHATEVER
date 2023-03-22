package com.example.whatever;

import android.content.Intent;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.example.whatever.models.Articles;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class FirstFragment extends Fragment {

    RecyclerView recyclerView;
    ApiInterface apiInterface;
    ProgressBar progressBar;

    Intent intent;

    RecycleAdapter.OnStateClickListener stateClickListener;

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);



        Call<ArrayList<Articles>> getArticlesList = apiInterface.getArticlesList();

        stateClickListener = new RecycleAdapter.OnStateClickListener() {
            @Override
            public void onStateClick(Articles state, int position) {

                intent = new Intent(getContext(), ArticlesActivity.class);
                intent.putExtra("item",state.getIdArticle());
                intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
                getContext().startActivity(intent);
            }
        };

        getArticlesList.enqueue(new Callback<ArrayList<Articles>>() {
            @Override
            public void onResponse(Call<ArrayList<Articles>> call, Response<ArrayList<Articles>> response) {
                if(response.isSuccessful()){
                    recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));
                    recyclerView.setHasFixedSize(true);
                    ArrayList<Articles> articles = response.body();
                    RecycleAdapter adapter = new RecycleAdapter(getContext(),articles,stateClickListener);
                    recyclerView.setAdapter(adapter);
                }
                else{
                    Toast.makeText(getContext(),"Error",Toast.LENGTH_LONG).show();
                    progressBar.setVisibility(View.VISIBLE);
                }
            }

            @Override
            public void onFailure(Call<ArrayList<Articles>> call, Throwable t) {
                Toast.makeText(getContext(),t.getMessage(),Toast.LENGTH_LONG).show();
            }
        });

    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View root = inflater.inflate(R.layout.fragment_first, container, false);
        recyclerView = (RecyclerView) root.findViewById(R.id.recycle_view_first);
        progressBar = root.findViewById(R.id.progress_first);
        apiInterface = RequestBuilder.buildRequest();
        return root;
    }
}