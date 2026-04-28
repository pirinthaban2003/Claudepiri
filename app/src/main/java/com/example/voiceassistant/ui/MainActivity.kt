package com.example.voiceassistant.ui

import android.Manifest
import android.content.pm.PackageManager
import android.os.Bundle
import android.view.View
import android.view.animation.AnimationUtils
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.voiceassistant.R
import com.example.voiceassistant.databinding.ActivityMainBinding
import com.example.voiceassistant.viewmodel.AssistantViewModel
import kotlinx.coroutines.launch

class MainActivity : AppCompatActivity() {

    private lateinit var binding: ActivityMainBinding
    private val viewModel: AssistantViewModel by viewModels()
    private lateinit var chatAdapter: ChatAdapter

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        setupRecyclerView()
        setupObservers()
        setupListeners()

        checkPermissions()
    }

    private fun setupRecyclerView() {
        chatAdapter = ChatAdapter()
        binding.chatRecyclerView.apply {
            adapter = chatAdapter
            layoutManager = LinearLayoutManager(this@MainActivity).apply {
                stackFromEnd = true
            }
        }
    }

    private fun setupObservers() {
        lifecycleScope.launch {
            viewModel.messages.collect { messages ->
                chatAdapter.submitList(messages)
                if (messages.isNotEmpty()) {
                    binding.chatRecyclerView.smoothScrollToPosition(messages.size - 1)
                }
            }
        }

        lifecycleScope.launch {
            viewModel.isListening.collect { isListening ->
                if (isListening) {
                    binding.micButton.setImageResource(android.R.drawable.ic_media_pause)
                    binding.statusText.text = getString(R.string.hint_listening)
                    startMicAnimation()
                    binding.waveformView.visibility = View.VISIBLE
                } else {
                    binding.micButton.setImageResource(android.R.drawable.ic_btn_speak_now)
                    binding.statusText.text = getString(R.string.hint_idle)
                    stopMicAnimation()
                    binding.waveformView.visibility = View.GONE
                }
            }
        }

        lifecycleScope.launch {
            viewModel.currentTranscription.collect { transcription ->
                if (transcription.isNotEmpty()) {
                    binding.statusText.text = transcription
                }
            }
        }
    }

    private fun startMicAnimation() {
        val pulse = AnimationUtils.loadAnimation(this, R.anim.pulse)
        binding.micButton.startAnimation(pulse)
    }

    private fun stopMicAnimation() {
        binding.micButton.clearAnimation()
    }

    private fun setupListeners() {
        binding.micButton.setOnClickListener {
            if (hasRecordPermission()) {
                viewModel.toggleListening()
            } else {
                requestRecordPermission()
            }
        }
    }

    private fun hasRecordPermission() = ContextCompat.checkSelfPermission(
        this, Manifest.permission.RECORD_AUDIO
    ) == PackageManager.PERMISSION_GRANTED

    private fun requestRecordPermission() {
        ActivityCompat.requestPermissions(
            this, arrayOf(Manifest.permission.RECORD_AUDIO), 100
        )
    }

    private fun checkPermissions() {
        if (!hasRecordPermission()) {
            requestRecordPermission()
        }
    }
}
