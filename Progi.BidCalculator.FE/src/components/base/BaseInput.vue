<template>
  <div class="w-full">
    <label v-if="label" :for="id" class="block text-sm font-medium text-gray-700 mb-2">
      {{ label }}
      <span v-if="required" class="text-red-500">*</span>
    </label>
    
    <div class="relative">
      <div
        v-if="$slots.prefix || prefix"
        class="absolute inset-y-0 left-0 pl-4 flex items-center pointer-events-none"
      >
        <slot name="prefix">
          <span class="text-gray-500 text-sm">{{ prefix }}</span>
        </slot>
      </div>

      <input
        :id="id"
        :type="type"
        :value="modelValue"
        :placeholder="placeholder"
        :disabled="disabled"
        :min="min"
        :max="max"
        :step="step"
        class="block w-full rounded-lg border-gray-300 shadow-sm transition-all duration-200 focus:border-primary-500 focus:ring-2 focus:ring-primary-500 focus:ring-opacity-20 disabled:bg-gray-100 disabled:cursor-not-allowed"
        :class="[
          error ? 'border-red-500 focus:border-red-500 focus:ring-red-500' : '',
          $slots.prefix || prefix ? 'pl-10' : 'pl-4',
          'pr-4 py-3',
        ]"
        @input="handleInput"
        @blur="handleBlur"
      />
    </div>

    <p v-if="error" class="mt-2 text-sm text-red-600 animate-fade-in">
      {{ errorMessage }}
    </p>

    <p v-else-if="hint" class="mt-2 text-sm text-gray-500">
      {{ hint }}
    </p>
  </div>
</template>

<script setup lang="ts">
interface Props {
  id?: string
  modelValue: string | number
  label?: string
  placeholder?: string
  type?: 'text' | 'number' | 'email' | 'password'
  disabled?: boolean
  required?: boolean
  error?: boolean
  errorMessage?: string
  hint?: string
  prefix?: string
  min?: number
  max?: number
  step?: number | string
}

const props = withDefaults(defineProps<Props>(), {
  id: undefined,
  label: '',
  placeholder: '',
  type: 'text',
  disabled: false,
  required: false,
  error: false,
  errorMessage: '',
  hint: '',
  prefix: '',
  min: undefined,
  max: undefined,
  step: undefined,
})

const emit = defineEmits<{
  'update:modelValue': [value: string | number]
  blur: []
}>()

function handleInput(event: Event) {
  const target = event.target as HTMLInputElement
  const value = props.type === 'number' ? Number(target.value) : target.value
  emit('update:modelValue', value)
}

function handleBlur() {
  emit('blur')
}
</script>

<style scoped>
@media (min-width: 1920px) {
  .text-sm {
    font-size: 0.9375rem;
  }

  .py-3 {
    padding-top: 0.875rem;
    padding-bottom: 0.875rem;
  }

  .pl-4 {
    padding-left: 1.125rem;
  }

  .pr-4 {
    padding-right: 1.125rem;
  }
}

@media (min-width: 2560px) {
  .text-sm {
    font-size: 1rem;
  }

  .py-3 {
    padding-top: 1rem;
    padding-bottom: 1rem;
  }

  .pl-4 {
    padding-left: 1.25rem;
  }

  .pr-4 {
    padding-right: 1.25rem;
  }

  input {
    font-size: 1.0625rem;
  }
}
</style>

