<template>
  <div
    class="base-card bg-white rounded-2xl shadow-lg border border-gray-100 overflow-hidden transition-all duration-300 hover:shadow-xl"
    :class="{ 'animate-slide-up': animated }"
  >
    <div
      v-if="$slots.header || title"
      class="base-card__header px-4 py-3 border-b border-gray-100 bg-gradient-to-r from-primary-50 to-white"
    >
      <slot name="header">
        <h3 class="text-base font-semibold text-gray-800">{{ title }}</h3>
        <p v-if="subtitle" class="text-xs text-gray-600 mt-0.5">{{ subtitle }}</p>
      </slot>
    </div>

    <div class="base-card__content p-4">
      <slot />
    </div>

    <div v-if="$slots.footer" class="base-card__footer px-5 py-3 bg-gray-50 border-t border-gray-100">
      <slot name="footer" />
    </div>
  </div>
</template>

<script setup lang="ts">
withDefaults(
  defineProps<{
    title?: string
    subtitle?: string
    animated?: boolean
  }>(),
  {
    title: '',
    subtitle: '',
    animated: true,
  }
)
</script>

<style scoped>
.base-card {
  height: 100%;
  display: flex;
  flex-direction: column;
}

.base-card__content {
  flex: 1;
  display: flex;
  flex-direction: column;
}

@media (min-width: 1920px) {
  .px-4 {
    padding-left: 1.25rem;
    padding-right: 1.25rem;
  }

  .py-3 {
    padding-top: 0.875rem;
    padding-bottom: 0.875rem;
  }

  .p-4 {
    padding: 1.25rem;
  }

  .text-base {
    font-size: 1.125rem;
  }

  .text-xs {
    font-size: 0.8125rem;
  }
}

@media (min-width: 2560px) {
  .px-4 {
    padding-left: 1.5rem;
    padding-right: 1.5rem;
  }

  .py-3 {
    padding-top: 1rem;
    padding-bottom: 1rem;
  }

  .p-4 {
    padding: 1.5rem;
  }

  .text-base {
    font-size: 1.25rem;
  }

  .text-xs {
    font-size: 0.875rem;
  }
}
</style>

