package com.example.voiceassistant.viewmodel

import android.app.Application
import android.util.Log
import androidx.lifecycle.AndroidViewModel
import androidx.lifecycle.viewModelScope
import com.example.voiceassistant.R
import com.example.voiceassistant.actions.ActionManager
import com.example.voiceassistant.ai.AIManager
import com.example.voiceassistant.ai.AiApiService
import com.example.voiceassistant.ai.ChatRequest
import com.example.voiceassistant.ai.Intent
import com.example.voiceassistant.ai.Message
import com.example.voiceassistant.data.AppDatabase
import com.example.voiceassistant.data.ChatMessage
import com.example.voiceassistant.voice.VoiceManager
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.launch

class AssistantViewModel(application: Application) : AndroidViewModel(application) {

    private val db = AppDatabase.getDatabase(application)
    private val chatDao = db.chatDao()
    private val actionManager = ActionManager(application)
    private val aiManager = AIManager()
    private val aiApiService = AiApiService.create()

    val messages = chatDao.getAllMessages()

    private val _isListening = MutableStateFlow(false)
    val isListening: StateFlow<Boolean> = _isListening

    private val _currentTranscription = MutableStateFlow("")
    val currentTranscription: StateFlow<String> = _currentTranscription

    private var isWaitingForCommand = false

    private val voiceManager = VoiceManager(
        application,
        onResult = { text ->
            handleSpeechResult(text)
        },
        onError = { error ->
            _isListening.value = false
            _currentTranscription.value = "Error: $error"
            isWaitingForCommand = false
        },
        onPartialResult = { partial ->
            _currentTranscription.value = partial
        }
    )

    fun toggleListening() {
        if (_isListening.value) {
            voiceManager.stopListening()
            _isListening.value = false
            isWaitingForCommand = false
        } else {
            _currentTranscription.value = ""
            voiceManager.startListening(continuous = true)
            _isListening.value = true
            isWaitingForCommand = false
        }
    }

    private fun handleSpeechResult(text: String) {
        val lowerText = text.lowercase()
        if (!isWaitingForCommand) {
            if (lowerText.contains("hey assistant") || lowerText.contains("hi assistant")) {
                isWaitingForCommand = true
                _currentTranscription.value = "Listening for command..."
                voiceManager.speak("How can I help you?")
            }
        } else {
            isWaitingForCommand = false
            processUserUtterance(text)
        }
    }

    private fun processUserUtterance(text: String) {
        viewModelScope.launch {
            chatDao.insertMessage(ChatMessage(text = text, isUser = true))

            val intent = aiManager.recognizeIntent(text)
            handleIntent(intent, text)
        }
    }

    private fun handleIntent(intent: Intent, originalText: String) {
        viewModelScope.launch {
            var response: String = ""

            when (intent) {
                is Intent.OpenYouTube -> {
                    actionManager.openYouTube()
                    response = getApplication<Application>().getString(R.string.resp_youtube)
                }
                is Intent.PlayMusic -> {
                    response = if (actionManager.playMusic()) {
                        getApplication<Application>().getString(R.string.resp_music)
                    } else {
                        "No music player found."
                    }
                }
                is Intent.WebSearch -> {
                    actionManager.performWebSearch(intent.query)
                    response = getApplication<Application>().getString(R.string.resp_search, intent.query)
                }
                is Intent.AskAI -> {
                    response = try {
                        val aiResponse = aiApiService.getChatCompletion(
                            apiKey = "Bearer YOUR_GROQ_API_KEY", // Placeholder for user
                            request = ChatRequest(
                                messages = listOf(Message(role = "user", content = originalText))
                            )
                        )
                        aiResponse.choices.firstOrNull()?.message?.content ?: "No response from AI."
                    } catch (e: Exception) {
                        Log.e("AssistantViewModel", "AI Error", e)
                        getApplication<Application>().getString(R.string.resp_ai_fallback, originalText)
                    }
                }
                is Intent.Unknown -> {
                    response = getApplication<Application>().getString(R.string.resp_unknown)
                }
            }

            chatDao.insertMessage(ChatMessage(text = response, isUser = false))
            voiceManager.speak(response)
        }
    }

    override fun onCleared() {
        super.onCleared()
        voiceManager.shutdown()
    }
}
