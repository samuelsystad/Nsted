//Funksjon som genererer et tilfeldig og unikt Ordre Nr

        function generateOrderNumber() {
            fetch('/Ansatt/GenerateUniqueOrderNumber')
                .then(response => response.json())
                .then(data => {
                    document.getElementById('orderNumberField').value = data.uniqueOrderNumber;
                })
                .catch(error => console.error('Error:', error));
    }
