import axios, { AxiosInstance, AxiosError } from 'axios'
import API_CONFIG from '@/config/api.config'

class HttpService {
  private axiosInstance: AxiosInstance

  constructor() {
    this.axiosInstance = axios.create({
      baseURL: API_CONFIG.baseURL,
      timeout: API_CONFIG.timeout,
      headers: {
        'Content-Type': 'application/json',
      },
    })

    this.setupInterceptors()
  }

  private setupInterceptors(): void {
    this.axiosInstance.interceptors.request.use(
      config => {
        console.log(`[HTTP] ${config.method?.toUpperCase()} ${config.url}`)
        return config
      },
      error => {
        console.error('[HTTP] Request error:', error)
        return Promise.reject(error)
      }
    )

    this.axiosInstance.interceptors.response.use(
      response => {
        console.log(`[HTTP] Response from ${response.config.url}:`, response.status)
        return response
      },
      (error: AxiosError) => {
        this.handleError(error)
        return Promise.reject(error)
      }
    )
  }

  private handleError(error: AxiosError): void {
    if (error.response) {
      console.error('[HTTP] Server error:', error.response.status, error.response.data)
    } else if (error.request) {
      console.error('[HTTP] No response received:', error.request)
    } else {
      console.error('[HTTP] Request setup error:', error.message)
    }
  }

  get instance(): AxiosInstance {
    return this.axiosInstance
  }

  async get<T>(url: string, config = {}): Promise<T> {
    const response = await this.axiosInstance.get<T>(url, config)
    return response.data
  }

  async post<T>(url: string, data?: unknown, config = {}): Promise<T> {
    const response = await this.axiosInstance.post<T>(url, data, config)
    return response.data
  }

  async put<T>(url: string, data?: unknown, config = {}): Promise<T> {
    const response = await this.axiosInstance.put<T>(url, data, config)
    return response.data
  }

  async delete<T>(url: string, config = {}): Promise<T> {
    const response = await this.axiosInstance.delete<T>(url, config)
    return response.data
  }
}

export const httpService = new HttpService()
export default httpService
