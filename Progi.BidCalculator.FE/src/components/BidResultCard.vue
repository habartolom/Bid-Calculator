<template>
  <BaseCard
    title="Calculation Result"
    subtitle="Detailed breakdown of fees and total price"
    :animated="true"
  >
    <div v-if="result" class="result-content">
      <div class="result-header p-2.5 bg-gradient-to-r from-primary-50 to-blue-50 rounded-lg">
        <div class="flex justify-between items-center">
          <div>
            <span class="text-xs text-gray-500 block">Base Price</span>
            <span class="text-base font-semibold text-gray-900">
              {{ formatCurrency(result.vehicleBasePrice) }}
            </span>
          </div>
          <div class="text-right">
            <span class="text-xs text-gray-500 block">Type</span>
            <span class="text-sm font-medium text-gray-700">
              {{ VehicleTypeLabels[store.vehicleType] }}
            </span>
          </div>
        </div>
      </div>

      <div class="result-fees space-y-1.5">
        <h4 class="text-xs font-semibold text-gray-500 uppercase tracking-wide mb-1.5">Fees</h4>

        <div class="space-y-1">
          <div 
            v-for="fee in sortedFees" 
            :key="fee.feeCode" 
            class="fee-item py-1.5 px-2.5 bg-gray-50 rounded"
          >
            <div class="flex justify-between items-center text-sm">
              <span class="text-gray-700">{{ fee.feeName }}</span>
              <span class="font-semibold text-gray-900">{{ formatCurrency(fee.amount) }}</span>
            </div>
          </div>
        </div>
      </div>

      <!-- Total - Más compacto -->
      <div class="result-total bg-gradient-to-r from-primary-600 to-primary-700 rounded-lg p-3 text-white">
        <div class="flex justify-between items-center">
          <span class="text-sm opacity-90">Total Price to Pay</span>
          <span class="text-xl font-bold">{{ formatCurrency(result.totalCost) }}</span>
        </div>
      </div>
    </div>
  </BaseCard>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { useBidCalculatorStore } from '@/stores'
import { VehicleTypeLabels } from '@/types'
import BaseCard from './base/BaseCard.vue'

const store = useBidCalculatorStore()

const result = computed(() => store.result)

const sortedFees = computed(() => {
  if (!result.value || !result.value.appliedFees) {
    return []
  }
  return [...result.value.appliedFees].sort((a, b) => a.displayOrder - b.displayOrder)
})

function formatCurrency(value: number): string {
  return store.formatCurrency(value)
}
</script>

<style scoped>
.result-content {
  display: flex;
  flex-direction: column;
  height: 100%;
  gap: 0.75rem;
}

.result-header {
  flex-shrink: 0;
}

.result-fees {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.fee-item {
  transition: all 0.2s ease;
}

.fee-item:hover {
  background-color: rgb(243 244 246) !important;
  transform: translateX(2px);
}

.result-total {
  flex-shrink: 0;
}

@media (min-width: 1024px) {
  .result-content {
    gap: 1rem;
  }
}

@media (min-width: 1920px) {
  .result-content {
    gap: 1.25rem;
  }

  .text-xs {
    font-size: 0.8125rem;
  }

  .text-sm {
    font-size: 0.9375rem;
  }

  .text-base {
    font-size: 1.0625rem;
  }

  .text-xl {
    font-size: 1.375rem;
  }

  .p-2\.5 {
    padding: 0.75rem;
  }

  .p-3 {
    padding: 0.875rem;
  }

  .py-1\.5 {
    padding-top: 0.5rem;
    padding-bottom: 0.5rem;
  }

  .px-2\.5 {
    padding-left: 0.75rem;
    padding-right: 0.75rem;
  }
}

@media (min-width: 2560px) {
  .result-content {
    gap: 1.5rem;
  }

  .text-xs {
    font-size: 0.875rem;
  }

  .text-sm {
    font-size: 1rem;
  }

  .text-base {
    font-size: 1.125rem;
  }

  .text-xl {
    font-size: 1.5rem;
  }

  .p-2\.5 {
    padding: 0.875rem;
  }

  .p-3 {
    padding: 1rem;
  }

  .py-1\.5 {
    padding-top: 0.625rem;
    padding-bottom: 0.625rem;
  }

  .px-2\.5 {
    padding-left: 0.875rem;
    padding-right: 0.875rem;
  }
}
</style>

