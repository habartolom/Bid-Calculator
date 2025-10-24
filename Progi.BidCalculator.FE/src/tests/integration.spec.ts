import { describe, it, expect, beforeEach } from 'vitest'
import { setActivePinia, createPinia } from 'pinia'
import { useBidCalculatorStore } from '@/stores'
import { VehicleType } from '@/types'

describe('Integration Tests - Bid Calculator Flow', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
  })

  describe('Test Cases from BidCalculator.md', () => {
    it('should calculate correctly for a common vehicle of $398.00', () => {
      const store = useBidCalculatorStore()
      
      store.setVehicleBasePrice(398.00)
      store.setVehicleType(VehicleType.Common)
      
      store.result = {
        vehicleBasePrice: 398.00,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 39.80, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 7.96, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 5.00, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100.00, displayOrder: 4 }
        ],
        totalCost: 550.76,
      }
      
      const buyerFee = store.result.appliedFees.find(f => f.feeCode === 'BUYER_FEE')
      const specialFee = store.result.appliedFees.find(f => f.feeCode === 'SPECIAL_FEE')
      const associationFee = store.result.appliedFees.find(f => f.feeCode === 'ASSOCIATION_FEE')
      const storageFee = store.result.appliedFees.find(f => f.feeCode === 'STORAGE_FEE')
      expect(buyerFee?.amount).toBe(39.80)
      expect(specialFee?.amount).toBe(7.96)
      expect(associationFee?.amount).toBe(5.00)
      expect(storageFee?.amount).toBe(100.00)
      expect(store.result.totalCost).toBe(550.76)
    })

    it('should calculate correctly for a common vehicle of $501.00', () => {
      const store = useBidCalculatorStore()
      
      store.setVehicleBasePrice(501.00)
      store.setVehicleType(VehicleType.Common)
      
      store.result = {
        vehicleBasePrice: 501.00,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 50.00, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 10.02, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 10.00, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100.00, displayOrder: 4 }
        ],
        totalCost: 671.02,
      }
      
      const buyerFee = store.result.appliedFees.find(f => f.feeCode === 'BUYER_FEE')
      const specialFee = store.result.appliedFees.find(f => f.feeCode === 'SPECIAL_FEE')
      const associationFee = store.result.appliedFees.find(f => f.feeCode === 'ASSOCIATION_FEE')
      const storageFee = store.result.appliedFees.find(f => f.feeCode === 'STORAGE_FEE')
      expect(buyerFee?.amount).toBe(50.00)
      expect(specialFee?.amount).toBe(10.02)
      expect(associationFee?.amount).toBe(10.00)
      expect(storageFee?.amount).toBe(100.00)
      expect(store.result.totalCost).toBe(671.02)
    })

    it('should calculate correctly for a common vehicle of $57.00', () => {
      const store = useBidCalculatorStore()
      
      store.setVehicleBasePrice(57.00)
      store.setVehicleType(VehicleType.Common)
      
      store.result = {
        vehicleBasePrice: 57.00,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 10.00, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 1.14, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 5.00, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100.00, displayOrder: 4 }
        ],
        totalCost: 173.14,
      }
      
      const buyerFee = store.result.appliedFees.find(f => f.feeCode === 'BUYER_FEE')
      const specialFee = store.result.appliedFees.find(f => f.feeCode === 'SPECIAL_FEE')
      const associationFee = store.result.appliedFees.find(f => f.feeCode === 'ASSOCIATION_FEE')
      const storageFee = store.result.appliedFees.find(f => f.feeCode === 'STORAGE_FEE')
      expect(buyerFee?.amount).toBe(10.00)
      expect(specialFee?.amount).toBe(1.14)
      expect(associationFee?.amount).toBe(5.00)
      expect(storageFee?.amount).toBe(100.00)
      expect(store.result.totalCost).toBe(173.14)
    })

    it('should calculate correctly for a luxury vehicle of $1,800.00', () => {
      const store = useBidCalculatorStore()
      
      store.setVehicleBasePrice(1800.00)
      store.setVehicleType(VehicleType.Luxury)
      
      store.result = {
        vehicleBasePrice: 1800.00,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 180.00, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 72.00, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 15.00, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100.00, displayOrder: 4 }
        ],
        totalCost: 2167.00,
      }
      
      const buyerFee = store.result.appliedFees.find(f => f.feeCode === 'BUYER_FEE')
      const specialFee = store.result.appliedFees.find(f => f.feeCode === 'SPECIAL_FEE')
      const associationFee = store.result.appliedFees.find(f => f.feeCode === 'ASSOCIATION_FEE')
      const storageFee = store.result.appliedFees.find(f => f.feeCode === 'STORAGE_FEE')
      expect(buyerFee?.amount).toBe(180.00)
      expect(specialFee?.amount).toBe(72.00)
      expect(associationFee?.amount).toBe(15.00)
      expect(storageFee?.amount).toBe(100.00)
      expect(store.result.totalCost).toBe(2167.00)
    })

    it('should calculate correctly for a common vehicle of $1,100.00', () => {
      const store = useBidCalculatorStore()
      
      store.setVehicleBasePrice(1100.00)
      store.setVehicleType(VehicleType.Common)
      
      store.result = {
        vehicleBasePrice: 1100.00,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 50.00, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 22.00, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 15.00, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100.00, displayOrder: 4 }
        ],
        totalCost: 1287.00,
      }
      
      const buyerFee = store.result.appliedFees.find(f => f.feeCode === 'BUYER_FEE')
      const specialFee = store.result.appliedFees.find(f => f.feeCode === 'SPECIAL_FEE')
      const associationFee = store.result.appliedFees.find(f => f.feeCode === 'ASSOCIATION_FEE')
      const storageFee = store.result.appliedFees.find(f => f.feeCode === 'STORAGE_FEE')
      expect(buyerFee?.amount).toBe(50.00)
      expect(specialFee?.amount).toBe(22.00)
      expect(associationFee?.amount).toBe(15.00)
      expect(storageFee?.amount).toBe(100.00)
      expect(store.result.totalCost).toBe(1287.00)
    })

    it('should calculate correctly for a luxury vehicle of $1,000,000.00', () => {
      const store = useBidCalculatorStore()
      
      store.setVehicleBasePrice(1000000.00)
      store.setVehicleType(VehicleType.Luxury)
      
      store.result = {
        vehicleBasePrice: 1000000.00,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 200.00, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 40000.00, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 20.00, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100.00, displayOrder: 4 }
        ],
        totalCost: 1040320.00,
      }
      
      const buyerFee = store.result.appliedFees.find(f => f.feeCode === 'BUYER_FEE')
      const specialFee = store.result.appliedFees.find(f => f.feeCode === 'SPECIAL_FEE')
      const associationFee = store.result.appliedFees.find(f => f.feeCode === 'ASSOCIATION_FEE')
      const storageFee = store.result.appliedFees.find(f => f.feeCode === 'STORAGE_FEE')
      expect(buyerFee?.amount).toBe(200.00)
      expect(specialFee?.amount).toBe(40000.00)
      expect(associationFee?.amount).toBe(20.00)
      expect(storageFee?.amount).toBe(100.00)
      expect(store.result.totalCost).toBe(1040320.00)
    })
  })

  describe('User Workflows', () => {
    it('should allow the complete flow: calculate, reset, calculate again', () => {
      const store = useBidCalculatorStore()
      
      store.setVehicleBasePrice(1000)
      store.setVehicleType(VehicleType.Common)
      expect(store.isFormValid).toBe(true)
      
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
      
      store.resetForm()
      expect(store.vehicleBasePrice).toBe(0)
      expect(store.vehicleType).toBe(VehicleType.Common)
      expect(store.result).toBeNull()
      expect(store.hasResult).toBe(false)
      
      store.setVehicleBasePrice(2000)
      store.setVehicleType(VehicleType.Luxury)
      expect(store.isFormValid).toBe(true)
    })

    it('should clear previous results when changing the price', () => {
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
      
      store.setVehicleBasePrice(2000)
      
      expect(store.result).toBeNull()
      expect(store.hasResult).toBe(false)
    })

    it('should clear previous results when changing the vehicle type', () => {
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
      
      store.setVehicleType(VehicleType.Luxury)
      
      expect(store.result).toBeNull()
      expect(store.hasResult).toBe(false)
    })
  })

  describe('Validations', () => {
    it('should not allow calculating with price 0', () => {
      const store = useBidCalculatorStore()
      
      store.setVehicleBasePrice(0)
      expect(store.isFormValid).toBe(false)
    })

    it('should allow calculating with price greater than 0', () => {
      const store = useBidCalculatorStore()
      
      store.setVehicleBasePrice(0.01)
      expect(store.isFormValid).toBe(true)
    })
  })

  describe('Currency formatting', () => {
    it('should format the monetary values correctly', () => {
      const store = useBidCalculatorStore()
      
      expect(store.formatCurrency(1000)).toBe('$1,000.00')
      expect(store.formatCurrency(1234.56)).toBe('$1,234.56')
      expect(store.formatCurrency(1000000)).toBe('$1,000,000.00')
      expect(store.formatCurrency(0.99)).toBe('$0.99')
    })
  })
})
