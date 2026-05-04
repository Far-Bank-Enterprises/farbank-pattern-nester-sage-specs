import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import PatternNester from './PatternNester'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <PatternNester />
  </StrictMode>,
)
