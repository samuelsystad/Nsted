function updateSelectedCustomer() {
            //Henter referansen til HTML-elementet med id 'customerInput'
            var customerInput = document.getElementById('customerInput');

    // Finner den valgte kunden i dropdown-menyen med id 'customers' som har verdien som samsvarer med customerInput-verdien
    var selectedOption = document.querySelector(`#customers option[value="${customerInput.value}"]`);

    // Henter referansen til HTML-elementet med id 'kundeId'
    var kundeIdField = document.getElementById('kundeId');

    // Sjekker om det valgte alternativet ble funnet
    if (selectedOption) {
        kundeIdField.value = selectedOption.getAttribute('data-value');
            }
        }
    //Legger til en 'change' hendelseslytter på HTML-elementet med id 'customerInput'
    document.getElementById('customerInput').addEventListener('change', updateSelectedCustomer);

    function generateOrderNumber() {
        var orderNumber = Math.floor(Math.random() * 1000000); // Generates a random number
    document.getElementById('orderNumberField').value = orderNumber; // Sets the value of the input field
}