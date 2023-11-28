
    function updateSelectedCustomer() {
         // Henter referansen til HTML-elementet med id 'customerInput' 
        var customerInput = document.getElementById('customerInput');

    // Finner den valgte kunden i dropdown-menyen med id 'customers' som har verdien som samsvarer med customerInput-verdien
    var selectedOption = document.querySelector(`#customers option[value="${customerInput.value}"]`);

    // Henter referansen til HTML-elementet med id 'kundeId'
    var kundeIdField = document.getElementById('kundeId');

    // Sjekker om det valgte alternativet ble funnet
    if (selectedOption) {
        // Henter verdien av 'data-value' attributtet fra det valgte alternativet
        kundeIdField.value = selectedOption.getAttribute('data-value');
        }
    }
    // Legger til en 'change' hendelseslytter på HTML-elementet med id 'customerInput'

    document.getElementById('customerInput').addEventListener('change', updateSelectedCustomer);


