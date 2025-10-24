import { describe, it, expect, vi, beforeEach } from 'vitest'
import { setActivePinia, createPinia } from 'pinia'
import { useBidCalculatorStore } from '../bid-calculator.store'
import { bidCalculatorService } from '@/services'
import { VehicleType } from '@/types'

describe('BidCalculatorStore', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
    vi.clearAllMocks()
  })

  describe('Initial state', () => {
    it('should have the initial state correct', () => {
      const store = useBidCalculatorStore()

      expect(store.vehicleBasePrice).toBe(0)
      expect(store.vehicleType).toBe(VehicleType.Common)
      expect(store.result).toBeNull()
      expect(store.loading).toBe(false)
      expect(store.error).toBeNull()
    })
  })

  describe('Getters', () => {
    it('hasResult should return true when there is a result', () => {
      const store = useBidCalculatorStore()
      store.result = {
        vehicleBasePrice: 1000,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 50, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 20, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 10, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100, displayOrder: 4 }
        ],
        totalCost: 1180,
      }

      expect(store.hasResult).toBe(true)
    })

    it('hasError should return true when there is an error', () => {
      const store = useBidCalculatorStore()
      store.error = 'Test error'

      expect(store.hasError).toBe(true)
    })

    it('isFormValid should return true when the price is greater than 0', () => {
      const store = useBidCalculatorStore()
      store.vehicleBasePrice = 100

      expect(store.isFormValid).toBe(true)
    })

    it('isFormValid should return false when the price is 0', () => {
      const store = useBidCalculatorStore()
      store.vehicleBasePrice = 0

      expect(store.isFormValid).toBe(false)
    })
  })

  describe('Actions', () => {
    describe('checkBackendHealth', () => {
      it('should set isBackendHealthy to true when the backend is OK', async () => {
        const store = useBidCalculatorStore()
        vi.spyOn(bidCalculatorService, 'checkHealth').mockResolvedValue(true)

        await store.checkBackendHealth()

        expect(store.isBackendHealthy).toBe(true)
      })

      it('should set isBackendHealthy to false when the backend fails', async () => {
        const store = useBidCalculatorStore()
        vi.spyOn(bidCalculatorService, 'checkHealth').mockResolvedValue(false)

        await store.checkBackendHealth()

        expect(store.isBackendHealthy).toBe(false)
      })
    })

    describe('calculateBid', () => {
      it('should calculate correctly and set the result', async () => {
        const store = useBidCalculatorStore()
        store.vehicleBasePrice = 1000
        store.vehicleType = VehicleType.Common

      const mockResponse = {
        vehicleBasePrice: 1000,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 50, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 20, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 10, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100, displayOrder: 4 }
        ],
        totalCost: 1180,
      }

        vi.spyOn(bidCalculatorService, 'calculateBid').mockResolvedValue(mockResponse)

        await store.calculateBid()

        expect(store.result).toEqual(mockResponse)
        expect(store.loading).toBe(false)
        expect(store.error).toBeNull()
      })

      it('should set error when the calculation fails', async () => {
        const store = useBidCalculatorStore()
        store.vehicleBasePrice = 1000

        const errorMessage = 'Test error'
        vi.spyOn(bidCalculatorService, 'calculateBid').mockRejectedValue(
          new Error(errorMessage)
        )

        await store.calculateBid()

        expect(store.error).toBe(errorMessage)
        expect(store.result).toBeNull()
        expect(store.loading).toBe(false)
      })

      it('should not calculate if the form is not valid', async () => {
        const store = useBidCalculatorStore()
        store.vehicleBasePrice = 0

        const spy = vi.spyOn(bidCalculatorService, 'calculateBid')

        await store.calculateBid()

        expect(spy).not.toHaveBeenCalled()
        expect(store.error).toBe('Vehicle base price must be greater than 0')
      })
    })

    describe('setVehicleBasePrice', () => {
      it('should update the price and clear results', () => {
        const store = useBidCalculatorStore()
        store.result = {
          vehicleBasePrice: 1000,
          appliedFees: [
            { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 50, displayOrder: 1 },
            { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 20, displayOrder: 2 },
            { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 10, displayOrder: 3 },
            { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100, displayOrder: 4 }
          ],
          totalCost: 1180,
        }

        store.setVehicleBasePrice(2000)

        expect(store.vehicleBasePrice).toBe(2000)
        expect(store.result).toBeNull()
        expect(store.error).toBeNull()
      })
    })

    describe('setVehicleType', () => {
      it('should update the type and clear results', () => {
        const store = useBidCalculatorStore()
        store.result = {
          vehicleBasePrice: 1000,
          appliedFees: [
            { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 50, displayOrder: 1 },
            { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 20, displayOrder: 2 },
            { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 10, displayOrder: 3 },
            { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100, displayOrder: 4 }
          ],
          totalCost: 1180,
        }

        store.setVehicleType(VehicleType.Luxury)

        expect(store.vehicleType).toBe(VehicleType.Luxury)
        expect(store.result).toBeNull()
        expect(store.error).toBeNull()
      })
    })

    describe('resetForm', () => {
      it('should reset the form to the initial state', () => {
        const store = useBidCalculatorStore()
        store.vehicleBasePrice = 1000
        store.vehicleType = VehicleType.Luxury
      store.result = {
        vehicleBasePrice: 1000,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 100, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 40, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 10, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100, displayOrder: 4 }
        ],
        totalCost: 1250,
      }
        store.error = 'Error'

        store.resetForm()

        expect(store.vehicleBasePrice).toBe(0)
        expect(store.vehicleType).toBe(VehicleType.Common)
        expect(store.result).toBeNull()
        expect(store.error).toBeNull()
      })
    })

    describe('clearError', () => {
      it('should clear the error', () => {
        const store = useBidCalculatorStore()
        store.error = 'Test error'

        store.clearError()

        expect(store.error).toBeNull()
      })
    })
  })

  describe('formatCurrency', () => {
    it('should format numbers as USD currency', () => {
      const store = useBidCalculatorStore()

      expect(store.formatCurrency(1000)).toBe('$1,000.00')
      expect(store.formatCurrency(1234.56)).toBe('$1,234.56')
      expect(store.formatCurrency(0.99)).toBe('$0.99')
    })
  })
})

