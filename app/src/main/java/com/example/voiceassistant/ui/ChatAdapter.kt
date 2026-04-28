package com.example.voiceassistant.ui

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.voiceassistant.data.ChatMessage
import com.example.voiceassistant.databinding.ItemChatMessageBinding
import android.view.View
import android.graphics.Color

class ChatAdapter : ListAdapter<ChatMessage, ChatAdapter.ChatViewHolder>(DiffCallback()) {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ChatViewHolder {
        val binding = ItemChatMessageBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ChatViewHolder(binding)
    }

    override fun onBindViewHolder(holder: ChatViewHolder, position: Int) {
        holder.bind(getItem(position))
    }

    class ChatViewHolder(private val binding: ItemChatMessageBinding) : RecyclerView.ViewHolder(binding.root) {
        fun bind(message: ChatMessage) {
            binding.messageText.text = message.text
            if (message.isUser) {
                binding.root.layoutDirection = View.LAYOUT_DIRECTION_RTL
                binding.messageCard.setCardBackgroundColor(Color.parseColor("#3700B3"))
            } else {
                binding.root.layoutDirection = View.LAYOUT_DIRECTION_LTR
                binding.messageCard.setCardBackgroundColor(Color.parseColor("#333333"))
            }
        }
    }

    class DiffCallback : DiffUtil.ItemCallback<ChatMessage>() {
        override fun areItemsTheSame(oldItem: ChatMessage, newItem: ChatMessage) = oldItem.id == newItem.id
        override fun areContentsTheSame(oldItem: ChatMessage, newItem: ChatMessage) = oldItem == newItem
    }
}
