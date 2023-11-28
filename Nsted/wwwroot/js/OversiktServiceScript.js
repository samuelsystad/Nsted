function enableEditing() {
        // Finn HTML-elementer med klassen 'view-only' og 'editable-field'
        var viewOnlyFields = document.querySelector('.view-only');
    var editableFields = document.querySelector('.editable-field');

    // Endrer visningsstilene for å bytte fra visnings- til redigeringsmodus
    viewOnlyFields.style.display = 'none';
    editableFields.style.display = 'block';

    // Fjerner 'readonly' attributtet fra inndatalinjene med klassen 'readonly-input'
    var inputs = document.querySelectorAll('.editable-field .readonly-input');
    inputs.forEach(function (input) {
        input.removeAttribute('readonly');
        });

    // Viser varselen med ID 'editAlert'
    document.getElementById('editAlert').style.display = 'block';

    // Skjuler redigeringsknappen ('editButton') og viser oppdateringsknappen ('updateButton')
    document.getElementById('editButton').style.display = 'none';
    document.getElementById('updateButton').style.display = 'block';
    }
