<template>
  <BaseCard title="Vehicle Information" subtitle="Enter the data to calculate the total price">
    <form @submit.prevent="handleSubmit" class="bid-form">
      <div class="bid-form__fields">
        <BaseInput
          v-model="store.vehicleBasePrice"
          type="number"
          label="Vehicle Base Price"
          placeholder="Enter the base price"
          prefix="$"
          :min="0"
          :step="0.01"
          :required="true"
          :error="priceError"
          error-message="Price must be greater than 0"
          hint="Winning bid price without additional fees"
        />

        <BaseSelect
          v-model.number="selectedVehicleType"
          label="Vehicle Type"
          placeholder="Select a type"
          :options="vehicleTypeOptions"
          :required="true"
          hint="Vehicle type affects applicable fees"
        />
      </div>

      <div class="bid-form__actions flex gap-4">
        <BaseButton
          type="submit"
          variant="primary"
          :loading="store.loading"
          :disabled="!store.isFormValid"
          full-width
        >
          <template #icon>
            <svg
              class="w-5 h-5"
              fill="none"
              stroke="currentColor"
              viewBox="0 0 24 24"
            >
              <path
                stroke-linecap="round"
                stroke-linejoin="round"
                stroke-width="2"
                d="M9 7h6m0 10v-3m-3 3h.01M9 17h.01M9 14h.01M12 14h.01M15 11h.01M12 11h.01M9 11h.01M7 21h10a2 2 0 002-2V5a2 2 0 00-2-2H7a2 2 0 00-2 2v14a2 2 0 002 2z"
              />
            </svg>
          </template>
          {{ store.loading ? 'Calculating...' : 'Calculate Total Price' }}
        </BaseButton>

        <BaseButton
          type="button"
          variant="outline"
          :disabled="store.loading"
          @click="handleReset"
        >
          Clear
        </BaseButton>
      </div>

      <BaseAlert
        v-model="showError"
        type="error"
        title="Calculation Error"
        :message="store.error || ''"
        dismissible
      />
    </form>
  </BaseCard>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useBidCalculatorStore } from '@/stores'
import { VehicleType, VehicleTypeLabels } from '@/types'
import BaseCard from './base/BaseCard.vue'
import BaseInput from './base/BaseInput.vue'
import BaseSelect from './base/BaseSelect.vue'
import BaseButton from './base/BaseButton.vue'
import BaseAlert from './base/BaseAlert.vue'

const store = useBidCalculatorStore()

const selectedVehicleType = ref<VehicleType>(VehicleType.Common)
const priceError = ref(false)
const showError = computed({
  get: () => store.hasError,
  set: (value: boolean) => {
    if (!value) {
      store.clearError()
    }
  },
})

const vehicleTypeOptions = computed(() => [
  { label: VehicleTypeLabels[VehicleType.Common], value: VehicleType.Common },
  { label: VehicleTypeLabels[VehicleType.Luxury], value: VehicleType.Luxury },
])

watch(selectedVehicleType, newType => {
  store.setVehicleType(newType)
})

function validatePrice(): boolean {
  priceError.value = !store.isFormValid
  return store.isFormValid
}

async function handleSubmit() {
  if (!validatePrice()) {
    return
  }

  await store.calculateBid()
}

function handleReset() {
  store.resetForm()
  selectedVehicleType.value = VehicleType.Common
  priceError.value = false
}
</script>

<style scoped>
.bid-form {
  display: flex;
  flex-direction: column;
  height: 100%;
  gap: 1rem;
}

.bid-form__fields {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  flex: 1;
  justify-content: flex-start;
}

.bid-form__actions {
  margin-top: auto;
}

@media (min-width: 1024px) {
  .bid-form {
    gap: 1.25rem;
  }

  .bid-form__fields {
    gap: 1.25rem;
    justify-content: center;
  }
}

@media (min-width: 1920px) {
  .bid-form {
    gap: 1.5rem;
  }

  .bid-form__fields {
    gap: 1.5rem;
  }
}

@media (min-width: 2560px) {
  .bid-form {
    gap: 2rem;
  }

  .bid-form__fields {
    gap: 2rem;
  }
}
</style>

