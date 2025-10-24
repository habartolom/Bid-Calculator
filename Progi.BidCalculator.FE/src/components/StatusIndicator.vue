<template>
  <div class="flex items-center space-x-2">
    <div
      class="w-2 h-2 rounded-full"
      :class="isHealthy ? 'bg-green-500 animate-pulse' : 'bg-red-500'"
    />
    <span class="text-xs font-medium" :class="isHealthy ? 'text-green-700' : 'text-red-700'">
      {{ isHealthy ? 'Backend Connected' : 'Backend Disconnected' }}
    </span>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  isHealthy: boolean
}>()
</script>

