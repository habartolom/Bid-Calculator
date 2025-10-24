import { VehicleType } from './enums'

export interface BidCalculationRequest {
  vehicleBasePrice: number
  vehicleType: VehicleType
}

export interface AppliedFee {
  feeCode: string
  feeName: string
  amount: number
  displayOrder: number
}

export interface BidCalculationResponse {
  vehicleBasePrice: number
  appliedFees: AppliedFee[]
  totalCost: number
}

export interface BidCalculationState {
  vehicleBasePrice: number
  vehicleType: VehicleType
  result: BidCalculationResponse | null
  loading: boolean
  error: string | null
}
