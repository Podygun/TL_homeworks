import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import DraggableBoxes from './App2.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <DraggableBoxes />
  </StrictMode>,
)
