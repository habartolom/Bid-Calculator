import { describe, it, expect } from 'vitest'
import { mount } from '@vue/test-utils'
import BaseButton from '../base/BaseButton.vue'

describe('BaseButton', () => {
  it('should render the button with the default slot', () => {
    const wrapper = mount(BaseButton, {
      slots: {
        default: 'Click me',
      },
    })

    expect(wrapper.text()).toContain('Click me')
  })

  it('should emit the click event when the button is clicked', async () => {
    const wrapper = mount(BaseButton)

    await wrapper.find('button').trigger('click')

    expect(wrapper.emitted('click')).toBeTruthy()
    expect(wrapper.emitted('click')?.length).toBe(1)
  })

  it('should not emit the click event when the button is disabled', async () => {
    const wrapper = mount(BaseButton, {
      props: {
        disabled: true,
      },
    })

    await wrapper.find('button').trigger('click')

    expect(wrapper.emitted('click')).toBeFalsy()
  })

  it('should show the spinner when the button is loading', () => {
    const wrapper = mount(BaseButton, {
      props: {
        loading: true,
      },
      slots: {
        default: 'Loading...',
      },
    })

    expect(wrapper.find('svg').exists()).toBe(true)
  })

  it('should apply the primary variant class by default', () => {
    const wrapper = mount(BaseButton)

    const button = wrapper.find('button')
    expect(button.classes()).toContain('bg-gradient-to-r')
  })

  it('should apply the correct size classes', () => {
    const wrapper = mount(BaseButton, {
      props: {
        size: 'lg',
      },
    })

    const button = wrapper.find('button')
    expect(button.classes().join(' ')).toContain('px-8')
  })
})

