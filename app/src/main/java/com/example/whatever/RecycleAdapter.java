package com.example.whatever;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.whatever.models.Articles;

import java.util.ArrayList;

public class RecycleAdapter extends RecyclerView.Adapter<RecycleAdapter.ViewHolder> {

    private Context context;
    private ArrayList<Articles> ArticlesArrayList;
    private Intent intent;

    interface OnStateClickListener{
        void onStateClick(Articles state, int position);
    }

    private final OnStateClickListener onClickListener;

    public RecycleAdapter(Context context, ArrayList<Articles> carsArrayList, OnStateClickListener onClickListener){
        this.context = context;
        this.ArticlesArrayList = carsArrayList;
        this.onClickListener = onClickListener;
    }

    @NonNull
    @Override
    public RecycleAdapter.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(context).inflate(R.layout.item_card, parent, false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RecycleAdapter.ViewHolder holder, @SuppressLint("RecyclerView") int position) {
        Articles articles = ArticlesArrayList.get(position);
        holder.ArticlesHeaderTV.setText(articles.getHeader());
        holder.ArticlesDateTV.setText(articles.getDateTimeArticle());
        holder.ArticlesHeaderTV.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
              onClickListener.onStateClick(articles,position);
            }
        });

    }

    @Override
    public int getItemCount() {
        return ArticlesArrayList.size();
    }

    public static class ViewHolder extends RecyclerView.ViewHolder{

        TextView ArticlesHeaderTV;
        TextView ArticlesDateTV;


        ViewHolder(View view) {
            super(view);
            ArticlesHeaderTV = view.findViewById(R.id.articles_header_textView);
            ArticlesDateTV = view.findViewById(R.id.articles_date_textView);

        }

    }
}
