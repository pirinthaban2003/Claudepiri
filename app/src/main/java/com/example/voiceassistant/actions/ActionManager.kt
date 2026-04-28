package com.example.voiceassistant.actions

import android.content.Context
import android.content.Intent
import android.net.Uri
import android.provider.MediaStore
import android.widget.Toast

class ActionManager(private val context: Context) {

    fun openYouTube(): Boolean {
        return try {
            val intent = Intent(Intent.ACTION_VIEW, Uri.parse("https://www.youtube.com"))
            intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK)
            context.startActivity(intent)
            true
        } catch (e: Exception) {
            openBrowser("https://www.youtube.com")
            true
        }
    }

    fun performWebSearch(query: String) {
        try {
            val intent = Intent(Intent.ACTION_WEB_SEARCH)
            intent.putExtra("query", query)
            intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK)
            context.startActivity(intent)
        } catch (e: Exception) {
            openBrowser("https://www.google.com/search?q=$query")
        }
    }

    fun playMusic(): Boolean {
        return try {
            val intent = Intent(MediaStore.INTENT_ACTION_MUSIC_PLAYER)
            intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK)
            context.startActivity(intent)
            true
        } catch (e: Exception) {
            false
        }
    }

    fun openBrowser(url: String) {
        val intent = Intent(Intent.ACTION_VIEW, Uri.parse(url))
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK)
        context.startActivity(intent)
    }
}
