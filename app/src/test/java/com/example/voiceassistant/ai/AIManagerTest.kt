package com.example.voiceassistant.ai

import org.junit.Assert.assertEquals
import org.junit.Assert.assertTrue
import org.junit.Test

class AIManagerTest {

    private val aiManager = AIManager()

    @Test
    fun testOpenYouTube() {
        val intent = aiManager.recognizeIntent("Open YouTube")
        assertTrue(intent is Intent.OpenYouTube)

        val intent2 = aiManager.recognizeIntent("please open video")
        assertTrue(intent2 is Intent.OpenYouTube)
    }

    @Test
    fun testPlayMusic() {
        val intent = aiManager.recognizeIntent("Play some music")
        assertTrue(intent is Intent.PlayMusic)

        val intent2 = aiManager.recognizeIntent("play a song")
        assertTrue(intent2 is Intent.PlayMusic)
    }

    @Test
    fun testWebSearch() {
        val intent = aiManager.recognizeIntent("Search for laptops")
        assertTrue(intent is Intent.WebSearch)
        assertEquals("laptops", (intent as Intent.WebSearch).query)

        val intent2 = aiManager.recognizeIntent("search android studio")
        assertTrue(intent2 is Intent.WebSearch)
        assertEquals("android studio", (intent2 as Intent.WebSearch).query)
    }

    @Test
    fun testAskAI() {
        val intent = aiManager.recognizeIntent("What is AI?")
        assertTrue(intent is Intent.AskAI)

        val intent2 = aiManager.recognizeIntent("Who is the president?")
        assertTrue(intent2 is Intent.AskAI)
    }

    @Test
    fun testUnknown() {
        val intent = aiManager.recognizeIntent("Hello world")
        assertTrue(intent is Intent.Unknown)
    }
}
