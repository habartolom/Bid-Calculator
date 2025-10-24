import { describe, it, expect, vi, beforeEach } from 'vitest'
import { bidCalculatorService } from '../bid-calculator.service'
import { httpService } from '../http.service'
import { VehicleType } from '@/types'
import type { BidCalculationResponse } from '@/types'

describe('BidCalculatorService', () => {
  beforeEach(() => {
    vi.clearAllMocks()
  })

  describe('checkHealth', () => {
    it('should return true when the backend is available', async () => { 
      vi.spyOn(httpService, 'get').mockResolvedValue({})

      const result = await bidCalculatorService.checkHealth()

      expect(result).toBe(true)
      expect(httpService.get).toHaveBeenCalledWith('/BidCalculator/health')
    })

    it('should return false when the backend is not available', async () => {
      vi.spyOn(httpService, 'get').mockRejectedValue(new Error('Network error'))

      const result = await bidCalculatorService.checkHealth()

      expect(result).toBe(false)
    })
  })

  describe('calculateBid', () => {
    it('should calculate correctly a bid for a common vehicle', async () => {
      const request = {
        vehicleBasePrice: 1000,
        vehicleType: VehicleType.Common,
      }

      const expectedResponse: BidCalculationResponse = {
        vehicleBasePrice: 1000,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 50, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 20, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 10, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100, displayOrder: 4 }
        ],
        totalCost: 1180,
      }

      vi.spyOn(httpService, 'post').mockResolvedValue(expectedResponse)

      const result = await bidCalculatorService.calculateBid(request)

      expect(result).toEqual(expectedResponse)
      expect(httpService.post).toHaveBeenCalledWith('/BidCalculator/calculate', request)
    })

    it('should calculate correctly a bid for a luxury vehicle', async () => {
      const request = {
        vehicleBasePrice: 1800,
        vehicleType: VehicleType.Luxury,
      }

      const expectedResponse: BidCalculationResponse = {
        vehicleBasePrice: 1800,
        appliedFees: [
          { feeCode: 'BUYER_FEE', feeName: 'Basic Buyer Fee', amount: 180, displayOrder: 1 },
          { feeCode: 'SPECIAL_FEE', feeName: 'Special Fee', amount: 72, displayOrder: 2 },
          { feeCode: 'ASSOCIATION_FEE', feeName: 'Association Fee', amount: 15, displayOrder: 3 },
          { feeCode: 'STORAGE_FEE', feeName: 'Storage Fee', amount: 100, displayOrder: 4 }
        ],
        totalCost: 2167,
      }

      vi.spyOn(httpService, 'post').mockResolvedValue(expectedResponse)

      const result = await bidCalculatorService.calculateBid(request)

      expect(result).toEqual(expectedResponse)
    })

    it('should throw an error when the request fails', async () => {
      const request = {
        vehicleBasePrice: 1000,
        vehicleType: VehicleType.Common,
      }

      vi.spyOn(httpService, 'post').mockRejectedValue(new Error('API Error'))

      await expect(bidCalculatorService.calculateBid(request)).rejects.toThrow(
        'Could not calculate bid'
      )
    })
  })
})

