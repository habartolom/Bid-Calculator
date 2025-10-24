import { ref, computed } from 'vue'

export interface ValidationRule {
  validate: (value: unknown) => boolean
  message: string
}

export function useValidation(initialValue: unknown, rules: ValidationRule[] = []) {
  const value = ref(initialValue)
  const touched = ref(false)
  const errors = ref<string[]>([])

  const isValid = computed(() => {
    errors.value = []
    
    for (const rule of rules) {
      if (!rule.validate(value.value)) {
        errors.value.push(rule.message)
      }
    }
    
    return errors.value.length === 0
  })

  const hasError = computed(() => touched.value && !isValid.value)

  function markAsTouched() {
    touched.value = true
  }

  function reset() {
    value.value = initialValue
    touched.value = false
    errors.value = []
  }

  return {
    value,
    touched,
    errors,
    isValid,
    hasError,
    markAsTouched,
    reset,
  }
}

export const ValidationRules = {
  required: (message = 'This field is required'): ValidationRule => ({
    validate: (value: unknown) => {
      if (typeof value === 'string') return value.trim().length > 0
      if (typeof value === 'number') return !isNaN(value)
      return value != null
    },
    message,
  }),

  min: (minValue: number, message?: string): ValidationRule => ({
    validate: (value: unknown) => {
      const num = Number(value)
      return !isNaN(num) && num >= minValue
    },
    message: message || `The value must be greater than or equal to ${minValue}`,
  }),

  max: (maxValue: number, message?: string): ValidationRule => ({
    validate: (value: unknown) => {
      const num = Number(value)
      return !isNaN(num) && num <= maxValue
    },
    message: message || `The value must be less than or equal to ${maxValue}`,
  }),

  positive: (message = 'The value must be positive'): ValidationRule => ({
    validate: (value: unknown) => {
      const num = Number(value)
      return !isNaN(num) && num > 0
    },
    message,
  }),
}


