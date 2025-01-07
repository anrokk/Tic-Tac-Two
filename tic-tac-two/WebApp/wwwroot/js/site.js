document.addEventListener("DOMContentLoaded", function() {
    const params = new URLSearchParams(window.location.search);

    if (params.get('x') !== null || params.get('y') !== null ||
        params.get('move') !== null || params.get('selectedX') !== null ||
        params.get('selectedY') !== null) {

        params.delete('x');
        params.delete('y');
        params.delete('move');
        params.delete('selectedX');
        params.delete('selectedY');

        const newUrl = `${window.location.pathname}${params.toString() ? '?' + params.toString() : ''}`;
        window.history.replaceState({}, document.title, newUrl);
    }
});

function copyInviteLink(buttonElement) {
    const url = buttonElement.getAttribute('data-url');
    navigator.clipboard.writeText(window.location.origin + url)
        .then(() => {
            const message = document.getElementById('copyMessage');
            if (message) {
                message.style.display = 'inline';
                setTimeout(() => { message.style.display = 'none'; }, 2000);
            }
        })
        .catch(err => {
            alert('Failed to copy link: ' + err);
        });
}

function refreshPage() {
    window.location.reload();
}
