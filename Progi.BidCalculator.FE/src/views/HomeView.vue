<template>
  <div class="calculator">
    <header class="calculator__header">
      <div class="calculator__header-container">
        <div class="calculator__header-content">
          <div>
            <h1 class="calculator__title">
              Vehicle Bid Calculator
            </h1>
            <p class="calculator__subtitle">
              Calculate the total price of your vehicle including all applicable fees
            </p>
          </div>
          <StatusIndicator :is-healthy="store.isBackendHealthy" />
        </div>
      </div>
    </header>

    <main class="calculator__main">
      <div class="calculator__content">
        <div class="calculator__grid">
          <div class="calculator__column calculator__column--form">
            <BidCalculatorForm />
          </div>

          <div class="calculator__column calculator__column--result">
            <BidResultCard :class="{ 'card-hidden': !store.hasResult && !store.loading }" />
            
            <BaseCard :class="{ 'card-hidden': store.hasResult || store.loading }" title="How it works?" animated>
                <div class="calculator__steps">
                  <div class="calculator__step">
                    <div class="calculator__step-icon">
                      <svg class="w-5 h-5 text-primary-500" fill="currentColor" viewBox="0 0 20 20">
                        <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clip-rule="evenodd" />
                      </svg>
                    </div>
                    <div class="calculator__step-content">
                      <p class="calculator__step-title">Step 1: Enter the base price</p>
                      <p class="calculator__step-text">Indicate the winning bid price of the vehicle</p>
                    </div>
                  </div>

                  <div class="calculator__step">
                    <div class="calculator__step-icon">
                      <svg class="w-5 h-5 text-primary-500" fill="currentColor" viewBox="0 0 20 20">
                        <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clip-rule="evenodd" />
                      </svg>
                    </div>
                    <div class="calculator__step-content">
                      <p class="calculator__step-title">Step 2: Select the type</p>
                      <p class="calculator__step-text">Choose between common or luxury vehicle</p>
                    </div>
                  </div>

                  <div class="calculator__step">
                    <div class="calculator__step-icon">
                      <svg class="w-5 h-5 text-primary-500" fill="currentColor" viewBox="0 0 20 20">
                        <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z" clip-rule="evenodd" />
                      </svg>
                    </div>
                    <div class="calculator__step-content">
                      <p class="calculator__step-title">Step 3: Calculate</p>
                      <p class="calculator__step-text">Get the complete breakdown of fees and total price</p>
                    </div>
                  </div>
                </div>

                <div class="calculator__fees-info">
                  <h4 class="calculator__fees-title">Fees Included:</h4>
                  <ul class="calculator__fees-list">
                    <li>• Basic buyer fee (10% with limits)</li>
                    <li>• Special fee (2% or 4%)</li>
                    <li>• Association fee (based on price)</li>
                    <li>• Storage fee ($100 fixed)</li>
                  </ul>
                </div>
              </BaseCard>
          </div>
        </div>
      </div>
    </main>

    <footer class="calculator__footer">
      <div class="calculator__footer-container">
        <p class="calculator__footer-text">
          Vehicle Bid Calculator &copy; {{ currentYear }} - Built with Vue.js 3 + TypeScript
        </p>
      </div>
    </footer>
  </div>
</template>

<script setup lang="ts">
import { onMounted, computed } from 'vue'
import { useBidCalculatorStore } from '@/stores'
import BidCalculatorForm from '@/components/BidCalculatorForm.vue'
import BidResultCard from '@/components/BidResultCard.vue'
import BaseCard from '@/components/base/BaseCard.vue'
import StatusIndicator from '@/components/StatusIndicator.vue'

const store = useBidCalculatorStore()
const currentYear = computed(() => new Date().getFullYear())

onMounted(() => {
  store.checkBackendHealth()
})
</script>

<style scoped>
.calculator {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  background: linear-gradient(135deg, #f9fafb 0%, #eff6ff 50%, #f0f9ff 100%);
}

.calculator__header {
  background-color: white;
  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1);
  border-bottom: 1px solid #e5e7eb;
}

.calculator__header-container {
  max-width: 80rem;
  margin: 0 auto;
  padding: 1rem 1rem;
}

.calculator__header-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 0.5rem;
}

.calculator__title {
  font-size: 1.5rem;
  font-weight: 700;
  background: linear-gradient(to right, #0284c7, #075985);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
}

.calculator__subtitle {
  margin-top: 0.125rem;
  font-size: 0.75rem;
  color: #4b5563;
}

.calculator__main {
  flex: 1;
  width: 100%;
}

.calculator__content {
  max-width: 80rem;
  margin: 0 auto;
  padding: 1.5rem 1rem;
}

.calculator__grid {
  display: grid;
  grid-template-columns: 1fr;
  gap: 1.25rem;
  align-items: start;
}

.calculator__column {
  width: 100%;
  max-width: 100%;
}

.card-hidden {
  position: absolute;
  opacity: 0;
  visibility: hidden;
  pointer-events: none;
  inset: 0;
}

.calculator__steps {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  font-size: 0.875rem;
  color: #4b5563;
}

.calculator__step {
  display: flex;
  align-items: flex-start;
}

.calculator__step-icon {
  flex-shrink: 0;
  margin-top: 0.25rem;
}

.calculator__step-content {
  margin-left: 0.75rem;
}

.calculator__step-title {
  font-weight: 500;
  color: #111827;
}

.calculator__step-text {
  margin-top: 0.25rem;
}

.calculator__fees-info {
  margin-top: 1.5rem;
  padding: 1rem;
  background-color: #f0f9ff;
  border-radius: 0.5rem;
  border: 1px solid #bae6fd;
}

.calculator__fees-title {
  font-size: 0.875rem;
  font-weight: 600;
  color: #0c4a6e;
  margin-bottom: 0.5rem;
}

.calculator__fees-list {
  font-size: 0.75rem;
  color: #075985;
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}

.calculator__footer {
  margin-top: auto;
  background-color: white;
  border-top: 1px solid #e5e7eb;
}

.calculator__footer-container {
  max-width: 80rem;
  margin: 0 auto;
  padding: 0.75rem 1rem;
}

.calculator__footer-text {
  text-align: center;
  font-size: 0.75rem;
  color: #6b7280;
}

@media (min-width: 640px) {
  .calculator__header-container {
    padding: 1rem 1.5rem;
  }

  .calculator__content {
    padding: 1.5rem 1.5rem;
  }

  .calculator__footer-container {
    padding: 0.75rem 1.5rem;
  }
}

@media (min-width: 768px) {
  .calculator__title {
    font-size: 1.75rem;
  }

  .calculator__subtitle {
    font-size: 0.875rem;
  }

  .calculator__grid {
    gap: 1.5rem;
  }
}

@media (min-width: 1024px) {
  .calculator__header-container {
    padding: 1rem 2rem;
  }

  .calculator__content {
    padding: 2rem 2rem;
  }

  .calculator__footer-container {
    padding: 0.75rem 2rem;
  }

  .calculator__grid {
    grid-template-columns: repeat(2, 1fr);
    gap: 2rem;
    align-items: stretch;
  }

  .calculator__column {
    display: flex;
    flex-direction: column;
    min-height: 450px;
  }

  .calculator__column--result {
    position: relative;
  }

  .calculator__column > :deep(*) {
    flex: 1;
    display: flex;
    flex-direction: column;
  }
}

@media (min-width: 1280px) {
  .calculator__content {
    max-width: 75rem;
  }

  .calculator__grid {
    gap: 2.5rem;
  }
}

@media (min-width: 1536px) {
  .calculator__content {
    max-width: 80rem;
  }

  .calculator__grid {
    gap: 3rem;
  }
}

@media (min-width: 1920px) {
  .calculator__content {
    max-width: 85rem;
  }

  .calculator__grid {
    gap: 3.5rem;
  }

  .calculator__main {
    display: flex;
    align-items: center;
  }

  .calculator__title {
    font-size: 2rem;
  }

  .calculator__subtitle {
    font-size: 0.95rem;
  }
}

@media (min-width: 2560px) {
  .calculator__content {
    max-width: 95rem;
    padding: 3rem 2rem;
  }

  .calculator__grid {
    gap: 4rem;
  }

  .calculator {
    font-size: 1.05rem;
  }

  .calculator__title {
    font-size: 2.25rem;
  }

  .calculator__subtitle {
    font-size: 1rem;
  }
}

@media (min-width: 3440px) {
  .calculator__content {
    max-width: 105rem;
  }

  .calculator__grid {
    gap: 4.5rem;
  }

  .calculator {
    font-size: 1.1rem;
  }

  .calculator__title {
    font-size: 2.5rem;
  }
}
</style>

