import { describe, it, expect } from 'vitest'
import { useValidation, ValidationRules } from '../useValidation'

describe('useValidation', () => {
  describe('Basic validation', () => {
    it('should initialize with the initial value', () => {
      const { value } = useValidation(100)
      expect(value.value).toBe(100)
    })

    it('should validate correctly with rules', () => {
      const { isValid } = useValidation(
        100,
        [ValidationRules.positive()]
      )

      expect(isValid.value).toBe(true)
    })

    it('should detect validation errors', () => {
      const { isValid, errors } = useValidation(
        -10,
        [ValidationRules.positive()]
      )

      expect(isValid.value).toBe(false)
      expect(errors.value.length).toBeGreaterThan(0)
    })
  })

  describe('ValidationRules', () => {
    describe('required', () => {
      it('should validate required values', () => {
        const rule = ValidationRules.required()

        expect(rule.validate('')).toBe(false)
        expect(rule.validate('   ')).toBe(false)
        expect(rule.validate('valor')).toBe(true)
        expect(rule.validate(100)).toBe(true)
      })
    })

    describe('min', () => {
      it('should validate minimum value', () => {
        const rule = ValidationRules.min(10)

        expect(rule.validate(5)).toBe(false)
        expect(rule.validate(10)).toBe(true)
        expect(rule.validate(15)).toBe(true)
      })
    })

    describe('max', () => {
      it('should validate maximum value', () => {
        const rule = ValidationRules.max(100)

        expect(rule.validate(150)).toBe(false)
        expect(rule.validate(100)).toBe(true)
        expect(rule.validate(50)).toBe(true)
      })
    })

    describe('positive', () => {
      it('should validate positive values', () => {
        const rule = ValidationRules.positive()

        expect(rule.validate(-5)).toBe(false)
        expect(rule.validate(0)).toBe(false)
        expect(rule.validate(5)).toBe(true)
      })
    })
  })

  describe('Methods of the composable', () => {
    it('markAsTouched should mark as touched', () => {
      const { touched, markAsTouched } = useValidation(0)

      expect(touched.value).toBe(false)
      markAsTouched()
      expect(touched.value).toBe(true)
    })

    it('reset should reset to the initial value', () => {
      const { value, touched, markAsTouched, reset } = useValidation(100)

      value.value = 200
      markAsTouched()

      reset()

      expect(value.value).toBe(100)
      expect(touched.value).toBe(false)
    })

    it('hasError should be true only if touched and has errors', () => {
      const { hasError, markAsTouched, value } = useValidation(
        -10,
        [ValidationRules.positive()]
      )

      expect(hasError.value).toBe(false)

      markAsTouched()
      expect(hasError.value).toBe(true)

      value.value = 10
      expect(hasError.value).toBe(false)
    })
  })
})

