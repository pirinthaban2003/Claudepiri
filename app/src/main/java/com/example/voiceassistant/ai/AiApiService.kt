package com.example.voiceassistant.ai

import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import retrofit2.http.Body
import retrofit2.http.Header
import retrofit2.http.POST

interface AiApiService {
    @POST("v1/chat/completions")
    suspend fun getChatCompletion(
        @Header("Authorization") apiKey: String,
        @Body request: ChatRequest
    ): ChatResponse

    companion object {
        private const val BASE_URL = "https://api.groq.com/openai/"

        fun create(): AiApiService {
            return Retrofit.Builder()
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build()
                .create(AiApiService::class.java)
        }
    }
}

data class ChatRequest(
    val model: String = "mixtral-8x7b-32768",
    val messages: List<Message>
)

data class Message(
    val role: String,
    val content: String
)

data class ChatResponse(
    val choices: List<Choice>
)

data class Choice(
    val message: Message
)
