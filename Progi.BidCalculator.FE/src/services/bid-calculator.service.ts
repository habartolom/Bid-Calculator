import httpService from './http.service'
import API_CONFIG from '@/config/api.config'
import type { BidCalculationRequest, BidCalculationResponse } from '@/types'

class BidCalculatorService {
  async checkHealth(): Promise<boolean> {
    try {
      await httpService.get(API_CONFIG.endpoints.health)
      return true
    } catch (error) {
      console.error('Health check failed:', error)
      return false
    }
  }

  async calculateBid(request: BidCalculationRequest): Promise<BidCalculationResponse> {
    try {
      const response = await httpService.post<BidCalculationResponse>(
        API_CONFIG.endpoints.calculate,
        request
      )
      return response
    } catch (error) {
      console.error('Error calculating bid:', error)
      throw new Error('Could not calculate bid. Please try again.')
    }
  }
}

export const bidCalculatorService = new BidCalculatorService()
export default bidCalculatorService
