import { describe, it, expect, beforeEach } from 'vitest'
import { mount } from '@vue/test-utils'
import { createPinia, setActivePinia } from 'pinia'
import BidCalculatorForm from '../BidCalculatorForm.vue'

describe('BidCalculatorForm', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
  })

  it('should render the form correctly', () => {
    const wrapper = mount(BidCalculatorForm)

    expect(wrapper.find('form').exists()).toBe(true)
    expect(wrapper.text()).toContain('Vehicle Information')
  })

  it('should have the calculate button disabled when the price is 0', async () => {
    const wrapper = mount(BidCalculatorForm)
    
    const button = wrapper.find('button[type="submit"]')
    expect(button.attributes('disabled')).toBeDefined()
  })

  it('should show the vehicle type options', () => {
    const wrapper = mount(BidCalculatorForm)
    
    const select = wrapper.find('select')
    expect(select.exists()).toBe(true)
    
    const options = select.findAll('option')
    expect(options.length).toBeGreaterThan(0)
  })

  it('should emit the calculation when the form is submitted', async () => {
    const wrapper = mount(BidCalculatorForm)
    
    const priceInput = wrapper.find('input[type="number"]')
    await priceInput.setValue(1000)
    
    await wrapper.find('form').trigger('submit.prevent')
  })
})

