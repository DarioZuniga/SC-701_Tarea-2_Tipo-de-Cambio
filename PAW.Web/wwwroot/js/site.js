// Caché global para el tipo de cambio
let _exchangeRateCache = null;

// Función que hace el fetch a la API del Ministerio de Hacienda
async function getExchangeRate() {
    // 1. Si ya está en caché, devolvemos los datos de inmediato
    if (_exchangeRateCache) {
        return _exchangeRateCache;
    }

    const myHeaders = new Headers();
    myHeaders.append("UserAgent", "curl/8.7.1");
    myHeaders.append("Accept", "*/*");

    const requestOptions = {
        method: "GET",
        headers: myHeaders,
        redirect: "follow"
    };

    try {
        // 2. Hacemos la petición
        const response = await fetch("https://api.hacienda.go.cr/indicadores/tc", requestOptions);
        if (!response.ok) throw new Error("Error en la petición");

        // 3. Convertimos a JSON correctamente (sin pasar por text())
        const data = await response.json();

        // 4. Guardamos en caché
        _exchangeRateCache = data;

        return data;
    } catch (error) {
        console.error("Error obteniendo tipo de cambio:", error);
        return null;
    }
}

// Función que pinta el widget flotante
async function loadExchangeRateWidget() {
    const container = document.getElementById('exchange-rate-widget');
    if (!container) return;

    // Mostrar estado de carga
    container.innerHTML = `<div class="exchange-rate-card">Cargando...</div>`;

    const data = await getExchangeRate();

    if (!data) {
        container.innerHTML = `<div class="exchange-rate-card bg-danger">No disponible</div>`;
        return;
    }

    // La API de Hacienda devuelve un JSON con esta estructura aproximada:
    
    const dolarCompra = data.dolar?.compra?.valor || 'N/A';
    const dolarVenta = data.dolar?.venta?.valor || 'N/A';
    const euroCompra = data.euro?.compra?.valor || 'N/A';

    container.innerHTML = `
        <div class="exchange-rate-card">
            <div class="rate-title">💵 Tipo de Cambio</div>
            <div class="rate-values">
                <span>Dólar Compra: <strong>₡${dolarCompra}</strong></span>
                <span>Dólar Venta: <strong>₡${dolarVenta}</strong></span>
                <span>Euro Compra: <strong>₡${euroCompra}</strong></span>
            </div>
            <div class="rate-date">${new Date().toLocaleDateString()}</div>
        </div>
    `;
}

// Ejecutar al cargar la página
document.addEventListener('DOMContentLoaded', loadExchangeRateWidget);