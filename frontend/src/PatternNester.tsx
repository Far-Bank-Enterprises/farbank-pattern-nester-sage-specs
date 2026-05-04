import { useState, useEffect } from 'react'


function PatternNester() {
  const [reply, setReply] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [exclusions, setExclusions] = useState<string[]>([])
  const [exclusionsLoading, setExclusionsLoading] = useState(false)
  const [exclusionsError, setExclusionsError] = useState<string | null>(null)

  const handlePost = async () => {
    setLoading(true)
    setError(null)
    setReply(null)
    try {
      const response = await fetch('/api/ping', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({})
      })
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`)
      }
      const data = await response.text()
      setReply(data)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to send POST request')
    } finally {
      setLoading(false)
    }
  }

  useEffect(() => {
    const fetchExclusions = async () => {
      setExclusionsLoading(true)
      setExclusionsError(null)
      try {
        const response = await fetch('/api/exclusions')
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`)
        }
        const data = await response.json()
        setExclusions(Array.isArray(data) ? data : [])
      } catch (err) {
        setExclusionsError(err instanceof Error ? err.message : 'Failed to fetch exclusions')
      } finally {
        setExclusionsLoading(false)
      }
    }
    fetchExclusions()
  }, [])

  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '4rem', width: '100%' }}>
      <button
        onClick={handlePost}
        disabled={loading}
        style={{
          padding: '0.75rem 2rem',
          fontSize: '1.1rem',
          borderRadius: '0.5rem',
          border: 'none',
          background: '#0078d4',
          color: 'white',
          cursor: loading ? 'not-allowed' : 'pointer',
          boxShadow: '0 2px 8px rgba(0,0,0,0.08)',
          transition: 'background 0.2s',
        }}
      >
        {loading ? 'Sending...' : 'Send POST'}
      </button>
      <div style={{ marginTop: '2rem', minHeight: '2rem', width: '100%', textAlign: 'center' }}>
        {error && <span style={{ color: 'red' }}>{error}</span>}
        {reply && <span>{reply}</span>}
      </div>

      <div style={{ marginTop: '2rem', width: '100%', maxWidth: 600 }}>
        <h3>Exclusions</h3>
        {exclusionsLoading && <div>Loading exclusions...</div>}
        {exclusionsError && <div style={{ color: 'red' }}>{exclusionsError}</div>}
        {!exclusionsLoading && !exclusionsError && (
          <table style={{ width: '100%', borderCollapse: 'collapse', marginTop: '1rem' }}>
            <thead>
              <tr>
                <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left', padding: '0.5rem' }}>SKU</th>
              </tr>
            </thead>
            <tbody>
              {exclusions.length === 0 ? (
                <tr><td style={{ padding: '0.5rem' }}>No exclusions found.</td></tr>
              ) : (
                exclusions.map((sku, idx) => (
                  <tr key={sku || idx}>
                    <td style={{ padding: '0.5rem', borderBottom: '1px solid #eee' }}>{sku}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        )}
      </div>
    </div>
  )
}

export default PatternNester
