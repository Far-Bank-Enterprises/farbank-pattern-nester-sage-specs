import { useState, useEffect } from 'react'

function PatternNester() {
  const [selectedDate, setSelectedDate] = useState<string>("");
  const [orders, setOrders] = useState<any[] | null>(null);
  const [ordersLoading, setOrdersLoading] = useState(false);
  const [ordersError, setOrdersError] = useState<string | null>(null);
  const [exclusions, setExclusions] = useState<string[]>([]);
  const [exclusionsLoading, setExclusionsLoading] = useState(false);
  const [exclusionsError, setExclusionsError] = useState<string | null>(null);
  const [selectedProdIds, setSelectedProdIds] = useState<string[]>([]);

  // Checkbox handlers
  const handleSelectAll = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (orders) {
      if (e.target.checked) {
        setSelectedProdIds(orders.map(order => String(order.prodId)));
      } else {
        setSelectedProdIds([]);
      }
    }
  };

  const handleSelectOne = (prodId: string, checked: boolean) => {
    setSelectedProdIds(prev =>
      checked ? [...prev, prodId] : prev.filter(id => id !== prodId)
    );
  };

  const handleUseSelected = () => {
    alert(`Selected prodIds: ${selectedProdIds.join(", ")}`);
  };


  const handleDateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const date = e.target.value;
    setSelectedDate(date);
    setOrders(null);
    setOrdersError(null);
  };

  const handleLoadOrders = async () => {
    if (!selectedDate) return;
    setOrders(null);
    setOrdersError(null);
    setOrdersLoading(true);
    try {
      const response = await fetch(`/api/production-orders?dateTime=${encodeURIComponent(selectedDate)}`);
      const contentType = response.headers.get('content-type');
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      if (contentType && contentType.includes('application/json')) {
        const data = await response.json();
        console.log('Fetched orders:', data);
        setOrders(Array.isArray(data) ? data : []);
      } else {
        throw new Error('Received non-JSON response from server');
      }
    } catch (err) {
      setOrdersError(err instanceof Error ? err.message : 'Failed to fetch production orders');
    } finally {
      setOrdersLoading(false);
    }
  };

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
      <div style={{ marginBottom: '2rem', width: '100%', maxWidth: 600 }}>
        <label htmlFor="prod-date" style={{ fontWeight: 600, marginRight: 12 }}>Select Production Date:</label>
        <input
          id="prod-date"
          type="date"
          value={selectedDate}
          onChange={handleDateChange}
          style={{ padding: '0.5rem', fontSize: '1rem', borderRadius: 4, border: '1px solid #ccc', marginRight: 12 }}
        />
        <button
          onClick={handleLoadOrders}
          disabled={!selectedDate || ordersLoading}
          style={{ padding: '0.5rem 1rem', fontSize: '1rem', borderRadius: 4, border: '1px solid #007bff', background: '#007bff', color: '#fff', cursor: !selectedDate || ordersLoading ? 'not-allowed' : 'pointer' }}
        >
          Load Orders
        </button>
      </div>
      <div style={{ minHeight: '2rem', width: '100%', maxWidth: 600, textAlign: 'center' }}>
        {ordersLoading && <span>Loading production orders...</span>}
        {ordersError && <span style={{ color: 'red' }}>{ordersError}</span>}
        {orders && (
          orders.length === 0 ? (
            <span>No production orders found for this date.</span>
          ) : (
            <div style={{
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'center',
              minHeight: '40vh',
              width: '100%'
            }}>
              <table style={{ width: '100%', borderCollapse: 'collapse', marginTop: '1rem', maxWidth: 600 }}>
                <thead>
                  <tr>
                    <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left', padding: '0.5rem' }}>
                      <input
                        type="checkbox"
                        checked={orders.length > 0 && selectedProdIds.length === orders.length}
                        onChange={handleSelectAll}
                        aria-label="Select all production orders"
                      />
                    </th>
                    <th style={{ borderBottom: '1px solid #ccc', textAlign: 'left', padding: '0.5rem' }}>prodId</th>
                  </tr>
                </thead>
                <tbody>
                  {orders.map((order, idx) => {
                    const prodId = String(order.prodId ?? '');
                    return (
                      <tr key={prodId}>
                        <td style={{ padding: '0.5rem', borderBottom: '1px solid #eee' }}>
                          <input
                            type="checkbox"
                            checked={selectedProdIds.includes(prodId)}
                            onChange={e => handleSelectOne(prodId, e.target.checked)}
                            aria-label={`Select production order ${prodId}`}
                          />
                        </td>
                        <td style={{ padding: '0.5rem', borderBottom: '1px solid #eee' }}>{prodId}</td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>
              <button
                style={{ marginTop: '1rem', padding: '0.5rem 1rem', fontSize: '1rem', borderRadius: 4, border: '1px solid #28a745', background: '#28a745', color: '#fff', cursor: selectedProdIds.length === 0 ? 'not-allowed' : 'pointer' }}
                disabled={selectedProdIds.length === 0}
                onClick={handleUseSelected}
              >
                Use Selected Orders
              </button>
            </div>
          )
        )}
      </div>

      <div style={{ marginTop: '2rem', width: '100%', maxWidth: 600 }}>
        <h3>Exclusions</h3>
        {exclusionsLoading && <div>Loading exclusions...</div>}
        {exclusionsError && <div style={{ color: 'red' }}>{exclusionsError}</div>}
        {!exclusionsLoading && !exclusionsError && (
          <div style={{
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            minHeight: '20vh',
            width: '100%'
          }}>
            <table style={{ width: '100%', borderCollapse: 'collapse', marginTop: '1rem', maxWidth: 600 }}>
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
          </div>
        )}
      </div>
    </div>
  )
}

export default PatternNester
