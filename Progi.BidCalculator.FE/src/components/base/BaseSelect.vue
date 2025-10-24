<template>
  <div class="w-full">
    <label v-if="label" :for="id" class="block text-sm font-medium text-gray-700 mb-2">
      {{ label }}
      <span v-if="required" class="text-red-500">*</span>
    </label>

    <select
      :id="id"
      :value="modelValue"
      :disabled="disabled"
      class="block w-full rounded-lg border-gray-300 shadow-sm px-4 py-3 transition-all duration-200 focus:border-primary-500 focus:ring-2 focus:ring-primary-500 focus:ring-opacity-20 disabled:bg-gray-100 disabled:cursor-not-allowed"
      :class="error ? 'border-red-500 focus:border-red-500 focus:ring-red-500' : ''"
      @change="handleChange"
    >
      <option v-if="placeholder" value="" disabled>{{ placeholder }}</option>
      <option v-for="option in options" :key="option.value" :value="option.value">
        {{ option.label }}
      </option>
    </select>

    <p v-if="error" class="mt-2 text-sm text-red-600 animate-fade-in">
      {{ errorMessage }}
    </p>

    <p v-else-if="hint" class="mt-2 text-sm text-gray-500">
      {{ hint }}
    </p>
  </div>
</template>

<script setup lang="ts">
export interface SelectOption {
  label: string
  value: string | number
}

interface Props {
  id?: string
  modelValue: string | number
  label?: string
  placeholder?: string
  options: SelectOption[]
  disabled?: boolean
  required?: boolean
  error?: boolean
  errorMessage?: string
  hint?: string
}

withDefaults(defineProps<Props>(), {
  id: undefined,
  label: '',
  placeholder: '',
  disabled: false,
  required: false,
  error: false,
  errorMessage: '',
  hint: '',
})

const emit = defineEmits<{
  'update:modelValue': [value: string | number]
}>()

function handleChange(event: Event) {
  const target = event.target as HTMLSelectElement
  emit('update:modelValue', target.value)
}
</script>


