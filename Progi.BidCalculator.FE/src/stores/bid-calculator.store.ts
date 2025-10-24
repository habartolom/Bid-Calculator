import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import { bidCalculatorService } from '@/services'
import { VehicleType } from '@/types/enums'
import type { BidCalculationResponse } from '@/types'

export const useBidCalculatorStore = defineStore('bidCalculator', () => {
  const vehicleBasePrice = ref<number>(0)
  const vehicleType = ref<VehicleType>(VehicleType.Common)
  const result = ref<BidCalculationResponse | null>(null)
  const loading = ref<boolean>(false)
  const error = ref<string | null>(null)
  const isBackendHealthy = ref<boolean>(false)

  const hasResult = computed(() => result.value !== null)
  const hasError = computed(() => error.value !== null)
  const isFormValid = computed(() => vehicleBasePrice.value > 0)
  
  const formatCurrency = (value: number): string => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      minimumFractionDigits: 2,
    }).format(value)
  }

  const checkBackendHealth = async (): Promise<void> => {
    try {
      isBackendHealthy.value = await bidCalculatorService.checkHealth()
    } catch (err) {
      isBackendHealthy.value = false
      console.error('Backend health check failed:', err)
    }
  }

  const calculateBid = async (): Promise<void> => {
    if (!isFormValid.value) {
      error.value = 'Vehicle base price must be greater than 0'
      return
    }

    loading.value = true
    error.value = null
    result.value = null

    try {
      const response = await bidCalculatorService.calculateBid({
        vehicleBasePrice: vehicleBasePrice.value,
        vehicleType: vehicleType.value,
      })
      
      result.value = response
    } catch (err) {
      error.value = err instanceof Error ? err.message : 'Unknown error calculating bid'
      console.error('Calculate bid error:', err)
    } finally {
      loading.value = false
    }
  }

  const setVehicleBasePrice = (price: number): void => {
    vehicleBasePrice.value = price
    result.value = null
    error.value = null
  }

  const setVehicleType = (type: VehicleType): void => {
    vehicleType.value = type
    result.value = null
    error.value = null
  }

  const resetForm = (): void => {
    vehicleBasePrice.value = 0
    vehicleType.value = VehicleType.Common
    result.value = null
    error.value = null
    loading.value = false
  }

  const clearError = (): void => {
    error.value = null
  }

  return {
    vehicleBasePrice,
    vehicleType,
    result,
    loading,
    error,
    isBackendHealthy,
    hasResult,
    hasError,
    isFormValid,
    checkBackendHealth,
    calculateBid,
    setVehicleBasePrice,
    setVehicleType,
    resetForm,
    clearError,
    formatCurrency,
  }
})
