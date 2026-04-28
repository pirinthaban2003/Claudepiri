package com.example.voiceassistant.ai

sealed class Intent {
    object OpenYouTube : Intent()
    data class WebSearch(val query: String) : Intent()
    object PlayMusic : Intent()
    data class AskAI(val question: String) : Intent()
    object Unknown : Intent()
}

class AIManager {
    private val youtubeRegex = Regex(".*open.*(youtube|video).*", RegexOption.IGNORE_CASE)
    private val musicRegex = Regex(".*play.*(music|song|songs).*", RegexOption.IGNORE_CASE)
    private val searchRegex = Regex(".*search (?:for )?(.+)", RegexOption.IGNORE_CASE)
    private val aiRegex = Regex(".*(what|who|tell me|why|how).*", RegexOption.IGNORE_CASE)

    fun recognizeIntent(text: String): Intent {
        val lowerText = text.trim().lowercase()

        if (youtubeRegex.matches(lowerText)) return Intent.OpenYouTube
        if (musicRegex.matches(lowerText)) return Intent.PlayMusic

        val searchMatch = searchRegex.find(lowerText)
        if (searchMatch != null) {
            val query = searchMatch.groupValues[1].trim()
            if (query.isNotEmpty()) {
                return Intent.WebSearch(query)
            }
        }

        if (aiRegex.matches(lowerText)) return Intent.AskAI(text)

        return Intent.Unknown
    }
}
