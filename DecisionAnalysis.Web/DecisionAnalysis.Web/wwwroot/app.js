function createCustomerPortalSession() {
    var apiUrl = '/api/payment/create-customer-portal';
    $.ajax({
        url: apiUrl,
        type: 'POST',
        success: function (sessionLink, _textStatus, xhr) {
            if (xhr.status === 204) {
                //204 means no content
                alert("You did not subscribe to any plan yet.");
            } else {
                window.location.href = sessionLink;
            }
        },
        error: function (xhr, status, error) {
            console.error('Error creating customer portal session:', status, error);
            // Check if the status code is 400 for BAD REQUEST
            if (xhr.status === 400) {
                try {
                    // Try to parse the response text to extract the message
                    var response = xhr.responseText;
                    if (response) {
                        alert(response); // Display the message from the response
                    } else {
                        alert("Bad request, but no message received.");
                    }
                } catch (e) {
                    // If parsing fails, show a generic alert
                    alert("Bad request and unable to parse the error message.");
                }
            }
        }
    });
} 