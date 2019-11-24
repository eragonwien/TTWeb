
// Accepts Cookies Agreement
const cookiesButton = document.querySelector("#cookie-consent-button");
if (cookiesButton) {
   cookiesButton.addEventListener("click", function () {
      document.cookie = cookiesButton.dataset.cookieString;
   }, false);
}

// Opens modal
const modalTriggers = document.querySelectorAll('.modal-trigger');
if (modalTriggers && modalTriggers.length > 0) {
   modalTriggers.forEach(function (modalTrigger) {
      modalTrigger.addEventListener('click', function () {
         modalTrigger.closest('.modal').classList.add('is-active');
      })
   });
}

const modalCloseButtons = document.querySelectorAll('.modal-close, .modal-close-button');
if (modalCloseButtons) {
   modalCloseButtons.forEach(function (closeButton) {
      closeButton.addEventListener('click', function () {
         closeButton.closest('.modal').classList.remove('is-active');
      });
   });
}