export const API_CONFIG = {
  baseURL: import.meta.env.VITE_API_BASE_URL || '/api',
  timeout: 10000,
  endpoints: {
    health: '/BidCalculator/health',
    calculate: '/BidCalculator/calculate',
  },
} as const

export default API_CONFIG


