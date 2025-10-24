import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: {
        title: 'Bid Calculator',
      },
    },
  ],
})

router.beforeEach((to, _from, next) => {
  const defaultTitle = 'Vehicle Bid Calculator'
  document.title = (to.meta.title as string) || defaultTitle
  next()
})

export default router