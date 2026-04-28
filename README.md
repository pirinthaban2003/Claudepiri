# Voice Assistant Android App

A production-ready Android voice assistant built with Kotlin, following MVVM architecture.

## Features

- **Continuous Listening**: Stays active to hear your commands.
- **Wake Word Detection**: Responds to "Hey Assistant" or "Hi Assistant".
- **Intent Recognition**: Uses regex-based matching to understand user requests.
- **Action Execution**:
  - Open YouTube
  - Perform Web Search
  - Play Music
  - General AI Query fallback
- **Text-to-Speech (TTS)**: Responds with natural-sounding voice.
- **Chat History**: Persists conversations using Room database.
- **Modern UI**: Dark themed, chat-style interface with animations.

## Folder Structure

```
app/src/main/java/com/example/voiceassistant/
├── actions/      # Logic for performing system actions (Intents)
├── ai/           # Intent recognition and AI processing logic
├── data/         # Room Database and Data entities for chat history
├── ui/           # Activities, Adapters and UI related classes
├── viewmodel/    # AssistantViewModel for managing UI state and logic
└── voice/        # Speech-to-Text and Text-to-Speech management
```

## Setup & Run Instructions

1. **Install Android Studio**: Ensure you have the latest version of Android Studio.
2. **Open Project**: Clone or copy this project and open the root folder in Android Studio.
3. **Sync Gradle**: Let Android Studio download dependencies and sync the project.
4. **Permissions**: The app requires `RECORD_AUDIO` and `INTERNET` permissions. It will prompt for them on launch.
5. **Run**: Connect a real device or start an emulator with microphone support and click **Run**.

## Sample Commands

- "Hey Assistant" (Wait for response) -> "Search for latest space news"
- "Hey Assistant" -> "Open YouTube"
- "Hey Assistant" -> "Play music"
- "Hey Assistant" -> "What is Kotlin?"

## Troubleshooting

- **Microphone not working**: Ensure you are using a real device or an emulator that has "Virtual Microphone" enabled in settings. Check permissions in Android settings.
- **No speech output**: Ensure the device volume is up and the Text-to-Speech engine is installed (usually default on most Android devices).
- **Network Errors**: Some speech recognition features require an internet connection if offline models are not available.

## Advanced Integration (Optional)

To integrate with a real AI like Groq or Ollama:
- Update `AIManager.kt` to call a Retrofit service instead of using basic regex.
- Use the provided `ChatMessage` flow to update the UI with AI-generated responses.
